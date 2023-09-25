using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Equippable
{
    public Vector3[] directions;
    public override void Equip(Piece piece) {
        base.Equip(piece);
        Movement top = piece.TopMovement();
        Linear pistolMove = piece.gameObject.AddComponent<Linear>();
        pistolMove.directions = directions;
        pistolMove.distance = 1;
        pistolMove.ranged = true;
        piece.AugmentMovement(pistolMove, top);
    }
    public override bool Counter(Piece killer) {
        if(killer.color == holder.color)
            return false;
        holder.equips.Remove(this);
        Object.Destroy(this.gameObject);
        killer.Die(holder);
        return true;
    }
    protected override Vector3 Scale() {
        return new Vector3(.5f, .5f, 1);
    }
    protected override Vector3 Offset() {
        return new Vector3(.25f, 0, 0);
    }
}
