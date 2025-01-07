using System;

namespace BusinessApp
{
    public class Entity
    {
        private int id;
        private string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public Entity(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return $"ID: {id}, Name: {name}";
        }
    }
}
