using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePiece : WildPiece
{
    protected override void DieEffect(Piece killer) {
        RemoveSelf();
        killer.TryKill(this);
        Object.Destroy(this.gameObject);
    }
}
