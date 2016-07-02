using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : Piece {
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



        colorValidMoves();
    }
}
