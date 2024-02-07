using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    internal class Mammals:Animal
    {
        public Mammals(string species, int weight, int age) : base(species, weight, age)
        {
        }

        public override void KreciSe()
        {
            Console.WriteLine("The elephant is walking.");
        }

        public override void AnimalName()
        {
            Console.Write("Elephant ");
        }

        public override void AnimalInfoOutput(string species, int weight, int age)
        {
            Console.WriteLine("My species is: " + species);
            Console.WriteLine("My weight is: " + weight);
            Console.WriteLine("My age is: " + age);
        }

        public override void AnimalLook()
        {
            Console.WriteLine("I am enormous.");
            Console.WriteLine();
        }
    }
}
