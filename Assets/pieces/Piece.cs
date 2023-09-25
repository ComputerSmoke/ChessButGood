using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Square square;
    public bool moved;
    public int color;
    public bool eatShadows;
    public bool eatSouls;
    public bool enableCastle;
    public bool blessed;
    public bool loot;
    public int xp;
    public bool placeOnPiece;
    public HashSet<Equippable> equips;
    void Start() {
        equips = new HashSet<Equippable>();
    }
    public virtual void Move(Square square) {
        if(!TopMovement().RangedSquares().Contains(square)) {
            this.square.Depart(this);
            square.Arrive(this);
            this.moved = true;
        } else {
            square.piece.Die(this);
        }
        Game.turn++;
    }
    public virtual bool TryKill(Piece killer) {
        if(Counter(killer))
            return false;
        Die(killer);
        return true;
    }
    public virtual void Die(Piece killer) {
        Debug.Log("Piece " + this + " dying to " + killer);
        square.Depart(this);
        GiveRewards(killer);
        if(killer.eatSouls || square.board != Game.earth)
            Object.Destroy(this.gameObject);
        else if(blessed)
            Game.heaven.PlacePiece(this, square);
        else
            Game.hell.PlacePiece(this, square);
    }
    protected virtual bool Counter(Piece killer) {
        foreach(Equippable equip in equips) {
            if(equip.Counter(killer))
                return true;
        }
        return false;
    }
    protected virtual void GiveRewards(Piece killer) {
        killer.xp++;
        if(Game.firstBlood) {
            killer.xp++;
            Game.firstBlood = false;
        }
        if(!killer.loot)
            return;
        if(killer.color == 0)
            Game.whiteGold++;
        else if(killer.color == 1) 
            Game.blackGold++;
    }
    public Movement TopMovement() {
        Movement[] movements = this.gameObject.GetComponents<Movement>();
        Movement maxRank = movements[0];
        foreach(Movement movement in movements) {
            if(movement.rank > maxRank.rank)
                maxRank = movement;
        }
        return maxRank;
    } 
    public bool CanReach(Square square) {
        return TopMovement().ValidSquares().Contains(square);
    }
    protected (int,int,int) GetDir(Square square1, Square square2) {
        int dx = GetDirOne(square1.x, square2.x);
        int dy = GetDirOne(square1.y, square2.y);
        int dz = GetDirOne(square1.z, square2.z);
        return (dx, dy, dz);
    }
    private int GetDirOne(int x1, int x2) {
        if(x1==x2) return 0;
        if(x1<x2) return 1;
        return -1;
    }
    public virtual bool Blocks(Piece piece) {
        return true;
    }
    public virtual bool CanLandMe(Piece piece) {
        return false;
    }
    public virtual bool CanCaptureMe(Piece piece) {
        return piece.color != color;
    }
    public Movement AugmentMovement(Movement newMove, Movement prevTop) {
        AugmentedMovement newTop = gameObject.AddComponent<AugmentedMovement>();
        newTop.movement1 = newMove;
        newTop.movement2 = prevTop;
        newTop.rank = prevTop.rank + 1;
        return newTop;
    }
}
