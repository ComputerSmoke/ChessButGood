using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGoldIndicator : Indicator
{
    protected override int TargetAmount() {
        return Game.whiteGold;
    }
}
