using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop
{
    internal class AnimalManager
    {        
        public void Run(List<Animal> animalList)
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Animal");
                Console.WriteLine("2. Display Animals");
                Console.WriteLine("3. Delete an animal");
                Console.WriteLine("4. Quit");

                Console.Write("Enter your choice (1/2/3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter the species of the animal: ");
                        string species = Console.ReadLine();
                        Console.WriteLine("Enter the weight of the animal: ");
                        int weight = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the age of the animal: ");
                        int age=int.Parse(Console.ReadLine());  
                        Animal animal = new Animal(species, weight, age);
                        AddAnimal(animal, animalList);
                        break;

                    case "2":
                        DisplayAnimals(animalList);
                        break;

                    case "3":
                        Console.Write("Enter the type of the animal to delete: ");
                        string animalToDelete = Console.ReadLine();
                        DeleteAnimal(animalToDelete, animalList);
                        break;

                    case "4":
                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
        }

            private void AddAnimal(Animal newAnimal, List<Animal> animalList)
            {
                animalList.Add(newAnimal);
                Console.WriteLine($"{newAnimal.getVrsta()} - {newAnimal.getTezina()} - {newAnimal.getDob()} has been added to the list.");
            }

            private void DisplayAnimals(List<Animal> animalList)
            {
                if (animalList.Count == 0)
                {
                    Console.WriteLine("The animal list is empty.");
                }
                else
                {
                    Console.WriteLine("List of Animals:");
                    foreach (var animal in animalList)
                    {
                        Console.WriteLine($"{animal.getVrsta()} - {animal.getTezina()} -{animal.getDob()} ");
                    }
                }
            }
            private void DeleteAnimal(string animalName, List<Animal> animalList)
            {
                Animal animalToDelete = animalList.Find(a => a.getVrsta().Equals(animalName, StringComparison.OrdinalIgnoreCase));

                if (animalToDelete != null)
                {
                    animalList.Remove(animalToDelete);
                    Console.WriteLine($"{animalToDelete.getVrsta()} has been deleted from the list.");
                }
                else
                {
                    Console.WriteLine($"Animal with the name {animalName} not found in the list.");
                }
            }
        
    }
}
