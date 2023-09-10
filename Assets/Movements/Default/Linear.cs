using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linear : Movement
{
    public Vector3[] directions;
    public int distance;
    public override List<Square> ValidSquares() {
        Piece piece = this.gameObject.GetComponent<Piece>();
        List<Square> res = new List<Square>();
        for(int i = 0; i < directions.Length; i++) 
            AppendLine(res, piece.square, Movement.IntVec(directions[i]), 0, piece);
        return res;
    }
    private void AppendLine(List<Square> res, Square square, (int, int, int) direction, int depth, Piece piece) {
        if(distance > 0 && depth == distance) return;
        if(square == null) return;
        if(square.TryAdjacent(direction, out Square nextSquare)) {
            if(nextSquare.piece == null || nextSquare.piece.color != piece.color)
                res.Add(nextSquare);
            if(nextSquare.piece == null) 
                AppendLine(res, nextSquare, direction, depth+1, piece);
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
