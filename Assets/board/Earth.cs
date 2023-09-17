using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Board
{
    public override void Init() {
        id = LayerMask.NameToLayer("Earth");
        whiteSquare = Game.initializer.whiteSquare;
        blackSquare = Game.initializer.blackSquare;
        CreateBoard();
        InitPieces();
    }
    protected override void InitPieces() {
        CreatePiece(Game.initializer.whiteRook, (0, 0, 0));
        CreatePiece(Game.initializer.whiteKnight, (1, 0, 0));
        CreatePiece(Game.initializer.whiteBishop, (2, 0, 0));
        CreatePiece(Game.initializer.whiteQueen, (3, 0, 0));
        CreatePiece(Game.initializer.whiteKing, (4, 0, 0));
        CreatePiece(Game.initializer.whiteBishop, (5, 0, 0));
        CreatePiece(Game.initializer.whiteKnight, (6, 0, 0));
        CreatePiece(Game.initializer.whiteRook, (7, 0, 0));
        
        CreatePiece(Game.initializer.blackRook, (0, 7, 0));
        CreatePiece(Game.initializer.blackKnight, (1, 7, 0));
        CreatePiece(Game.initializer.blackBishop, (2, 7, 0));
        CreatePiece(Game.initializer.blackQueen, (3, 7, 0));
        CreatePiece(Game.initializer.blackKing, (4, 7, 0));
        CreatePiece(Game.initializer.blackBishop, (5, 7, 0));
        CreatePiece(Game.initializer.blackKnight, (6, 7, 0));
        CreatePiece(Game.initializer.blackRook, (7, 7, 0));
        for(int i = 0; i < 8; i++) {
            CreatePiece(Game.initializer.whitePawn, (i, 1, 0));
            CreatePiece(Game.initializer.blackPawn, (i, 6, 0));
        }

        CreatePiece(Game.initializer.coinPiece, (0, 4, 0));
        CreatePiece(Game.initializer.coinPiece, (7, 3, 0));
    }
}
