using UnityEngine;
using System.Collections;

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
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameMaster.selectedPiece == this.transform.root.gameObject && GameMaster.selectedMove) {
			print ("Moving");
			GameObject tempPiece = GameMaster.selectedPiece;
			GameObject tempTile = GameMaster.selectedMove;
			GameMaster.selectedPiece = null; 
			GameMaster.selectedMove = null; 
			StartCoroutine (lerpToPos (tempPiece, tempPiece.transform.position, tempTile.transform.root.transform.position + new Vector3 (0, 1, 0)));
		}


	}
	IEnumerator isValidMove(){
		yield return null;
	}
	IEnumerator displayAllValidMoves(){
		yield return null;
	}



	/// <summary>
	/// Moves the piece from its start position to the end position over 1 second.
	/// </summary>
	/// <returns></returns>
	/// <param name="tempPiece">Temp piece.</param>
	/// <param name="start">Start.</param>
	/// <param name="end">End.</param>
	IEnumerator lerpToPos (GameObject tempPiece, Vector3 start, Vector3 end)
	{

		for (float i = 0; i < 1f; i += Time.deltaTime) {
			yield return null;
			tempPiece.transform.position = Vector3.Lerp (start, end, i / 1f);
		}
		tempPiece.GetComponent<Renderer> ().material.color = color == 0 ? Color.white : Color.black;
	}

}
