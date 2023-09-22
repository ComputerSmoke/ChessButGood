using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentedMovement : Movement
{
    public Movement movement1;
    public Movement movement2;
    
    public override HashSet<Square> ValidSquares() {
        HashSet<Square> res = movement1.ValidSquares();
        res.UnionWith(movement2.ValidSquares());
        return res;
    }
}
