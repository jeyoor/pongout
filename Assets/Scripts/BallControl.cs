using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	public static GameSetup gameSetup;
	public float minSpeed = 5, maxSpeed = 25;

	public GameObject[] playerOneLives;
    public GameObject[] playerTwoLives;

	private TextMesh gameMessage;
	private TextMesh scoreMessage;
	private GameObject resetButton;
	private int score = 0;
	Vector3 initialPos = new Vector3(0f, 0f, 0f);

	int playerOneLifeCount;
    int playerTwoLifeCount;

	// Use this for initialization
	void Start () {
		gameMessage = GameObject.Find("gameMessage").GetComponentInChildren<TextMesh>();
		gameMessage.text = "";
		scoreMessage = GameObject.Find("scoreMessage").GetComponentInChildren<TextMesh>();
		UpdateScoreMessage(0);
		resetButton = GameObject.Find("resetButton");
		resetButton.SetActive(false);
		// save the initial position
		initialPos = transform.position;
		// set the speed
		Reset();
		// find out how many spare balls we have
		playerOneLifeCount = playerOneLives.Length;
        playerTwoLifeCount = playerTwoLives.Length;
	}
	
	// Resets the ball with the initial position and speed.
	void Reset() {
		transform.position = initialPos; //recover the initial position
		int randomNumber = Random.Range (0, 2);
		if (randomNumber == 0) { // go left or right
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (30f, 50f), 10));
		} else {
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (-50f, -30f), 10));
		}
	}

	void AverageSpeed(Collision2D colInfo) {
		//average the velocity over x between the player and the ball if the player is moving
		float velX = GetComponent<Rigidbody2D>().velocity.x;
		if (colInfo.collider.GetComponent<Rigidbody2D>().velocity.x != 0) {
			velX = velX / 2 + colInfo.collider.GetComponent<Rigidbody2D>().velocity.x / 3;
			if (velX >= 0)
				velX = Mathf.Clamp (velX, minSpeed, maxSpeed);
			else
				velX = Mathf.Clamp (velX, -maxSpeed, -minSpeed);
		}
		// then the same over y
		float velY = GetComponent<Rigidbody2D>().velocity.y;
		if (colInfo.collider.GetComponent<Rigidbody2D>().velocity.y != 0) {
			velY = velY / 2 + colInfo.collider.GetComponent<Rigidbody2D>().velocity.y / 3;
			if (velY >= 0)
				velY = Mathf.Clamp (velY, minSpeed, maxSpeed);
			else
				velY = Mathf.Clamp (velY, -maxSpeed, -minSpeed);
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
	}

	// PlayerOne loses a ball if we hit the ground
	void PlayerOneLoseBall() {
		if (playerOneLifeCount > 0) {
			playerOneLifeCount--;
			playerOneLives [playerOneLifeCount].SetActive (false);
			Reset();
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
			Reset();
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
		score += pointsGained;
		UpdateScoreMessage(score);
        if (gameSetup.IsGameWon()) {
			GameWon();
		}
	}

    //TODO: replace with level rebuild logic here???
	//The game has been won because the last brick was destroyed
	private void GameWon() {
		for (int i = 0; i < playerOneLifeCount; i++) {
			score += 5;
			UpdateScoreMessage(score);
			gameObject.SetActive(false);
		}
		resetButton.SetActive(true);
		gameMessage.text = "You won... :D";
	}

	//The game has been lost because the last ball was lost
	private void GameOver(string playerWon) {
		gameMessage.text = "Player " + playerWon + " Won!";
		resetButton.SetActive(true);
	}

	//Update the score message with a new value
	private void UpdateScoreMessage(int newScore) {
		if (scoreMessage != null) {
			scoreMessage.text = "Score: " + newScore;
		}
	}

	// collision detection function
	void OnCollisionEnter2D(Collision2D colInfo) {
		if (colInfo.collider.tag == "Player") {
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
