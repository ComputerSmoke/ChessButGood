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
    public int kills;
    public bool placeOnPiece;
    public bool ethereal;
    //size offset by 1. That is, size = 0 --> 1x1 square piece
    public int size;
    public bool metaphysical;
    public bool wrathful;
    private bool dying;
    private LevelMenu menu;
    private HashSet<Equippable> equips;
    public HashSet<Equippable> Equips() {
        if(equips != null)
            return equips;
        equips = new HashSet<Equippable>();
        return equips;
    }
    public virtual void Move(Square square) {
        if(!TopMovement().RangedSquares().Contains(square)) {
            square.Arrive(this);
            this.moved = true;
        } else {
            square.piece.TryKill(this);
        }
    }
    public virtual void OnArrive(Square square) {}
    public virtual bool TryKill(Piece killer) {
        if(Counter(killer))
            return false;
        Die(killer);
        return true;
    }
    public void Die(Piece killer) {
        if(dying)
            return;
        dying = true;
        Debug.Log("Piece " + this + " dying to " + killer);
        DieEffect(killer);
        dying = false;
    }
    protected virtual void DieEffect(Piece killer) {
        RemoveSelf();
        GiveRewards(killer);
        if(wrathful)
            killer.TryKill(this);
        if(killer.eatSouls || square.board != Game.earth)
            Object.Destroy(this.gameObject);
        else if(blessed)
            Game.heaven.PlacePiece(this, square);
        else
            Game.hell.PlacePiece(this, square);
    }
    public virtual bool CanKillMe(Piece killer) {
        return !CanCounter(killer);
    }
    protected virtual bool CanCounter(Piece killer) {
        foreach(Equippable equip in Equips()) {
            if(equip.CanCounter(killer))
                return true;
        }
        return false;
    }
    protected virtual bool Counter(Piece killer) {
        foreach(Equippable equip in Equips()) {
            if(equip.Counter(killer))
                return true;
        }
        return false;
    }
    protected virtual void GiveRewards(Piece killer) {
        killer.kills++;
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
        return !ethereal;
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
    protected virtual Quaternion Rotation() {
        return Game.initializer.mainCamera.transform.rotation;
    }
    void Update() {
        gameObject.transform.rotation = Rotation();
        if(square != null) {
            gameObject.transform.position = Board.Pos(square.x, square.y);
            if(size % 2 == 1)
                gameObject.transform.position += new Vector3(.5f, .5f, 0);

            gameObject.layer = square.board.id;
        }
    }
    public int Size() {
        return size+1;
    }
    public void Resize(int size) {
        gameObject.transform.localScale = new Vector3(size+1, size+1, 0);
        if(square == null) {
            this.size = size;
            return;
        }
        foreach(Square square in square.AdjacentBlock(Size())) 
            square.Depart(this);
        this.size = size;
        Square temp = square;
        square = null;
        temp.Arrive(this);
    } 
    public void RemoveSelf() {
        if(square == null)
            return;
        List<Square> block = square.AdjacentBlock(Size());
        foreach(Square adj in block)
            adj.Depart(this);
    }
    public virtual void OnCreate() {}
    private LevelMenu Menu() {
        if(menu == null && gameObject.TryGetComponent(out LevelMenu levelMenu))
            menu = levelMenu;
        return menu;
    }
    public void ToggleMenu() {
        if(Menu() != null)
            Menu().Toggle();
    }
}
