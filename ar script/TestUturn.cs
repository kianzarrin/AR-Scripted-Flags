﻿using AdaptiveRoads.Manager;
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

    public class TestUturn : PredicateBase {
        public override bool Condition() => SegmentEnd.Has(Uturn);
    }
}