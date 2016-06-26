using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public GameObject piece;
	void OnMouseDown(){
		if (GameMaster.selectedPiece != null) {
			print ("Moving");
			//GameMaster.selectedPiece.transform.position = transform.root.gameObject.transform.position + new Vector3 (0, 1, 0);
			StartCoroutine(lerpToPos(GameMaster.selectedPiece.transform.position,transform.root.transform.position + new Vector3(0,1,0)));
			//canMove?
			//GameMaster.selectedPiece = null;

		}
	}
	IEnumerator lerpToPos(Vector3 start, Vector3 end){
		for (float i = 0; i < 1f; i += Time.deltaTime) {
			yield return null;
		//	print (i);
			GameMaster.selectedPiece.transform.position = Vector3.Lerp (GameMaster.selectedPiece.transform.position, transform.root.transform.position + new Vector3(0,1,0), i / 1f);
		}
		GameMaster.selectedPiece = null; 
	}
}
