using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAI : AI
{
    public int directionOffset;
    private int moves;
    public int startTurn;
    public DevilAI clone;
    /*
        When on earth, do the following:
            1. Set panels on fire
            2. flame clone (player teamup becomes available)
            3. Spawn demons 
            4. move to center (if alone), grow to 4x4
        Then spend subsequent moves going 1 square down, 2 up, 3 down, 4 up, etc.
        When in hell, devil is passive
    */
    public override Square GetMove() {
        if(startTurn == Game.turn || AttachedPiece().square.board == Game.hell)
            return AttachedPiece().square;
        if(moves == 0)
            SpawnFire();
        else if(moves == 1)
            FlameClone();
            //TODO: give players teamup option
        else if(moves == 2)
            SpawnDemons();
        else if(moves == 4)
            Grow();
        else if (moves > 4) {
            Square move = MoveVert();
            moves++;
            return move;
        }
        moves++;
        return AttachedPiece().square;
    }
    public void SpawnFire() {
        List<Square> boarder = Boarder();
        foreach(Square square in boarder) 
            AttachedPiece().square.board.CreatePiece(Game.initializer.fire, square);
    }
    private List<Square> Boarder() {
        List<Square> res = new();
        int size = AttachedPiece().Size();
        for(int i = -(size-1)/2-1; i <= size/2+1; i++) {
            for(int j = -(size-1)/2-1; j <= size/2+1; j++) {
                if(i != -(size-1)/2-1 && i != size/2+1 && j != -(size-1)/2-1 && j != size/2+1)
                    continue;
                if(AttachedPiece().square.TryAdjacent((i, j, 0), out Square adj))
                    res.Add(adj);
            }
        }
        return res;
    }
    private void FlameClone() {
        int cx = (AttachedPiece().square.x + 4) % 8;
        int cy = AttachedPiece().square.y;
        int cz = AttachedPiece().square.z;
        GameObject cloneObject = AttachedPiece().square.board.CreatePiece(Game.initializer.devil, (cx, cy, cz));
        clone = cloneObject.GetComponent<DevilAI>();
        clone.clone = this;
        clone.moves = moves + 1;
        clone.startTurn = Game.turn;
        cloneObject.GetComponent<Piece>().Resize(1);
        clone.SpawnFire();
    }
    private void SpawnDemons() {
        Debug.Log("demons spawning");
        Square square = AttachedPiece().square;
        Board board = square.board;
        GameObject demon1 = board.CreatePiece(Game.initializer.demon, (square.x-1, square.y-1, square.z));
        GameObject demon2 = board.CreatePiece(Game.initializer.demon, (square.x+2, square.y-1, square.z));
        board.CreatePiece(Game.initializer.demon, (square.x+2, square.y+2, square.z));
        board.CreatePiece(Game.initializer.demon, (square.x-1, square.y+2, square.z));
        demon1.GetComponent<DemonAI>().directionOffset = 2;
        demon2.GetComponent<DemonAI>().directionOffset = 2;
    }
    private void Grow() {
        //TODO: increase hp
        Debug.Log("growing");
        Square square = AttachedPiece().square;
        Board board = AttachedPiece().square.board;
        if(clone == null) 
            board.PlacePiece(AttachedPiece(), (3, 3, square.z));
        AttachedPiece().Resize(3);
    }
    private Square MoveVert() {
        int dir = ((moves-3) % 2) * 2 - 1;
        int dist = moves-3;
        int tx = AttachedPiece().square.x;
        int ty = AttachedPiece().square.y + dist*dir;
        return NearestSquare(tx, ty, AttachedPiece().square.z);
    }
    public int NextDir() {
        return (moves + directionOffset) % 4;
    }
}
