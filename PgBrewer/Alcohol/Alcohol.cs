namespace PgBrewer;

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Data;

public class Alcohol : INotifyPropertyChanged
{
    #region Init
    public static Alcohol None { get; } = new Alcohol(string.Empty);

    public Alcohol(string name)
    {
        Name = name;
        Previous = None;
        Next = None;
    }

    protected async Task ReadEffectList()
    {
        EffectList = await DataArchive.ReadEffectList(Name);

        EffectTextList = new List<string>();
        foreach (Effect Effect in EffectList)
            EffectTextList.Add(Effect.Text);
    }
    #endregion

    #region Properties
    public string Name { get; }
    public List<Effect> EffectList { get; private set; } = null!;
    public List<string> EffectTextList { get; private set; } = null!;
    public AlcoholLineCollection Lines { get; } = new AlcoholLineCollection();
    public Alcohol Previous { get; private set; }
    public Alcohol Next { get; private set; }
    public List<ComponentAssociationCollection> PreviousToNext { get; private set; } = new List<ComponentAssociationCollection>();

    public int MismatchCount
    {
        get => MismatchCountInternal;
        set
        {
            if (MismatchCountInternal != value)
            {
                MismatchCountInternal = value;
                NotifyThisPropertyChanged();
            }
        }
    }

    private int MismatchCountInternal;

    public int SelectedLine
    {
        get { return SelectedLineInternal; }
        set
        {
            if (SelectedLineInternal != value)
            {
                SelectedLineInternal = value;
                NotifyThisPropertyChanged();

                if (SelectedLineInternal < SectionOffset || SelectedLineInternal >= SectionOffset + SectionLength)
                    BringIntoView();

                NotifyLineSelected(SelectedLineInternal);
            }
        }
    }

    private int SelectedLineInternal = -1;

    public delegate void LineSelectedHandler(Alcohol alcohol, AlcoholLine? alcoholLine);
    public event LineSelectedHandler? LineSelected;

    protected void NotifyLineSelected(int lineIndex)
    {
        AlcoholLine? AlcoholLine;

        if (lineIndex >= 0 && lineIndex < Lines.Count)
            AlcoholLine = Lines[lineIndex];
        else
            AlcoholLine = null;

        LineSelected?.Invoke(this, AlcoholLine);
    }

    public bool IsSelected { get; private set; }
    #endregion

    #region Client Interface
    public void SetSelected(bool isSelected)
    {
        IsSelected = isSelected;
        NotifyPropertyChanged(nameof(IsSelected));
    }

    public virtual async Task Save()
    {
        List<int> Indexes = new List<int>();

        for (int i = 0; i < Lines.Count; i++)
            Indexes.Add(Lines[i].EffectIndex);

        await DataArchive.SetIndexList(Name, Indexes);
    }

    public virtual void ClearCalculateIndexes()
    {
        foreach (AlcoholLine Line in Lines)
            Line.ClearCalculateIndex();
    }

    public virtual void RecalculateMismatchCount()
    {
        int NewCount = 0;

        foreach (AlcoholLine Line in Lines)
            if (!Line.IsMatching)
                NewCount++;

        MismatchCount = NewCount;
    }

    public virtual void Export(StreamWriter writer)
    {
        writer.WriteLine(Name);

        foreach (AlcoholLine Line in Lines)
        {
            string ExportedComponents = Line.GetExportedComponents();
            int EffectIndex = Line.EffectIndex;

            if (EffectIndex >= 0)
                writer.WriteLine($"{ExportedComponents};{EffectList[EffectIndex].Text}");
            else
                writer.WriteLine($"{ExportedComponents};");
        }

        writer.WriteLine();
    }

    public virtual bool Import(StreamReader reader, ref int changeCount)
    {
        if (Name != reader.ReadLine())
            return false;

        for (int i = 0; i < Lines.Count; i++)
        {
            AlcoholLine Line = Lines[i];
            string LineString = reader.ReadLine()!;

            string ExportedComponents = Line.GetExportedComponents();
            ExportedComponents += ";";

            if (!LineString.StartsWith(ExportedComponents))
                return false;

            LineString = LineString.Substring(ExportedComponents.Length);
            if (LineString.Length > 0)
            {
                int SelectedLineIndex = -1;
                for (int j = 0; j < EffectList.Count; j++)
                {
                    if (LineString == EffectList[j].Text)
                    {
                        SelectedLineIndex = j;
                        break;
                    }
                }

                if (SelectedLineIndex < 0)
                    return false;

                if (Line.EffectIndex != SelectedLineIndex)
                {
                    Line.EffectIndex = SelectedLineIndex;
                    changeCount++;
                }
            }
            else if (Line.EffectIndex >= 0)
            {
                Line.EffectIndex = -1;
                changeCount++;
            }
        }

        reader.ReadLine();

        return true;
    }

    public static void Chain(Alcohol previous, Alcohol next, List<ComponentAssociationCollection> previousToNext)
    {
        previous.Next = next;
        next.Previous = previous;

        previous.PreviousToNext = previousToNext;
    }
    #endregion

    #region Implementation of INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    public void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region Debugging
    public override string ToString()
    {
        return Name;
    }
    #endregion

    public bool CanGoUp
    {
        get { return SectionOffset > 0; }
    }

    public bool CanGoDown
    {
        get { return SectionOffset + SectionLength < Lines.Count; }
    }

    public WpfObservableRangeCollection<AlcoholLine> SectionLines { get; } = new();

    public int SelectedSectionLine
    {
        get
        {
            return SelectedLine >= SectionOffset && SelectedLine < SectionOffset + SectionLength ? SelectedLine - SectionOffset : -1;
        }
        set
        {
            if (value >= 0 && value < SectionLength)
                SelectedLine = SectionOffset + value;
        }
    }

    public void MoveSection(bool isUp)
    {
        MoveSection(SectionOffset + (isUp ? -SectionLength : SectionLength));
    }

    private void BringIntoView()
    {
        MoveSection(SelectedLine - (SectionLength / 2));
    }

    private void MoveSection(int newOffset)
    {
        if (newOffset + SectionLength > Lines.Count)
            newOffset = Lines.Count - SectionLength;
        if (newOffset < 0)
            newOffset = 0;

        if (SectionOffset != newOffset)
        {
            SectionOffset = newOffset;

            FillSectionLines();
            NotifySectionChanged();
        }
    }

    protected void FillSectionLines()
    {
        int Count = Lines.Count - SectionOffset;
        if (Count > SectionLength)
            Count = SectionLength;

        List<AlcoholLine> VisibleLines = new();
        for (int i = 0; i < Count; i++)
            VisibleLines.Add(Lines[SectionOffset + i]);

        SectionLines.ReplaceRange(VisibleLines);
    }

    private void NotifySectionChanged()
    {
        NotifyPropertyChanged(nameof(CanGoUp));
        NotifyPropertyChanged(nameof(CanGoDown));
        NotifyPropertyChanged(nameof(SelectedSectionLine));
    }

    private const int SectionLength = 15;
    private int SectionOffset;
}
