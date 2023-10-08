using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int rank;
    protected Piece piece;
    public bool ranged;
    void Start() {
        piece = this.gameObject.GetComponent<Piece>();
    }
    public abstract HashSet<Square> ValidSquares();
    public virtual HashSet<Square> RangedSquares() {
        if(ranged)
            return ValidSquares();
        return new HashSet<Square>();
    }
    public static (int, int, int) IntVec(Vector3 vec) {
        return ((int)vec.x, (int)vec.y, (int)vec.z);
    }
    protected bool Blocks(List<Square> block) {
        foreach(Square square in block) {
            if(square.piece != null && square.piece != piece && square.piece.Blocks(piece)) 
                return true;
        }
        return false;
    }
    protected bool Available(List<Square> block) {
        if(block.Count < piece.Size()*piece.Size())
            return false;
        foreach(Square square in block) {
            if(square.piece != null && square.piece != piece && !square.piece.CanLandMe(piece))
                return false;
        }
        return true;
    }
    protected bool Capturable(List<Square> block) {
        if(block.Count < piece.Size()*piece.Size())
            return false;
        bool hasCapture = false;
        foreach(Square square in block) {
            if(square.piece == null || square.piece == piece)
                continue;
            if(!square.piece.CanLandMe(piece) && !square.piece.CanCaptureMe(piece))
                return false;
            if(square.piece.CanCaptureMe(piece))
                hasCapture = true;
        }
        return hasCapture;
    }
    protected bool Capturable(Square square) {
        return square.piece != null && square.piece != piece && square.piece.CanCaptureMe(piece);
    }
    protected bool Blocks(Square square) {
        return square.piece != null && square.piece != piece && square.piece.Blocks(piece);
    }
}
