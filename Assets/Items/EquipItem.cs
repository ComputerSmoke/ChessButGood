using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : Item
{
    public GameObject equippable;
    protected override void Acquire(Piece piece) {
        Equippable equip = Instantiate(equippable).GetComponent<Equippable>();
        equip.Equip(piece);
    }
}
