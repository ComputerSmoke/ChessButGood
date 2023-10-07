using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPotion : Item
{
    public override void Acquire(Piece piece) {
        Square square = piece.square;
        piece.RemoveSelf();
        Object.Destroy(piece.gameObject);
        square.board.CreatePiece(Game.initializer.demon, square);
    }
}
