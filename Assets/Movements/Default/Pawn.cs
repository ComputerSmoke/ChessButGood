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
            List<Square> block = forward.AdjacentBlock(piece.Size());
            if(Available(block))
                res.Add(forward);
            if(!Blocks(block))
                AppendDouble(res, forward);
        }
    }
    private void AppendDouble(HashSet<Square> res, Square forward) {
        if(!piece.moved && forward.TryAdjacent(Movement.IntVec(direction), out Square forward2)) {
            List<Square> block = forward.AdjacentBlock(piece.Size());
            if(Available(block))
                res.Add(forward2);
        }
    }
    private void AppendCaptures(HashSet<Square> res) {
        (int dx, int dy, int dz) = Movement.IntVec(direction);
        AppendCapture(res, (dx-1, dy, dz));
        AppendCapture(res, (dx+1, dy, dz));
    }
    private void AppendCapture(HashSet<Square> res, (int, int, int) dir) {
        if(piece.square.TryAdjacent(dir, out Square target)) {
            List<Square> block = target.AdjacentBlock(piece.Size());
            if(Capturable(block)) 
                res.Add(target);
        }
    }
}
