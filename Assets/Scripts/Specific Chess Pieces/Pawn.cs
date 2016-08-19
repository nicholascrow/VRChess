using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pawn : Piece {
    
    public override void findAllValidMoves()
    {
        validMoves.Clear();
  
        int colorDirection = color == pieceColor.White ? 1 : -1;

                //this would indicate getting a new piece in the pawn's place.
                if ((int)locationIndices.x > 6 || (int)locationIndices.x < 1)
                {
                }

                //this would indicate any other position on the board (a-h 2-7)
                else
                {
                    GameMaster.Move m = new GameMaster.Move();
                    m.OldLocation = locationIndices;
                    m.MovedPiece = GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y];


                 //   if(GameMaster.debugMode) print("Checking [" + (locationIndices.x + colorForPiece) + ", " + locationIndices.y + "]");
                    if (GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y] == null)
                    {                        
                        m.NewLocation = new Vector2((int)locationIndices.x + colorDirection, (int)locationIndices.y);
                        m.TakenPiece = GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y];
                        validMoves.Add(m);
                    }
                    if (firstMove) {
                        if (GameMaster.pieceBoard[(int)locationIndices.x + colorDirection*2, (int)locationIndices.y] == null) {

                            m.NewLocation = new Vector2((int)locationIndices.x + colorDirection*2, (int)locationIndices.y);
                            m.TakenPiece = GameMaster.pieceBoard[(int)locationIndices.x + colorDirection*2, (int)locationIndices.y];
                            validMoves.Add(m);

                        }
                    }


                    if ((int)locationIndices.y > 0)
                    {
                   //     if(GameMaster.debugMode) print("Checking [" + (locationIndices.x + colorForPiece) + ", " + (locationIndices.y - 1) + "]");
                        if (GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y - 1] != null)
                        {
                            if (GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y - 1].GetComponent<Piece>().color != color)
                            {
                                m.NewLocation = new Vector2((int)locationIndices.x + colorDirection, (int)locationIndices.y - 1);
                                m.TakenPiece = GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y - 1];
                                validMoves.Add(m);
                            }
                        }
                    }

                   // if(GameMaster.debugMode) print("Checking [" + (locationIndices.x + colorForPiece) + ", " + (locationIndices.y + 1) + "]");
                    if ((int)locationIndices.y < 7)
                    {
     
                        if (GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y + 1] != null)
                        {
                            if (GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y + 1].GetComponent<Piece>().color != color)
                            {
                                m.NewLocation = new Vector2((int)locationIndices.x + colorDirection, (int)locationIndices.y + 1);
                                m.TakenPiece = GameMaster.pieceBoard[(int)locationIndices.x + colorDirection, (int)locationIndices.y + 1];
                                validMoves.Add(m);
                            }
                        }
                    }
                }

      //  colorValidMoves();
    }













}
