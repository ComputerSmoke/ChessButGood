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
    private LayerController layerController;
    public GameObject gameUI;
    // Start is called before the first frame update
    void Start()
    {
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
        StartGame();
    }
    private void StartGame() {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        Game.earth = ScriptableObject.CreateInstance<Board>();
        Game.earth.Init(LayerMask.NameToLayer("Earth"), whiteSquare, blackSquare);
        Game.hell = ScriptableObject.CreateInstance<Board>();
        Game.hell.Init(LayerMask.NameToLayer("Hell"), whiteSquareHell, blackSquareHell);
        Game.heaven = ScriptableObject.CreateInstance<Board>();
        Game.heaven.Init(LayerMask.NameToLayer("Heaven"), whiteSquareHeaven, blackSquareHeaven);
        Game.initializer = this;
        PlacePieces(Game.earth);
        Game.playing = true;
        layerController.SetLayer("Earth");
    }
    public void PlacePieces(Board board) {
        PlacePiece(whiteRook, 0, 0, board);
        PlacePiece(whiteKnight, 1, 0, board);
        PlacePiece(whiteBishop, 2, 0, board);
        PlacePiece(whiteQueen, 3, 0, board);
        PlacePiece(whiteKing, 4, 0, board);
        PlacePiece(whiteBishop, 5, 0, board);
        PlacePiece(whiteKnight, 6, 0, board);
        PlacePiece(whiteRook, 7, 0, board);
        
        PlacePiece(blackRook, 0, 7, board);
        PlacePiece(blackKnight, 1, 7, board);
        PlacePiece(blackBishop, 2, 7, board);
        PlacePiece(blackQueen, 3, 7, board);
        PlacePiece(blackKing, 4, 7, board);
        PlacePiece(blackBishop, 5, 7, board);
        PlacePiece(blackKnight, 6, 7, board);
        PlacePiece(blackRook, 7, 7, board);
        for(int i = 0; i < 8; i++) {
            PlacePiece(whitePawn, i, 1, board);
            PlacePiece(blackPawn, i, 6, board);
        }
    }
    private void PlacePiece(GameObject prefab, int col, int row, Board board) {
        board.CreatePiece(prefab, board.squares[(col, row, 0)]);
    }

    // Update is called once per frame
    void Update()
    {
        Game.Update(layerController.ActiveBoard());
    }
}
