using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static Initializer initializer;
    public static Earth earth;
    public static Heaven heaven;
    public static Hell hell;
    private static Piece selected;
    private static HashSet<Square> highlightSquares;
    public static int turn = 0;
    public static List<int> myColors = new List<int>();
    public static bool playing;
    public static int whiteGold;
    public static int blackGold;
    public static bool firstBlood;
    public static GameObject placing;
    private static GameObject placeGhost;
    public static Board activeBoard;
    public static bool flip;
    public static Piece devilLander;
    public static int winner;

    public static void Update(Board newActiveBoard)
    {
        activeBoard = newActiveBoard;
        if(!playing)
            return;
        highlightSquares = null;
        RotateBoard();
        MoveGhost();
        ExpandBoard(activeBoard);
        CheckSquareClick(activeBoard);
    }
    private static void RotateBoard() {
        if(activeBoard == null) 
            return;
        if(flip && turn%2 == 1)
            initializer.mainCamera.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        else
            initializer.mainCamera.transform.rotation = Quaternion.identity;     
    }
    private static void CheckSquareClick(Board activeBoard) {
        if(!Controls.KeyDown(Controls.Key.LMB) && !Controls.KeyDown(Controls.Key.RMB) || activeBoard == null) 
            return;
        Vector3 mousePos = Controls.MousePos();
        (int, int, int) gridPos = Board.GridPos(mousePos);
        Debug.Log("mouse click at: " + gridPos);
        if(activeBoard.squares.ContainsKey(gridPos)) {
            Square square = activeBoard.squares[gridPos];
            if(Controls.KeyDown(Controls.Key.LMB))
                LeftClickSquare(square);
            if(Controls.KeyDown(Controls.Key.RMB))
                RightClickSquare(square);
        }
    }
    private static void MoveGhost() {
        if(placeGhost == null || placing == null)
            return;
        Vector3 mousePos = Controls.MousePos();
        placeGhost.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        placeGhost.transform.rotation = initializer.mainCamera.transform.rotation;
    }
    private static void ExpandBoard(Board activeBoard) {
        if(placing != null && activeBoard.AllPlaceOccupied(turn%2)) 
            activeBoard.ExpandBackrow(turn%2);
    }

    private static void LeftClickSquare(Square square) {
        Debug.Log("clicked square");
        if(placing != null && PlacingHighlight().Contains(square))
            Place(square);
        else if(selected != null && selected.color == turn % 2 && selected.CanReach(square))
            MoveSelected(square);
        else if(square.piece != null && square.piece != selected) {
            selected = square.piece;
            Debug.Log("Selected: " + square.piece);
        }
        else if(selected != null && selected == square.piece) 
            selected = null;
    } 
    private static void RightClickSquare(Square square) {
        Debug.Log("Right clicked square");
        if(square.piece != null)
            square.piece.ToggleMenu();
    }
    private static void Place(Square square) {
        CreatePieceSignal create = UnityEngine.Object.Instantiate(initializer.createPieceSignal).GetComponent<CreatePieceSignal>();
        create.Init(placing, square);
        create.Execute();
        placing = null;
        Object.Destroy(placeGhost);
    }
    private static void MoveSelected(Square square) {
        if(selected.color != turn%2 || !selected.CanReach(square) || !myColors.Contains(selected.color)) 
            return;
        MoveSignal move = UnityEngine.Object.Instantiate(initializer.moveSignal).GetComponent<MoveSignal>();
        move.Init(selected.square, square, initializer.moveSignal);
        move.Execute();
        selected = null;
    }
    public static HashSet<Square> HighlightSquares() {
        if(highlightSquares != null)
            return highlightSquares;
        if(placing != null) 
            highlightSquares = PlacingHighlight();
        else if(selected == null)
            highlightSquares = new HashSet<Square>();
        else
            highlightSquares = new HashSet<Square>(selected.TopMovement().ValidSquares());
        return highlightSquares;
    }
    private static HashSet<Square> PlacingHighlight() {
        Piece piece = placing.GetComponent<Piece>();
        if(!piece.placeOnPiece)
            return activeBoard.OpenSquares(turn%2);
        return activeBoard.OpenAndFriendlySquares(turn%2);
    }
    public static Board BoardById(int id) {
        if(id == earth.id) 
            return earth;
        if(id == hell.id)
            return hell;
        if(id == heaven.id)
            return heaven;
        return null;
    }
    public static void ChoosePlacement(GameObject prefab) {
        placing = prefab;
        initializer.layerController.SetLayer("Earth");
        placeGhost = new GameObject();
        SpriteRenderer ghost = placeGhost.AddComponent<SpriteRenderer>();
        ghost.sortingLayerName = "PlaceGhosts";
        ghost.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }
    public static void PassTurn() {
        earth.PassTurn(earth == activeBoard);
        hell.PassTurn(hell == activeBoard);
        heaven.PassTurn(heaven == activeBoard);
        turn++;
    }
    public static void DevilChoice(Piece piece) {
        devilLander = piece;
        initializer.layerController.SetLayer("DevilChoice");
    }
    public static void End(int winner)
    {
        playing = false;
        initializer.gameUI.SetActive(false);
        initializer.winScreen.SetActive(true);
        Game.winner = winner;
    }
}
