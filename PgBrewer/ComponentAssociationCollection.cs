namespace PgBrewer
{
    using System.Collections.Generic;

    public class ComponentAssociationCollection : List<ComponentAssociation>
    {
        #region Init
        public static ComponentAssociationCollection None { get; } = new ComponentAssociationCollection(string.Empty, new List<ComponentAssociation>());

        public ComponentAssociationCollection(string name, IEnumerable<ComponentAssociation> collection)
            : base(collection)
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
}
