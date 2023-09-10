using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : ScriptableObject
{
    public Initializer initializer;
    public Board earth;
    private Piece selected;
    private HashSet<Square> highlightSquares;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highlightSquares = null;
        if(Controls.KeyDown(Controls.Key.LMB)) {
            Vector3 mousePos = Controls.MousePos();
            (int, int, int) gridPos = GridPos(mousePos);
            if(earth.squares.ContainsKey(gridPos)) {
                Square square = earth.squares[gridPos];
                if(selected != null)
                    MoveClick(square);
                else
                    SelectClick(square);
            }
        }
    }

    private void MoveClick(Square square) {
        if(selected.square == square) {
            selected = null;
            return;
        }
        //TODO: move piece
    } 
    private void SelectClick(Square square) {
        if(square.piece == null) return;
        selected = square.piece;
    }
    private static (int, int, int) GridPos(Vector3 pos) {
        return ((int)(pos.x + 4), (int)(pos.y + 4), 0);
    }
    private HashSet<Square> HighlightSquares() {
        if(highlightSquares == null) {
            if(selected == null)
                highlightSquares = new HashSet<Square>();
            else    
                highlightSquares = new HashSet<Square>(selected.Movement().ValidSquares());
        }
        return highlightSquares;
    }
}
