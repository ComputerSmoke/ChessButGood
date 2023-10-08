using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Square : MonoBehaviour
{
    public Piece piece;
    public Board board;
    public int x,y,z;
    private Color defaultColor;
    public List<Trigger> triggers;
    public void Arrive(Piece arrival) {
        foreach(Trigger trigger in triggers)
            trigger.Arrive(arrival);
        List<Square> block = AdjacentBlock(arrival.Size());
        Piece counterer = SearchCounterer(arrival, block);
        if(counterer != null) 
            CounteredArrival(arrival, counterer, block);
        else 
            UncounteredArrival(arrival, block);
    }
    private void UncounteredArrival(Piece arrival, List<Square> block) {
        Debug.Log("uncountered arrive. block size: " + block.Count);
        arrival.RemoveSelf();
        arrival.square = this;
        HashSet<Piece> dying = new HashSet<Piece>();
        foreach(Square square in block) {
            if(square.piece != null && square.piece != arrival) {
                Debug.Log("Adding to dyers: " + square.piece + " with killer: " + arrival);
                dying.Add(square.piece);
                square.piece.RemoveSelf();
            }
            square.piece = arrival;
        }
        foreach(Piece dyer in dying) {
            dyer.Die(arrival);
            arrival.CapturedOther(dyer);
        }
        foreach(Square square in block)
            arrival.OnArrive(square);
    }
    private void CounteredArrival(Piece arrival, Piece counterer, List<Square> block) {
        Debug.Log("Countered arrive. piece: " + arrival + "counterer: " + counterer);
        Square piecePrevSquare = arrival.square;
        List<Square> startBlock = new List<Square>();
        if(arrival.square != null) 
            startBlock = arrival.square.AdjacentBlock(arrival.Size());
        arrival.RemoveSelf();
        arrival.square = this;
        counterer.TryKill(arrival);
        arrival.square = piecePrevSquare;
        foreach(Square square in startBlock) 
            square.piece = arrival;
    }
    private Piece SearchCounterer(Piece arrival, List<Square> block) {
        Piece counterer = null;
        foreach(Square square in block) {
            if(square == null)
                throw new Exception("null square in search counterer block.");
            if(square.piece == null || square.piece == arrival || square.piece.CanKillMe(arrival))
                continue;
            counterer = square.piece;
            break;
        }
        return counterer;
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
    public List<Square> AdjacentBlock(int size) {
        List<Square> res = new();
        for(int i = -(size-1)/2; i <= size/2; i++) {
            for(int j = -(size-1)/2; j <= size/2; j++) {
                if(TryAdjacent((i, j, 0), out Square square))
                    res.Add(square);
            }
        }
        return res;
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
