using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour {


    public GameObject Pawn, Rook, Knight, Bishop, Queen, King;
    //this makes the first square black
    private bool isBlack = false;
    GameObject board;

    //make the board and lay out the pieces
    void Start() {
        board = new GameObject("Board");

        StartCoroutine(MakeBoard());
        MakePlayer();
    }
    void MakePlayer() {
        GameMaster.Player1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameMaster.Player1.transform.position = new Vector3(3.5f, 2f, -2f);
        GameMaster.Player1.AddComponent<Player>();
        GameMaster.Player1.GetComponent<Player>().type = Player.playerNumber.Player1;
        GameMaster.Player1.GetComponent<Player>().color = pieceColor.White;

        GameMaster.Player2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameMaster.Player2.transform.position = new Vector3(3.5f, 2f, 9f);
        GameMaster.Player2.AddComponent<Player>();
        GameMaster.Player2.GetComponent<Player>().type = Player.playerNumber.Player2;
        GameMaster.Player2.GetComponent<Player>().color = pieceColor.Black;


        Player.SwitchTurn(GameMaster.Player1.GetComponent<Player>());
    }

    #region Create Board

    IEnumerator MakeBoard() {
        GameMaster.kings = new List<King>();
        //create new chessboard (each index is a gameobject)
        GameMaster.gameBoard = new GameObject[8, 8];
        GameMaster.pieceBoard = new GameObject[8, 8];


        //position of the first square
        Vector3 position = new Vector3(0, 0, -1);

        //i is rows of the chessboard
        for(int i = 0; i < 8; i++) {
            position = position + new Vector3(0, 0, 1);
            position.x = 0;
            isBlack = !isBlack;

            //j is columns of the chessboard
            for(int j = 0; j < 8; j++) {
                yield return new WaitForSeconds(.02f);
                //GameMaster.gameBoard [i, j] = new Tile ();	

                GameMaster.gameBoard[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameMaster.gameBoard[i, j].name = "Tile<" + i + "," + j + ">";
                // GameMaster.gameBoard[i, j].transform.parent = board.transform;
                GameMaster.gameBoard[i, j].AddComponent<Tile>();

                GameMaster.gameBoard[i, j].GetComponent<Tile>().locationIndices = new Vector2(i, j);
                GameMaster.gameBoard[i, j].transform.position = position;
                position = position + new Vector3(1, 0, 0);
                GameMaster.gameBoard[i, j].GetComponent<Tile>().c = isBlack ? Color.black : Color.white;
                GameMaster.gameBoard[i, j].GetComponent<Renderer>().material.color = isBlack ? Color.black : Color.white;
                isBlack = !isBlack;
                addPiece(i, j);
            }
        }

        /*
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if(GameMaster.pieceBoard[i,j] != null)
					StartCoroutine (GameMaster.pieceBoard [i, j].GetComponent<Piece> ().findAllValidMoves ());
			}
		}
        */
    }

/// <summary>
/// instantiates a piece
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
/// <param name="pieces"></param>
/// <param name="isPawn"></param>
    void InstPiece(int x, int y, GameObject pieces, bool isPawn) {
        GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = Instantiate(pieces);
        GetComponent(pieces.name);
        Piece p = GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent(pieces.name) as Piece;
        p.locationIndices = new Vector2(x, y);
        GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
        GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = pieces.name;
        foreach(var child in GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponentsInChildren<Renderer>()) {
            //child.gameObject.AddComponent<MeshCollider>();
            child.GetComponent<Renderer>().material.color = x == (isPawn ? 0 : 1) ? Color.white : Color.black;

        }
        p.color = x == (isPawn ? 0 : 1) ? pieceColor.White : pieceColor.Black;

        if(pieces.name == "King") {
            GameMaster.kings.Add(GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>());
        }
    }

    /// <summary>
    /// Add a piece based on the square
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void addPiece(int x, int y) {
        if(x == 1 || x == 6) {
              // InstPiece(x, y, Pawn,false);
            #region old

            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = Instantiate(Pawn);
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<CapsuleCollider>();
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Pawn>();
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Pawn>().type = pieceType.Pawn;
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Pawn>().locationIndices = new Vector2(x, y);
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = "Pawn";
            foreach(var child in GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponentsInChildren<Renderer>()) {
                //child.gameObject.AddComponent<MeshCollider>();
                child.GetComponent<Renderer>().material.color = x == 1 ? Color.white : Color.black;

            }

            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Pawn>().color = x == 1 ? pieceColor.White : pieceColor.Black;
            
            #endregion
        }
        if(x == 0 || x == 7) {
            switch(y) {
                case 0:
                case 7:
                    #region old
                    /*
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Rook>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Rook>().type = pieceType.Rook;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Rook>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Rook>().color = x == 0 ? pieceColor.White : pieceColor.Black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = "Rook";
                    */
                    #endregion
                    InstPiece(x, y, Rook, true);
                    break;
                case 1:
                case 6:
                    #region old
                    /*
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Knight>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Knight>().type = pieceType.Knight;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Knight>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Knight>().color = x == 0 ? pieceColor.White : pieceColor.Black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = "Knight";
                    */
                    #endregion
                    InstPiece(x, y, Knight, true);
                    break;
                case 2:
                case 5:
                    #region old
                    /*
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Bishop>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Bishop>().type = pieceType.Bishop;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Bishop>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Bishop>().color = x == 0 ? pieceColor.White : pieceColor.Black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = "Bishop";
                    */
                    #endregion
                    InstPiece(x, y, Bishop, true);
                    break;
                case 3:
                    #region old
                    /*
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Queen>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Queen>().type = pieceType.Queen;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Queen>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Queen>().color = x == 0 ? pieceColor.White : pieceColor.Black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = "Queen";
                    */
                    #endregion
                    InstPiece(x, y, Queen, true);
                    break;
                case 4:
                    #region old
                    /*
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<King>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>().type = pieceType.King;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>().color = x == 0 ? pieceColor.White : pieceColor.Black;
                    GameMaster.kings.Add(GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>());
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.name = "King";
                    */
                    #endregion
                    InstPiece(x, y, King, true);
                    break;
            }
        }
        GameMaster.pieceBoard[x, y] = GameMaster.gameBoard[x, y].GetComponent<Tile>().piece;
    }

    #endregion

}