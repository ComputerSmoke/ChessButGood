using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
