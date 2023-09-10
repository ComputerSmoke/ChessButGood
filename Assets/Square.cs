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
        if(this.piece != null) 
            this.piece.Die(piece);
        this.piece = piece;
        piece.square = this;
        piece.gameObject.transform.position = Board.Pos(x, y);
    }
    public void Depart(Piece piece) {
        if(piece != this.piece)
            return;
        this.piece = null;
        piece.square = null;
    }
    public void Place(GameObject piece) {
        this.piece = piece.GetComponent<Piece>();
        if(this.piece == null)
            Debug.Log("Warning! Placing piece without Piece script.");
        else
            this.piece.square = this;
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
        if(piece != null && piece.color != arrival.color) 
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
