using UnityEngine;
using System.Collections;

public class BoardDelegate : MonoBehaviour {

    public delegate void PieceHandler(GameObject piece);

    public static event PieceHandler onPieceDestroy;
    public static event PieceHandler onPieceMove;

    public static void pieceDestroy(GameObject piece) {
        if(onPieceDestroy != null) {
            onPieceDestroy(piece);
        }
    }
    public static void pieceMove(GameObject piece) {
        if(onPieceMove != null) {
            onPieceMove(piece);
        }
    }

}
