using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPiece : Item
{
    public override void Die(Piece killer) {
        if(killer.color == 0)
            Game.whiteGold++;
        else if(killer.color == 1)
            Game.blackGold++;
        Object.Destroy(gameObject);
    }
}
