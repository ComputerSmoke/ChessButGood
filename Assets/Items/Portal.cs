using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Item
{
    public string targetBoardName;
    private Board targetBoard;
    void Start() {
        int id = LayerMask.NameToLayer(targetBoardName);
        targetBoard = Game.BoardById(id);
    }
    protected override void Acquire(Piece killer) {
        targetBoard.PlacePiece(killer, killer.square);
    }
}
