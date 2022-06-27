using AdaptiveRoads.Manager;
namespace Hizu {
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

    public class SmallSegment16 : PredicateBase {
        float SegmentLen => Segment.VanillaSegment.m_averageLength;
        public override bool Condition() => SegmentLen < 16;
    }
}