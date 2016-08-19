using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region Enums
public enum pieceColor {
    White,
    Black
}

public enum pieceType {
    Pawn,
    Rook,
    Knight,
    Bishop,
    King,
    Queen
}
#endregion

public abstract class Piece : MonoBehaviour {

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

    #region Private Vars
    #endregion

    #region Abstract Classes
    public abstract void findAllValidMoves();
    #endregion

    //initialize all variables
    void Start() {
        validMoves = new List<GameMaster.Move>();
    }


    void OnMouseDown() {

        //select the piece we're going to move
        if(GameMaster.selectedPiece != null) {
            

            GameMaster.selectedMove = GameMaster.gameBoard[(int)locationIndices.x, (int)locationIndices.y];
            //SetTransparency(GameMaster.selectedPiece, 1f);
            //GameMaster.selectedPiece = null;
        }
        else {
            if(GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y].GetComponent<Piece>().color == GameMaster.current.color) {
                //set the current selected piece
                GameMaster.selectedPiece = this.transform.root.gameObject;

                //get the current color and change transparency
                SetTransparency(GameMaster.selectedPiece, .5f);
                if(GameMaster.debugModeLevel >= (int)logLevel.LOW) print(type + " Selected!");
                findAllValidMoves();
                colorValidMoves();
            }
        }
    }

    bool hasChecked = false;
    // Update is called once per frame
    void Update() {
        /*
        for(int i = 0; i < 8; i++) {
            for(int j = 0; j < 8; j++) {
                if(GameMaster.pieceBoard[i, j] != null && GameMaster.pieceBoard[i, j].GetComponent<Piece>() != null) { GameMaster.pieceBoard[i, j].GetComponent<Piece>().findAllValidMoves(); }
            }
        }*/
        for(int i = 0; i < GameMaster.kings.Count; i++) {
            if(GameMaster.kings[i].kingInCheck() && !hasChecked) {
                // if(GameMaster.debugMode) print("KING IN CHECK");
                GameMaster.selectedPiece = null;
                GameMaster.selectedMove = null;
                hasChecked = true;
                //return;
            }
        }
        

        if(GameMaster.selectedPiece == this.transform.root.gameObject && GameMaster.selectedMove) {
            resetColors();


            GameObject tempPiece = GameMaster.selectedPiece;
            GameObject tempTile = GameMaster.selectedMove;
            SetTransparency(GameMaster.selectedPiece, 1f);
            GameMaster.selectedPiece = null;
            GameMaster.selectedMove = null;

            if(isValidMove(tempPiece, tempTile)) {

                //logging
                if(GameMaster.debugModeLevel >= (int)logLevel.MED)
                    Debug.Log("Move from " + locationIndices + " to " + tempTile.GetComponent<Tile>().locationIndices + " is valid.");

                //this is no longer the piece's first move (used in PAWN)
                firstMove = false;

                //if the piece has been taken, then we destroy its gameobject.
                if(GameMaster.pieceBoard[(int)tempTile.GetComponent<Tile>().locationIndices.x, (int)tempTile.GetComponent<Tile>().locationIndices.y] != null) {
                    Destroy(GameMaster.pieceBoard[(int)tempTile.GetComponent<Tile>().locationIndices.x, (int)tempTile.GetComponent<Tile>().locationIndices.y]);

                    //change piece board?
                }


                GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y] = null;
                GameMaster.pieceBoard[(int)tempTile.GetComponent<Tile>().locationIndices.x, (int)tempTile.GetComponent<Tile>().locationIndices.y] = tempPiece;

                StartCoroutine(lerpToPos(tempPiece, tempTile, tempPiece.transform.position, tempTile.transform.root.transform.position + new Vector3(0, 1, 0)));
                Player.SwitchTurn();
                

            }
            else {
                //logging
                if(GameMaster.debugModeLevel >= (int)logLevel.MED)
                    Debug.LogWarning("Move from " + locationIndices + " to " + tempTile.GetComponent<Tile>().locationIndices + " is NOT valid.");

            }

            //REDO HERE
           /* for(int i = 0; i < validMoves.Count; i++) {
                GameMaster.gameBoard[(int)validMoves[i].NewLocation.x, (int)validMoves[i].NewLocation.y].GetComponent<Renderer>().material.color =
                    GameMaster.gameBoard[(int)validMoves[i].NewLocation.x, (int)validMoves[i].NewLocation.y].GetComponent<Tile>().c;
            }*/
        }


    }


    // returns true if valid, false otherwise
    bool isValidMove(GameObject tempPiece, GameObject tempTile) {
        if(GameMaster.debugModeLevel >= (int)logLevel.MED)
            Debug.Log("Checking if " + tempTile.GetComponent<Tile>().locationIndices + " is a valid move.");
        for(int i = 0; i < validMoves.Count; i++) {
            // if(validMoves[i].NewLocation
            if(validMoves[i].NewLocation.Equals(tempTile.GetComponent<Tile>().locationIndices)) {

                return true;
            }
            else {
                //if the move is invalid
            }
        }
        return false;
    }

    #region Move Highlighting
    public void colorValidMoves() {
        for(int i = 0; i < validMoves.Count; i++) {
            GameMaster.gameBoard[(int)validMoves[i].NewLocation.x, (int)validMoves[i].NewLocation.y].GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    public void resetColors() {
        for(int i = 0; i < 8; i++) {
            for(int j = 0; j < 8; j++) {
                GameMaster.gameBoard[i, j].GetComponent<Tile>().GetComponent<Renderer>().material.color = GameMaster.gameBoard[i, j].GetComponent<Tile>().c;
            }
        }
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// Moves the piece from its start position to the end position over 1 second.
    /// </summary>
    /// <returns></returns>
    /// <param name="tempPiece">Temp piece.</param>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    protected IEnumerator lerpToPos(GameObject tempPiece, GameObject tempTile, Vector3 start, Vector3 end) {
        for(float i = 0; i < .3f; i += Time.deltaTime) {
            yield return null;
            tempPiece.transform.position = Vector3.Lerp(start, end, i / .3f);
        }
        tempPiece.GetComponent<Piece>().locationIndices = tempTile.GetComponent<Tile>().locationIndices;

    }
    
    /// <summary>
    /// Set the transparency of the material on the object passed in.
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="value"></param>
    protected void SetTransparency(GameObject piece, float value) {
        //make the material transparent if it isnt already
        foreach(var child in piece.GetComponentsInChildren<Renderer>()) {

            Material material = child.GetComponent<Renderer>().material;
            material.SetFloat("_Mode", 3);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;


            Color c = child.GetComponent<Renderer>().material.color;
            c.a = value;
            child.GetComponent<Renderer>().material.color = c;
        }

    }

    /// <summary>
    /// This method creates a movement pattern based on the passed in location
    /// </summary>
    /// <param name="addX"></param>
    /// <param name="addY"></param>
    /// <returns></returns>
    protected GameMaster.Move moveCreator(int addX, int addY) {
        GameMaster.Move m = new GameMaster.Move();
        m.OldLocation = locationIndices;
        m.MovedPiece = GameMaster.pieceBoard[(int)locationIndices.x, (int)locationIndices.y];
        m.NewLocation = new Vector2(locationIndices.x + addX, locationIndices.y + addY);
        if(m.NewLocation.x != -1) {
            m.TakenPiece = GameMaster.pieceBoard[(int)m.NewLocation.x, (int)m.NewLocation.y];
        }
        return m;
    }

    #endregion
}
