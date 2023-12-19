public record Position(int x, int y) {
    public int x = x;
    public int y = y;

    public Position GetOffsetBy(Position offset) {
        return new Position(x + offset.x, y + offset.y);
    }

    public bool Matches(Position other) {
        return x == other.x && y == other.y;
    }
}