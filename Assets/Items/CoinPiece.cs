using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPiece : Item
{
    protected override void Acquire(Piece piece) {
        if(piece.color == 0)
            Game.whiteGold++;
        else if(piece.color == 1)
            Game.blackGold++;
    }
}
