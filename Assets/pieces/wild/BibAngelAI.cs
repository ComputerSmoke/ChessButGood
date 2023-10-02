using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BibAngelAI : AI
{
    public int directionOffset;
    /*
        When in heaven, biblically accurate angel is passive.
        When on earth, angel moves 1 square towards piece with most kills
    */
    public override Square GetMove() {
        if(AttachedPiece().square.board != Game.earth)
            return AttachedPiece().square;
        Square target = AcquireTarget();
        int dx = 0;
        int dy = 0;
        if(target.x - AttachedPiece().square.x != 0)
            dx = (target.x - AttachedPiece().square.x) / Math.Abs(target.x - AttachedPiece().square.x);
        if(target.y - AttachedPiece().square.y != 0)
            dy = (target.y - AttachedPiece().square.y) / Math.Abs(target.y - AttachedPiece().square.y);

        HashSet<Square> squares = AttachedPiece().TopMovement().ValidSquares();
        Square res = AttachedPiece().square;
        int maxMove = 0;
        foreach(Square square in squares) {
            if((square.x - AttachedPiece().square.x) * dx + (square.y - AttachedPiece().square.y) * dy > maxMove) {
                res = square;
                maxMove = (square.x - AttachedPiece().square.x) * dx + (square.y - AttachedPiece().square.y) * dy;
            }
        }
        return res;
    }
    private Square AcquireTarget() {
        HashSet<Piece> pieces = AttachedPiece().square.board.Pieces();
        Piece mostKills = null;
        bool tiedForMost = false;
        foreach(Piece piece in pieces) {
            if(piece == AttachedPiece())
                continue;
            if(mostKills == null || piece.kills > mostKills.kills) {
                mostKills = piece;
                tiedForMost = false;
            }
            else if(piece.kills == mostKills.kills)
                tiedForMost = true;
        }
        if(mostKills == null)
            return AttachedPiece().square;
        Square target = mostKills.square;
        if(tiedForMost)
            target = AttachedPiece().square;
        return target;
    }
}
