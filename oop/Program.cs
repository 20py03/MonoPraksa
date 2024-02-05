// See https://aka.ms/new-console-template for more information
using oop;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our mini zoo!");
            Mammals elephant = new Mammals("Elephant", 1000, 5);
            Fish tropicalFish = new Fish("Tropical Fish", 1, 2);
            Bird penguin = new Bird("Penguin", 300, 2);
            AnimalManager manager = new AnimalManager();
            int choose;
            List<Animal> speciesOfAnimal = new List<Animal>();
            List<Animal> animalList = new List<Animal>();


            do
            {
                Console.WriteLine("\nHow can we serve you:");
                Console.WriteLine("1. Show me species in the zoo");
                Console.WriteLine("2. Short animal information");
                Console.WriteLine("3. Animal Manager");
                Console.WriteLine("4. The end!");
                Console.WriteLine();
                Console.Write("Your choice: ");
                while (!int.TryParse(Console.ReadLine(), out choose))
                {
                    Console.WriteLine("Wrong input. Please enter a number.");
                    Console.Write("Your choice: ");
                }
                switch (choose)
                {
                    case 1:
                        animalList.Add(elephant);
                        animalList.Add(tropicalFish);
                        animalList.Add(penguin);
                        foreach (Animal typeOfAnimal in animalList)
                        {
                            typeOfAnimal.AnimalName();
                        }
                        break;
                    case 2:
                        elephant.AnimalInfoOutput("Elephant",1000,5);
                        elephant.AnimalLook();
                        penguin.AnimalInfoOutput("Penguin",300);
                        penguin.AnimalLook();
                        tropicalFish.AnimalInfoOutput("Tropical Fish", 2);
                        tropicalFish.AnimalInfoOutput("Baracuda", 3, 1);
                        tropicalFish.AnimalLook();
                        break;
                    case 3:
                        manager.Run(animalList);
                        break;
                    case 4:
                        Console.WriteLine("The end. Thank you!");
                        break;
                    default:
                        Console.WriteLine("Wrong choice!");
                        break;
                }
            } while (choose != 4);
        }   
    }
}