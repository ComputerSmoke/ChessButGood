using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealPiece : Item
{
    public override void Acquire(Piece piece) {
        piece.ethereal = true;
    }
}
