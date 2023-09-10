using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public Piece piece;
    public Board board;
    int x,y,z;
    public void Arrive(Piece piece) {
        this.piece = piece;
    }
    public void Place(GameObject piece) {
        this.piece = piece.GetComponent<Piece>();
    }
    public void SetPos(int x, int y, int z) {
        this.x = x;
        this.y = y; 
        this.z = z;
    }
}
