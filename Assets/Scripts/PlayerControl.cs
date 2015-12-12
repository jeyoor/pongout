using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public int playerNum;

    public PlayerType playerType;
    public GameObject gameBall;
    public ManualControlType manualControlType;
    public AutomatedControlType automatedControlType;

    private KeyCode moveLeft, moveRight;

    public float playerSpeed = 20;
    public float linearFollowSpeed = 3;
    public float fastLinearFollowSpeed = 6;

	private float speedX=0;

    public void Initialize() {
        if (playerType == PlayerType.Manual) {
            speedX = playerSpeed;
            switch (manualControlType) {
                case ManualControlType.WASD:
                    moveLeft = KeyCode.A;
                    moveRight = KeyCode.D;
                break;
                case ManualControlType.ArrowKeys:
                    moveLeft = KeyCode.LeftArrow;
                    moveRight = KeyCode.RightArrow;
                break;
            }
        }
        else if (playerType == PlayerType.Automated) {
            switch (automatedControlType) {
                case AutomatedControlType.LinearFollow:
                    speedX = linearFollowSpeed;
                break;
                case AutomatedControlType.FastLinearFollow:
                    speedX = fastLinearFollowSpeed;
                break;
                //TODO: setup other algorithmic strategies here
            }
        }
    }

	void Update () {
        if (playerType == PlayerType.Manual) {
            if (Input.GetKey (moveLeft)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, 0f);
            } else if (Input.GetKey (moveRight)) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0f);
            } else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
        }
        else if (playerType == PlayerType.Automated){
            switch (automatedControlType) {
                case AutomatedControlType.LinearFollow:
                    if (gameBall.transform.position.x > transform.position.x) {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0f);
                    }
                    else if (gameBall.transform.position.x < transform.position.x) {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, 0f);
                    }
                break;
                case AutomatedControlType.FastLinearFollow:
                    if (gameBall.transform.position.x > transform.position.x) {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0f);
                    }
                    else if (gameBall.transform.position.x < transform.position.x) {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, 0f);
                    }
                break;
                //TODO: other algorithmic strategies here
            }
        }
	}
}