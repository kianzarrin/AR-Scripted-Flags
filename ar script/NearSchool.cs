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

    public static class MyExtensions {
        public static ref Building ToBuilding(this ushort buildingID) =>
            ref BuildingManager.instance.m_buildings.m_buffer[buildingID];
    }

    public class NearSchool : PredicateBase {
        public IEnumerable<ushort> ScanBuildingsInArea() {
            Grid buildingGrid = new Grid(BuildingManager.BUILDINGGRID_CELL_SIZE, BuildingManager.BUILDINGGRID_RESOLUTION);
            GridVector2 gridVector = new GridVector2(Node.VanillaNode.m_position, buildingGrid);
            foreach (var v in gridVector.ScanArea()) {
                ushort buildingID = BuildingManager.instance.m_buildingGrid[v.Index];
                while (buildingID != 0) {
                    yield return buildingID;
                    buildingID = buildingID.ToBuilding().m_nextGridBuilding;
                }
            }
        }

        public override bool Condition() {
            foreach (ushort buildingId in ScanBuildingsInArea()) {
                ref var building = ref buildingId.ToBuilding();
                var v = building.m_position - NodeID.ToNode().m_position;
                if(v.magnitude < 64 && building.Info.m_buildingAI is SchoolAI) {
                    return true;
                }
            }
            return false;
        }
    }
}