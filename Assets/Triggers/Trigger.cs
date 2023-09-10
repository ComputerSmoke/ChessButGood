using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : ScriptableObject
{
    public abstract void Arrive(Piece piece);
    public virtual bool IsCapture(Piece piece) {
        return false;
    }
}
