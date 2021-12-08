using AdaptiveRoads.Manager;
namespace Coreybpa {
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
    using KianCommons.Math;
    using ColossalFramework;
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    public static class MyExtensions {
        public static ref Building ToBuilding(this ushort buildingID) =>
            ref BuildingManager.instance.m_buildings.m_buffer[buildingID];
    }

    public class AtFireHouse : PredicateBase {
        public static IEnumerable<ushort> ScanBuildingsInArea(Vector3 pos) {
            Grid grid = new Grid(BuildingManager.BUILDINGGRID_CELL_SIZE, BuildingManager.BUILDINGGRID_RESOLUTION);
            GridVector2 gridVector = new GridVector2(pos, grid);
            foreach (var v in gridVector.ScanArea()) {
                ushort buildingID = BuildingManager.instance.m_buildingGrid[v.Index];
                while (buildingID != 0) {
                    yield return buildingID;
                    buildingID = buildingID.ToBuilding().m_nextGridBuilding;
                }
            }
        }

        public static IEnumerable<ushort> ScanSegmentsInArea(Vector3 pos) {
            Grid grid = new Grid(NetManager.NODEGRID_CELL_SIZE, NetManager.NODEGRID_RESOLUTION);
            GridVector2 gridVector = new GridVector2(pos, grid);
            foreach (var v in gridVector.ScanArea()) {
                ushort segmentID = NetManager.instance.m_segmentGrid[v.Index];
                while (segmentID != 0) {
                    yield return segmentID;
                    segmentID = segmentID.ToSegment().m_nextGridSegment;
                }
            }
        }

        public override bool Condition() {
            var pos = Node.VanillaNode.m_position;
            var otherPos = SegmentID.ToSegment().GetOtherNode(NodeID).ToNode().m_position;
            foreach (ushort buildingId in ScanBuildingsInArea(pos)) {
                ref var building = ref buildingId.ToBuilding();
                if(building.Info.m_buildingAI is FireStationAI ai) {
                    ushort segmentID = FindRoadAccess(buildingId, out var lanePos);
                    if (segmentID == SegmentID) {
                        // only check for this segment end.
                        var dist1 = (lanePos - pos).sqrMagnitude;
                        var dist2 = (lanePos - otherPos).sqrMagnitude;
                        if (dist1 <= dist2)
                            return true;
                    }
                }
            }
            return false;
        }

        static ushort FindRoadAccess(ushort buildingID, out Vector3 lanePos ) {
            ref Building building = ref buildingID.ToBuilding();
            var ai = building.Info.m_buildingAI;
            Vector3 position;
            if (ai.m_info.m_zoningMode == BuildingInfo.ZoningMode.CornerLeft) {
                position = building.CalculateSidewalkPosition(building.Width * 4f, 4f);
            } else if (ai.m_info.m_zoningMode == BuildingInfo.ZoningMode.CornerRight) {
                position = building.CalculateSidewalkPosition(building.Width * -4f, 4f);
            } else {
                position = building.CalculateSidewalkPosition(0f, 4f);
            }
            Bounds bounds = new Bounds(position, new Vector3(40f, 40f, 40f));
            int xlower = Mathf.Max((int)((bounds.min.x - 64f) / 64f + 135f), 0);
            int zlower = Mathf.Max((int)((bounds.min.z - 64f) / 64f + 135f), 0);
            int xhigher = Mathf.Min((int)((bounds.max.x + 64f) / 64f + 135f), 269);
            int zhigher = Mathf.Min((int)((bounds.max.z + 64f) / 64f + 135f), 269);
            for (int xgrid = zlower; xgrid <= zhigher; xgrid++) {
                for (int zgrid = xlower; zgrid <= xhigher; zgrid++) {
                    ushort segmentID = NetManager.instance.m_segmentGrid[xgrid * 270 + zgrid];
                    int watchDog = 0;
                    while (segmentID != 0) {
                        ref var segment = ref segmentID.ToSegment();
                        NetInfo info = segment.Info;
                        if (info.m_class.m_service == ItemClass.Service.Road && !info.m_netAI.IsUnderground() && !info.m_netAI.IsOverground() && info.m_netAI is RoadBaseAI && info.m_hasPedestrianLanes && (info.m_hasForwardVehicleLanes || info.m_hasBackwardVehicleLanes)) {
                            ushort startNode = segment.m_startNode;
                            ushort endNode = segment.m_endNode;
                            Vector3 startPos = startNode.ToNode().m_position;
                            Vector3 endPos = endNode.ToNode().m_position;
                            float startDist = Mathf.Max(Mathf.Max(bounds.min.x - 64f - startPos.x, bounds.min.z - 64f - startPos.z), Mathf.Max(startPos.x - bounds.max.x - 64f, startPos.z - bounds.max.z - 64f));
                            float endDist = Mathf.Max(Mathf.Max(bounds.min.x - 64f - endPos.x, bounds.min.z - 64f - endPos.z), Mathf.Max(endPos.x - bounds.max.x - 64f, endPos.z - bounds.max.z - 64f));
                            if ((startDist < 0f || endDist < 0f) && 
                                segment.m_bounds.Intersects(bounds) && 
                                segment.GetClosestLanePosition(position, NetInfo.LaneType.Vehicle | NetInfo.LaneType.TransportVehicle, VehicleInfo.VehicleType.Car, VehicleInfo.VehicleType.None, false, out lanePos, out _, out _, out _, out _, out _)) 
                                {
                                float dist = Vector3.SqrMagnitude(position - lanePos);
                                if (dist < 400f) {
                                    return segmentID;
                                }
                            }
                        }
                        segmentID = segmentID.ToSegment().m_nextGridSegment;
                        if (++watchDog >= 36864) {
                            throw new Exception("Invalid list detected!");
                        }
                    }
                }
            }

            lanePos = default;
            return 0;
        }
    }
}