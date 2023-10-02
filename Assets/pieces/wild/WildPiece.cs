using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPiece : Piece
{
    public AI ai;
    
    protected AI Ai() {
        if(ai != null) 
            return ai;
        ai = gameObject.GetComponent<DemonAI>();
        return ai;
    }
}
