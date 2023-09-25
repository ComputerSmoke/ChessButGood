using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : Movement
{
    public Vector3[] directions;
    public int distance;
    public override HashSet<Square> ValidSquares() {
        HashSet<Square> res = new HashSet<Square>();
        for(int i = 0; i < directions.Length; i++) 
            AppendLine(res, piece.square, Movement.IntVec(directions[i]), 0);
        return res;
    }
    private void AppendLine(HashSet<Square> res, Square square, (int, int, int) direction, int depth) {
        if(distance > 0 && depth == distance) return;
        if(square == null) return;
        if(square.TryAdjacent(direction, out Square nextSquare)) {
            if(Available(piece, nextSquare))
                res.Add(nextSquare);
            if(nextSquare.piece == null || !nextSquare.piece.Blocks(piece)) 
                AppendLine(res, nextSquare, direction, depth+1);
        }
    }
    private bool Available(Piece piece, Square square) {
        if(!ranged && (square.piece == null || square.piece.CanLandMe(piece)))
            return true;
        return square.piece != null && square.piece.CanCaptureMe(piece);
    }
}
