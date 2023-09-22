using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Equippable
{
    public override void Equip(Piece piece) {
        base.Equip(piece);
        //TODO: augment movement with shoot
    }
    protected override Vector3 Scale() {
        return new Vector3(.5f, .5f, 1);
    }
    protected override Vector3 Offset() {
        return new Vector3(0, .25f, 1);
    }
}
