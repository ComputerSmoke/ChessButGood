using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : ScriptableObject
{
    public Initializer initializer;
    public Board earth;
    private Piece selected;
    private HashSet<Square> highlightSquares;
    public int turn = 0;

    public void Update()
    {
        highlightSquares = null;
        if(Controls.KeyDown(Controls.Key.LMB)) {
            Vector3 mousePos = Controls.MousePos();
            (int, int, int) gridPos = Board.GridPos(mousePos);
            Debug.Log("mouse click at: " + gridPos);
            if(earth.squares.ContainsKey(gridPos)) {
                Square square = earth.squares[gridPos];
                ClickSquare(square);
            }
        }
    }

    private void ClickSquare(Square square) {
        if(selected != null && selected.CanReach(square))
            MoveSelected(square);
        if(square.piece != null && square.piece.color == turn%2 && square.piece != selected)
            selected = square.piece;
        else if(selected != null && selected.square == square) 
            selected = null;
        else if(selected != null)
            MoveSelected(square);
    } 
    private void MoveSelected(Square square) {
        if(selected.color != turn%2 || !selected.CanReach(square)) 
            return;
        selected.Move(square);
        turn++;
        selected = null;
    }
    public HashSet<Square> HighlightSquares() {
        if(highlightSquares == null) {
            if(selected == null)
                highlightSquares = new HashSet<Square>();
            else    
                highlightSquares = new HashSet<Square>(selected.Movement().ValidSquares());
        }
        return highlightSquares;
    }
}
