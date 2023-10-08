using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericPiece : RevivePiece
{
    protected override Board ReviveSource() {
        return Game.heaven;
    }
}
