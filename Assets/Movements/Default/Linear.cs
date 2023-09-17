using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : Movement
{
    public Vector3[] directions;
    public int distance;
    public override List<Square> ValidSquares() {
        return CompValid(directions, distance, piece);
    }
    public static List<Square> CompValid(Vector3[] directions, int distance, Piece piece) {
        List<Square> res = new List<Square>();
        for(int i = 0; i < directions.Length; i++) 
            AppendLine(distance, res, piece.square, Movement.IntVec(directions[i]), 0, piece);
        return res;
    }
    private static void AppendLine(int distance, List<Square> res, Square square, (int, int, int) direction, int depth, Piece piece) {
        if(distance > 0 && depth == distance) return;
        if(square == null) return;
        if(square.TryAdjacent(direction, out Square nextSquare)) {
            if(Available(piece, nextSquare))
                res.Add(nextSquare);
            if(nextSquare.piece == null || !nextSquare.piece.Blocks(piece)) 
                AppendLine(distance, res, nextSquare, direction, depth+1, piece);
        }
    }
    static bool Available(Piece piece, Square square) {
        return square.piece == null || square.piece.CanLandMe(piece) || square.piece.CanCaptureMe(piece);
    }
}
