using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {
	public enum pieceType{
		Pawn,
		Rook,
		Knight,
		Bishop,
		King,
		Queen
	}

	//GameMaster.PieceAndLocation piece;
	public pieceType type;


	void OnMouseDown(){
		GameMaster.selectedPiece = this.transform.root.gameObject;
		print (type + " Selected!");
		//create joint?
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
