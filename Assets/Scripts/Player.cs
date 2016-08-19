using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public pieceColor color;
    public playerNumber type;

    public enum playerNumber { 
    Player1,
    Player2
    }

    public static void SwitchTurn(Player p = null) {
        if (p != null) {
            GameMaster.state = GameMaster.gameState.P1Turn;
            GameMaster.current = p;
            GameMaster.current.transform.root.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            GameMaster.Player2.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            if (GameMaster.state == GameMaster.gameState.P1Turn)
            {
                GameMaster.state = GameMaster.gameState.P2Turn;
                GameMaster.current = GameMaster.Player2.GetComponent<Player>();
                GameMaster.current.transform.root.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                GameMaster.Player1.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                GameMaster.state = GameMaster.gameState.P1Turn;
                GameMaster.current = GameMaster.Player1.GetComponent<Player>();
                GameMaster.current.transform.root.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                GameMaster.Player2.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
	// Update is called once per frame
    void Update()
    {
    }
}
