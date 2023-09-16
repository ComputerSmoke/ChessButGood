using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPiece : Piece
{
    public int explosionRadius;
    private bool exploding;
    public override void Die(Piece killer) {
        if(exploding)
            return;
        Debug.Log("exploding");
        exploding = true;
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
        if(blessed && square.board.id == Game.earth.id) {
            Game.heaven.PlacePiece(this, square);
        } else
            Object.Destroy(this.gameObject);
    }
}
