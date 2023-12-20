public class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";
    const string STARTING_WORKFLOW_CODE = "in";
    const string ACCEPT = "A";
    const string REJECT = "R";
    const int RATING_LOWER_BOUND = 1;
    const int RATING_UPPER_BOUND = 4000;
    
    public static Dictionary<string, Workflow> Workflows = [];

    static void Main() {
        Workflows = ReadPuzzleFile();

        Dictionary<char, Bounds> startingCondition = new() {
            { 'x', new(RATING_LOWER_BOUND, RATING_UPPER_BOUND) },
            { 'm', new(RATING_LOWER_BOUND, RATING_UPPER_BOUND) },
            { 'a', new(RATING_LOWER_BOUND, RATING_UPPER_BOUND) },
            { 's', new(RATING_LOWER_BOUND, RATING_UPPER_BOUND) }};
        
        var acceptedCondition = GetPassingConditions(STARTING_WORKFLOW_CODE, startingCondition);

        int combinations = 0;

        foreach (var condition in acceptedCondition) {
            int conditionCombinations = 1;
            bool valid = true;
            foreach (var kvp in condition) {
                Bounds bounds = kvp.Value;
                conditionCombinations *= bounds.Upper - bounds.Lower + 1;
                if (conditionCombinations <= 0) {
                    valid = false;
                    break;
                }
            }
            if (valid)
                combinations += conditionCombinations;
        }

        Console.WriteLine($"{combinations}");
    }

    private static List<Dictionary<char, Bounds>> GetPassingConditions(string workflowCode, Dictionary<char, Bounds> condition) {
        List<Dictionary<char, Bounds>> acceptedConditions = [];
        
        Workflow workflow = Workflows[workflowCode];
        
        foreach (Rule rule in workflow.Rules) {
            Dictionary<char, Bounds> conditionFork = new(condition);
            char category = rule.RatingCategory;
            int bound = rule.RatingBound;

            if (rule.Comparator == '>') {
                conditionFork[category] = new(bound + 1, conditionFork[category].Upper);
                condition[category] = new(condition[category].Lower, bound);
            } else if (rule.Comparator == '<') {
                conditionFork[category] = new(conditionFork[category].Lower, bound - 1);
                condition[category] = new(bound, condition[category].Upper);
            } else {
                continue;
            }

            if (rule.PassAction == ACCEPT) {
                acceptedConditions.Add(conditionFork);
            } else if (rule.PassAction == REJECT) {
            } else {
                acceptedConditions.AddRange(GetPassingConditions(rule.PassAction, conditionFork));
            }
        }

        if (workflow.EndAction == ACCEPT) {
            acceptedConditions.Add(condition);
        } else if (workflow.EndAction == REJECT) {
        } else {
            acceptedConditions.AddRange(GetPassingConditions(workflow.EndAction, condition));
        }

        return acceptedConditions;
    }

    private static Dictionary<string, Workflow> ReadPuzzleFile() {
        IEnumerable<string> puzzleFile = File.ReadLines(PUZZLE_INPUT_FILE);
        Dictionary<string, Workflow> workflows = [];

        foreach (var line in puzzleFile) {
            if (line == "")
                return workflows;
            else {
                string[] split = line.Split('{');
                string[] rulesRaw = split[1][..^1].Split(',');
                List<Rule> rules = [];
                foreach (string raw in rulesRaw[..^1]) {
                    rules.Add(new Rule(raw[0], raw[1], int.Parse(raw[2..raw.IndexOf(':')]), raw[(raw.IndexOf(':') + 1)..]));
                }
                workflows.Add(split[0], new Workflow([.. rules], rulesRaw.Last()));
            }
        } return workflows;
    }
}
