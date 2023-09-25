using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAI : AI
{
    public int directionOffset;
    private int moves;
    bool waited;
    /*
        When on earth, demons want to move as far as possible along one orthogonal, which rotates counterclockwise each turn.
        When in hell, demons are passive (but kill pieces which attack them - in demon AttachedPiece() script)
        Demons entering heaven are cast down to hell (in demon AttachedPiece() script)
    */
    public override Square GetMove() {
        if(!waited) {
            waited = true;
            return AttachedPiece().square;
        }
        if(AttachedPiece().square.board == Game.hell)
            return AttachedPiece().square;
        int direction = NextDir();
        HashSet<Square> squares = AttachedPiece().TopMovement().ValidSquares();
        Square res = AttachedPiece().square;
        foreach(Square square in squares) {
            if(direction == 0 && square.y > res.y || direction == 1 && square.x < res.x || direction == 2 && square.y < res.y || direction == 3 && square.x > res.x)
                res = square;
        }
        moves++;
        return res;
    }
    public int NextDir() {
        return (moves + directionOffset) % 4;
    }
}
