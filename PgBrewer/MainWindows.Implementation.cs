namespace PgBrewer;

using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Main window implementation.
/// </summary>
public partial class MainWindow
{
    public void Recalculate()
    {
        RecalculateFromBottom(PageBeers.OrcishBock);
        RecalculateFromBottom(PageLiquors.PotatoVodka);
    }

    public void RecalculateFromBottom(IFourComponentsAlcohol start)
    {
        IFourComponentsAlcohol Previous = start;
        IFourComponentsAlcohol Next;
        List<ComponentAssociationCollection> AssociationLists;

        ((Alcohol)Previous).ClearCalculateIndexes();

        for (; ;)
        {
            Alcohol NextAlcohol = ((Alcohol)Previous).Next;
            if (NextAlcohol == Alcohol.None)
                break;

            Next = (IFourComponentsAlcohol)NextAlcohol;

            ((Alcohol)Next).ClearCalculateIndexes();

            AssociationLists = ((Alcohol)Previous).PreviousToNext;
            RecalculateBottomToTop(Previous, Next, AssociationLists[0], AssociationLists[1], AssociationLists[2], AssociationLists[3]);

            Previous = Next;
        }

        Next = Previous;

        for (; ;)
        {
            ((Alcohol)Next).RecalculateMismatchCount();

            Alcohol PreviousAlcohol = ((Alcohol)Next).Previous;
            if (PreviousAlcohol == Alcohol.None)
                break;

            Previous = (IFourComponentsAlcohol)PreviousAlcohol;

            AssociationLists = ((Alcohol)Previous).PreviousToNext;
            RecalculateTopToBottom(Next, Previous, AssociationLists[0], AssociationLists[1], AssociationLists[2], AssociationLists[3]);

            Next = Previous;
        }
    }

    public void RecalculateBottomToTop(IFourComponentsAlcohol previous, IFourComponentsAlcohol next, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4)
    {
        Debug.Assert((previous is Alcohol) && (next is Alcohol) && ((Alcohol)previous).Next == next && ((Alcohol)next).Previous == previous);

        int Multiplier1 = previous.Multiplier1;
        int Multiplier2 = previous.Multiplier2;
        int Multiplier3 = previous.Multiplier3;
        Debug.Assert(Multiplier1 == next.Multiplier1);
        Debug.Assert(Multiplier2 == next.Multiplier2);
        Debug.Assert(Multiplier3 == next.Multiplier3);

        for (int PreviousLineIndex = 0; PreviousLineIndex < previous.Lines.Count; PreviousLineIndex++)
            if (GetNextLineIndex(previous, next, associationList1, associationList2, associationList3, associationList4, PreviousLineIndex, out int NextLineIndex))
                next.Lines[NextLineIndex].CalculatedIndex = previous.Lines[PreviousLineIndex].BestIndex;
    }

    public bool GetNextLineIndex(IFourComponentsAlcohol previous, IFourComponentsAlcohol next, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4, int previousLineIndex, out int nextLineIndex)
    {
        IFourComponentsAlcoholLine Line = (IFourComponentsAlcoholLine)previous.Lines[previousLineIndex];
        int BestIndex = Line.BestIndex;
        if (BestIndex >= 0)
        {
            int NextIndex1 = GetPreviousToNextIndex(associationList1, Line.Index1);
            int NextIndex2 = GetPreviousToNextIndex(associationList2, Line.Index2);
            int NextIndex3 = GetPreviousToNextIndex(associationList3, Line.Index3);
            int NextIndex4 = GetPreviousToNextIndex(associationList4, Line.Index4);

            if (NextIndex1 >= 0 && NextIndex2 >= 0 && NextIndex3 >= 0 && NextIndex4 >= 0)
            {
                int Multiplier1 = previous.Multiplier1;
                int Multiplier2 = previous.Multiplier2;
                int Multiplier3 = previous.Multiplier3;

                nextLineIndex = (NextIndex1 * Multiplier1) + (NextIndex2 * Multiplier2) + (NextIndex3 * Multiplier3) + NextIndex4;
                return true;
            }
        }

        nextLineIndex = -1;
        return false;
    }

    public void RecalculateTopToBottom(IFourComponentsAlcohol next, IFourComponentsAlcohol previous, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4)
    {
        int Multiplier1 = next.Multiplier1;
        int Multiplier2 = next.Multiplier2;
        int Multiplier3 = next.Multiplier3;
        Debug.Assert(Multiplier1 == previous.Multiplier1);
        Debug.Assert(Multiplier2 == previous.Multiplier2);
        Debug.Assert(Multiplier3 == previous.Multiplier3);

        for (int NextLineIndex = 0; NextLineIndex < next.Lines.Count; NextLineIndex++)
            if (GetPreviousLineIndex(next, previous, associationList1, associationList2, associationList3, associationList4, NextLineIndex, out int PreviousLineIndex))
                previous.Lines[PreviousLineIndex].CalculatedIndex = next.Lines[NextLineIndex].BestIndex;
    }

    public bool GetPreviousLineIndex(IFourComponentsAlcohol next, IFourComponentsAlcohol previous, ComponentAssociationCollection associationList1, ComponentAssociationCollection associationList2, ComponentAssociationCollection associationList3, ComponentAssociationCollection associationList4, int nextLineIndex, out int previousLineIndex)
    {
        IFourComponentsAlcoholLine Line = (IFourComponentsAlcoholLine)next.Lines[nextLineIndex];
        int BestIndex = Line.BestIndex;
        if (BestIndex >= 0)
        {
            int PreviousIndex1 = GetNextToPreviousIndex(associationList1, Line.Index1);
            int PreviousIndex2 = GetNextToPreviousIndex(associationList2, Line.Index2);
            int PreviousIndex3 = GetNextToPreviousIndex(associationList3, Line.Index3);
            int PreviousIndex4 = GetNextToPreviousIndex(associationList4, Line.Index4);

            if (PreviousIndex1 >= 0 && PreviousIndex2 >= 0 && PreviousIndex3 >= 0 && PreviousIndex4 >= 0)
            {
                int Multiplier1 = next.Multiplier1;
                int Multiplier2 = next.Multiplier2;
                int Multiplier3 = next.Multiplier3;

                previousLineIndex = (PreviousIndex1 * Multiplier1) + (PreviousIndex2 * Multiplier2) + (PreviousIndex3 * Multiplier3) + PreviousIndex4;
                return true;
            }
        }

        previousLineIndex = -1;
        return false;
    }

    private int GetPreviousToNextIndex(ComponentAssociationCollection associationList, int previousIndex)
    {
        int NextIndex;

        if (associationList.IsNone)
            NextIndex = previousIndex;
        else if (associationList[previousIndex].AssociationIndex >= 0)
            NextIndex = associationList[previousIndex].AssociationIndex;
        else
            NextIndex = -1;

        return NextIndex;
    }

    private int GetNextToPreviousIndex(ComponentAssociationCollection associationList, int nextIndex)
    {
        int PreviousIndex;

        if (associationList.IsNone)
            PreviousIndex = nextIndex;
        else
        {
            PreviousIndex = -1;

            for (int i = 0; i < associationList.Count; i++)
                if (associationList[i].AssociationIndex == nextIndex)
                {
                    PreviousIndex = i;
                    break;
                }
        }

        return PreviousIndex;
    }

    public static void SetChanged()
    {
        MainWindow Window = (MainWindow)App.Current.MainWindow;
        Window.SetIsChanged(true);
    }
}
