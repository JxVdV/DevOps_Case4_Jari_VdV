using System;

namespace Dev_OpsWPF.Model
{
    class Character : IComparable<Character>
    {
        public string Name { get; set; }
        public Character()
        {
        }
        public Character(string name)
        {
            Name = name;
        }

        public int CompareTo(Character other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
