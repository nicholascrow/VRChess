using UnityEngine;
using System.Collections;

public class Rook : Piece
{
    public override void findAllValidMoves()
    {
        validMoves.Clear();

        GameMaster.Move m = new GameMaster.Move();
        m.OldLocation = locationIndices;
        m.MovedPiece = GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y];

       //forward x position
        for (int forwardX = (int)locationIndices.x+1; forwardX <= 7; forwardX++)
        {
            
            if (GameMaster.pieceBoard[forwardX, (int)locationIndices.y] != null)
            {
                if (GameMaster.pieceBoard[forwardX, (int)locationIndices.y].GetComponent<Piece>().color != color)
                {
                    m.NewLocation = new Vector2(forwardX, locationIndices.y);
                    m.TakenPiece = null;
                    validMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2(forwardX, locationIndices.y);
                m.TakenPiece = null;
                validMoves.Add(m);
            }
        }

        for (int backwardX = (int)locationIndices.x -1; backwardX >= 0 ; backwardX--)
        {
            if (GameMaster.pieceBoard[backwardX, (int)locationIndices.y] != null)
            {
                if (GameMaster.pieceBoard[backwardX, (int)locationIndices.y].GetComponent<Piece>().color != color)
                {
                    m.NewLocation = new Vector2(backwardX, locationIndices.y);
                    m.TakenPiece = null;
                    validMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2(backwardX, locationIndices.y);
                m.TakenPiece = null;
                validMoves.Add(m);
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
                    validMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2((int)locationIndices.x, leftX);
                m.TakenPiece = null;
                validMoves.Add(m);
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
                    validMoves.Add(m);
                }
                break;
            }
            else
            {
                m.NewLocation = new Vector2((int)locationIndices.x, rightX);
                m.TakenPiece = null;
                validMoves.Add(m);
            }
        }

       // colorValidMoves();
    }

}
