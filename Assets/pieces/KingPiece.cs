using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : Piece
{
    public override void Move(Square square) {
        if(square.piece != null && square.piece.color == color && square.piece.enableCastle) {
            if(this.square.TryAdjacent(GetDir(this.square, square), out Square adjacent) && adjacent.TryAdjacent(GetDir(this.square, square), out Square adjacent2)) {
                Piece otherPiece = square.piece;
                square.Depart(otherPiece);
                adjacent.Arrive(otherPiece);
                this.square.Depart(this);
                adjacent2.Arrive(this);
            }
            Game.turn++;
        } else
            base.Move(square);
    }
    //TODO: override death to make game loss
}
