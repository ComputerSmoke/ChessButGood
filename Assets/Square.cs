using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Piece piece;
    public Board board;
    public int x,y,z;
    private Color defaultColor;
    public List<Trigger> triggers;
    public void Arrive(Piece piece) {
        foreach(Trigger trigger in triggers)
            trigger.Arrive(piece);
        Piece prevPiece = this.piece;
        this.piece = piece;
        piece.square = this;
        piece.gameObject.transform.position = Board.Pos(x, y);
        piece.gameObject.layer = board.id;
        if(prevPiece != null) 
            prevPiece.Die(piece);
    }
    public void Depart(Piece piece) {
        if(piece != this.piece)
            return;
        this.piece = null;
    }
    public void Place(GameObject piece) {
        Arrive(piece.GetComponent<Piece>());
    }
    public void SetPos(int x, int y, int z) {
        this.x = x;
        this.y = y; 
        this.z = z;
    }
    public bool TryAdjacent(Vector3 dir, out Square res) {
        if(TryAdjacent(Movement.IntVec(dir), out Square square)) {
            res = square;
            return true;
        }
        res = null;
        return false;
    }
    public bool TryAdjacent((int, int, int) dir, out Square res) {
        (int dx, int dy, int dz) = dir;
        if(!board.squares.ContainsKey((x+dx,y+dy,z+dz))) {
            res = null;
            return false;
        }
        res = board.squares[(x+dx,y+dy,z+dz)];
        return true;
    }
    public bool HasCapture(Piece arrival) {
        if(piece != null && piece.CanCaptureMe(arrival)) 
            return true;
        foreach(Trigger trigger in triggers) {
            if(trigger.IsCapture(arrival))
                return true;
        }
        return false;
    }
    void Start() {
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void Update() {
        bool highlighted = Game.HighlightSquares().Contains(this);
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if(!highlighted) 
            renderer.color = defaultColor;
        else
            renderer.color = Color.green;
    }
}
