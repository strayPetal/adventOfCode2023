struct Path {
    public Position current;
    public Position direction;
    public List<Position> previousPositions;
    public int heatLoss;
    public int straightRun;

    public Path() {
        current = new Position(0, 0);
        direction = new Position(1, 0);
        previousPositions = [];
        heatLoss = 0;
        straightRun = 0;
    }

    public Path(Path other) {
        current = new Position(other.current.x, other.current.y);
        direction = new Position(other.direction.x, other.direction.y);
        previousPositions = [];
        previousPositions.AddRange(other.previousPositions);
        heatLoss = other.heatLoss;
        straightRun = other.straightRun;
    }

    public void movePosition() {
        current.moveBy(direction);
    }

    public void incrementHeatLossByCurrentPosition() {
        heatLoss += Program.getHeatLossForCityBlock(current);
    }

    public bool isAtEndPosition() {
        return current.x == Program.cityMap.First().Length - 1 && current.y == Program.cityMap.Length - 1;
    }

    public void addCurrentPositionToPreviousPositions() {
        previousPositions.Add(current);
    }

    public Path clone() {
        return new Path(this);
    }
}