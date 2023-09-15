using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : ScriptableObject
{
    public Dictionary<(int, int, int), Square> squares;
    public int id;
    private GameObject whiteSquare;
    private GameObject blackSquare;
    public void Init(int id, GameObject whiteSquare, GameObject blackSquare) {
        squares = new ();
        this.id = id;
        this.whiteSquare = whiteSquare;
        this.blackSquare = blackSquare;
        for(int i = 0; i < 8; i++) {
            for(int j = 0; j < 8; j++) 
                ForceSquare((i, j, 0));
        }
    }
    private void AddSquare(GameObject square, int x, int y, int z) {
        Square squareScript = square.GetComponent<Square>();
        squares[(x, y, z)] = squareScript;
        squareScript.board = this;
        squareScript.SetPos(x, y, z);
        square.layer = id;
    }
    public static Vector3 Pos(int col, int row) {
        return new Vector3((float)col - 3.5f, (float)row - 3.5f, 0);
    }
    public static (int, int, int) GridPos(Vector3 pos) {
        return ((int)(pos.x + 4), (int)(pos.y + 4), 0);
    }
    public void CreatePiece(GameObject prefab, Square square) {
        GameObject piece = Instantiate(prefab, Board.Pos(square.x, square.y), Quaternion.identity);
        piece.layer = id;
        piece.GetComponent<SpriteRenderer>().sortingLayerName = "Pieces";
        square.Place(piece);
    }
    public void PlacePiece(Piece piece, Square prevSquare) {
        Square targetSquare = ForceSquare((prevSquare.x, prevSquare.y, prevSquare.z));
        targetSquare.Arrive(piece);
    }
    public Square ForceSquare((int, int, int) pos) {
        (int x, int y, int z) = pos;
        if(squares.ContainsKey(pos))
            return squares[(x, y, z)];
        GameObject prefab;
        if((x+y)%2 == 1) 
            prefab = whiteSquare;
        else 
            prefab = blackSquare;
        GameObject square = Instantiate(prefab, Pos(x, y), Quaternion.identity);
        square.GetComponent<SpriteRenderer>().sortingLayerName = "Boards";
        AddSquare(square, x, y, z);
        return squares[pos];
    }
}
