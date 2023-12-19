public record IncomingCase(Position direction, int heatLoss) {
    public Position Direction = direction;
    public int HeatLoss = heatLoss;
}