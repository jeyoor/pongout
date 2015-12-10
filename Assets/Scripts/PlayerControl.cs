using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public int playerNum;
    
    public PlayerType playerType;
    public ManualControlType manualControlType;
    public AutomatedControlType automatedControlType;
    
    private KeyCode moveUp, moveDown, moveLeft, moveRight;

	public float speedX=0, speedY=0;
    
    void Start() {
        if (playerType == PlayerType.Manual) {
            switch (manualControlType) {
                case ManualControlType.WASD:
                    moveUp = KeyCode.W;
                    moveDown = KeyCode.S;
                    moveLeft = KeyCode.A;
                    moveRight = KeyCode.D;
                break;
                case ManualControlType.ArrowKeys:
                    moveUp = KeyCode.UpArrow;
                    moveDown = KeyCode.DownArrow;
                    moveLeft = KeyCode.LeftArrow;
                    moveRight = KeyCode.RightArrow;
                break;
            }
        }
        else if (playerType == PlayerType.Automated) {
            //TODO: setup based on algorithm choice here
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (playerType == PlayerType.Manual) {
            // move the player in the 4 directions based on the keys we set up for it
            if (Input.GetKey (moveUp)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, speedY);
            } else if (Input.GetKey (moveDown)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -speedY);
            } else if (Input.GetKey (moveLeft)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, 0f);
            } else if (Input.GetKey (moveRight)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0f);
            } else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
        }
        else {
            //TODO: algorithmic strategies here
        }
	}
}
