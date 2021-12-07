using AdaptiveRoads.Manager;
namespace Zeldslayer {
    using AdaptiveRoads.CustomScript;
    using System.Linq;
    using static NetLaneExt.Flags;
    using static NetLane.Flags;
    using static NetSegmentExt.Flags;
    using static NetSegment.Flags;
    using static NetSegmentEnd.Flags;
    using static NetNodeExt.Flags;
    using static NetNode.Flags;
    using KianCommons;
    using ColossalFramework;

    public class Tracks3 : PredicateBase {
        public override bool Condition() => TrackCount == 3;
        int TrackCount => Node.VanillaNode.Info.m_lanes.Count(IsTrack);
        bool IsTrack(NetInfo.Lane laneInfo) => laneInfo.m_vehicleType.IsFlagSet(VehicleInfo.VehicleType.Train);
    }
}