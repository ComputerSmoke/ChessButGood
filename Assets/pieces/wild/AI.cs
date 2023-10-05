using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AI : MonoBehaviour
{
    protected Piece piece;
    public int lastMoved;
    public abstract Square GetMove();
    protected Piece AttachedPiece() {
        if(piece != null)
            return piece;
        piece = gameObject.GetComponent<Piece>();
        return piece;
    }
    protected Square NearestSquare(int tx, int ty, int tz) {
        HashSet<Square> moveOptions = AttachedPiece().TopMovement().ValidSquares();
        Square res = AttachedPiece().square;
        foreach(Square square in moveOptions) {
            if(SquareDist(square, tx, ty, tz) < SquareDist(res, tx, ty, tz))
                res = square;
        }
        return res;
    }
    private static int SquareDist(Square square, int tx, int ty, int tz) {
        return Math.Abs(square.x-tx) + Math.Abs(square.y - ty) + Math.Abs(square.z - tz);
    }
}
