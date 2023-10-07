using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPiece : Item
{
    public override void Acquire(Piece piece) {
        if(piece.Size() > 1)
            piece.Resize(piece.Size()-1);
    }
}
