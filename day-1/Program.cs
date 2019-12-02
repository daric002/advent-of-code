using System;
using System.Collections.Generic;

namespace day_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var modules = GetModules();

            var totalFuel = CalculateFuelConsumtion(modules);

            Console.WriteLine(totalFuel);
        }

        private static int CalculateFuelConsumtion(List<int> modules)
        {
            var totaltFuel = 0;
            foreach (var module in modules)
            {
                totaltFuel += Convert.ToInt32(Math.Floor((decimal)module / 3)) - 2;
            }
            return totaltFuel;
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
