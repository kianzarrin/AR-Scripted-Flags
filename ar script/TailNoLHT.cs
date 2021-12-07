using AdaptiveRoads.Manager;
namespace Nien  {
    using AdaptiveRoads.CustomScript;
    using static NetLaneExt.Flags;
    using static NetLane.Flags;
    using static NetSegmentExt.Flags;
    using static NetSegment.Flags;
    using static NetSegmentEnd.Flags;
    using static NetNodeExt.Flags;
    using static NetNode.Flags;

    public class TailNoLHT : PredicateBase {
        public override bool Condition() => SegmentEnd.Has(IsTailNode) ^ SegmentEnd.Has(LeftHandTraffic);
    }
}