public class Row {
    const char workingSpring = '.';
    const char damagedSpring = '#';
    const char unknownSpring = '?';

    private string springs;
    private List<int> damagedConfiguration = new();

    public Row(string rawData) {
        string[] split = rawData.Split(" ");
        springs = split[0];
        foreach (string s in split[1].Split(","))
            damagedConfiguration.Add(int.Parse(s));
    }

    public void findValid() {
        findValid(new RowConfig(0, 0, 0, false));
    }

    private void findValid(RowConfig config) {
        switch (springs[config.position]) {
            case workingSpring:
                if (config.damagedGroupCount > 0)
                    return;
                next(config, false);
                break;

            case damagedSpring:
                if (config.exitingDamagedGroup)
                    return;
                next(config, true);
                break;

            case unknownSpring:
                if (config.exitingDamagedGroup)
                    next(config, false);
                else if (config.damagedGroupCount > 0)
                    next(config, true);
                else {
                    next(config, true);
                    next(config, false);
                } break;
        }
    }

    private void next(RowConfig config, bool addDamaged) {
        config.position++;

        if (config.exitingDamagedGroup) {
            config.exitingDamagedGroup = false;
        } else if (addDamaged) {
            config.damagedGroupCount++;
            if (damagedGroupOutOfRange(config.damagedGroupIndex)) {
                return;
            } else if (damagedGroupComplete(config.damagedGroupIndex, config.damagedGroupCount)) {
                config.damagedGroupCount = 0;
                config.damagedGroupIndex++;
                config.exitingDamagedGroup = true;
            }
        }

        if (config.position < springs.Length) {
            findValid(config);
        } else if (config.damagedGroupIndex == damagedConfiguration.Count) {
            incrementValidCount();
        }
    }

    private void incrementValidCount() {
        Main.validCount++;
    }

    private bool damagedGroupOutOfRange(ushort index) {
        return index >= damagedConfiguration.Count;
    }

    private bool damagedGroupComplete(ushort index, ushort count) {
        return damagedConfiguration[index] == count;
    }
}