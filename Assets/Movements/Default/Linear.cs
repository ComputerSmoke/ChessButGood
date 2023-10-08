using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : Movement
{
    public Vector3[] directions;
    public int distance;
    public override HashSet<Square> ValidSquares() {
        HashSet<Square> res = new HashSet<Square>();
        for(int i = 0; i < directions.Length; i++) 
            AppendLine(res, piece.square, Movement.IntVec(directions[i]), 0);
        return res;
    }
    protected virtual void AppendLine(HashSet<Square> res, Square square, (int, int, int) direction, int depth) {
        if(distance > 0 && depth == distance) return;
        if(square == null) return;
        if(square.TryAdjacent(direction, out Square nextSquare)) {
            if(nextSquare == square) return;
            List<Square> block = nextSquare.AdjacentBlock(ranged ? 1 : piece.Size());
            if((!ranged && (Capturable(block) || Available(block))))
                res.Add(nextSquare);
            else if(ranged && Capturable(nextSquare))
                res.Add(nextSquare);
            if(!ranged && !Blocks(block) || ranged && !Blocks(nextSquare)) 
                AppendLine(res, nextSquare, direction, depth+1);
        }
    }
}
