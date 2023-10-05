using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePiece : WildPiece
{
    private bool dying;
    public override void Die(Piece killer) {
        if(dying)
            return;
        dying = true;
        RemoveSelf();
        killer.TryKill(this);
        Object.Destroy(this.gameObject);
    }
}
