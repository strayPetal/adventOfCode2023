class Step {
    public string sequence;
    public string label;
    public int focalLength;
    public int boxAllocation = 0;

    public Step(string sequence) {
        this.sequence = sequence;

        label = sequence.Substring(0, sequence.IndexOfAny(['=', '-']));
        focalLength = sequence.Contains('=')
            ? int.Parse(sequence.Substring(sequence.IndexOf('=') + 1))
            : 0;

        foreach (char c in label) {
            boxAllocation += (int)c;
            boxAllocation *= 17;
            boxAllocation %= 256;
        }
    }

    public void boxie(List<Step> box) {
        if (sequence.Contains('=')) {
            for (int i = 0; i < box.Count; i++) {
                Step step = box[i];
                if (step.label == this.label) {
                    box.RemoveAt(i);
                    box.Insert(i, this);
                    return;
                }
            }

            box.Add(this);
        } else if (sequence.Contains('-')) {
            foreach (Step step in box) {

                if (step.label == this.label) {
                    box.Remove(step);
                    return;
                }
                

            }
        } else {
            Console.WriteLine($"nothing.. {sequence}");
        }
    }
}