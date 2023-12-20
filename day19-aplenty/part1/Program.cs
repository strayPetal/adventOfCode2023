class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";
    const string STARTING_WORKFLOW_CODE = "in";
    const string ACCEPT = "A";
    const string REJECT = "R";
    
    static void Main() {
        ReadPuzzleFile(out var workflows, out var parts);

        int total = 0;

        foreach (var part in parts) {
            bool accepted = EvaluatePartAgainstWorkflow(part, STARTING_WORKFLOW_CODE, workflows);
            if (accepted) {
                total += GetPartRatingsSum(part);
            }
        }

        Console.WriteLine(total);
    }

    public static bool EvaluatePartAgainstWorkflow(Dictionary<char, int> partRatings, string workflowCode, Dictionary<string, string[]> workflows) {
        string[] workflowRules = workflows[workflowCode];
        
        for (int i = 0; i < workflowRules.Length - 1; i++) {
            string rule = workflowRules[i];
            
            char ratingCategory = rule[0];
            char comparator = rule[1];
            int ratingBound = int.Parse(rule[2..rule.IndexOf(':')]);
            string passAction = rule[(rule.IndexOf(':') + 1)..];

            int partRating = partRatings[ratingCategory];

            bool pass;
            if (comparator == '<')
                pass = partRating < ratingBound;
            else if (comparator == '>')
                pass = partRating > ratingBound;
            else
                pass = false;
            
            if (pass)
                return ExecuteAction(passAction, partRatings, workflows);
        }

        return ExecuteAction(workflowRules.Last(), partRatings, workflows);
    }

    public static bool ExecuteAction(string action, Dictionary<char, int> partRatings, Dictionary<string, string[]> workflows) {
        if (action == ACCEPT)
            return true;
        else if (action == REJECT)
            return false;
        else
            return EvaluatePartAgainstWorkflow(partRatings, action, workflows);
    }

    public static int GetPartRatingsSum(Dictionary<char, int> part) {
        int sum = 0;
        foreach (var kvp in part)
            sum += kvp.Value;
        return sum;
    }

    private static void ReadPuzzleFile(out Dictionary<string, string[]> workflows, out List<Dictionary<char, int>> parts) {
        var puzzleFile = File.ReadLines(PUZZLE_INPUT_FILE);
        workflows = [];
        parts = [];

        bool evaluatingParts = false;
        foreach (var line in puzzleFile) {
            if (line == "") {
                evaluatingParts = true;
            } else if (!evaluatingParts) {
                string[] split = line.Split('{');
                string[] rules = split[1][..^1].Split(',');
                workflows.Add(split[0], rules);
            } else {
                Dictionary<char, int> part = [];
                foreach (string rating in line[1..^1].Split(',')) {
                    part.Add(rating[0], int.Parse(rating[2..]));
                }
                parts.Add(part);
            }
        }
    }
}
