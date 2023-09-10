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
    private Game game;
    // Start is called before the first frame update
    void Start()
    {
        Controls.BindKeys();
        game = ScriptableObject.CreateInstance<Game>();
        game.earth = ScriptableObject.CreateInstance<Board>();
        game.earth.Init(game);
        game.initializer = this;
        MakeBoard(game.earth);
        PlacePieces(game.earth);
    }
    private void MakeBoard(Board board) {
        for(int i = 0; i < 8; i++) {
            for(int j = 0; j < 8; j++) {
                GameObject prefab;
                if((i+j)%2 == 1) prefab = whiteSquare;
                else prefab = blackSquare;
                GameObject square = Instantiate(prefab, Board.Pos(i, j), Quaternion.identity);
                board.AddSquare(square, i, j, 0);
            }
        }
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
        game.Update();
    }
}
