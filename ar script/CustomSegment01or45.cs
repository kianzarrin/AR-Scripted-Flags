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
    using TrafficManager.API;
    using TrafficManager.API.Manager;

    public class CustomSegment01or45 : PredicateBase {
        public override bool Condition() =>
            (Segment.Has(NetSegmentExt.Flags.Custom0) && Segment.Has(NetSegmentExt.Flags.Custom0)) ||
            (Segment.Has(NetSegmentExt.Flags.Custom4) && Segment.Has(NetSegmentExt.Flags.Custom5));
    }
}