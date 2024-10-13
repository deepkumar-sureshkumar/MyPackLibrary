using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPackLibrary
{
    public class Person
    {
        #region Properties

        public String? Name { get; set; }
        public DateTimeOffset Born { get; set; }
        public List<Person> Children = new();

        // Allow multiple spouses to be stored for a person
        public List<Person> Spouses = new();

        // A readonly field to show if a person is married
        public bool Married => Spouses.Count > 0;

        // Added property for adoptive children
        public List<Person> AdoptedChildren = new();

        #endregion

        #region Methods

        public void WriteToConsole()
        {
            Console.WriteLine($"{Name} was born on a {Born:dddd}");
        }

        public void WriteChildrenToConsole()
        {
            string term = Children.Count == 1 ? "child" : "children";
            Console.WriteLine($"{Name} has {Children.Count} biological {term}.");
        }

        public void WriteAdoptedChildrenToConsole()
        {
            string term = AdoptedChildren.Count == 1 ? "child" : "children";
            Console.WriteLine($"{Name} has {AdoptedChildren.Count} adopted {term}.");
        }

        // Static method to marry two people
        public static void Marry(Person p1, Person p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);

            if (p1.Spouses.Contains(p2) || p2.Spouses.Contains(p1))
            {
                throw new ArgumentException($"{p1.Name} is already married to {p2.Name}.");
            }

            p1.Spouses.Add(p2);
            p2.Spouses.Add(p1);
        }

        public void Marry(Person partner)
        {
            Marry(this, partner);
        }

        public void OutputSpouses()
        {
            if (Married)
            {
                string term = Spouses.Count == 1 ? "person" : "people";
                Console.WriteLine($"{Name} is married to {Spouses.Count} {term}:");

                foreach (var spouse in Spouses)
                {
                    Console.WriteLine($"{spouse.Name}");
                }
            }
            else
            {
                Console.WriteLine($"{Name} is single.");
            }
        }

        // Static method to procreate
        public static Person Procreate(Person p1, Person p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);
            if (!p1.Spouses.Contains(p2) || !p2.Spouses.Contains(p1))
            {
                throw new ArgumentException($"{p1.Name} must be married to {p2.Name} to procreate.");
            }

            Person baby = new()
            {
                Name = $"Baby of {p1.Name} and {p2.Name}",
                Born = DateTimeOffset.Now
            };

            p1.Children.Add(baby);
            p2.Children.Add(baby);

            return baby;
        }

        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }

        // 1. Check if a married couple has kids, and if not, let them adopt
        public static void Adopt(Person p1, Person p2, Person child)
        {
            if (!p1.Spouses.Contains(p2) || !p2.Spouses.Contains(p1))
            {
                throw new ArgumentException($"{p1.Name} and {p2.Name} need to be married to adopt.");
            }

            p1.AdoptedChildren.Add(child);
            p2.AdoptedChildren.Add(child);
        }

        public bool HasChildren => Children.Count > 0 || AdoptedChildren.Count > 0;

        // 2. Check if a child is a stepchild
        public bool IsStepChild(Person child)
        {
            return !Children.Contains(child) && AdoptedChildren.Contains(child);
        }

        // 3. Show parents of a child
        public void ShowParents()
        {
            if (Married && Children.Count > 0)
            {
                Console.WriteLine($"{Name}'s parents are:");

                foreach (var spouse in Spouses)
                {
                    Console.WriteLine($"{spouse.Name}");
                }
            }
            else
            {
                Console.WriteLine($"{Name} does not have parents listed.");
            }
        }

        #endregion

        #region Operators

        // Define the + operator to marry two people
        public static bool operator +(Person a, Person b)
        {
            Marry(a, b);
            return a.Married && b.Married;
        }

        // Define the * operator to procreate (multiply)
        public static Person operator *(Person a, Person b)
        {
            return Procreate(a, b);
        }

        #endregion
    }
}
