using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bishop : Piece {

    public override void findAllValidMoves()
    {
        validMoves.Clear();

        GameMaster.Move m = moveCreator(1, 1);
        List<GameMaster.Move> possibleMoves = new List<GameMaster.Move>();

        for (int rightDiagonalUp = 1; rightDiagonalUp < 8; rightDiagonalUp++) {
           
            if (!((int)locationIndices.x + rightDiagonalUp < 8) || !((int)locationIndices.y + rightDiagonalUp < 8)) { break; }
            
            if (GameMaster.pieceBoard[(int)locationIndices.x + rightDiagonalUp, (int)locationIndices.y + rightDiagonalUp] == null)
            {
               
                possibleMoves.Add(moveCreator(rightDiagonalUp, rightDiagonalUp));
            }
            else if (GameMaster.pieceBoard[(int)locationIndices.x +  rightDiagonalUp, (int)locationIndices.y + rightDiagonalUp].GetComponent<Piece>().color != color)
            {
                
                possibleMoves.Add(moveCreator(rightDiagonalUp, rightDiagonalUp));
                break;
            }
            else
            {
                print(GameMaster.pieceBoard[(int)locationIndices.x + rightDiagonalUp, (int)locationIndices.y + rightDiagonalUp].GetComponent<Piece>().type);
                
                break;
            }

        }

        for (int leftDiagonalUp = 1; leftDiagonalUp < 8; leftDiagonalUp++) {
           
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

        for (int i = 0; i < possibleMoves.Count; i++)
        {

            if (!possibleMoves[i].NewLocation.Equals(new Vector2(-1, -1)))
            {
                if (possibleMoves[i].TakenPiece != null && possibleMoves[i].TakenPiece.GetComponent<Piece>().color != color)
                {
                    validMoves.Add(possibleMoves[i]);
                }
                else if (possibleMoves[i].TakenPiece == null)
                {
                    validMoves.Add(possibleMoves[i]);
                }
            }
        }

        colorValidMoves();
    }

}
