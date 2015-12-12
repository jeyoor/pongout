using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameSetup : MonoBehaviour {

	private float brickYOffset = -4f;

	public Camera mainCam;

	public GameObject topWall;
	public GameObject bottomWall;
	public GameObject leftWall;
	public GameObject rightWall;

    public GameObject gameStartPanel;
    public GameObject headsUpDisplayPanel;

    public Toggle playerOneManual;
    public Toggle playerOneLinearFollow;
    public Toggle playerOneFastLinearFollow;
    public Toggle playerOnePhysicsForecast;
    public Toggle playerTwoManual;
    public Toggle playerTwoLinearFollow;
    public Toggle playerTwoFastLinearFollow;
    public Toggle playerTwoPhysicsForecast;

    public PlayerControl playerOne;
    public PlayerControl playerTwo;
    
    public BallControl gameBall;

	BoxCollider2D topWallCol;
	BoxCollider2D bottomWallCol;
	BoxCollider2D leftWallCol;
	BoxCollider2D rightWallCol;

	public GameObject blueBrickRef;
	public GameObject redBrickRef;
	public GameObject[,] brickArray;

	public int redRowsStart = 1;
    public int redRowsEnd = 1;
	public int blueRows = 3;
	public int columns = 8;

	public Transform[] players;

	float spriteSize = 0;

	// Use this for initialization
	void Start () {
        BallControl.gameSetup = this;
		if (topWall.GetComponent<SpriteRenderer> ()) { // do we have a sprite?
			// get its width and store it 
			spriteSize = topWall.GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
		}
		else {
			// we don't have a sprite, so we get references to the colliders directly.
			topWallCol = topWall.GetComponent<BoxCollider2D> ();
			bottomWallCol = bottomWall.GetComponent<BoxCollider2D> ();
			leftWallCol = leftWall.GetComponent<BoxCollider2D> ();
			rightWallCol = rightWall.GetComponent<BoxCollider2D> ();
		}

        InstantiateBricks();
	}

    public void InstantiateBricks() {
		brickArray = new GameObject[columns, redRowsStart + blueRows + redRowsEnd];
		for (int i = 0; i < columns; i++) {
			for (int j = 0; j < redRowsStart + blueRows + redRowsEnd; j++) {
				brickArray [i, j] = Instantiate ((j < blueRows && j > blueRows) ? redBrickRef : blueBrickRef) as GameObject;
				brickArray [i, j].transform.position = new Vector3 (i - columns / 2, (j - redRowsStart + blueRows + redRowsEnd / 2) + brickYOffset, 0f);
				brickArray [i, j].GetComponent<Brick>().SetInfo((j < blueRows && j > blueRows) ? Brick.BrickType.RED : Brick.BrickType.BLUE);
			}
		}
    }
    
    public void PlayGame() {
        if (playerOneManual != null && playerOneManual.isOn) {
            playerOne.playerType = PlayerType.Manual;
            playerOne.manualControlType = ManualControlType.ArrowKeys;
        }
        else if (playerOneLinearFollow != null && playerOneLinearFollow.isOn) {
            playerOne.playerType = PlayerType.Automated;
            playerOne.automatedControlType = AutomatedControlType.LinearFollow;
        }
        else if (playerOneFastLinearFollow != null && playerOneFastLinearFollow.isOn) {
            playerOne.playerType = PlayerType.Automated;
            playerOne.automatedControlType = AutomatedControlType.FastLinearFollow;
        }
        else if (playerOnePhysicsForecast != null && playerOnePhysicsForecast.isOn) {
            playerOne.playerType = PlayerType.Automated;
            playerOne.automatedControlType = AutomatedControlType.PhysicsForecast;
        }
        playerOne.Initialize();

        if (playerTwoManual != null && playerTwoManual.isOn) {
            playerTwo.playerType = PlayerType.Manual;
            playerTwo.manualControlType = ManualControlType.ArrowKeys;
        }
        else if (playerTwoLinearFollow != null && playerTwoLinearFollow.isOn) {
            playerTwo.playerType = PlayerType.Automated;
            playerTwo.automatedControlType = AutomatedControlType.LinearFollow;
        }
        else if (playerTwoFastLinearFollow != null && playerTwoFastLinearFollow.isOn) {
            playerTwo.playerType = PlayerType.Automated;
            playerTwo.automatedControlType = AutomatedControlType.FastLinearFollow;
        }
        else if (playerTwoPhysicsForecast != null && playerTwoPhysicsForecast.isOn) {
            playerTwo.playerType = PlayerType.Automated;
            playerTwo.automatedControlType = AutomatedControlType.PhysicsForecast;
        }
        playerTwo.Initialize();
        
        gameStartPanel.SetActive(false);
        headsUpDisplayPanel.SetActive(true);
        gameBall.InitialSetup();
    }
	
	// Update is called once per frame
	void Update () {
		if (spriteSize > 0) { // we have a sprite, we use it to size and place the walls
			//Assuming that the sprite is square, find its size on screen
			float spriteScale = mainCam.WorldToScreenPoint (new Vector3 (spriteSize, 0f, 0f)).x - 
								mainCam.WorldToScreenPoint (new Vector3 (0, 0f, 0f)).x;

			// move each wall to its edge location.
			topWall.transform.localScale = new Vector3 (Screen.width / spriteScale, 0.5f, 1f);
			topWall.transform.position = new Vector3 (0f, mainCam.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y, 0f);

			bottomWall.transform.localScale = new Vector3 (Screen.width / spriteScale, 0.5f, 1f);
			bottomWall.transform.position = new Vector3 (0f, mainCam.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).y, 0f);
		
			leftWall.transform.localScale = new Vector3 (0.5f, Screen.height / spriteScale, 1f);
			leftWall.transform.position = new Vector3 (mainCam.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).x, 0f, 0f);
		
			rightWall.transform.localScale = new Vector3 (0.5f, Screen.height / spriteScale, 1f);
			rightWall.transform.position = new Vector3 (mainCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x, 0f, 0f);
		}
        else { 
			// no sprite, size and place the colliders themselves
			topWallCol.size = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(Screen.width *2f, 0f, 0f)).x, 1f);
			topWallCol.offset = new Vector2 (0f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y+0.5f);

			bottomWallCol.size = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(Screen.width *2f, 0f, 0f)).x, 1f);
			bottomWallCol.offset = new Vector2 (0f, mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y-0.5f);

			leftWallCol.size = new Vector2 (1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height *2f, 0f)).y);
			leftWallCol.offset = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x-0.5f, 0f);

			rightWallCol.size = new Vector2 (1f, mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height *2f, 0f)).y);
			rightWallCol.offset = new Vector2 (mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x+0.5f, 0f);
		}
		// now place the players based on screen size
		if (players.Length == 1) { // one player, it must be horizontal
			players [0].position = new Vector3 (players [0].position.x, 
			                                   mainCam.ScreenToWorldPoint (new Vector3 (0f, 20f, 0f)).y, 
			                                   0f);
		}
        else if (players.Length > 1) {
			players [0].position = new Vector3 (players [0].position.x, 
			                                   mainCam.ScreenToWorldPoint (new Vector3 (0f, 20f, 0f)).y, 
			                                   0f);
			players [1].position = new Vector3 (players [1].position.x, 
			                                   mainCam.ScreenToWorldPoint (new Vector3 (0f,  Screen.height - 20f, 0f)).y, 
			                                   0f);
		} 
	}

	public bool IsLevelOver() {
		bool result = true;
		for (int i = 0; i < columns; i++) {
			for (int j = 0; j < redRowsStart + blueRows + redRowsEnd; j++) {
				Brick brickData = brickArray[i, j].GetComponent<Brick>();
				if (!brickData.GetDestroyed()) {
					result = false;
					j = redRowsStart + blueRows + redRowsEnd;
					i = columns;
				}
			}
		}
		return result;
	}
    
    //called from Play Again? button
    public void ReloadGame() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
