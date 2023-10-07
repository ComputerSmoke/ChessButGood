using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPiece : Piece
{
    public int explosionRadius;
    //TODO: blow up all squares within radius of boarder
    protected override void DieEffect(Piece killer) {
        for(int i = -explosionRadius; i <= explosionRadius; i++) {
            for(int j = -explosionRadius; j <= explosionRadius; j++) {
                for(int k = -explosionRadius; k <= explosionRadius; k++) {
                    if(square.TryAdjacent((i, j, k), out Square adj) && adj.piece != null)
                        adj.piece.Die(this);
                }
            } 
        }
        Debug.Log("explosion base:");
        GiveRewards(killer);
        square.Depart(this);
        if(wrathful)
            killer.TryKill(this);
        if(blessed && square.board.id == Game.earth.id) {
            Game.heaven.PlacePiece(this, square);
        } else
            Object.Destroy(this.gameObject);
    }
}
