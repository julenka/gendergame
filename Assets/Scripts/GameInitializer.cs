using UnityEngine;
using System.Collections;

public class GameInitializer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		PlayerState.CurrentLevel = 1;
		PlayerState.CurrentScore = 0;
	}

}
