using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilPiece : WildPiece
{
    private int counterTurn;
    //TODO: implement multiple hp with counter function?
    protected override bool Counter(Piece killer) {
        if(killer.color == 2)
            return true;
        return false;
    }
    public override void OnArrive(Square square) {
        //TODO: consider making devil do something cool in heaven
        if(square.board == Game.heaven)
            Game.hell.PlacePiece(this, square);
    }
    protected override void DieEffect(Piece killer) {
        if(square.board != Game.hell) {
            RemoveSelf();
            GiveRewards(killer);
            Object.Destroy(this.gameObject);
        } else {
            Game.DevilChoice(killer);
            RemoveSelf();
            Object.Destroy(this.gameObject);
        }
    }
    public override bool CanCaptureMe(Piece piece) {
        return square.board != Game.hell && base.CanCaptureMe(piece);
    }
    public override bool CanLandMe(Piece piece) {
        return square.board == Game.hell && piece.color < 2;
    }
}
