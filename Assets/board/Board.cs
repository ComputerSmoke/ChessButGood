using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board : ScriptableObject
{
    public Dictionary<(int, int, int), Square> squares;
    public int id;
    protected GameObject whiteSquare;
    protected GameObject blackSquare;
    protected int minRow;
    protected int maxRow;
    protected float prevPieceRotation;
    public abstract void Init();
    protected abstract void InitPieces();
    protected virtual void CreateBoard() {
        squares = new ();
        for(int i = 0; i < 8; i++) {
            for(int j = 0; j < 8; j++) 
                ForceSquare((i, j, 0));
        }
        minRow = 0;
        maxRow = 7;
    }
    protected void AddSquare(GameObject square, int x, int y, int z) {
        Square squareScript = square.GetComponent<Square>();
        squares[(x, y, z)] = squareScript;
        squareScript.board = this;
        squareScript.SetPos(x, y, z);
        square.layer = id;
    }
    public static Vector3 Pos(int col, int row) {
        return new Vector3((float)col - 3.5f, (float)row - 3.5f, 0);
    }
    public static (int, int, int) GridPos(Vector3 pos) {//TODO: somehow getting y=0 when clicking rank -1 added by mercs
        return ((int)(pos.x + 4), (int)(pos.y + 4), 0);
    }
    public void CreatePiece(GameObject prefab, (int, int, int) pos) {
        CreatePiece(prefab, ForceSquare(pos));
    }
    public void CreatePiece(GameObject prefab, Square square) {
        GameObject piece = Instantiate(prefab, Board.Pos(square.x, square.y), Quaternion.identity);
        piece.layer = id;
        piece.GetComponent<SpriteRenderer>().sortingLayerName = "Pieces";
        square.Place(piece);
    }
    public void PlacePiece(Piece piece, Square prevSquare) {
        Square targetSquare = ForceSquare((prevSquare.x, prevSquare.y, prevSquare.z));
        piece.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, prevPieceRotation));
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
    public bool AllPlaceOccupied(int color) {
        return OpenSquares(color).Count == 0;
    }
    public void ExpandBackrow(int color) {
        int y;
        if(color == 0)
            y = minRow - 1;
        else
            y = maxRow + 1;
        for(int x = 0; x < 8; x++) 
            ForceSquare((x, y, 0));
    }
    public HashSet<Square> OpenSquares(int side) {
        HashSet<Square> res = new HashSet<Square>();
        int start,end;
        if(side == 0) {
            start = minRow;
            end = 3;
        } else {
            start = 4;
            end = maxRow;
        }
        foreach((int, int, int) pos in squares.Keys) {
            (int x, int y, int z) = pos;
            if(y >= start && y <= end && squares[(x, y, z)].piece == null)
                res.Add(squares[(x, y, z)]);
        }
        return res;
    }
    public HashSet<Square> OpenAndFriendlySquares(int side) {
        HashSet<Square> res = new HashSet<Square>();
        foreach(Square square in squares.Values) {
            if(square.piece == null || square.piece.color == side)
                res.Add(square);
        }
        return res;
    }
    public void RotatePieces(float rot) {
        if(rot == prevPieceRotation) 
            return;
        prevPieceRotation = rot;
        Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, rot));
        foreach(Square square in squares.Values) {
            if(square.piece != null)
                square.piece.gameObject.transform.rotation = newRot;
        }
    }
}
