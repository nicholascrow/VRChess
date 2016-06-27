using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour {

	//this makes the first square black
	private bool isBlack = false;

	IEnumerator MakeBoard ()
	{
		//create new chessboard (each index is a gameobject)
		GameMaster.gameBoard = new GameObject[8, 8];
		GameMaster.pieceBoard = new GameObject[8, 8];


		//position of the first square
		Vector3 position = new Vector3 (10, 0, 9);

		//i is rows of the chessboard
		for (int i = 0; i < 8; i++) {
			position = position + new Vector3 (0, 0, 1);
			position.x = 0;
			isBlack = !isBlack;

			//j is columns of the chessboard
			for (int j = 0; j < 8; j++) {
				yield return new WaitForSeconds (.1f);
				//GameMaster.gameBoard [i, j] = new Tile ();	

				GameMaster.gameBoard [i, j] = GameObject.CreatePrimitive (PrimitiveType.Cube);
				GameMaster.gameBoard [i, j].AddComponent<Tile> ();
				GameMaster.gameBoard [i, j].GetComponent<Tile> ().locationIndices = new Vector2 (i, j);
				GameMaster.gameBoard [i, j].transform.position = position;
				position = position + new Vector3 (1, 0, 0);
				GameMaster.gameBoard [i, j].GetComponent<Renderer> ().material.color = isBlack ? Color.black : Color.white;
				isBlack = !isBlack;
				addPiece (i, j);
			}
		}
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if(GameMaster.pieceBoard[i,j] != null)
					StartCoroutine (GameMaster.pieceBoard [i, j].GetComponent<Piece> ().findAllValidMoves ());
			}
		}
	}
	//make the board and lay out the pieces
	void Start(){
		GameMaster.p1Color = Piece.pieceColor.White;
		GameMaster.p2Color = Piece.pieceColor.Black;
		StartCoroutine (MakeBoard ());
	}


	void Update(){

	}
	void addPiece(int x, int y){
		if (x == 1 || x == 6) {
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece.AddComponent<Piece> ();
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece.GetComponent<Piece> ().type = Piece.pieceType.Pawn;
			GameMaster.gameBoard [x, y].GetComponent<Tile> ().piece.GetComponent<Piece> ().locationIndices = new Vector2 (x, y);
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard [x, y].transform.position + new Vector3 (0, 1, 0);
			GameMaster.gameBoard [x, y].GetComponent<Tile> ().piece.GetComponent<Renderer> ().material.color = x == 1 ? Color.black : Color.white;
		}
		GameMaster.pieceBoard[x, y] = GameMaster.gameBoard [x, y].GetComponent<Tile> ().piece;

			}


}

