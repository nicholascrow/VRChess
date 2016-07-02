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

    public struct Move {
        Vector2 oldLoc, newLoc;
        GameObject piece1,piece2;

        public Vector2 OldLocation
        {
            get { return oldLoc ; }
            set { oldLoc = value; }
        }
        public Vector2 NewLocation
        {
            get { return newLoc; }
            set
            {
                if (value.x > 7 || value.x < 0 || value.y > 7 || value.y < 0) { newLoc = new Vector2(-1,-1); }
                else { newLoc = value; }
            }
        }
        public GameObject MovedPiece
        {
            get { return piece1; }
            set { piece1 = value; }
        }
        public GameObject TakenPiece
        {
            get { return piece2; }
            set { piece2 = value; }
        }
    
    
    }

    public static Player current;

	public static GameObject[,] gameBoard;
	public static GameObject[,] pieceBoard;

	public static GameObject selectedPiece;
	public static GameObject selectedMove;

	public static gameState state;

    public static GameObject Player1, Player2;
}
