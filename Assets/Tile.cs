using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public GameObject piece;
	public Vector2 locationIndices;
    public Color c;
	void OnMouseDown(){
		if (GameMaster.selectedPiece != null) {
			GameMaster.selectedMove = transform.root.gameObject;
		}
	}
}
