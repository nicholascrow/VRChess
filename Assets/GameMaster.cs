using UnityEngine;
using System.Collections;

public static class GameMaster {
	public enum gameState
	{
		P1Turn,
		P2Turn,
		gameOver
	}
	public static GameObject[,] gameBoard;

	public static GameObject selectedPiece;
	public static GameObject selectedMove;

}
