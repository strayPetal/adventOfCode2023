class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";
    public static CityBlock[,] CityMap = new CityBlock[1, 1];   // to avoid me having to deal with MAKE IT NULLABLE YOU IDIOT from the compiler

    public static void Main() {
        CreateCityMap();
        InitializeEntryPosition();
        Solve();
    }

    public static void CreateCityMap() {
        string[] rawMap = File.ReadAllLines(PUZZLE_INPUT_FILE);
        
        int cityMapYRange = rawMap.Length;
        int cityMapXRange = rawMap[0].Length;

        CityMap = new CityBlock[cityMapXRange, cityMapYRange];

        for (int x = 0; x < cityMapXRange; x++) {
            for (int y = 0; y < cityMapYRange; y++) {
                int heatLoss = (int)char.GetNumericValue(rawMap[y][x]);
                CityMap[x, y] = new CityBlock(heatLoss);
            }
        }
    }

    public static void InitializeEntryPosition() {
        IncomingCase incomingCase = new(new Position(0, 0), 0);
        CityBlock entryPosition = CityMap[0, 0];
        
        foreach (var keyValuePair in entryPosition.IncomingCases) {
            keyValuePair.Value[0] = incomingCase;
        }
    }

    public static void Solve() {
        int fullDistance = Math.Max(CityMap.GetLength(0), CityMap.GetLength(1));
        for (int distance = 1; distance < fullDistance; distance++) {
            for (int x = 0; x < distance; x++) {
                EvaluatePosition(new Position(x, distance - x));
            }
        }
    }

    public static void EvaluatePosition(Position pos) {
        CityBlock cityBlock = GetCityBlockAt(pos);

        foreach (Position offset in new[] { new Position(-1, 0), new Position(0, -1) }) {
            Position offsetPos = pos.GetOffsetBy(offset);
            if (PositionOutsideBounds(offsetPos))
                continue;
            CityBlock offsetBlock = GetCityBlockAt(offsetPos);
            
            IncomingCase[] cases = offsetBlock.GetCases(offset);
            cityBlock.ApplyIncomingCases(offset, cases);
        }
    }

    public static CityBlock GetCityBlockAt(Position pos) {
        return CityMap[pos.x, pos.y];
    }

    public static bool PositionOutsideBounds(Position pos) {
        return pos.x < 0 || pos.y < 0 || pos.x >= CityMap.GetLength(0) || pos.y >= CityMap.GetLength(1);
    }

    public static Position[] GetForwardDirections(Position pos) {
        return [pos,
                new Position(pos.y, pos.x),
                new Position(pos.y * -1, pos.x * -1)];
    }
}