using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leap : Movement
{
    public Vector3[] directions;
    public override List<Square> ValidSquares() {
        Piece piece = this.gameObject.GetComponent<Piece>();
        List<Square> res = new List<Square>();
        for(int i = 0; i < directions.Length; i++) {
            if(piece.square.TryAdjacent(directions[i], out Square target)) {
                if(target.piece != null && target.piece.color == piece.color)
                    continue;
                res.Add(target);
            }
        }
        return res;
    }
}
