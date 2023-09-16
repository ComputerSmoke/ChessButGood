using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Multiunity.Unity;

public class ChargeSignal : Signal
{
    int player;
    int amount;
    public override void Init(byte[] msg, GameObject prefab) {
        player = (int)msg[0];
        amount = (int)msg[1];
    }
    public override byte[] Message() {
        List<byte> res = new List<byte>();
        res.Add((byte)player);
        res.Add((byte)amount);
        return res.ToArray();
    }
    public void Init(int player, int amount) {
        this.player = player;
        this.amount = amount;
    }
    public override void Execute() {
        Debug.Log("charging. Player: " + player + " amount: " + amount);
        if(player == 0)
            Game.whiteGold -= amount;
        else if(player == 1) 
            Game.blackGold -= amount;
        Forward();
    }
}
