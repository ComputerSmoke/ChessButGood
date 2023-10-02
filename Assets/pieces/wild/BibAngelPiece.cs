using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibAngelPiece : WildPiece
{
    //TODO: turn into devil upon reaching hell
    public override void OnArrive(Square square) {
        if(square.board == Game.hell)
            return;
    }
    public override void Die(Piece killer) {
        //Die normal outside heaven
        if(square.board != Game.heaven) {
            Debug.Log("angel killed by " + killer);
            RemoveSelf();
            GiveRewards(killer);
            Object.Destroy(this.gameObject);
        } else {//If in heaven, get released to earth
            Debug.Log("angel released by " + killer);
            Game.earth.PlacePiece(this, square);
        }
    }
}
