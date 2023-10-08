using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPiece : Piece
{
    public GameObject spawn;
    protected override void DieEffect(Piece killer) {
        Square prevSquare = square;
        base.DieEffect(killer);
        prevSquare.board.CreatePiece(spawn, prevSquare);
    }
}
