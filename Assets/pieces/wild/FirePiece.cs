using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePiece : WildPiece
{
    protected override void DieEffect(Piece killer) {
        Square mySquare = this.square;
        RemoveSelf();
        if(killer.square == mySquare)
            killer.TryKill(this);
        Object.Destroy(this.gameObject);
    }
}
