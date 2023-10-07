using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    public Dictionary<(int, int, int), Square> squares;
    public int id;
    protected GameObject whiteSquare;
    protected GameObject blackSquare;
    protected int minRow;
    protected int maxRow;
    protected float prevPieceRotation;
    public bool destroyed;
    public void Destroy() {
        foreach(Square square in squares.Values) {
            if(square.piece != null)
                Object.Destroy(square.piece.gameObject);
            Object.Destroy(square.gameObject);
        }
        squares.Clear();
        destroyed = true;
    }
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
    public GameObject CreatePiece(GameObject prefab, (int, int, int) pos) {
        return CreatePiece(prefab, ForceSquare(pos));
    }
    public GameObject CreatePiece(GameObject prefab, Square square) {
        if(destroyed) return null;
        GameObject piece = Instantiate(prefab, Board.Pos(square.x, square.y), Quaternion.identity);
        piece.layer = id;
        Piece pieceScript = piece.GetComponent<Piece>();
        pieceScript.Resize(pieceScript.size);
        piece.GetComponent<SpriteRenderer>().sortingLayerName = "Pieces";
        square.Place(piece);
        pieceScript.OnCreate();
        return piece;
    }
    public void PlacePiece(Piece piece, (int, int, int) pos) {
        if(destroyed) {
            piece.RemoveSelf();
            Object.Destroy(piece.gameObject);
            return;
        }
        (int tx, int ty, int tz) = pos;
        Square targetSquare = ForceSquare((tx, ty, tz));
        piece.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, prevPieceRotation));
        targetSquare.Arrive(piece);
    }
    public void PlacePiece(Piece piece, Square prevSquare) {
        int ty = prevSquare.y;
        if(prevSquare.y > 3 && piece.color == 0) 
            ty = 7 - prevSquare.y;
        else if(prevSquare.y < 4 && piece.color == 1) 
            ty = 7 - prevSquare.y;
        PlacePiece(piece, (prevSquare.x, ty, prevSquare.z));
    }
    public Square ForceSquare((int, int, int) pos) {
        if(destroyed) return null;
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
    public void PassTurn(bool moveDelay) {
        if(destroyed) return;
        float delayMult = moveDelay ? .5f : 0f;
        int i = 1;
        //Move AI pieces
        foreach(Square square in squares.Values) {
            if(square.piece != null && square.piece.gameObject.TryGetComponent<AI>(out AI ai) && ai.lastMoved < Game.turn) {
                ai.lastMoved = Game.turn;
                StartCoroutine(DelayedMove(((float)i)*delayMult, square.piece, ai));
                i++;
            }
        }
    }
    public HashSet<Piece> Pieces() {
        HashSet<Piece> pieces = new HashSet<Piece>();
        foreach(Square square in squares.Values) {
            if(square.piece != null)
                pieces.Add(square.piece);
        }
        return pieces;
    }
    private IEnumerator DelayedMove(float time, Piece piece, AI ai) {
        yield return new WaitForSeconds(time);
        Square target = ai.GetMove();
        if(target != piece.square)
            piece.Move(target);
    }
}
