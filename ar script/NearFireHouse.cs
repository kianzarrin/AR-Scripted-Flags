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
    using ColossalFramework;
    using UnityEngine;

    public class NearFireHouse : PredicateBase {
        public override bool Condition() {
            ZoneManager.instance.m_zoneGrid[]
        }

        Vector3 Pos => Node.VanillaNode.m_position;
        int ZoneIndex() {
            int x = Mathf.Clamp((int)(Pos.x / ZoneManager.ZONEGRID_CELL_SIZE + 75), 0, ZoneManager.ZONEGRID_RESOLUTION-1);
            int z = Mathf.Clamp((int)(Pos.z / 64f + 75f), 0, 149);
            int num3 = z * 150 + x;
        }
        
    }
}