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

    public static void Update(Board activeBoard)
    {
        if(!playing)
            return;
        highlightSquares = null;
        if(Controls.KeyDown(Controls.Key.LMB) && activeBoard != null) {
            Vector3 mousePos = Controls.MousePos();
            (int, int, int) gridPos = Board.GridPos(mousePos);
            Debug.Log("mouse click at: " + gridPos);
            if(activeBoard.squares.ContainsKey(gridPos)) {
                Square square = activeBoard.squares[gridPos];
                ClickSquare(square);
            }
        }
    }

    private static void ClickSquare(Square square) {
        Debug.Log("clicked square");
        if(selected != null && selected.CanReach(square))
            MoveSelected(square);
        else if(square.piece != null && square.piece.color == turn%2 && myColors.Contains(square.piece.color) && square.piece != selected)
            selected = square.piece;
        else if(selected != null && selected.square == square) 
            selected = null;
        else if(selected != null)
            MoveSelected(square);
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
        if(highlightSquares == null) {
            if(selected == null)
                highlightSquares = new HashSet<Square>();
            else    
                highlightSquares = new HashSet<Square>(selected.Movement().ValidSquares());
        }
        return highlightSquares;
    }
    public static Board BoardById(int id) {
        if(id == earth.id) 
            return earth;
        return null;
    }
}
