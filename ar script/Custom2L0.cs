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

    public class Custom2L0 : PredicateBase {
        public override bool Condition() => Lanes(0).Has(NetLaneExt.Flags.Custom2);
    }
}
