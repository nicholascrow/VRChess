using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
	public enum pieceColor
	{
		White,
		Black
	}

	public enum pieceType
	{
		Pawn,
		Rook,
		Knight,
		Bishop,
		King,
		Queen
	}

	//the type of this piece
	public pieceType type;
	public pieceColor color = pieceColor.White;
	public Vector2 locationIndices;
	public List<Vector2> validMoves;

	void OnMouseDown ()
	{
		if (GameMaster.selectedPiece != null) {
			GameMaster.selectedPiece.GetComponent<Renderer> ().material.color = color == 0 ? Color.white : Color.black;
			GameMaster.selectedPiece = null;
		}
		GameMaster.selectedPiece = this.transform.root.gameObject;
		GameMaster.selectedPiece.GetComponent<Renderer> ().material.color = Color.cyan;
		print (type + " Selected!");
	}



	// Use this for initialization
	void Start ()
	{
		validMoves = new List<Vector2> ();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameMaster.selectedPiece == this.transform.root.gameObject && GameMaster.selectedMove) {
			GameObject tempPiece = GameMaster.selectedPiece;
			GameObject tempTile = GameMaster.selectedMove;
			GameMaster.selectedPiece = null; 
			GameMaster.selectedMove = null; 
			StartCoroutine (isValidMove (tempPiece, tempTile));
			print ("Checking validity of move");
			//StartCoroutine (findAllValidMoves ());
			//StartCoroutine (lerpToPos (tempPiece, tempTile,tempPiece.transform.position, tempTile.transform.root.transform.position + new Vector3 (0, 1, 0)));
		}


	}
	IEnumerator isValidMove(GameObject tempPiece, GameObject tempTile){
		for (int i = 0; i < validMoves.Count; i++) {
			print (validMoves [i]);
			print (tempTile.GetComponent<Tile> ().locationIndices);
			if(validMoves[i].Equals(tempTile.GetComponent<Tile>().locationIndices)){
				StartCoroutine (lerpToPos (tempPiece, tempTile,tempPiece.transform.position, tempTile.transform.root.transform.position + new Vector3 (0, 1, 0)));
			}
				
		}
		yield return null;
	}






	void validWhite(pieceColor currentPlayerColor){
		if(GameMaster.pieceBoard[(int)locationIndices.x + 1,(int)locationIndices.y] == null){
			validMoves.Add (new Vector2 ((int)locationIndices.x + 1, (int)locationIndices.y));
		}
		if ((int)locationIndices.y > 0) {
			if (GameMaster.pieceBoard [(int)locationIndices.x + 1, (int)locationIndices.y - 1] != null) {

				if(GameMaster.pieceBoard [(int)locationIndices.x + 1, (int)locationIndices.y - 1].GetComponent<Piece>().color != currentPlayerColor) {
					validMoves.Add (new Vector2 ((int)locationIndices.x + 1, (int)locationIndices.y - 1));
				}
			}
		}
		if ((int)locationIndices.y < 7) {
			if (GameMaster.pieceBoard [(int)locationIndices.x + 1, (int)locationIndices.y + 1] != null) {

				if (GameMaster.pieceBoard [(int)locationIndices.x + 1, (int)locationIndices.y + 1].GetComponent<Piece> ().color != currentPlayerColor) {
					validMoves.Add (new Vector2 ((int)locationIndices.x + 1, (int)locationIndices.y + 1));
				}
			}
		}
	}
	void validBlack(pieceColor currentPlayerColor){
		if (GameMaster.pieceBoard [(int)locationIndices.x + -1, (int)locationIndices.y] == null) {
			validMoves.Add (new Vector2 ((int)locationIndices.x + -1, (int)locationIndices.y));
		}
		if ((int)locationIndices.y > 0) {
			if (GameMaster.pieceBoard [(int)locationIndices.x + -1, (int)locationIndices.y - 1] != null) {

				if (GameMaster.pieceBoard [(int)locationIndices.x + -1, (int)locationIndices.y - 1].GetComponent<Piece> ().color != currentPlayerColor) {
					validMoves.Add (new Vector2 ((int)locationIndices.x + -1, (int)locationIndices.y - 1));
				}
			}
		}
		if ((int)locationIndices.y < 7) {
			if (GameMaster.pieceBoard [(int)locationIndices.x + -1, (int)locationIndices.y + 1] != null) {

				if (GameMaster.pieceBoard [(int)locationIndices.x + -1, (int)locationIndices.y + 1].GetComponent<Piece> ().color != currentPlayerColor) {
					validMoves.Add (new Vector2 ((int)locationIndices.x + -1, (int)locationIndices.y + 1));
				}
			}
		}
	}
	public IEnumerator findAllValidMoves(){
		pieceColor currentPlayerColor;
		if (GameMaster.state == GameMaster.gameState.P1Turn)
			currentPlayerColor = GameMaster.p1Color;
		else
			currentPlayerColor = GameMaster.p2Color;
		switch (type) {
		case pieceType.Pawn:

			//this would indicate getting a new piece in the pawn's place.
			if ((int)locationIndices.x > 6 || (int)locationIndices.x < 1) {
			} 

			//this would indicate any other position on the board (a-h 2-7)
			else {
				if (color == pieceColor.White)
					validWhite (currentPlayerColor);
				else
					validBlack (currentPlayerColor);
			}
			break;
		case pieceType.Rook:

			break;
		case pieceType.Knight:

			break;

		case pieceType.Bishop:

			break;
		case pieceType.Queen:

			break;
		case pieceType.King:

			break;

		}
		yield return null;

		for (int i = 0; i < validMoves.Count; i++) {
			print ("Valid Moves: " + validMoves [i]);
		}
	}



	/// <summary>
	/// Moves the piece from its start position to the end position over 1 second.
	/// </summary>
	/// <returns></returns>
	/// <param name="tempPiece">Temp piece.</param>
	/// <param name="start">Start.</param>
	/// <param name="end">End.</param>
	IEnumerator lerpToPos (GameObject tempPiece,GameObject tempTile, Vector3 start, Vector3 end)
	{

		for (float i = 0; i < 1f; i += Time.deltaTime) {
			yield return null;
			tempPiece.transform.position = Vector3.Lerp (start, end, i / 1f);
		}
		tempPiece.GetComponent<Renderer> ().material.color = color == 0 ? Color.white : Color.black;
		tempPiece.GetComponent<Piece> ().locationIndices = tempTile.GetComponent<Tile>().locationIndices;
	}

}
