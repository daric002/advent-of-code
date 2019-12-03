using System;
using System.Collections.Generic;
using System.Linq;

namespace day_1
{
    public class Program
    {
        private const int ADD = 1;
        private const int MULTIPLY = 2;
        public static void Main(string[] args)
        {
            var result = TryInputs();
            Console.WriteLine($"{result.Item1} {result.Item2}");
            Console.WriteLine(100 * result.Item1 + result.Item2);
        }

        private static Tuple<int, int> TryInputs()
        {

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var opcodes = GetOpcodeFromFile();
                    opcodes[1] = i;
                    opcodes[2] = j;
                    var output = RunProgram(opcodes);
                    if (output == 19690720)
                        return new Tuple<int, int>(opcodes[1], opcodes[2]);
                }
            }
            return new Tuple<int, int>(-1, -1);

        }

        private static int RunProgram(List<int> opcode)
        {
            for (int i = 0; opcode[i] != 99; i += 4)
            {
                var result = opcode[i] == ADD ? Addition(opcode[opcode[i + 1]], opcode[opcode[i + 2]]) : Multiply(opcode[opcode[i + 1]], opcode[opcode[i + 2]]);
                opcode[opcode[i + 3]] = result;
            }

            return opcode[0];
        }

        private static int Multiply(int first, int second)
        {
            return first * second;
        }

        private static int Addition(int first, int second)
        {
            return first + second;
        }

        private static int CalculateFuelConsumption(int mass)
        {
            var fuel = Convert.ToInt32(Math.Floor((decimal)mass / 3)) - 2;
            if (fuel <= 0)
                return 0;
            else
                return fuel + CalculateFuelConsumption(fuel);
        }

        //private static List<int> GetOpcodeRandom()
        //{
        //    var opcodes = new List<int>();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        opcodes[i + 1] = i;
        //       ;
        //    }
        //    string line;
        //    line = file.ReadToEnd();
        //    opcodes = line.Split(',').Select(int.Parse).ToList();
        //    return opcodes;
        //}

        private static List<int> GetOpcodeFromFile()
        {
            var file = new System.IO.StreamReader("Input.txt");
            var opcodes = new List<int>();
            string line;
            line = file.ReadToEnd();
            opcodes = line.Split(',').Select(int.Parse).ToList();
            return opcodes;
        }
    }
}
