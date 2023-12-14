/*
    Beware! The messiest code ever is incoming!
*/

using System;
using System.IO;
using System.Linq;

class Program {
    const string PUZZLE_INPUT_FILE = "input.txt";
    
    const char EMPTY_SPACE = '.';
    const char ROUNDED_ROCK = 'O';
    const char CUBE_ROCK = '#';
    
    static void Main(string[] args) {
        var mirrorRows = File.ReadLines(PUZZLE_INPUT_FILE);
        
        int[] mirrorFreeSpaces = new int[mirrorRows.First().Length];
        Array.Fill(mirrorFreeSpaces, 0);

        int totalLoad = 0;

        for (int rowIndex = 0; rowIndex < mirrorRows.Count(); rowIndex++) {
            string row = mirrorRows.ElementAt(rowIndex);
            for (int spaceInRow = 0; spaceInRow < row.Length; spaceInRow++) {
                char spot = row[spaceInRow];
                if (spot == ROUNDED_ROCK) {
                    int moveTo = mirrorFreeSpaces[spaceInRow] + 1;
                    totalLoad += mirrorRows.Count() - mirrorFreeSpaces[spaceInRow];
                    mirrorFreeSpaces[spaceInRow]++;
                } else if (spot == CUBE_ROCK) {
                    mirrorFreeSpaces[spaceInRow] = rowIndex + 1;
                }
            }
        }

        Console.WriteLine($"Total load on support beams along north side of platform is: {totalLoad}");
    }
}
