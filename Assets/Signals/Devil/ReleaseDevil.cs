using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class ReleaseDevil : EmptySignal
{
    public override void Execute() {
        Debug.Log("releasing devil");
        Piece piece = Game.earth.CreatePiece(Game.initializer.devil, (1, 3, 0)).GetComponent<Piece>();
        piece.Resize(1);
        Forward();
    }
}
