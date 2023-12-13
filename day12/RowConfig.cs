public struct RowConfig {
    public RowConfig(ushort position, ushort damagedGroupIndex, ushort damagedGroupCount, bool exitingDamagedGroup) {
        this.position = position;
        this.damagedGroupIndex = damagedGroupIndex;
        this.damagedGroupCount = damagedGroupCount;
        this.exitingDamagedGroup = exitingDamagedGroup;
    }
    
    public ushort position;
    public ushort damagedGroupIndex;
    public ushort damagedGroupCount;
    public bool exitingDamagedGroup;
}