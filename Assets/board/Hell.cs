using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hell : Board
{
    public override void Init() {
        id = LayerMask.NameToLayer("Hell");
        whiteSquare = Game.initializer.whiteSquareHell;
        blackSquare = Game.initializer.blackSquareHell;
        CreateBoard();
        InitPieces();
    }
    //TODO: add hell pieces
    protected override void InitPieces() {
        CreatePiece(Game.initializer.coinPiece, (0, 0, 0));
        CreatePiece(Game.initializer.coinPiece, (5, 0, 0));
        CreatePiece(Game.initializer.coinPiece, (1, 5, 0));
        CreatePiece(Game.initializer.coinPiece, (3, 7, 0));
        CreatePiece(Game.initializer.coinPiece, (7, 7, 0));
    }
    protected override void CreateBoard() {
        base.CreateBoard();
        ForceSquare((8, 7, 0));
        ForceSquare((9, 6, 0));
        ForceSquare((10, 6, 0));
        ForceSquare((9, 5, 0));
        ForceSquare((10, 5, 0));
    }
}
