using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearLeap : Movement
{
    public Vector3[] linearDirections;
    public Vector3[] leapDirections;
    public int linearDistance;
    public int leapDistance;
    public override List<Square> ValidSquares() {
        List<Square> leap = Linear.CompValid(leapDirections, leapDistance, piece);
        List<Square> linear = Linear.CompValid(linearDirections, linearDistance, piece);
        HashSet<Square> res = new HashSet<Square>(leap);
        res.UnionWith(linear);
        return new List<Square>(res);
    }
}
