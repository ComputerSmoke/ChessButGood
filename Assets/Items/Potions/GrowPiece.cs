using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPiece : Item
{
    public override void Acquire(Piece piece) {
        piece.Resize(piece.size+1);
    }
}
