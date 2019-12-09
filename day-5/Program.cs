using System;
using System.Collections.Generic;
using System.Linq;

namespace day_5
{
    public enum Operations
    {
        Add = 1,
        Multiply = 2,
        Save = 3,
        Write = 4,
        Exit = 99
    }

    public enum Parametermode
    {
        Position = 0,
        Immediate = 1
    }
    public class Program
    {
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

        private static int RunProgram(List<int> instructions)
        {

            for (int i = 0; instructions[i] != 99; i += 4)
            {
                var opCodeAsString = instructions[i].ToString();
                int parameter1, parameter2, result;
                Parametermode parametermode3;

                var opcode = (Operations)Enum.Parse(typeof(Operations), opCodeAsString.Substring(opCodeAsString.Length - 2));
                switch (opcode)
                {
                    case Operations.Add:
                        GetParameters(opCodeAsString, instructions, i, out parameter1, out parameter2);
                        parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opCodeAsString.Substring(opCodeAsString.Length - 5, 1));
                        result = Addition(parameter1, parameter2);

                        if (parametermode3 == Parametermode.Immediate)
                        {
                            instructions[i + 3] = result;
                        }
                        else
                        {
                            instructions[instructions[i + 3]] = result;
                        }
                        break;
                    case Operations.Multiply:
                        GetParameters(opCodeAsString, instructions, i, out parameter1, out parameter2);
                        parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opCodeAsString.Substring(opCodeAsString.Length - 5, 1));
                        result = Multiply(parameter1, parameter2);

                        if (parametermode3 == Parametermode.Immediate)
                        {
                            instructions[i + 3] = result;
                        }
                        else
                        {
                            instructions[instructions[i + 3]] = result;
                        }
                        break;
                    case Operations.Save:
                        var paramtermode1 = (Parametermode)Enum.Parse(typeof(Parametermode), opCodeAsString.Substring(opCodeAsString.Length - 3, 1));
                        parameter1 = paramtermode1 == Parametermode.Immediate ? instructions[i + 1] : instructions[instructions[i + 1]];
                        Save(parameter1);
                        break;
                    case Operations.Write:
                        Write();
                        break;
                    case Operations.Exit:
                        Exit();
                        break;

                }
                //var result = opcode[i] == ADD ? , opcode[opcode[i + 2]]) : ;

            }

            return instructions[0];
        }

        private static void GetParameters(string opcodeAsString, List<int> instructions, int index, out int parameter1, out int parameter2)
        {
            var paramtermode1 = (Parametermode)Enum.Parse(typeof(Parametermode), opcodeAsString.Substring(opcodeAsString.Length - 3, 1));
            var paramtermode2 = (Parametermode)Enum.Parse(typeof(Parametermode), opcodeAsString.Substring(opcodeAsString.Length - 4, 1));


            parameter1 = paramtermode1 == Parametermode.Immediate ? instructions[index + 1] : instructions[instructions[index + 1]];
            parameter2 = paramtermode2 == Parametermode.Immediate ? instructions[index + 2] : instructions[instructions[index + 2]];
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
