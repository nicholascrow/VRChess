using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Knight : Piece
{

    public override void findAllValidMoves()
    {
        validMoves.Clear();

        List<GameMaster.Move> knight = new List<GameMaster.Move>();

        knight.Add(moveCreator(2, 1));
        knight.Add(moveCreator(2, -1));
        knight.Add(moveCreator(-2, 1));
        knight.Add(moveCreator(-2, -1));
        knight.Add(moveCreator(1, 2));
        knight.Add(moveCreator(1, -2));
        knight.Add(moveCreator(-1, 2));
        knight.Add(moveCreator(-1, -2));
 
        for (int i = 0; i < knight.Count; i++)
        {

            if (!knight[i].NewLocation.Equals(new Vector2(-1, -1)))
            {
                if (knight[i].TakenPiece != null && knight[i].TakenPiece.GetComponent<Piece>().color != color)
                {
                    validMoves.Add(knight[i]);
                }
                else if (knight[i].TakenPiece == null) 
                {
                    validMoves.Add(knight[i]);
                }
            }
        }



    //    colorValidMoves();
    }
  
}
