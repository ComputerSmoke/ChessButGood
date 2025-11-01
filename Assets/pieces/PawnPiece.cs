using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : Piece
{
    //TODO: work with larger pawns
    public override void Move(Square square) {
        List<Square> squaresBetween = SquaresBetween(this.square, square);
        foreach(Square intermittent in squaresBetween) {
            ShadowVuln trigger = ScriptableObject.CreateInstance<ShadowVuln>();
            trigger.Init(this, Game.turn);
            intermittent.triggers.Add(trigger);
        }
        base.Move(square);
        if(square.piece == this && !square.TryAdjacent(gameObject.GetComponent<Pawn>().direction, out Square adj)) {
            //TODO: add option to promote to non-queen pieces, promote to demon in hell
            Object.Destroy(this.gameObject);
            if(color == 0) 
                square.board.CreatePiece(Game.initializer.whiteQueen, square);
            else 
                square.board.CreatePiece(Game.initializer.blackQueen, square);
        }
    }
    private List<Square> SquaresBetween(Square square1, Square square2) {
        return SquaresBetweenRec(square1, square1, square2);
    }
    private List<Square> SquaresBetweenRec(Square startSquare, Square square1, Square square2) {
        if(square1 == square2) return new List<Square>();
        (int, int, int) dir = GetDir(square1, square2);
        if(square1.TryAdjacent(dir, out Square next)) {
            List<Square> res = SquaresBetweenRec(startSquare, next, square2);
            if(square1 != startSquare) 
                res.Add(square1);
            return res;
        }
        return new List<Square>();
    }
}
