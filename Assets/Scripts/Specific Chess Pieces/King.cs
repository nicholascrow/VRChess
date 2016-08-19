using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : Piece {
    public bool isInCheck;



    public override void findAllValidMoves()
    {
        validMoves.Clear();

        List<GameMaster.Move> king = new List<GameMaster.Move>();

        king.Add(moveCreator(1, 1));
        king.Add(moveCreator(1, 0));
        king.Add(moveCreator(1, -1));
        king.Add(moveCreator(0, 1));
        king.Add(moveCreator(0, -1));
        king.Add(moveCreator(-1, 1));
        king.Add(moveCreator(-1, 0));
        king.Add(moveCreator(-1, -1));

        for (int i = 0; i < king.Count; i++)
        {

            if (!king[i].NewLocation.Equals(new Vector2(-1, -1)))
            {
                if (king[i].TakenPiece != null && king[i].TakenPiece.GetComponent<Piece>().color != color)
                {
                    validMoves.Add(king[i]);
                }
                else if (king[i].TakenPiece == null)
                {
                    validMoves.Add(king[i]);
                }
            }
        }
       // colorValidMoves();
    }



   public bool kingInCheck()
    {

        List<GameMaster.Move> possibleMoves = new List<GameMaster.Move>();
        GameMaster.Move m = new GameMaster.Move();
        m.OldLocation = locationIndices;
        m.MovedPiece = GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y];

        //forward x position
        for (int forwardX = (int)locationIndices.x + 1; forwardX <= 7; forwardX++)
        {

            if (GameMaster.pieceBoard[forwardX, (int)locationIndices.y] != null)
            {
                if (GameMaster.pieceBoard[forwardX, (int)locationIndices.y].GetComponent<Piece>().color != color)
                {
                    m.NewLocation = new Vector2(forwardX, locationIndices.y);
                    m.TakenPiece = null;
                    possibleMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2(forwardX, locationIndices.y);
                m.TakenPiece = null;
                possibleMoves.Add(m);
            }
        }

        for (int backwardX = (int)locationIndices.x - 1; backwardX >= 0; backwardX--)
        {
            if (GameMaster.pieceBoard[backwardX, (int)locationIndices.y] != null)
            {
                if (GameMaster.pieceBoard[backwardX, (int)locationIndices.y].GetComponent<Piece>().color != color)
                {
                    m.NewLocation = new Vector2(backwardX, locationIndices.y);
                    m.TakenPiece = null;
                    possibleMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2(backwardX, locationIndices.y);
                m.TakenPiece = null;
                possibleMoves.Add(m);
            }
        }

        for (int leftX = (int)locationIndices.y + 1; leftX <= 7; leftX++)
        {
            if (GameMaster.pieceBoard[(int)locationIndices.x, leftX] != null)
            {
                if (GameMaster.pieceBoard[(int)locationIndices.x, leftX].GetComponent<Piece>().color != color)
                {
                    m.NewLocation = new Vector2((int)locationIndices.x, leftX);
                    m.TakenPiece = null;
                    possibleMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2((int)locationIndices.x, leftX);
                m.TakenPiece = null;
                possibleMoves.Add(m);
            }
        }



        for (int rightX = (int)locationIndices.y - 1; rightX >= 0; rightX--)
        {
            if (GameMaster.pieceBoard[(int)locationIndices.x, rightX] != null)
            {
                if (GameMaster.pieceBoard[(int)locationIndices.x, rightX].GetComponent<Piece>().color != color)
                {
                    m.NewLocation = new Vector2((int)locationIndices.x, rightX);
                    m.TakenPiece = null;
                    possibleMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2((int)locationIndices.x, rightX);
                m.TakenPiece = null;
                possibleMoves.Add(m);
            }
        }


        m = moveCreator(1, 1);
       

        for (int rightDiagonalUp = 1; rightDiagonalUp < 8; rightDiagonalUp++)
        {

            if (!((int)locationIndices.x + rightDiagonalUp < 8) || !((int)locationIndices.y + rightDiagonalUp < 8)) { break; }

            if (GameMaster.pieceBoard[(int)locationIndices.x + rightDiagonalUp, (int)locationIndices.y + rightDiagonalUp] == null)
            {

                possibleMoves.Add(moveCreator(rightDiagonalUp, rightDiagonalUp));
            }
            else if (GameMaster.pieceBoard[(int)locationIndices.x + rightDiagonalUp, (int)locationIndices.y + rightDiagonalUp].GetComponent<Piece>().color != color)
            {

                possibleMoves.Add(moveCreator(rightDiagonalUp, rightDiagonalUp));
                break;
            }
            else
            {
              //  if(GameMaster.debugMode) print(GameMaster.pieceBoard[(int)locationIndices.x + rightDiagonalUp, (int)locationIndices.y + rightDiagonalUp].GetComponent<Piece>().type);

                break;
            }

        }

        for (int leftDiagonalUp = 1; leftDiagonalUp < 8; leftDiagonalUp++)
        {

            if (!((int)locationIndices.x + leftDiagonalUp < 8) || !((int)locationIndices.y - leftDiagonalUp >= 0)) { break; }

            if (GameMaster.pieceBoard[(int)locationIndices.x + leftDiagonalUp, (int)locationIndices.y - leftDiagonalUp] == null)
            {
                possibleMoves.Add(moveCreator(leftDiagonalUp, -leftDiagonalUp));
            }
            else if (GameMaster.pieceBoard[(int)locationIndices.x + leftDiagonalUp, (int)locationIndices.y - leftDiagonalUp].GetComponent<Piece>().color != color)
            {
                possibleMoves.Add(moveCreator(leftDiagonalUp, -leftDiagonalUp));
                break;
            }
            else
            {
                break;
            }
        }

        for (int rightDiagonalDown = 1; rightDiagonalDown < 8; rightDiagonalDown++)
        {
            if (!((int)locationIndices.x - rightDiagonalDown >= 0) || !((int)locationIndices.y + rightDiagonalDown < 8)) { break; }

            if (GameMaster.pieceBoard[(int)locationIndices.x - rightDiagonalDown, (int)locationIndices.y + rightDiagonalDown] == null)
            {
                possibleMoves.Add(moveCreator(-rightDiagonalDown, rightDiagonalDown));
            }
            else if (GameMaster.pieceBoard[(int)locationIndices.x - rightDiagonalDown, (int)locationIndices.y + rightDiagonalDown].GetComponent<Piece>().color != color)
            {
                possibleMoves.Add(moveCreator(-rightDiagonalDown, rightDiagonalDown));
                break;
            }
            else
            {
                break;
            }

        }

        for (int leftDiagonalDown = 1; leftDiagonalDown < 8; leftDiagonalDown++)
        {
            if (!((int)locationIndices.x - leftDiagonalDown >= 0) || !((int)locationIndices.y - leftDiagonalDown >= 0)) { break; }

            if (GameMaster.pieceBoard[(int)locationIndices.x - leftDiagonalDown, (int)locationIndices.y - leftDiagonalDown] == null)
            {
                possibleMoves.Add(moveCreator(-leftDiagonalDown, -leftDiagonalDown));
            }
            else if (GameMaster.pieceBoard[(int)locationIndices.x - leftDiagonalDown, (int)locationIndices.y - leftDiagonalDown].GetComponent<Piece>().color != color)
            {
                possibleMoves.Add(moveCreator(-leftDiagonalDown, -leftDiagonalDown));
                break;
            }
            else
            {
                break;
            }

        }

        List<GameMaster.Move> possibleCheck = new List<GameMaster.Move>();


        for (int i = 0; i < possibleMoves.Count; i++) {

            //if this condition is true then that piece cannot cause check
            if(possibleMoves[i].TakenPiece != null && possibleMoves[i].TakenPiece.GetComponent<Piece>().color != color) {
                //if(GameMaster.debugMode) print("Possible check: " + possibleMoves[i].TakenPiece);
                possibleCheck.Add(possibleMoves[i]);
            }
        }

        
        for (int i = 0; i < possibleCheck.Count; i++)
        {
            for(int j = 0; j <  possibleCheck[i].TakenPiece.GetComponent<Piece>().validMoves.Count;j++)
                {
                possibleCheck[i].TakenPiece.GetComponent<Piece>().findAllValidMoves();
               // possibleCheck[i].TakenPiece.GetComponent<Piece>().colorValidMoves();
                if(possibleCheck[i].TakenPiece.GetComponent<Piece>().validMoves[j].TakenPiece == this.gameObject) {
                    if(GameMaster.debugModeLevel >= (int)logLevel.MED) print("IN CHECK!");
                    return true;
                }
            }

        }







        return false;
    }




}
