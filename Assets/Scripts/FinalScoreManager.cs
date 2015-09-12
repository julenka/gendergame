using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FinalScoreManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text finalScoreText = GetComponent<Text> ();
		finalScoreText.text = "final score: " + PlayerState.CurrentScore;
		PlayerState.UpdateHighScore ();
		PlayerState.UpdateStreak (); 
	}
}
