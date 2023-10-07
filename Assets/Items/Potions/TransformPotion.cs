using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPotion : Item
{
    public override void Acquire(Piece piece) {
        Square square = piece.square;
        piece.RemoveSelf();
        Object.Destroy(piece.gameObject);
        GameObject demonObj = square.board.CreatePiece(Game.initializer.demon, square);
        demonObj.GetComponent<DemonPiece>().createTurn = Game.turn;
        if(piece.color == 1)
            demonObj.GetComponent<DemonAI>().directionOffset = 2;
        else if(piece.color > 1)
            demonObj.GetComponent<DemonAI>().directionOffset = 1;
    }
}
