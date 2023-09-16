using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static Initializer initializer;
    public static Board earth;
    public static Board heaven;
    public static Board hell;
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
    private static Board activeBoard;

    public static void Update(Board newActiveBoard)
    {
        activeBoard = newActiveBoard;
        if(!playing)
            return;
        highlightSquares = null;
        MoveGhost();
        ExpandBoard(activeBoard);
        CheckSquareClick(activeBoard);
    }
    private static void CheckSquareClick(Board activeBoard) {
        if(!Controls.KeyDown(Controls.Key.LMB) || activeBoard == null) 
            return;
        Vector3 mousePos = Controls.MousePos();
        (int, int, int) gridPos = Board.GridPos(mousePos);
        Debug.Log("mouse click at: " + gridPos);
        if(activeBoard.squares.ContainsKey(gridPos)) {
            Square square = activeBoard.squares[gridPos];
            ClickSquare(square);
        }
    }
    private static void MoveGhost() {
        if(placeGhost == null || placing == null)
            return;
        Vector3 mousePos = Controls.MousePos();
        placeGhost.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
    private static void ExpandBoard(Board activeBoard) {
        if(placing != null && activeBoard.AllPlaceOccupied(turn%2)) 
            activeBoard.ExpandBackrow(turn%2);
    }

    private static void ClickSquare(Square square) {
        Debug.Log("clicked square");
        if(placing != null && square.piece == null && (turn%2 == 0 && square.y < 4 || turn%2 == 1 && square.y > 3))
            Place(square);
        else if(selected != null && selected.CanReach(square))
            MoveSelected(square);
        else if(square.piece != null && square.piece.color == turn%2 && myColors.Contains(square.piece.color) && square.piece != selected)
            selected = square.piece;
        else if(selected != null && selected.square == square) 
            selected = null;
        else if(selected != null)
            MoveSelected(square);
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
            highlightSquares = activeBoard.OpenSquares(turn%2);
        else if(selected == null)
            highlightSquares = new HashSet<Square>();
        else
            highlightSquares = new HashSet<Square>(selected.Movement().ValidSquares());
        return highlightSquares;
    }
    public static Board BoardById(int id) {
        if(id == earth.id) 
            return earth;
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
}
