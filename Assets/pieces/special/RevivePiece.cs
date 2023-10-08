using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RevivePiece : Piece
{
    protected abstract Board ReviveSource();
    public override void OnArrive(Square square) {
        if(square.board != Game.earth)
            return;
        if(!ReviveSource().squares.ContainsKey((square.x, square.y, square.z)))
            return;
        Square hellSquare = ReviveSource().squares[(square.x, square.y, square.z)];
        if(hellSquare.piece == null)
            return;
        (int x, int y, int z) = ReviveOffset(hellSquare.piece);
        (int, int, int) pos = (square.x+x, square.y+y, square.z+z);
        Game.earth.PlacePiece(hellSquare.piece, pos);
    }
    private (int, int, int) ReviveOffset(Piece revivePiece) {
        if(color == 1) 
            return ReviveOffsetBlack(revivePiece);
        else if(color == 0) 
            return ReviveOffsetWhite(revivePiece);
        else {
            (int x, int y, int z) = ReviveOffsetWhite(revivePiece);
            return (y, x, z);
        }
    }
    private (int, int, int) ReviveOffsetWhite(Piece revivePiece) {
        int x = 0;
        int z = 0;
        int y = -(Size()-1)/2-revivePiece.Size()/2-1;
        return (x, y, z);
    }
    private (int, int, int) ReviveOffsetBlack(Piece revivePiece) {
        int x = 0;
        int z = 0;
        int y = Size()/2+(revivePiece.Size()-1)/2+1;
        return (x, y, z);
    }
}
