class Spot {
    public readonly char Type;
    public bool Energized = false;
    private bool xPassed = false;
    private bool yPassed = false;

    public Spot(char type) {
        this.Type = type;
    }

    public bool markEnergized() {
        if (!Energized) {
            Energized = true;
            return true;
        } else {
            return false;
        }
    }

    public bool checkAndUpdatePreviousPasses(int moveX, int moveY) {
        if (Type != Program.EMPTY)
            return false;

        if (moveX != 0) {
            if (!xPassed) {
                xPassed = true;
                return false;
            } else {
                return true;
            }
        } else if (moveY != 0) {
            if (!yPassed) {
                yPassed = true;
                return false;
            } else {
                return true;
            }
        } else {
            return true;
        }
    }
}