using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPiece : Item
{
    public override void Acquire(Piece piece) {
        if(piece.color == 0)
            Game.whiteGold += 4;
        else if(piece.color == 1)
            Game.blackGold += 4;
    }
}
