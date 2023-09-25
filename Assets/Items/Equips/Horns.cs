using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horns : Equippable
{
    public override void Equip(Piece piece) {
        base.Equip(piece);
        piece.eatSouls = true;
    }
    protected override Vector3 Scale() {
        return new Vector3(.5f, .5f, 1);
    }
    protected override Vector3 Offset() {
        return new Vector3(0, .25f, 0);
    }
}
