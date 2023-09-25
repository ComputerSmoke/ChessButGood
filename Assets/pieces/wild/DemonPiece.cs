using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonPiece : Piece
{
    private DemonAI ai;
    private int counterTurn;
    private bool dying;
    private DemonAI AI() {
        if(ai != null) 
            return ai;
        ai = gameObject.GetComponent<DemonAI>();
        return ai;
    }
    protected override bool Counter(Piece killer) {
        if(square.board != Game.hell || counterTurn > Game.turn)
            return false;
        counterTurn = Game.turn;
        return true;
    }
    public override void OnArrive(Square square) {
        if(square.board == Game.heaven)
            Game.hell.PlacePiece(this, square);
    }
    public override void Die(Piece killer) {
        if(dying)
            return;
        dying = true;
        square.Depart(this);
        GiveRewards(killer);
        killer.TryKill(this);
        Object.Destroy(this.gameObject);
    }
    protected override Quaternion Rotation() {
        return Quaternion.Euler(0, 0, 90*AI().NextDir());
    }
}
