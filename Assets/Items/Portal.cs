using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Item
{
    public string targetBoardName;
    private Board targetBoard;
    bool used;
    void Start() {
        int id = LayerMask.NameToLayer(targetBoardName);
        targetBoard = Game.BoardById(id);
        equips = new HashSet<Equippable>();
    }
    public override void Acquire(Piece killer) {
        if(used)
            return;
        used = true;
        Debug.Log("Portal!");
        targetBoard.PlacePiece(killer, killer.square);

    }
}
