using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    internal class Fish : Animal
    {
        public Fish(string species, int weight, int age) : base(species, weight, age)
        {
        }

        public override void KreciSe()
        {
            Console.WriteLine("The fish is swimming.");
        }

        public override void AnimalName()
        {
            Console.Write("Tropical Fish ");
        }

        public override void AnimalInfoOutput(string species, int weight, int age)
        {
            Console.WriteLine("My species is: " + species);
            Console.WriteLine("My weight is: " + weight);
            Console.WriteLine("My age is: " + age);
        }
        public virtual void AnimalInfoOutput(string species, int age)
        {
            Console.WriteLine("My species is: " + species);
            Console.WriteLine("My age is: " + age);
        }

        public override void AnimalLook()
        {
            Console.WriteLine("I have colorful feathers.");
            Console.WriteLine();
        }
    }
}
