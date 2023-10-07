using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrath : Item
{
    public override void Acquire(Piece piece) {
        piece.wrathful = true;
    }
}
