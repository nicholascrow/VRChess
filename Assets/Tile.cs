using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public GameObject piece;
	void OnMouseDown(){
		if (GameMaster.selectedPiece != null) {
			GameMaster.selectedMove = transform.root.gameObject;
		}
	}
}
