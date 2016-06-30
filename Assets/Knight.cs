using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Knight : Piece
{

    public override void findAllValidMoves()
    {
        validMoves.Clear();

        List<GameMaster.Move> knight = new List<GameMaster.Move>();

        knight.Add(makeMove(2, 1));
        knight.Add(makeMove(2, -1));
        knight.Add(makeMove(-2, 1));
        knight.Add(makeMove(-2, -1));
        knight.Add(makeMove(1, 2));
        knight.Add(makeMove(1, -2));
        knight.Add(makeMove(-1, 2));
        knight.Add(makeMove(-1, -2));
        print(knight.Count);
        for (int i = 0; i < knight.Count; i++)
        {
            print(knight[i].NewLocation);
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



        colorValidMoves();
    }
    GameMaster.Move makeMove(int addX, int addY)
    {
        GameMaster.Move m = new GameMaster.Move();
        m.OldLocation = locationIndices;
        m.MovedPiece = GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y];
        m.NewLocation = new Vector2(locationIndices.x + addX, locationIndices.y + addY);
        if (m.NewLocation.x != -1)
        {
            m.TakenPiece = GameMaster.pieceBoard[(int)m.NewLocation.x, (int)m.NewLocation.y];
        }
        return m;
    }
}
