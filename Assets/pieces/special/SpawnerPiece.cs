using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPiece : Piece
{
    public GameObject spawn;
    public Vector3[] spawnDirs;
    protected override void DieEffect(Piece killer) {
        Square prevSquare = square;
        base.DieEffect(killer);
        foreach(Vector3 dir in spawnDirs) {
            int x = prevSquare.x + (int)dir.x;
            int y = prevSquare.y + (int)dir.y;
            int z = prevSquare.z + (int)dir.z;
            prevSquare.board.CreatePiece(spawn, (x, y, z));
        }
    }
}
