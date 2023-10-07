using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Linear
{
    //TODO: work with larger kings
    public Vector3[] castleDirections;
    public override HashSet<Square> ValidSquares() {
        Piece piece = this.gameObject.GetComponent<Piece>();
        HashSet<Square> res = base.ValidSquares();
        foreach(Vector3 dir in castleDirections) {
            Square target = SearchCastle(Movement.IntVec(dir), piece.square, piece);
            if(target != null)
                res.Add(target);
        }
        return res;
    }
    private Square SearchCastle((int, int, int) dir, Square square, Piece piece) {
        if(square == null) return null;
        if(square.TryAdjacent(dir, out Square next)) {
            if(next.piece != null && next.piece.enableCastle && next.piece.color == piece.color && square.TryAdjacent(Reverse(dir), out Square prev) && (prev.piece == null || prev.piece == piece)) 
                return next;
            if(next.piece == null)
                return SearchCastle(dir, next, piece);
        }
        return null;
    }
    private (int, int, int) Reverse((int, int, int) dir) {
        (int x, int y, int z) = dir;
        return (-x, -y, -z);
    }
}
