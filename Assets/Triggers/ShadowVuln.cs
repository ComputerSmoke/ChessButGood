using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowVuln : Trigger
{
    Piece piece;
    int turn;
    public void Init(Piece piece, int turn) {
        this.piece = piece;
        this.turn = turn;
    }
    public override void Arrive(Piece arrival) {
        if(IsCapture(arrival))
            piece.Die(arrival);
    }
    public override bool IsCapture(Piece arrival) {
        if(piece == null) return false;
        int currentTurn = piece.square.board.game.turn;
        return currentTurn - turn < 2 && arrival.color != piece.color && arrival.eatShadows;
    }
}
