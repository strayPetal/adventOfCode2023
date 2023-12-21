namespace day20part1;

public class Program {
    const string PUZZLE_INPUT_FILE = "../puzzleInput.txt";

    const char FLIP_FLOP_MODULE_PREFIX = '%';
    const char CONJUNCTION_MODULE_PREFIX = '&';

    const string BROADCASTER_MODULE_NAME = "broadcaster";

    public const bool HIGH_PULSE = true;
    public const bool LOW_PULSE = false;

    private static Dictionary<string, Module> modules = [];
    private static List<(string origin, string destination, bool pulse)> pulseQueue = [];

    private static int lowPulsesSent = 0;
    private static int highPulsesSent = 0;

    // to make compiler shut up about "Non-constant fields should not be visible" //
    public static Dictionary<string, Module> Modules { get => modules; set => modules = value; }
    public static List<(string origin, string destination, bool pulse)> PulseQueue { get => pulseQueue; set => pulseQueue = value; }
    public static int LowPulsesSent { get => lowPulsesSent; set => lowPulsesSent = value; }
    public static int HighPulsesSent { get => highPulsesSent; set => highPulsesSent = value; }
    // it is shut up now. peace and quiet. //

    private static void Main() {
        Modules = GetModulesFromFile();
        SetupModules();
        for (int i = 0; i < 1000; i++) {
            TriggerBroadcasterModule();
            // Console.WriteLine($"It's called pancakes: Low {LowPulsesSent}; High {HighPulsesSent};");
            // LowPulsesSent = 0;
            // HighPulsesSent = 0;
        }
        Console.WriteLine($"It's called jam: {LowPulsesSent} * {HighPulsesSent} = {LowPulsesSent * HighPulsesSent}");
    }

    private static Dictionary<string, Module> GetModulesFromFile() {
        Dictionary<string, Module> modulesMap = [];
        IEnumerable<string> puzzleFile = File.ReadLines(PUZZLE_INPUT_FILE);
        
        foreach (var textLine in puzzleFile) {
            string[] split = textLine.Split(" -> ");
            ModuleType type =
                textLine[0] == FLIP_FLOP_MODULE_PREFIX ? ModuleType.FlipFlop
                : textLine[0] == CONJUNCTION_MODULE_PREFIX ? ModuleType.Conjunction
                : ModuleType.Broadcaster;
            string name =
                type == ModuleType.Broadcaster
                ? split[0]
                : split[0][1..];
            string[] destinations = split[1].Split(", ");
            Module module =
                type == ModuleType.FlipFlop ? new FlipFlopModule(name, destinations)
                : type == ModuleType.Conjunction ? new ConjuctionModule(name, destinations)
                : type == ModuleType.Broadcaster ? new BroadcasterModule(name, destinations)
                : throw new Exception("What the heck did you do");
            modulesMap.Add(name, module);
        }

        return modulesMap;
    }

    public static void SetupModules() {
        Modules[BROADCASTER_MODULE_NAME].ReceiveSetup("");
    }

    public static void TriggerBroadcasterModule() {
        PulseQueue.Add(("button", BROADCASTER_MODULE_NAME, LOW_PULSE));
        ExecuteQueuedPulsesUntilEmpty();
    }

    public static void ExecuteQueuedPulsesUntilEmpty() {
        while (PulseQueue.Count > 0)
            ExecuteFirstQueuedPulse();
    }

    public static void ExecuteFirstQueuedPulse() {
        var pulse = PulseQueue.First();
        if (Modules.TryGetValue(pulse.destination, out Module? destination)) {
            destination.ReceivePulse(pulse.origin, pulse.pulse);
        }

        // Console.WriteLine($"\t{pulse.origin} --{(pulse.pulse ? "high" : "low")}-> {pulse.destination}");
        if (pulse.pulse)    HighPulsesSent++;
        else                LowPulsesSent++;
        
        PulseQueue.RemoveAt(0);
    }
}
