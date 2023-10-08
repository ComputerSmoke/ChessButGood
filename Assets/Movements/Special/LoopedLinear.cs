using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedLinear : Linear
{
    
    protected override void AppendLine(HashSet<Square> res, Square square, (int, int, int) direction, int depth) {
        AppendLineRec(res, new HashSet<Square>(), square, square, direction, depth);
    }
    private void AppendLineRec(HashSet<Square> res, HashSet<Square> visited, Square square, Square firstSquare, (int, int, int) direction, int depth) {
        if(distance > 0 && depth == distance) return;
        if(square == null) return;
        if(square.TryAdjacent(direction, out Square nextSquare)) {
            AppendStep(res, visited, nextSquare, firstSquare, direction, depth);
        } else {
            (int x, int y, int z) = direction;
            Square farSquare = FurthestInDir((-x, -y, -z), square);
            if(!visited.Contains(farSquare))
                AppendStep(res, visited, farSquare, firstSquare, direction, depth);
        }
    }
    private void AppendStep(HashSet<Square> res, HashSet<Square> visited, Square nextSquare, Square firstSquare, (int, int, int) direction, int depth) {
        visited.Add(nextSquare);
        List<Square> block = nextSquare.AdjacentBlock(ranged ? 1 : piece.Size());
        if(nextSquare != firstSquare && (Capturable(block) || (!ranged && Available(block))))
            res.Add(nextSquare);
        if(!Blocks(block)) 
            AppendLineRec(res, visited, nextSquare, firstSquare, direction, depth+1);
    }
    private Square FurthestInDir((int, int, int) direction, Square square) {
        if(square.TryAdjacent(direction, out Square nextSquare))
            return FurthestInDir(direction, nextSquare);
        return square;
    }
}
