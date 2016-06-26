using UnityEngine;
using System.Collections;

public class ChessBoard : MonoBehaviour {

	//this makes the first square black
	private bool isBlack = false;

	IEnumerator MakeBoard ()
	{
		//create new chessboard (each index is a gameobject)
		GameMaster.gameBoard = new GameObject[8, 8];

		//position of the first square
		Vector3 position = new Vector3 (0, 0, -1);

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
				GameMaster.gameBoard [i, j].transform.position = position;
				position = position + new Vector3 (1, 0, 0);
				GameMaster.gameBoard [i, j].GetComponent<Renderer> ().material.color = isBlack ? Color.black : Color.white;
				isBlack = !isBlack;
				addPawn (i, j);
			}
		}
	}
	//make the board and lay out the pieces
	void Start(){
		StartCoroutine (MakeBoard ());
	}


	void Update(){

	}
	void addPawn(int x, int y){
		if (x == 1 || x == 6) {
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece.AddComponent<Piece> ();
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece.GetComponent<Piece> ().type = Piece.pieceType.Pawn;
			GameMaster.gameBoard [x, y].GetComponent<Tile>().piece.transform.position = GameMaster.gameBoard [x, y].transform.position + new Vector3 (0, 1, 0);
		}
	}


}

