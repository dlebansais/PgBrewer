namespace PgBrewer;

using System.Collections.Generic;
using System.Diagnostics;

public class ComponentAssociationCollection : List<ComponentAssociation>
{
    #region Init
    public static ComponentAssociationCollection None { get; } = new ComponentAssociationCollection();

    private ComponentAssociationCollection()
    {
        Name = string.Empty;
    }

    public ComponentAssociationCollection(string name, IEnumerable<ComponentAssociation> collection)
        : base(collection)
    {
        Debug.Assert(name.Length > 0);
        Name = name;

        Debug.Assert(Count > 0);
    }
    #endregion

    #region Properties
    public string Name { get; }
    public bool IsNone { get { return Name.Length == 0; } }
    #endregion

    #region Debugging
    public override string ToString()
    {
        return Name;
    }
    #endregion
}
