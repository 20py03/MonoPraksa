using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    internal  class Bird : Animal
    {
        public Bird(string species, int weight, int age) : base(species, weight, age)
        {
        }

        public override void KreciSe()
        {
            Console.WriteLine("Ptica se krece letenjem.");
        }

        public override void AnimalName()
        {
            Console.Write("Penguin ");
        }


        public virtual void AnimalInfoOutput(string species, int weight)
        {
            Console.WriteLine("My species is: " + species);
            Console.WriteLine("My weight is: " + weight);
        }

        public override void AnimalLook()
        {
            Console.WriteLine("I am very cute animal.");
            Console.WriteLine();
        }

    }
}
