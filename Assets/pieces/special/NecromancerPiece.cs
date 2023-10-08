using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerPiece : RevivePiece
{
    protected override Board ReviveSource() {
        return Game.hell;
    }
    protected override void DieEffect(Piece killer) {
        RemoveSelf();
        GiveRewards(killer);
        if(wrathful)
            killer.TryKill(this);
        Object.Destroy(this.gameObject);
    }
}
