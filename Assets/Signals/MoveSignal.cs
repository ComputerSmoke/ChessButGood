using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class MoveSignal : Signal
{
    Square square;
    Square target;
    public override void Init(byte[] msg, GameObject prefab) {
        Debug.Log("init from external");
        this.prefab = prefab;
        Board sBoard = Game.BoardById(msg[0]);
        int sx = Grow(msg[1]);
        int sy = Grow(msg[2]);
        int sz = Grow(msg[3]);
        Debug.Log("sx,sy,sz" + sx + " " + sy + " " + sz);
        square = sBoard.squares[(sx, sy, sz)];
        Board tBoard = Game.BoardById(msg[4]);
        int tx = Grow(msg[5]);
        int ty = Grow(msg[6]);
        int tz = Grow(msg[7]);
        Debug.Log("tx,ty,tz" + tx + " " + ty + " " + tz);
        target = tBoard.squares[(tx, ty, tz)];
    }
    public override byte[] Message() {
        List<byte> res = new List<byte>();
        res.Add((byte)square.board.id);
        res.Add(Shrink(square.x));
        res.Add(Shrink(square.y));
        res.Add(Shrink(square.z));
        res.Add((byte)target.board.id);
        res.Add(Shrink(target.x));
        res.Add(Shrink(target.y));
        res.Add(Shrink(target.z));
        return res.ToArray();
    }
    public void Init(Square square, Square target, GameObject prefab) {
        this.square = square;
        this.target = target;
        this.prefab = prefab;
    }
    public override void Execute() {
        Debug.Log("moving. piece pos: " + square.x + "," + square.y + "," + square.z + " targetPos: " + target.x + "," + target.y + "," + target.z);
        square.piece.Move(target);
        Forward();
    }
    //To use half the byte for negative numbers
    private static byte Shrink(int x) {
        if(x >= 0) 
            return (byte)x;
        return (byte)(255+x);
    }
    private static int Grow(byte x) {
        if(x < 128) 
            return (int)x;
        return (int)x-255;
    }
}
