using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Item
{
    public override void Acquire(Piece piece) {
        Movement top = piece.TopMovement();
        Linear teleportMove = piece.gameObject.AddComponent<Linear>();
        teleportMove.directions = Directions(piece);
        teleportMove.distance = 1;
        piece.AugmentMovement(teleportMove, top);
    }
    private static Vector3[] Directions(Piece piece) {
        List<Vector3> dirs = new List<Vector3>();
        for(int i = -2; i < 3; i++) {
            for(int j = -2; j < 3; j++) {
                if(i == 0 && j == 0)
                    continue;
                dirs.Add(new Vector3(i, j, 0));
            }
        }
        return dirs.ToArray();
    }
}
