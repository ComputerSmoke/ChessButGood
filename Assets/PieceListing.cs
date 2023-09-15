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
        Debug.Log("Buy more!");
    }
}
