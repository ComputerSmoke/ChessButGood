using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : ScriptableObject
{
    public Piece piece;
    public abstract List<Square> ValidSquares();
}
