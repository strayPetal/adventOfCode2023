using System;
using System.IO;

class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";

    static void Main(string[] args) {
        string initializationSequence = File.ReadAllText(PUZZLE_INPUT_FILE);
        
        int sum = 0;
        int value = 0;

        foreach (char c in initializationSequence) {
            if (c == ',') {
                sum += value;
                value = 0;
                continue;
            }

            value += (int)c;
            value *= 17;
            value %= 256;
        }

        sum += value;

        Console.WriteLine(sum);
    }
}
