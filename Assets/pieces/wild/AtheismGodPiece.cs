using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtheismGodPiece : WildPiece
{
    private int counterTurn;
    protected override void DieEffect(Piece killer) {
        Game.initializer.layerController.SetLayer("AtheistChoice");
        RemoveSelf();
        Object.Destroy(this.gameObject);
    }
    public override bool CanLandMe(Piece piece) {
        return piece.color < 2;
    }
}
