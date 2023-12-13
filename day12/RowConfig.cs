public struct RowConfig {
    public RowConfig(ushort position, ushort damagedGroupIndex, ushort damagedGroupCount, bool exitingDamagedGroup, string row) {
        this.position = position;
        this.damagedGroupIndex = damagedGroupIndex;
        this.damagedGroupCount = damagedGroupCount;
        this.exitingDamagedGroup = exitingDamagedGroup;
        this.row = row;
    }
    
    public ushort position;
    public ushort damagedGroupIndex;
    public ushort damagedGroupCount;
    public bool exitingDamagedGroup;
    public string row;
}