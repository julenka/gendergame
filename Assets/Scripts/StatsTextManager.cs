using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsTextManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text text = GetComponent<Text> ();
		text.text = "Best score: " + PlayerState.GetHighScore () 
			+ "\nBest streak: " + PlayerState.GetHighestStreak() 
				+ "\nCurrent streak: " + PlayerState.GetStreak ();
	}
	
}
