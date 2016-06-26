using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {
	GameMaster.PieceAndLocation pawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		GameMaster.selectedPiece = this.transform.root.gameObject;
		print ("Pawn Selected!");
		//create joint?
	}

	public void setTile(Tile tile){
		pawn.pieceTile = tile;
	}
		
}

