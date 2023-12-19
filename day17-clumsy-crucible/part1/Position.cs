class Position(int x, int y) {
    public int x = x;
    public int y = y;

    public void moveBy(Position offset) {
        x += offset.x;
        y += offset.y;
    }

    public Position getOffsetPosition(Position offset) {
        return new Position(x + offset.x, y + offset.y);
    }

    public bool matches(Position other) {
        return x == other.x && y == other.y;
    }
}