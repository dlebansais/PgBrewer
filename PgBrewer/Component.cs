namespace PgBrewer;

public class Component
{
    #region Init
    public Component(string name)
    {
        Name = name;
    }
    #endregion

    #region Properties
    public string Name { get; }
    #endregion

    #region Debugging
    public override string ToString()
    {
        return Name;
    }
    #endregion
}
