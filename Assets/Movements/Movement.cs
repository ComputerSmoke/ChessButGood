using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected Piece piece;
    void Start() {
        piece = this.gameObject.GetComponent<Piece>();
    }
    public abstract List<Square> ValidSquares();
    public static (int, int, int) IntVec(Vector3 vec) {
        return ((int)vec.x, (int)vec.y, (int)vec.z);
    }
}
