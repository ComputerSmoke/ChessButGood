using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGoldIndicator : Indicator
{
    protected override int TargetAmount() {
        return Game.blackGold;
    }
}
