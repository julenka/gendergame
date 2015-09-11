using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	private StartOptions m_startOptions;
	private GameObject m_GameUI;
	private int m_levelToLoad;

	// Use this for initialization
	void Start () {
		m_GameUI = GameObject.Find ("UI");
		m_startOptions = m_GameUI.GetComponent<StartOptions> ();
	}
	
	public void LoadLevelWithFade(Level level) {
		m_levelToLoad = (int)level;
		Invoke ("LoadLevelDelayed", m_startOptions.fadeColorAnimationClip.length * 0.5f);
		m_startOptions.animColorFade.SetTrigger ("fade");
	}

	public void LoadLevel(Level level) {
		m_levelToLoad = (int)level;
		LoadLevelDelayed ();
	}

	public void PlayAgain() {
		LoadLevelWithFade (Level.Level1);
	}

	private void LoadLevelDelayed() {
		Application.LoadLevel (m_levelToLoad);
	}
}
