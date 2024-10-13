using MyPackLibrary;
using System;

// Implementing functionality using methods.

Person john = new Person()
{
    Name = "John"
};
Person jane = new Person()
{
    Name = "Jane"
};
Person sarah = new Person()
{
    Name = "Sarah"
};

// Call the instance method to marry John and Jane
john.Marry(jane);

// Output spouses for John and Jane
john.OutputSpouses();
jane.OutputSpouses();

// Output Sarah's spouses
sarah.OutputSpouses();

// Call instance method to make a baby
Person baby1 = john.ProcreateWith(jane);
baby1.Name = "John II";
Console.WriteLine($"{baby1.Name} was born on {baby1.Born}");

// Marry John and Sarah before procreation
john.Marry(sarah);
Console.WriteLine($"{john.Name} is now married to {sarah.Name}.");

// Now you can call the static method to make a baby with Sarah
Person baby2 = Person.Procreate(john, sarah);
baby2.Name = "John III";

john.WriteChildrenToConsole();
sarah.WriteChildrenToConsole();
jane.WriteChildrenToConsole();

// Adopt a child if no biological children
if (!john.HasChildren)
{
    Person adoptedChild = new Person { Name = "Adopted Child" };
    Person.Adopt(john, jane, adoptedChild);
    adoptedChild.ShowParents();
}

// Check if a child is a stepchild
if (john.IsStepChild(baby2))
{
    Console.WriteLine($"{baby2.Name} is a stepchild.");
}
else
{
    Console.WriteLine($"{baby2.Name} is not a stepchild.");
}

john.WriteAdoptedChildrenToConsole();

// Procreate with Sarah (this works now after marriage)
Person baby3 = john * sarah;
baby3.Name = "John IV";

Person baby4 = john * jane;
baby4.Name = "John V";

john.WriteChildrenToConsole();
john.OutputSpouses();
sarah.WriteChildrenToConsole();
jane.WriteChildrenToConsole();
Console.Read();
