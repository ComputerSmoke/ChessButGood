using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
    void Move(Square square);
    void Die(Piece killer);
    Movement Movement();
    bool CanReach(Square square);
}
