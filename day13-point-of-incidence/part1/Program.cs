class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";

    static void Main(string[] args) {
        var fullPuzzle = File.ReadLines(PUZZLE_INPUT_FILE);
        List<string> puzzle = [];
        int notesSummary = 0;

        foreach (var puzzleLine in fullPuzzle) {
            if (puzzleLine == "" || puzzleLine == null) {
                notesSummary += solve(puzzle);
                puzzle = [];
            } else {
                puzzle.Add(puzzleLine);
            }
        }

        notesSummary += solve(puzzle);

        Console.WriteLine($"Full summary: {notesSummary}");
    }

    static int solve(List<string> puzzle) {
        return solveHorizontal(puzzle) + solveVertical(puzzle);
    }

    static int solveHorizontal(List<string> puzzle) {
        for (int i = 1; i < puzzle.Count; i++) {
            if (checkHorizontal(puzzle, i)) {
                return i * 100;
            }
        }

        return 0;
    }

    static bool checkHorizontal(List<string> puzzle, int bottomReflectRow) {
        int topReflectRow = bottomReflectRow - 1;

        do {
            if (puzzle[topReflectRow] != puzzle[bottomReflectRow]) return false;
            topReflectRow--;
            bottomReflectRow++;
        } while (topReflectRow >= 0 && bottomReflectRow < puzzle.Count);

        return true;
    }

    static int solveVertical(List<string> puzzle) {
        for (int i = 1; i < puzzle[0].Length; i++) {
            if (checkVertical(puzzle, i)) {
                return i;
            }
        }

        return 0;
    }

    static bool checkVertical(List<string> puzzle, int rightReflectColumn) {
        int leftReflectColumn = rightReflectColumn - 1;

        do {
            for (int i = 0; i < puzzle.Count; i++) {
                string row = puzzle[i];
                if (row[leftReflectColumn] != row[rightReflectColumn]) return false;
            }

            leftReflectColumn--;
            rightReflectColumn++;
        } while (leftReflectColumn >= 0 && rightReflectColumn < puzzle[0].Length);

        return true;
    }
}
