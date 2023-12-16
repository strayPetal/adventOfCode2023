class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";

    static void Main(string[] args) {
        List<Step>[] boxes = new List<Step>[256];

        string initializationSequence = File.ReadAllText(PUZZLE_INPUT_FILE);
        string[] sequences = initializationSequence.Split(',');

        foreach (string sequence in sequences) {
            Step step = new(sequence);
            if (boxes[step.boxAllocation] == null) {
                boxes[step.boxAllocation] = [];
            }

            List<Step> box = boxes[step.boxAllocation];
            step.boxie(box);
        }

        int totalFocusingPower = 0;

        for (int boxNumber = 0; boxNumber < boxes.Length; boxNumber++) {
            List<Step> box = boxes[boxNumber];

            if (box == null)
                continue;

            for (int positionInBox = 0; positionInBox < box.Count; positionInBox++) {
                Step lens = box[positionInBox];
                totalFocusingPower += (boxNumber + 1) * (positionInBox + 1) * lens.focalLength;
                Console.WriteLine($"{lens.sequence} no. {boxNumber}, at position {positionInBox}, with length {lens.focalLength}... new total {totalFocusingPower}");
            }
        }

        Console.WriteLine(totalFocusingPower);
    }
}
