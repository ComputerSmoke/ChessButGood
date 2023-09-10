using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Movement
{
    public Vector3 direction;
    public override List<Square> ValidSquares() {
        Piece piece = this.gameObject.GetComponent<Piece>();
        List<Square> res = new List<Square>();
        AppendForward(res, piece);
        AppendCaptures(res, piece);
        return res;
    }
    private void AppendForward(List<Square> res, Piece piece) {
        if(piece.square.TryAdjacent(Movement.IntVec(direction), out Square forward) && forward.piece == null) {
            res.Add(forward);
            AppendDouble(res, piece, forward);
        }
    }
    private void AppendDouble(List<Square> res, Piece piece, Square forward) {
        if(!piece.moved && forward.TryAdjacent(Movement.IntVec(direction), out Square forward2) && forward2.piece == null) 
            res.Add(forward2);
    }
    private void AppendCaptures(List<Square> res, Piece piece) {
        //TODO: add enpassant
        (int dx, int dy, int dz) = Movement.IntVec(direction);
        AppendCapture(res, piece, (dx-1, dy, dz));
        AppendCapture(res, piece, (dx+1, dy, dz));
    }
    private void AppendCapture(List<Square> res, Piece piece, (int, int, int) dir) {
        if(piece.square.TryAdjacent(dir, out Square target)) {
            //TODO: add friendly/ally protection
            if(!target.HasCapture(piece)) return;
            res.Add(target);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
