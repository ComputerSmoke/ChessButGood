using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Piece
{
    public override bool Blocks(Piece piece) {
        return false;
    }
    public override bool CanLandMe(Piece piece) {
        return true;
    }
    public override bool CanCaptureMe(Piece piece) {
        return false;
    }
}
