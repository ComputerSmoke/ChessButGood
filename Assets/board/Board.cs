using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : ScriptableObject
{
    public Dictionary<(int, int, int), Square> squares;
    public void Init() {
        squares = new ();
    }
    public void AddSquare(GameObject square, int x, int y, int z) {
        Square squareScript = square.GetComponent<Square>();
        squares[(x, y, z)] = squareScript;
        squareScript.board = this;
        squareScript.SetPos(x, y, z);
    }
}
