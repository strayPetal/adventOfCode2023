public class CityBlock(int heatLoss) {
    private const int STRAIGHT_RUN_POSSIBILITIES = 3;
    
    public int HeatLoss = heatLoss;
    public Dictionary<Position, IncomingCase[]> IncomingCases = new() {
        { new Position(-1, 0), new IncomingCase[STRAIGHT_RUN_POSSIBILITIES] },
        { new Position(0, -1), new IncomingCase[STRAIGHT_RUN_POSSIBILITIES] },
        { new Position(1, 0), new IncomingCase[STRAIGHT_RUN_POSSIBILITIES] },
        { new Position(0, 1), new IncomingCase[STRAIGHT_RUN_POSSIBILITIES] }
    };

    public IncomingCase[] GetCases(Position offset) {
        return IncomingCases[offset];
    }

    public void ApplyIncomingCases(Position offset, IncomingCase[] cases) {
        for (int i = 0; i < STRAIGHT_RUN_POSSIBILITIES; i++) {
            IncomingCase incomingCase = cases[i];
            if (offset == incomingCase.Direction) {
                // same direction, increment straightRun
                if (i < STRAIGHT_RUN_POSSIBILITIES) {
                    IncomingCases[offset][i + 1] = new IncomingCase(offset, incomingCase.HeatLoss + HeatLoss);
                }
            } else {
                // change direction, set back
            }
        }
    }
}