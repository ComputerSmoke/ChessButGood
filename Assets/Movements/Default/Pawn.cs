using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Movement
{
    public Vector3 direction;
    public override HashSet<Square> ValidSquares() {
        HashSet<Square> res = new HashSet<Square>();
        AppendForward(res);
        AppendCaptures(res);
        return res;
    }
    private void AppendForward(HashSet<Square> res) {
        if(piece.square == null)
            return;
        if(piece.square.TryAdjacent(Movement.IntVec(direction), out Square forward)) {
            if(forward.piece == null || forward.piece.CanLandMe(piece))
                res.Add(forward);
            if(forward.piece == null || !forward.piece.Blocks(piece))
                AppendDouble(res, forward);
        }
    }
    private void AppendDouble(HashSet<Square> res, Square forward) {
        if(!piece.moved && forward.TryAdjacent(Movement.IntVec(direction), out Square forward2) && (forward2.piece == null || forward2.piece.CanLandMe(piece)))
            res.Add(forward2);
    }
    private void AppendCaptures(HashSet<Square> res) {
        (int dx, int dy, int dz) = Movement.IntVec(direction);
        AppendCapture(res, (dx-1, dy, dz));
        AppendCapture(res, (dx+1, dy, dz));
    }
    private void AppendCapture(HashSet<Square> res, (int, int, int) dir) {
        if(piece.square.TryAdjacent(dir, out Square target)) {
            if(target.HasCapture(piece)) 
                res.Add(target);
        }
    }
}
