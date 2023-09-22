using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullMovement : Movement
{
    public override HashSet<Square> ValidSquares() {
        return new HashSet<Square>();
    }
}
