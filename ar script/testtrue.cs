using AdaptiveRoads.Manager;
namespace Kian3 {
    using AdaptiveRoads.CustomScript;
    using static NetLaneExt.Flags;
    using static NetLane.Flags;
    using static NetSegmentExt.Flags;
    using static NetSegment.Flags;
    using static NetSegmentEnd.Flags;
    using static NetNodeExt.Flags;
    using static NetNode.Flags;
    using System;

    public class testtrue : PredicateBase {
        public override bool Condition() => true;
    }
}