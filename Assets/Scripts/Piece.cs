using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Piece : MonoBehaviour
{
    #region Enums
    public enum pieceColor
    {
        White,
        Black
    }

    public enum pieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        King,
        Queen
    }
    #endregion

    #region Public Vars
    //the type of this piece
    public pieceType type;

    //the color of this piece
    public pieceColor color;

    //the index on the board of this piece
    public Vector2 locationIndices;

    //list of the valid moves of the current piece
    public List<GameMaster.Move> validMoves;

    //has this piece moved yet?
    public bool firstMove = true;
    #endregion

    #region Abstract Classes
    public abstract void findAllValidMoves();
    #endregion

    //initialize all variables
    void Start()
    {
        validMoves = new List<GameMaster.Move>();
    }


    void OnMouseDown()
    {
        
            if (GameMaster.selectedPiece != null)
            {

                GameMaster.selectedMove = GameMaster.gameBoard[(int)locationIndices.x, (int)locationIndices.y];
                //SetTransparency(GameMaster.selectedPiece, 1f);
                //GameMaster.selectedPiece = null;
            }
            else
            {
                if (GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y].GetComponent<Piece>().color == GameMaster.current.color)
                {
                //set the current selected piece
                GameMaster.selectedPiece = this.transform.root.gameObject;

                //get the current color and change transparency
                SetTransparency(GameMaster.selectedPiece, .5f);
                print(type + " Selected!");
                findAllValidMoves();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameMaster.selectedPiece == this.transform.root.gameObject && GameMaster.selectedMove)
        {
            GameObject tempPiece = GameMaster.selectedPiece;
            GameObject tempTile = GameMaster.selectedMove;
            SetTransparency(GameMaster.selectedPiece,1f);
            GameMaster.selectedPiece = null;
            GameMaster.selectedMove = null;
            
            if (isValidMove(tempPiece, tempTile))
            {
                Debug.Log("Move from " + locationIndices + " to " + tempTile.GetComponent<Tile>().locationIndices + " is valid.");
                firstMove = false;
                if (GameMaster.pieceBoard[(int)tempTile.GetComponent<Tile>().locationIndices.x, (int)tempTile.GetComponent<Tile>().locationIndices.y] != null) {
                    Destroy(GameMaster.pieceBoard[(int)tempTile.GetComponent<Tile>().locationIndices.x, (int)tempTile.GetComponent<Tile>().locationIndices.y]);
                }
                GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y] = null;
                GameMaster.pieceBoard[(int)tempTile.GetComponent<Tile>().locationIndices.x, (int)tempTile.GetComponent<Tile>().locationIndices.y] = tempPiece;
                
                StartCoroutine(lerpToPos(tempPiece, tempTile, tempPiece.transform.position, tempTile.transform.root.transform.position + new Vector3(0, 1, 0)));
                Player.SwitchTurn();
                for (int i = 0; i < 8; i++) { for (int j = 0; j < 8; j++) {
                    if (GameMaster.pieceBoard[i, j] != null) { GameMaster.pieceBoard[i, j].GetComponent<Piece>().findAllValidMoves(); }
                } }
                for(int i = 0; i < GameMaster.kings.Count;i++){
                    GameMaster.kings[i].kingInCheck();
                }
            }
            else
            {
                Debug.LogWarning("Move from " + locationIndices + " to " + tempTile.GetComponent<Tile>().locationIndices + " is NOT valid.");

            }
            for (int i = 0; i < validMoves.Count; i++)
            {
                GameMaster.gameBoard[(int)validMoves[i].NewLocation.x, (int)validMoves[i].NewLocation.y].GetComponent<Renderer>().material.color =
                    GameMaster.gameBoard[(int)validMoves[i].NewLocation.x, (int)validMoves[i].NewLocation.y].GetComponent<Tile>().c;
            }
            //StartCoroutine (findAllValidMoves ());
            //StartCoroutine (lerpToPos (tempPiece, tempTile,tempPiece.transform.position, tempTile.transform.root.transform.position + new Vector3 (0, 1, 0)));
        }


    }


    // returns true if valid, false otherwise
    bool isValidMove(GameObject tempPiece, GameObject tempTile)
    {
        Debug.Log("Checking if " + tempTile.GetComponent < Tile>().locationIndices + " is a valid move.");
        for (int i = 0; i < validMoves.Count; i++)
        {
           // if(validMoves[i].NewLocation
            if (validMoves[i].NewLocation.Equals(tempTile.GetComponent<Tile>().locationIndices))
            {
                
                return true;
            }
            else
            {
                //if the move is invalid
            }
        }
        return false;
    }

  

    /// <summary>
    /// Moves the piece from its start position to the end position over 1 second.
    /// </summary>
    /// <returns></returns>
    /// <param name="tempPiece">Temp piece.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    IEnumerator lerpToPos(GameObject tempPiece, GameObject tempTile, Vector3 start, Vector3 end)
    {
        for (float i = 0; i < .3f; i += Time.deltaTime)
        {
            yield return null;
            tempPiece.transform.position = Vector3.Lerp(start, end, i / .3f);
        }
        tempPiece.GetComponent<Piece>().locationIndices = tempTile.GetComponent<Tile>().locationIndices;

    }








    #region Move Highlighting
    public void colorValidMoves()
    {
        for (int i = 0; i < validMoves.Count; i++)
        {
            GameMaster.gameBoard[(int)validMoves[i].NewLocation.x, (int)validMoves[i].NewLocation.y].GetComponent<Renderer>().material.color = Color.yellow;

        }
    }

    #endregion




    #region Helper Methods

    //set the transparency of the material on the object passed in.
    public void SetTransparency(GameObject piece, float value)
    {
        //make the material transparent if it isnt already
        foreach (var child in piece.GetComponentsInChildren<Renderer>())
        {
           
            Material material = child.GetComponent<Renderer>().material;
            material.SetFloat("_Mode", 3);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;


            Color c = piece.GetComponent<Renderer>().material.color;
            c.a = value;
            child.GetComponent<Renderer>().material.color = c;
        }

    }

   protected GameMaster.Move moveCreator(int addX, int addY)
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
    #endregion
}
