using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePiece : Item
{
    public override void Acquire(Piece piece) {
        piece.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
