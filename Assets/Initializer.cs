using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whitePawn;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whiteBishop;
    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackPawn;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackBishop;
    public GameObject whiteSquare;
    public GameObject blackSquare;
    public GameObject blackSquareHell;
    public GameObject whiteSquareHell;
    public GameObject blackSquareHeaven;
    public GameObject whiteSquareHeaven;
    public GameObject moveSignal;
    public GameObject mainMenu;
    public Camera mainCamera;
    public GameObject layerControllerObject;
    public LayerController layerController;
    public GameObject gameUI;
    public GameObject chargeSignal;
    public GameObject createPieceSignal;
    public GameObject coinPiece;
    public GameObject hellPortal;
    public GameObject heavenPortal;
    public GameObject hornsItem;
    public GameObject demon;
    public GameObject bibAngel;
    public GameObject releaseDevilSignal;
    public GameObject fire;
    public GameObject devil;
    // Start is called before the first frame update
    void Start()
    {
        Game.whiteGold = 5;
        Game.blackGold = 5;
        gameUI.SetActive(false);
        layerController = layerControllerObject.GetComponent<LayerController>();
        layerController.Init(mainCamera);
        Controls.BindKeys();
    }
    public void Online() {//TODO: set color and stuff after adding menus for create/join lobbies
        StartGame();
        Multiunity.Unity.MultiSession.Connect(11_000);
        Multiunity.Unity.MultiSession.Join(1);
    }
    public void Local() {
        Game.myColors = new List<int>();
        Game.myColors.Add(0);
        Game.myColors.Add(1);
        Game.flip = true;
        StartGame();
    }
    private void StartGame() {
        //TODO: reduce starting gold, high for testing
        //Game.whiteGold = 20;
        //Game.blackGold = 20;
        Game.initializer = this;
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        Game.earth = ScriptableObject.CreateInstance<Earth>();
        Game.earth.Init();
        Game.hell = ScriptableObject.CreateInstance<Hell>();
        Game.hell.Init();
        Game.heaven = ScriptableObject.CreateInstance<Heaven>();
        Game.heaven.Init();
        Game.playing = true;
        layerController.SetLayer("Earth");
    }

    // Update is called once per frame
    void Update()
    {
        Game.Update(layerController.ActiveBoard());
    }
}
