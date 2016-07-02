using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour {

	//this makes the first square black
	private bool isBlack = false;


	//make the board and lay out the pieces
	void Start(){
        if (Random.value > .5f)
        {
     //       GameMaster.p1Color = Piece.pieceColor.White;
       //     GameMaster.p2Color = Piece.pieceColor.Black;
            //GameMaster.Player1.GetComponent<Player>().color = Piece.pieceColor.White;
            //GameMaster.Player2.GetComponent<Player>().color = Piece.pieceColor.Black;

        }


		StartCoroutine (MakeBoard ());
        MakePlayer();
	}
    void MakePlayer() {
        GameMaster.Player1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameMaster.Player1.transform.position = new Vector3(3.5f, 2f, -2f);
        GameMaster.Player1.AddComponent<Player>();
        GameMaster.Player1.GetComponent<Player>().type = Player.playerNumber.Player1;
        GameMaster.Player1.GetComponent<Player>().color = Piece.pieceColor.White;
        
        GameMaster.Player2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameMaster.Player2.transform.position = new Vector3(3.5f, 2f, 9f);
        GameMaster.Player2.AddComponent<Player>();
        GameMaster.Player2.GetComponent<Player>().type = Player.playerNumber.Player2;
        GameMaster.Player2.GetComponent<Player>().color = Piece.pieceColor.Black;


        Player.SwitchTurn(GameMaster.Player1.GetComponent<Player>());
    }

    #region Create Board
    IEnumerator MakeBoard()
    {
        //create new chessboard (each index is a gameobject)
        GameMaster.gameBoard = new GameObject[8, 8];
        GameMaster.pieceBoard = new GameObject[8, 8];


        //position of the first square
        Vector3 position = new Vector3(0, 0, -1);

        //i is rows of the chessboard
        for (int i = 0; i < 8; i++)
        {
            position = position + new Vector3(0, 0, 1);
            position.x = 0;
            isBlack = !isBlack;

            //j is columns of the chessboard
            for (int j = 0; j < 8; j++)
            {
                yield return new WaitForSeconds(.02f);
                //GameMaster.gameBoard [i, j] = new Tile ();	

                GameMaster.gameBoard[i, j] = GameObject.CreatePrimitive(PrimitiveType.Cube);
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


    void addPiece(int x, int y)
    {
        if (x == 1 || x == 6)
        {
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Pawn>();
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Pawn>().type = Piece.pieceType.Pawn;
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Pawn>().locationIndices = new Vector2(x, y);
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 1 ? Color.white : Color.black;
            GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Pawn>().color = x == 1 ? Piece.pieceColor.White : Piece.pieceColor.Black;
        }
        if (x == 0 || x == 7)
        {
            switch (y)
            {
                case 0:
                case 7:
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Rook>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Rook>().type = Piece.pieceType.Rook;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Rook>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Rook>().color = x == 0 ? Piece.pieceColor.White : Piece.pieceColor.Black;
                    break;
                case 1:
                case 6:
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Knight>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Knight>().type = Piece.pieceType.Knight;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Knight>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Knight>().color = x == 0 ? Piece.pieceColor.White : Piece.pieceColor.Black;
                    break;
                case 2:
                case 5:
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Bishop>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Bishop>().type = Piece.pieceType.Bishop;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Bishop>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Bishop>().color = x == 0 ? Piece.pieceColor.White : Piece.pieceColor.Black;
                    break;
                case 3:

                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<Queen>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Queen>().type = Piece.pieceType.Queen;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Queen>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Queen>().color = x == 0 ? Piece.pieceColor.White : Piece.pieceColor.Black;
                    break;
                case 4:
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.AddComponent<King>();
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>().type = Piece.pieceType.King;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>().locationIndices = new Vector2(x, y);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard[x, y].transform.position + new Vector3(0, 1, 0);
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<Renderer>().material.color = x == 0 ? Color.white : Color.black;
                    GameMaster.gameBoard[x, y].GetComponent<Tile>().piece.GetComponent<King>().color = x == 0 ? Piece.pieceColor.White : Piece.pieceColor.Black;
                    break;
            }
        }
        GameMaster.pieceBoard[x, y] = GameMaster.gameBoard[x, y].GetComponent<Tile>().piece;
    }

    #endregion

}

