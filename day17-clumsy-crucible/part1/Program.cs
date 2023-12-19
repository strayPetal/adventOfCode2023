class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";
    public static int smallestHeatLoss = int.MaxValue;
    public static string[] cityMap = [];

    public static void Main(string[] args) {
        Console.WriteLine("begin!");
        cityMap = File.ReadAllLines(PUZZLE_INPUT_FILE);
        moveFromStart();

        Console.WriteLine($"{smallestHeatLoss}");
    }

    public static void moveFromStart() {
        Path path = new Path();
        path.addCurrentPositionToPreviousPositions();

        foreach (Position direction in new[]{new Position(1, 0), new Position(0, 1)}) {
            Console.WriteLine("egg");
            Path newPath = path.clone();
            newPath.direction = direction;
            newPath.straightRun = 1;
            tryMoveTo(newPath);
        }
    }

    public static void moveFrom(Path path) {
        path.addCurrentPositionToPreviousPositions();

        foreach (Position direction in getForwardDirections(path.direction)) {
            Path newPath = path.clone();
            newPath.direction = direction;

            if (!direction.matches(path.direction))
                newPath.straightRun = 0;

            newPath.straightRun++;

            if (newPath.straightRun >= 4)
                continue;

            tryMoveTo(newPath);
        }
    }

    public static void tryMoveTo(Path path) {
        path.movePosition();

        if (cityBlockOutsideBounds(path.current)) {
            Console.WriteLine("out of bounds");
            return;
        }

        foreach (Position offset in getForwardDirections(path.direction)) {
            Position check = path.current.getOffsetPosition(offset);
            foreach (Position previouslyPassed in path.previousPositions) {
                if (check.matches(previouslyPassed)) {
                    Console.WriteLine("redundant path");
                    return;
                }
            }
        }

        path.incrementHeatLossByCurrentPosition();
        if (path.heatLoss > smallestHeatLoss) {
            return;
        } else if (path.isAtEndPosition()) {
            endRoute(path.heatLoss);
            return;
        }

        moveFrom(path);
    }
    
    public static void endRoute(int endHeatLoss) {
        if (endHeatLoss < smallestHeatLoss) {
            Console.WriteLine($"new champion! {endHeatLoss}");
            smallestHeatLoss = endHeatLoss;
        }
    }

    public static int getHeatLossForCityBlock(Position position) {
        if (cityBlockOutsideBounds(position))
            throw new Exception("Called getHeatLossForCityBlock() with coordinates that are out of bounds.");        
        return (int)char.GetNumericValue(cityMap[position.y][position.x]);
    }

    public static bool cityBlockOutsideBounds(Position position) {
        return position.x < 0 || position.y < 0 || position.x >= cityMap.First().Length || position.y >= cityMap.Length;
    }

    public static Position[] getForwardDirections(Position position) {
        return [position,
                new Position(position.y, position.x),
                new Position(position.y * -1, position.x * -1)];
    }
}