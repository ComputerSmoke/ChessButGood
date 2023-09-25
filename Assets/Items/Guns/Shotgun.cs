using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Equippable
{
    public override void Equip(Piece piece) {
        base.Equip(piece);
        Movement top = piece.TopMovement();
        Linear shotMove = piece.gameObject.AddComponent<Linear>();
        shotMove.directions = Directions(piece);
        shotMove.distance = 1;
        shotMove.ranged = true;
        piece.AugmentMovement(shotMove, top);
    }
    private static Vector3[] Directions(Piece piece) {
        Vector3[] whiteDirs = {
            new Vector3(-1, 1, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), 
            new Vector3(-2, 2, 0), new Vector3(-1, 2, 0), new Vector3(0, 2, 0), new Vector3(1, 2, 0), new Vector3(2, 2, 0)
        };
        Vector3[] blackDirs = new Vector3[whiteDirs.Length];
        for(int i = 0; i < whiteDirs.Length; i++)
            blackDirs[i] = whiteDirs[i]*-1;
        if(piece.color == 1) 
            return blackDirs;
        else if(piece.color == 0)
            return whiteDirs;
        Vector3[] allDirs = new Vector3[2*whiteDirs.Length];
        for(int i = 0; i < whiteDirs.Length; i++) {
            allDirs[i] = whiteDirs[i];
            allDirs[i+whiteDirs.Length] = blackDirs[i];
        }
        return allDirs;
    }
    protected override Vector3 Scale() {
        return new Vector3(.75f, .75f, 1);
    }
    protected override Vector3 Offset() {
        return new Vector3(.15f, 0, 0);
    }
    protected override float Rotation() {
        return 45;
    }
}
