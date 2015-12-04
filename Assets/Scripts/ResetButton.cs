using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
	//Reset the game when this button is clicked
	void OnMouseUpAsButton() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
