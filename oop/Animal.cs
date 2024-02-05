using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    public class Animal : Look, IMove
    {
        protected string species;
        protected int weight;
        protected int age;

        public Animal(string species, int weight, int age)
        {
            this.species = species;
            this.weight = weight;
            this.age = age;
        }

        public string getVrsta() { return species; }
        public int getTezina() { return weight; }
        public int getDob() { return age; }

        void setVrsta(string species) { this.species = species; }
        void setTezina(int weight) { this.weight = weight; }
        void setDob(int age) { this.age = age; }


        public virtual void KreciSe()
        {
            Console.WriteLine("Animal is moving!");
        }

        public virtual void AnimalName()
        {
            Console.WriteLine("Name");
        }

        public virtual void AnimalInfoOutput(string species, int weight, int age)
        {
            Console.WriteLine("My species is: " + species);
            Console.WriteLine("My weight is: " + weight);
            Console.WriteLine("My age is: " + age);
        }

        public override void AnimalLook()
        {
            Console.WriteLine("Description of a look.");
        }
    }
}
