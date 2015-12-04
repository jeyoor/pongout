using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public KeyCode moveUp, moveDown, moveLeft, moveRight;

	public float speedX=0, speedY=0;
	
	// Update is called once per frame
	void Update () {
		// move the player in the 4 directions based on the keys we set up for it
		if (Input.GetKey (moveUp)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, speedY);
			// if we were going to use a force instead
			//rigidbody2D.AddForce(new Vector2( 0, -rigidbody2D.velocity.y));
			//rigidbody2D.AddForce(new Vector2(0, speed));
		} else if (Input.GetKey (moveDown)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -speedY);
			//rigidbody2D.AddForce(new Vector2(0, -rigidbody2D.velocity.y));
			//rigidbody2D.AddForce(new Vector2(0, -speed));
		} else if (Input.GetKey (moveLeft)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX, 0f);
			//rigidbody2D.AddForce(new Vector2( 0, -rigidbody2D.velocity.y));
			//rigidbody2D.AddForce(new Vector2(0, speed));
		} else if (Input.GetKey (moveRight)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, 0f);
			//rigidbody2D.AddForce(new Vector2(0, -rigidbody2D.velocity.y));
			//rigidbody2D.AddForce(new Vector2(0, -speed));
		} else {
			GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
			//rigidbody2D.AddForce(new Vector2(0, -rigidbody2D.velocity.y));
		}
	}
}
