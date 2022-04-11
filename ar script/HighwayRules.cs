namespace Hock {
    using AdaptiveRoads.CustomScript;
    public class HighwayRules : PredicateBase {
        public override bool Condition() => TrafficManager.State.Options.highwayRules;
    }
}