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
        private static int input = 1;
        private static List<int> instructions;

        public static void Main(string[] args)
        {
            Run();
            Console.WriteLine($"{instructions[0]}");
        }

        private static void Run()
        {
            instructions = GetOpcodeFromFile();
            RunProgram();
        }

        private static Tuple<int, int> TryInputs()
        {

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    var opcodes = GetOpcodeFromFile();
                    RunProgram();
                    //if ( == 19690720)
                    //    return new Tuple<int, int>(opcodes[1], opcodes[2]);
                }
            }
            return new Tuple<int, int>(-1, -1);

        }

        private static void RunProgram()
        {
            int i = 0;
            while (i <= instructions.Count)
            {
                var opCodeAsString = instructions[i].ToString("D5");
                var opcode = (Operations)Enum.Parse(typeof(Operations), opCodeAsString.Substring(opCodeAsString.Length - 2));
                switch (opcode)
                {
                    case Operations.Add:
                        AddOperation(opCodeAsString, i);
                        i += 4;
                        break;
                    case Operations.Multiply:
                        MultiplyOperations(opCodeAsString, i);
                        i += 4;
                        break;
                    case Operations.Save:
                        Save(opCodeAsString, i);
                        i += 2;
                        break;
                    case Operations.Write:
                        Write(opCodeAsString, i);
                        i += 2;
                        break;
                    case Operations.Exit:
                        i = 1000000;
                        break;
                    default:
                        Console.WriteLine($"{i}: Wrong opcode");
                        throw new Exception($"{i}: Wrong opcode");
                }
            }
        }

        private static void Write(string opcode, int i)
        {
            var parameter1 = GetParameter1(opcode, i);
            Console.WriteLine($"{i}: {instructions[parameter1]}");
        }

        private static void Save(string opcode, int i)
        {
            var parameter1 = GetParameter1(opcode, i);
            instructions[parameter1] = input;
        }


        private static void AddOperation(string opCode, int i)
        {
            GetParameters(opCode, i, out int parameter1, out int parameter2);
            Parametermode parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opCode.Substring(opCode.Length - 5, 1));
            var result = Addition(parameter1, parameter2);

            if (parametermode3 == Parametermode.Immediate)
            {
                instructions[i + 3] = result;
            }
            else
            {
                instructions[instructions[i + 3]] = result;
            }
        }

        private static void MultiplyOperations(string opCode, int i)
        {
            GetParameters(opCode, i, out int parameter1, out int parameter2);
            var parametermode3 = (Parametermode)Enum.Parse(typeof(Parametermode), opCode.Substring(opCode.Length - 5, 1));
            var result = Multiply(parameter1, parameter2);

            if (parametermode3 == Parametermode.Immediate)
            {
                instructions[i + 3] = result;
            }
            else
            {
                instructions[instructions[i + 3]] = result;
            }
        }

        private static int GetParameter1(string opcode, int i)
        {
            var paramtermode1 = (Parametermode)Enum.Parse(typeof(Parametermode), opcode.Substring(opcode.Length - 3, 1));
            //var parameter1 = paramtermode1 == Parametermode.Immediate ? instructions[i + 1] : instructions[instructions[i + 1]];
            var parameter1 = instructions[i + 1];
            return parameter1;
        }

        private static void GetParameters(string opcodeAsString, int index, out int parameter1, out int parameter2)
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
