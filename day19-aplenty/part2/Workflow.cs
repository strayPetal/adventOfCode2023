public record Workflow(Rule[] Rules, string EndAction) {
}

public record Rule(char RatingCategory, char Comparator, int RatingBound, string PassAction) {
}