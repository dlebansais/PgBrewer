namespace PgBrew
{
    using System.Collections.Generic;

    public class ComponentAssociationCollection : List<ComponentAssociation>
    {
        public ComponentAssociationCollection(string name, IEnumerable<ComponentAssociation> collection)
            : base(collection)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
