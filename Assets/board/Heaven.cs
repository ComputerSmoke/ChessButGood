using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heaven : Board
{
    public override void Init() {
        id = LayerMask.NameToLayer("Heaven");
        whiteSquare = Game.initializer.whiteSquareHeaven;
        blackSquare = Game.initializer.blackSquareHeaven;
        CreateBoard();
        InitPieces();
    }
    protected override void InitPieces() {
        CreatePiece(Game.initializer.coinPiece, (7, 7, 0));
    }
}
