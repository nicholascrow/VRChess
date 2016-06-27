using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameMaster {
	public enum gameState
	{
		P1Turn,
		P2Turn,
		gameOver
	}
	public static GameObject[,] gameBoard;
	public static GameObject[,] pieceBoard;

	public static GameObject selectedPiece;
	public static GameObject selectedMove;

	public static gameState state;
	public static Piece.pieceColor p1Color, p2Color;
}
