using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (Application.loadedLevelName);
	}

	//Reset the game when this button is clicked
	void OnMouseUpAsButton() {
		//Debug.Log ("Clicked reset button ya'll");
		Application.LoadLevel(Application.loadedLevel);
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
}
