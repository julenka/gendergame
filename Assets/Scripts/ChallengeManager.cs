using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour {
	public GameObject m_questionPanel;
	public GameObject[] m_answerButtons = new GameObject[4];
	public GameObject m_feedbackTint;
	public GameObject m_feedbackPanel;
	public GameObject m_scoreText;

	public GameObject m_feedbackSubtitle;
	public GameObject m_feedbackImageCorrect;
	public GameObject m_feedbackImageWrong;

	public TextAsset m_questionText;

	private IChallengeClient m_challengeClient;
	private LevelManager m_levelManager;

	const int MAX_LEVEL_INDEX = 2;
	private int m_challengeIndex;
	private int m_challengeLevel;
	private int m_currentSelectedIndex;

	// Use this for initialization
	void Start () {
		m_challengeClient = new TextChallengeClient (m_questionText);
		m_challengeLevel = 1;	
		m_challengeIndex = 0;
		m_levelManager = FindObjectOfType<LevelManager> ();
		updateUI ();
	}


	
	// Update is called once per frame
	void Update () {
	
	}

	private void updateUI() {
		Challenge currentChallenge = m_challengeClient.GetQuestion(m_challengeLevel, m_challengeIndex);
		setQuestionText (currentChallenge.Question);
		setQuestionOptions (currentChallenge.AnswerOptions);

		m_scoreText.GetComponent<Text> ().text = "score: " + PlayerState.CurrentScore;
	}



	private void setQuestionText(string text) {
		m_questionPanel.GetComponentInChildren<Text> ().text = text;
	}

	private void setQuestionOptions(string[] options) {
		int i = 0;
		foreach (GameObject button in m_answerButtons) {
			button.GetComponentInChildren<Text>().text = options[i];
			i++;
		}
	}



	public void OnAnswerSelected(int answerIndex) {
		m_currentSelectedIndex = answerIndex;
		Challenge currentChallenge = m_challengeClient.GetQuestion(m_challengeLevel, m_challengeIndex);
		if (currentChallenge.AnswerIndex == answerIndex) {
			PlayerState.IncrementScore();
			updateUI ();
			ShowCorrectFeedback();
		} else {
			ShowIncorrectFeedback();
		}
	}

	private void SetFeedbackPanelVisibility(bool visible) {
		m_feedbackTint.SetActive (visible);
		m_feedbackPanel.SetActive (visible);
	}

	private void ShowCorrectFeedback() {
		SetFeedbackPanelVisibility (true);
		m_feedbackSubtitle.GetComponent<Text> ().text = "";
		m_feedbackImageCorrect.SetActive (true);
		m_feedbackImageWrong.SetActive (false);
	}
	
	private void ShowIncorrectFeedback() {
		SetFeedbackPanelVisibility (true);
		m_feedbackImageCorrect.SetActive (false);
		m_feedbackImageWrong.SetActive (true);

		Challenge currentChallenge = m_challengeClient.GetQuestion(m_challengeLevel, m_challengeIndex);
		m_feedbackSubtitle.GetComponent<Text> ().text = currentChallenge.Question + ": " + currentChallenge.AnswerOptions[currentChallenge.AnswerIndex];
	}
	
	public void OnFeedbacNextSelected() {

		Challenge currentChallenge = m_challengeClient.GetQuestion(m_challengeLevel, m_challengeIndex);
		if (currentChallenge.AnswerIndex == m_currentSelectedIndex) {
			m_challengeIndex++;
			if (m_challengeIndex > m_challengeClient.GetCountQuestionsForLevel (m_challengeLevel)) {
				IncreaseLevel();
			} else {
				updateUI ();
				SetFeedbackPanelVisibility (false);
			}
		} else {
			m_levelManager.LoadLevel (Level.Lose);
		}
	}

	private void IncreaseLevel() {
		// TODO: Consider using a different scene for a different level
		m_challengeLevel++;
		m_challengeIndex = 0;
		if (m_challengeIndex > MAX_LEVEL_INDEX) {
			m_levelManager.LoadLevel (Level.Win);
		} else {
			updateUI ();
			SetFeedbackPanelVisibility (false);
		}

	}
}
