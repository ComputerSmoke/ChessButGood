using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Square square;
    public bool moved;
    public int color;
    public bool eatShadows;
    public bool eatSouls;
    public bool enableCastle;
    public bool blessed;
    public virtual void Move(Square square) {
        this.square.Depart(this);
        square.Arrive(this);
        this.moved = true;
        Game.turn++;
    }
    public virtual void Die(Piece killer) {
        Square prevSquare = square;
        square.Depart(this);
        if(killer.eatSouls || prevSquare.board.id != Game.earth.id)
            Object.Destroy(this.gameObject);
        else if(blessed)
            Game.heaven.PlacePiece(this, prevSquare);
        else
            Game.hell.PlacePiece(this, prevSquare);

    }
    public Movement Movement() {
        return this.gameObject.GetComponent<Movement>();
    } 
    public bool CanReach(Square square) {
        return Movement().ValidSquares().Contains(square);
    }
    protected (int,int,int) GetDir(Square square1, Square square2) {
        int dx = GetDirOne(square1.x, square2.x);
        int dy = GetDirOne(square1.y, square2.y);
        int dz = GetDirOne(square1.z, square2.z);
        return (dx, dy, dz);
    }
    private int GetDirOne(int x1, int x2) {
        if(x1==x2) return 0;
        if(x1<x2) return 1;
        return -1;
    }
}
