using System;
using System.Linq;
using System.Collections.Generic;
using intcode;

namespace day_7{
    public class AmplifierStarter
    {

        public static int CalculateAmplifierOutputs(List<int> instructions, List<int> phaseSettings)
        {
            IntcodeResult outputA, outputB, outputC, outputD, outputE = new IntcodeResult(0, false);
            var amplifierA = new Intcode(instructions, phaseSettings[0]);
            var amplifierB = new Intcode(instructions, phaseSettings[1]);
            var amplifierC = new Intcode(instructions, phaseSettings[2]);
            var amplifierD = new Intcode(instructions, phaseSettings[3]);
            var amplifierE = new Intcode(instructions, phaseSettings[4]);
            Console.WriteLine("PhaseSettings: " + string.Join(", ", phaseSettings));
            if(phaseSettings.All(p => p>4)){
                Console.WriteLine($"Kör med loop");
                while(!outputE.Exit)
                {
                    outputA = amplifierA.RunWithInput(outputE.Signal);
                    outputB = amplifierB.RunWithInput(outputA.Signal);
                    outputC = amplifierC.RunWithInput(outputB.Signal);
                    outputD = amplifierD.RunWithInput(outputC.Signal);
                    outputE = amplifierE.RunWithInput(outputD.Signal);
                    
                    Console.WriteLine("A" + outputA);
                    Console.WriteLine("B" + outputB);
                    Console.WriteLine("C" + outputC);
                    Console.WriteLine("D" + outputD);
                    Console.WriteLine("E" + outputE);
                }
            }
            else {
                Console.WriteLine($"Kör utan loop");
                outputA = amplifierA.RunWithInput(outputE.Signal);
                outputB = amplifierB.RunWithInput(outputA.Signal);
                outputC = amplifierC.RunWithInput(outputB.Signal);
                outputD = amplifierD.RunWithInput(outputC.Signal);
                outputE = amplifierE.RunWithInput(outputD.Signal);

                Console.WriteLine("A" + outputA);
                Console.WriteLine("B" + outputB);
                Console.WriteLine("C" + outputC);
                Console.WriteLine("D" + outputD);
                Console.WriteLine("E" + outputE);
            }
            return outputE.Signal;
        }
    }
}
