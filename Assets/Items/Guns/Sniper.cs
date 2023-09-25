using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Equippable
{
    public override void Equip(Piece piece) {
        base.Equip(piece);
        Movement top = piece.TopMovement();
        Linear shotMove = piece.gameObject.AddComponent<Linear>();
        shotMove.directions = Directions(piece);
        shotMove.ranged = true;
        piece.AugmentMovement(shotMove, top);
    }
    private static Vector3[] Directions(Piece piece) {
        if(piece.color == 0) {
            Vector3[] whiteDirs = {new Vector3(0, 1, 0)};
            return whiteDirs;
        } else if(piece.color == 1) {
            Vector3[] blackDirs = {new Vector3(0, -1, 0)};
            return blackDirs;
        } 
        Vector3[] allDirs = {new Vector3(0, 1, 0), new Vector3(0, -1, 0)};
        return allDirs;
    }
    protected override Vector3 Scale() {
        return new Vector3(1, 1, 1);
    }
    protected override Vector3 Offset() {
        return new Vector3(0, 0, 0);
    }
    protected override float Rotation() {
        return 90;
    }
    protected override bool RotateWithParent() {
        return false;
    }
}
