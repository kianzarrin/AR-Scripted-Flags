using AdaptiveRoads.Manager;
namespace Hockenheim {
    using AdaptiveRoads.CustomScript;
    using static NetLaneExt.Flags;
    using static NetLane.Flags;
        using static NetSegmentExt.Flags;
    using static NetSegment.Flags;
    using static NetSegmentEnd.Flags;
    using static NetNodeExt.Flags;
    using static NetNode.Flags;
    using System;

    public class Custom0L1 : PredicateBase {
        public override bool Condition() => Lanes(1).Has(NetLaneExt.Flags.Custom0);
    }
}
