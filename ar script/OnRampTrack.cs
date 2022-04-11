using AdaptiveRoads.Manager;
namespace Hock {
    using AdaptiveRoads.CustomScript;
    using static NetLaneExt.Flags;
    using static NetLane.Flags;
    using static NetSegmentExt.Flags;
    using static NetSegment.Flags;
    using static NetSegmentEnd.Flags;
    using static NetNodeExt.Flags;
    using static NetNode.Flags;
    using System;
    using AdaptiveRoads.Util;
    using TrafficManager.API.Manager;
    using KianCommons;

    public class OnRampTrack : PredicateBase {
        static IManagerFactory TMPE => TrafficManager.API.Implementations.ManagerFactory;
        bool HighwayRules => TrafficManager.State.Options.highwayRules;

        static int OuterSimilarLaneIndex(NetInfo.Lane laneInfo) =>
            laneInfo.m_similarLaneCount - laneInfo.m_similarLaneIndex - 1;
        
        static int OuterSimilarLaneIndex(uint laneId) {
            return OuterSimilarLaneIndex(NetUtil.GetLaneInfo(laneId));
        }

        public override bool Condition() {
            if (HighwayRules) {
                var routings = TMPEHelpers.GetForwardRoutings(LaneId, Segment.Head.NodeID);
                return routings.Length == 1 && OuterSimilarLaneIndex(routings[0].laneId) == 0; // outermost lane
            } else {
                return false;
            }
        }
    }
}