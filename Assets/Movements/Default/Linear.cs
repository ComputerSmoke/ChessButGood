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
            List<Square> block = square.AdjacentBlock(piece.Size());
            if(Available(piece, block))
                res.Add(nextSquare);
            if(!Blocks(piece, block)) 
                AppendLine(res, nextSquare, direction, depth+1);
        }
    }
    private bool Blocks(Piece piece, List<Square> block) {
        foreach(Square square in block) {
            if(square.piece != null && square.piece != piece && square.piece.Blocks(piece)) 
                return true;
        }
        return false;
    }
    private bool Available(Piece piece, List<Square> block) {
        foreach(Square square in block) {
            if(ranged && (square.piece == null || square.piece == piece || !square.piece.CanCaptureMe(piece)))
                return false;
            if(!ranged && square.piece != null && square.piece != piece && !square.piece.CanLandMe(piece))
                return false;
        }
        return true;
    }
}
