using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceListing : MonoBehaviour
{
    public GameObject whitePiece;
    public GameObject blackPiece;
    private SpriteRenderer whiteRenderer;
    private SpriteRenderer blackRenderer;
    public int price;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        whiteRenderer = whitePiece.GetComponent<SpriteRenderer>();
        blackRenderer = blackPiece.GetComponent<SpriteRenderer>();
        img = gameObject.AddComponent<Image>();
    }
    void Update() {
        if (Game.turn % 2 == 0) 
            img.sprite = whiteRenderer.sprite;
        else
            img.sprite = blackRenderer.sprite;
    }
    public void Buy() {
        if(TurnGold() < price)
            return;
        Charge(price);
        Game.ChoosePlacement(Piece());
    }
    private GameObject Piece() {
        if(Game.turn%2 == 0)
            return whitePiece;
        return blackPiece;
    }
    private static void Charge(int amount) {
        ChargeSignal charge = Instantiate(Game.initializer.chargeSignal).GetComponent<ChargeSignal>();
        charge.Init(Game.turn % 2, amount);
        charge.Execute();
    }
    private static int TurnGold() {
        if(Game.turn%2 == 0)
            return Game.whiteGold;
        return Game.blackGold;
    }
}
