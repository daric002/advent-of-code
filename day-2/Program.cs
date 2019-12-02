using System;
using System.Collections.Generic;

namespace day_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var modules = GetModules();

            var totalFuel = CalculateTotalFuelConsumtion(modules);

            Console.WriteLine(totalFuel);
        }

        private static int CalculateTotalFuelConsumtion(List<int> modules)
        {
            var totaltFuel = 0;
            foreach (var module in modules)
            {
                totaltFuel += CalculateFuelConsumption(module);
            }
            return totaltFuel;
        }

        private static int CalculateFuelConsumption(int mass)
        {
            var fuel = Convert.ToInt32(Math.Floor((decimal)mass / 3)) - 2;
            if (fuel <= 0)
                return 0;
            else
                return fuel + CalculateFuelConsumption(fuel);
        }

        private static List<int> GetModules()
        {
            var file = new System.IO.StreamReader("Input.txt");
            var modules = new List<int>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                modules.Add(int.Parse(line));
            }
            return modules;
        }
    }
}
