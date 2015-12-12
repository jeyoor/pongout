using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public enum BrickType { BLUE, RED }
	private BrickType brickType;
	private bool isDestroyed = false;
	// Use this for initialization
	void Start () {
	}

	public void SetDestroyed(bool destroyed) {
		isDestroyed = destroyed;
	}

	public bool GetDestroyed() {
		return isDestroyed;
	}

	public void SetInfo(BrickType brickType) {
		this.brickType = brickType;
	}


	public BrickType GetBrickType() {
		return brickType;
	}
}