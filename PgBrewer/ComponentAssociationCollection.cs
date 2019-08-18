namespace PgBrew
{
    using System.Collections.Generic;

    public class ComponentAssociationCollection : List<ComponentAssociation>
    {
        #region Init
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
