static class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";

    public const char EMPTY = '.';
    public const char MIRROR_1 = '\\';
    public const char MIRROR_2 = '/';
    public const char SPLITTER_X = '-';
    public const char SPLITTER_Y = '|';

    static Spot[,] Grid = new Spot[1, 1];
    static int EnergizedCount = 0;
    static int HighestEnergizedCount = 0;

    static void Main() {
        initializeGrid();

        for (int x = 0; x < Grid.GetLength(0); x++) {
            testFor(x, 0, 0, 1);
            testFor(x, Grid.GetLength(1) - 1, 0, -1);
        }

        for (int y = 0; y < Grid.GetLength(0); y++) {
            testFor(0, y, 1, 0);
            testFor(Grid.GetLength(0) - 1, y, -1, 0);
        }        

        Console.WriteLine($"Best number of energized spots is: {HighestEnergizedCount}");
    }

    static void initializeGrid() {
        var rawGrid = File.ReadLines(PUZZLE_INPUT_FILE);
        
        int xLength = rawGrid.First().Length;
        int yLength = rawGrid.Count();
        Grid = new Spot[xLength, yLength];

        for (int y = 0; y < yLength; y++) {
            string row = rawGrid.ElementAt(y);
            for (int x = 0; x < xLength; x++) {
                char type = row[x];
                Grid[x, y] = new Spot(type);
            }
        }
    }

    static void reset() {
        for (int y = 0; y < Grid.GetLength(0); y++) {
            for (int x = 0; x < Grid.GetLength(0); x++) {
                Spot spot = Grid[x, y];
                spot.reset();
            }
        }

        EnergizedCount = 0;
    }

    static void testFor(int beamX, int beamY, int moveX, int moveY) {
        processCurrentSpot(beamX, beamY, moveX, moveY);

        if (EnergizedCount > HighestEnergizedCount)
            HighestEnergizedCount = EnergizedCount;
        
        reset();
    }

    static void move(int beamX, int beamY, int moveX, int moveY) {
        beamX += moveX;
        beamY += moveY;
        processCurrentSpot(beamX, beamY, moveX, moveY);
    }

    static void processCurrentSpot(int beamX, int beamY, int moveX, int moveY) {
        if (beamX >= Grid.GetLength(0) || beamY >= Grid.GetLength(1) || beamX < 0 || beamY < 0) {
            return;
        }

        Spot currentSpot = getSpot(beamX, beamY);
        
        bool previouslyPassed = currentSpot.checkAndUpdatePreviousPasses(moveX, moveY);
        if (previouslyPassed)
            return;
        
        bool newlyEnergized = currentSpot.markEnergized();
        if (newlyEnergized)
            EnergizedCount++;

        switch (currentSpot.Type) {
            case EMPTY:
                move(beamX, beamY, moveX, moveY);
                break;
            
            case MIRROR_1:
                // swap x and y movement
                move(beamX, beamY, moveY, moveX);
                break;
            
            case MIRROR_2:
                // same, but multiply by -1
                move(beamX, beamY, moveY * -1, moveX * -1);
                break;
            
            case SPLITTER_X:
                if (moveY != 0) {
                    move(beamX, beamY, 1, 0);
                    move(beamX, beamY, -1, 0);
                } else {
                    move(beamX, beamY, moveX, moveY);
                }
                break;
            
            case SPLITTER_Y:
                if (moveX != 0) {
                    move(beamX, beamY, 0, 1);
                    move(beamX, beamY, 0, -1);
                } else {
                    move(beamX, beamY, moveX, moveY);
                }
                break;
            
            default:
                Console.WriteLine("What the hell did you do");
                break;
        }
    }

    static Spot getSpot(int x, int y) {
        return Grid[x, y];
    }
}
