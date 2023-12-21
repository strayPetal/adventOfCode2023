using static day20part1.Program;

namespace day20part1;

public abstract class Module(string name, string[] pulseDestinations) {
    public string Name = name;
    public string[] PulseDestinations = pulseDestinations;

    public abstract void ReceivePulse(string origin, bool pulse);
    
    public void SendPulse(bool pulse) {
        foreach (string destination in PulseDestinations)
            PulseQueue.Add((Name, destination, pulse));
    }

    public abstract void ReceiveSetup(string origin);
    
    public void SendSetup() {
        foreach (string destinationName in PulseDestinations) {
            if (Modules.TryGetValue(destinationName, out Module? destination)) {
                destination.ReceiveSetup(Name);
                destination.SendSetup();
            }
        }
    }
}

public class BroadcasterModule(string name, string[] pulseDestinations) : Module(name, pulseDestinations) {
    public override void ReceivePulse(string origin, bool pulse) {
        SendPulse(pulse);
    }

    public override void ReceiveSetup(string origin) {}
}

public class FlipFlopModule(string name, string[] pulseDestinations) : Module(name, pulseDestinations) {
    public bool Powered = false;

    public override void ReceivePulse(string origin, bool pulse) {
        if (pulse) return;

        Powered = !Powered;
        SendPulse(Powered);
    }

    public override void ReceiveSetup(string origin) {}
}

public class ConjuctionModule(string name, string[] pulseDestinations) : Module(name, pulseDestinations) {
    public Dictionary<string, bool> RecentPulseHigh = [];

    public override void ReceivePulse(string origin, bool pulse) {
        RecentPulseHigh[origin] = pulse;
        
        if (!RecentPulseHigh.ContainsValue(LOW_PULSE)) {
            // all high
            SendPulse(LOW_PULSE);
        } else {
            SendPulse(HIGH_PULSE);
        }
    }

    public override void ReceiveSetup(string origin) {
        RecentPulseHigh.TryAdd(origin, LOW_PULSE);
    }
}


public enum ModuleType {
    Broadcaster,
    FlipFlop,
    Conjunction
}