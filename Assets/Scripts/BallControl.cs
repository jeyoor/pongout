using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallControl : MonoBehaviour {

	public static GameSetup gameSetup;
	public float minSpeed = 5, maxSpeed = 25;

    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    
    public GameObject gameOverPanel;
    
	public GameObject[] playerOneLives;
    public GameObject[] playerTwoLives;

	private TextMesh gameMessage;

	private GameObject resetButton;
	Vector3 initialPosPlayer1;
    Vector3 initialPosPlayer2;

	int playerOneLifeCount;
    int playerTwoLifeCount;
    
    int playerOneScore = 0;
    int playerTwoScore = 0;
    
    int lastPlayerTouched = -1;

	// Use this for initialization
	void Start () {
        // save the initial position
		initialPosPlayer1 = transform.position;
        initialPosPlayer2 = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
		UpdateScoreTexts();
		// set the speed
		ResetBallPosition();
		// find out how many spare balls we have
		playerOneLifeCount = playerOneLives.Length;
        playerTwoLifeCount = playerTwoLives.Length;
	}
	
	// Resets the ball with the initial position and speed.
	void ResetBallPosition() {
		
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        int playerOneOrPlayerTwo = Random.Range (0, 2);
        float upOrDown = 0;
        if (playerOneOrPlayerTwo == 0) {
            transform.position = initialPosPlayer1;
            upOrDown = -5;
        }
        else {
            transform.position = initialPosPlayer2;
            upOrDown = 5;
        }
        int leftOrRight = Random.Range (0, 2);
        if (leftOrRight == 0) { // go left or right
            GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (15f, 25f), upOrDown));
        }
        else {
            GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (-25f, -15f), upOrDown));
        }
	}

    void AverageSpeed(Collision2D colInfo) {
        //average the velocity over x between the player and the ball if the player is moving
        float velX = GetComponent<Rigidbody2D>().velocity.x;
        if (colInfo.collider.GetComponent<Rigidbody2D>().velocity.x != 0) {
            velX = velX / 2 + colInfo.collider.GetComponent<Rigidbody2D>().velocity.x / 3;
        }
        if (velX >= 0)
            velX = Mathf.Clamp (velX, minSpeed, maxSpeed);
        else
            velX = Mathf.Clamp (velX, -maxSpeed, -minSpeed);

        // then the same over y
        float velY = GetComponent<Rigidbody2D>().velocity.y;
        if (colInfo.collider.GetComponent<Rigidbody2D>().velocity.y != 0) {
            velY = velY / 2 + colInfo.collider.GetComponent<Rigidbody2D>().velocity.y / 3;
        }
        if (velY >= 0)
            velY = Mathf.Clamp (velY, minSpeed, maxSpeed);
        else
            velY = Mathf.Clamp (velY, -maxSpeed, -minSpeed);
        GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
    }

	// PlayerOne loses a ball if we hit the ground
	void PlayerOneLoseBall() {
		if (playerOneLifeCount > 0) {
			playerOneLifeCount--;
			playerOneLives [playerOneLifeCount].SetActive (false);
			ResetBallPosition();
		} else {
			GameOver("Two");
			gameObject.SetActive(false);
		}
	}
    
    // PlayerTwo loses a ball if we hit the ceiling
	void PlayerTwoLoseBall() {
		if (playerTwoLifeCount > 0) {
			playerTwoLifeCount--;
			playerTwoLives [playerTwoLifeCount].SetActive (false);
			ResetBallPosition();
		} else {
			GameOver("One");
			gameObject.SetActive(false);
		}
	}

	//delete the brick and increment the score
	void BrickCollision(Collision2D colInfo) {
		colInfo.collider.gameObject.SetActive(false);
		int pointsGained = 1;
		Brick brickData = colInfo.collider.GetComponent<Brick>();
		if (brickData != null) {
			brickData.SetDestroyed(true);
			if (brickData.GetBrickType () == Brick.BrickType.RED) {
			    pointsGained = 2;
			}
		}
        if (lastPlayerTouched == 1) {
            playerOneScore += pointsGained;
        }
        else if (lastPlayerTouched == 2) {
            playerTwoScore += pointsGained;
        }
		UpdateScoreTexts();
        if (gameSetup.IsGameWon()) {
			GameWon();
		}
	}

    //TODO: replace with level rebuild logic here???
	//The game has been won because the last brick was destroyed
	private void GameWon() {
		for (int i = 0; i < playerOneLifeCount; i++) {
			playerOneScore += 5;
			UpdateScoreTexts();
		}
        for (int i = 0; i < playerTwoLifeCount; i++) {
			playerTwoScore += 5;
			UpdateScoreTexts();
		}
        gameObject.SetActive(false);
		gameOverPanel.SetActive(true);
	}

	//The game has been lost because the last ball was lost
	private void GameOver(string playerWon) {
		gameOverPanel.SetActive(true);
	}

	//Update the score message with a new value
	private void UpdateScoreTexts() {
		if (playerOneScoreText != null) {
			playerOneScoreText.text = "Player 1: " + playerOneScore;
		}
        if (playerTwoScoreText != null) {
			playerTwoScoreText.text = "Player 2: " + playerTwoScore;
		}
	}

	// collision detection function
	void OnCollisionEnter2D(Collision2D colInfo) {
		if (colInfo.collider.tag == "Player") {
            
            PlayerControl playerInfo = colInfo.collider.GetComponent<PlayerControl>();
            lastPlayerTouched = playerInfo.playerNum;
            
			//average the velocity over x between the player and the ball
			AverageSpeed (colInfo);
		}
		else if (colInfo.collider.tag == "Ground") {
			PlayerOneLoseBall ();
		}
        else if (colInfo.collider.tag == "Ceiling") {
			PlayerTwoLoseBall ();
		}
		else if (colInfo.collider.tag == "Brick") {
			BrickCollision(colInfo);
		}
	}
}
