using System;
using System.Linq;
using intcode;
using System.Collections.Generic;

namespace day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = IntcodeReader.GetOpcodeFromFile();

            var outputs = TryInputs(instructions, 0, 5);
            
            Console.WriteLine($"MaxOutput: {outputs.Max()}");
        }
        private static List<int> TryInputs(List<int> instructions, int lowerPhaseSettingLimit, int higherPhaseSettingLimit){
            int lowerlimit = lowerPhaseSettingLimit;
            int higherLimit = higherPhaseSettingLimit;
            var outputs = new List<int>();
            for(var i =lowerlimit; i<higherLimit; i++){
                for(var j=lowerlimit;j<higherLimit; j++){
                    if(j!=i){
                    for(var k=lowerlimit; k<higherLimit; k++){
                        if(k!=i && k!=j){
                        for(var l=lowerlimit; l<higherLimit; l++){
                            if(l!=k && l != j && l!=i) {
                            for(var m=lowerlimit; m<higherLimit; m++){
                                if(m!=i && m!= j && m!=k && m!=l) {
                                var phaseSettings = new List<int>{i,j,k,l,m};
                                var result = AmplifierStarter.CalculateAmplifierOutputs(instructions, phaseSettings);
                                Console.WriteLine(string.Join(", ", phaseSettings) + " " +result);
                                outputs.Add(result);
                                }
                            } }
                        }}
                    }}
                }
            }
            return outputs;
        }
    }
}

