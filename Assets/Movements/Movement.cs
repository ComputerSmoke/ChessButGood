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
}
