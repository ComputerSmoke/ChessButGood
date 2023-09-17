using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullMovement : Movement
{
    public override List<Square> ValidSquares() {
        return new List<Square>();
    }
}
