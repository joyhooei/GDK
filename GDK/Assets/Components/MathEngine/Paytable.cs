﻿using System;
using System.Collections.Generic;

namespace GDK.MathEngine
{
    /// <summary>
    /// Represents a simple paytable for a slot game.
    /// </summary>
    [Serializable]
    public class Paytable
    {
        public ReelGroup BaseGameReelGroup { get; set; }
        public ReelGroup FreeGamesReelGroup { get; set; }
        public PaylineGroup PaylineGroup { get; set; }
        public PayComboGroup PayComboGroup { get; set; }
        public PayComboGroup ScatterComboGroup { get; set; }
        public PaytableTriggerGroup PaytableTriggerGroup { get; set; }
        public PickTableGroup PickTableGroup { get; set; }
    }
}
