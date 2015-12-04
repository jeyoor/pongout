using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	public enum BrickType { BLUE, RED }
	private BrickType brickType;
	private bool isDestroyed = false;
	private int column;
	private int row;
	// Use this for initialization
	void Start () {
	}

	public void SetDestroyed(bool destroyed) {
		isDestroyed = destroyed;
	}

	public bool GetDestroyed() {
		return isDestroyed;
	}

	public void SetId(int column, int row, BrickType brickType) {
		this.column = column;
		this.row = row;
		this.brickType = brickType;
	}


	public BrickType GetBrickType() {
		return brickType;
	}
}