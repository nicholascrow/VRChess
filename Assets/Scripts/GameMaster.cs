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

    //the current player (its this players turn)
    public static Player current;


    //game board and piece board are overlayed arrays. 
	public static GameObject[,] gameBoard;
	public static GameObject[,] pieceBoard;


    //the first one is the piece selected to move, and the second is the square they want to move to.
	public static GameObject selectedPiece;
	public static GameObject selectedMove;


    //the state of the game (changes per turn)
	public static gameState state;


    //player 1 and player 2
    public static GameObject Player1, Player2;

    //list of the kings used to check for check and checkmate
    public static List<King> kings;


    public static int debugModeLevel = 0;
}
public enum logLevel {
OFF = 0,
LOW,
MED,
HIGH
}