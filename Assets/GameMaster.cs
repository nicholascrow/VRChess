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

	public struct PieceAndLocation{
		public GameObject piece;
		public Tile pieceTile;

	}

	public static GameObject selectedPiece;


}
