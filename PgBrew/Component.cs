namespace PgBrew
{
    public class Component
    {
        public Component(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
