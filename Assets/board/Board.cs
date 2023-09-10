using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : ScriptableObject
{
    public Dictionary<(int, int, int), Square> squares;
    public int id;
    public void Init(int id) {
        squares = new ();
        this.id = id;
    }
    public void AddSquare(GameObject square, int x, int y, int z) {
        Square squareScript = square.GetComponent<Square>();
        squares[(x, y, z)] = squareScript;
        squareScript.board = this;
        squareScript.SetPos(x, y, z);
    }
    public static Vector3 Pos(int col, int row) {
        return new Vector3((float)col - 3.5f, (float)row - 3.5f, 0);
    }
    public static (int, int, int) GridPos(Vector3 pos) {
        return ((int)(pos.x + 4), (int)(pos.y + 4), 0);
    }
    public void CreatePiece(GameObject prefab, Square square) {
        GameObject piece = Instantiate(prefab, Board.Pos(square.x, square.y), Quaternion.identity);
        piece.GetComponent<SpriteRenderer>().sortingLayerName = "Pieces";
        square.Place(piece);
    }
}
