using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Piece piece;
    public GameObject upgrade;
    public void Buy() {
        if(piece.xp < 2)
            return;
        Square square = piece.square;
        piece.RemoveSelf();
        Object.Destroy(piece.gameObject);
        square.board.CreatePiece(upgrade, square);
    }
    void Start() {
        gameObject.GetComponent<Image>().sprite = upgrade.GetComponent<SpriteRenderer>().sprite;
    }
}
