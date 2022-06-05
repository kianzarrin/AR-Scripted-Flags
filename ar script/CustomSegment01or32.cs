using AdaptiveRoads.Manager;
namespace Clus {
    using AdaptiveRoads.CustomScript;
    using static NetLaneExt.Flags;
    using static NetLane.Flags;
    using static NetSegmentExt.Flags;
    using static NetSegment.Flags;
    using static NetSegmentEnd.Flags;
    using static NetNodeExt.Flags;
    using static NetNode.Flags;
    using System;

    public class CustomSegment01or32 : PredicateBase {
        public override bool Condition() =>
            (Segment.Has(NetSegmentExt.Flags.Custom0) & Segment.Has(NetSegmentExt.Flags.Custom0)) |
            (Segment.Has(NetSegmentExt.Flags.Custom3) & Segment.Has(NetSegmentExt.Flags.Custom4));
    }
}