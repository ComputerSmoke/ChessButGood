using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class CreatePieceSignal : Signal
{
    Piece piece;
    Square square;
    public override void Init(byte[] msg, GameObject prefab) {
        piece = IdMappings.PrefabById(msg[0]).GetComponent<Piece>();
        Board board = Game.BoardById(msg[1]);
        int x = Grow(msg[2]);
        int y = Grow(msg[3]);
        int z = Grow(msg[4]);
        Debug.Log("sx,sy,sz" + x + " " + y + " " + z);
        square = board.squares[(x, y, z)];
    }
    public override byte[] Message() {
        List<byte> res = new List<byte>();
        res.Add((byte)IdMappings.IdByPrefab(piece.gameObject));
        res.Add((byte)square.board.id);
        res.Add(Shrink(square.x));
        res.Add(Shrink(square.y));
        res.Add(Shrink(square.z));
        return res.ToArray();
    }
    public void Init(GameObject piece, Square square) {
        this.square = square;
        this.piece = piece.GetComponent<Piece>();
    }
    public override void Execute() {
        Debug.Log("placing. pos: " + square.x + "," + square.y + "," + square.z);
        if(square.piece != null && piece is Item) 
            ((Item)piece).Acquire(square.piece);
        else
            square.board.CreatePiece(piece.gameObject, square);
        Game.turn++;
        Forward();
    }
}
