using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour {
	public GameObject m_questionPanel;
	public GameObject[] m_answerButtons = new GameObject[4];
	public GameObject m_feedbackTint;
	public GameObject m_feedbackPanel;
	public GameObject m_scoreText;

	private IChallengeClient m_challengeClient;
	private LevelManager m_levelManager;

	private int m_challengeIndex;
	private int m_challengeLevel;
	private int m_currentSelectedIndex;

	// Use this for initialization
	void Start () {
//		m_challengeClient = new DummyChallengeClient ();
		m_challengeClient = new CsvChallengeClient ();
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

	private void incrementScore() {
		PlayerState.CurrentScore += 10;
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
			incrementScore();
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
		foreach (Text text in m_feedbackPanel.GetComponentsInChildren<Text>()) {
			if (text.name == "FeedbackTitle") {
				text.text = "правилно!";
			} else if (text.name == "FeedbackSubtitle") {
				text.text = "";
			}
		}
	}
	
	private void ShowIncorrectFeedback() {
		SetFeedbackPanelVisibility (true);
		foreach (Text text in m_feedbackPanel.GetComponentsInChildren<Text>()) {
			if (text.name == "FeedbackTitle") {
				text.text = "ошибка";
			} else if (text.name == "FeedbackSubtitle" ) {
				Challenge currentChallenge = m_challengeClient.GetQuestion(m_challengeLevel, m_challengeIndex);
				text.text = currentChallenge.Question + ": " + currentChallenge.AnswerOptions[currentChallenge.AnswerIndex];
			}
		}
	}
	
	public void OnFeedbacNextSelected() {

		Challenge currentChallenge = m_challengeClient.GetQuestion(m_challengeLevel, m_challengeIndex);
		if (currentChallenge.AnswerIndex == m_currentSelectedIndex) {
			m_challengeIndex++;
			if (m_challengeIndex > m_challengeClient.GetCountQuestionsForLevel (m_challengeLevel)) {
				m_levelManager.LoadLevel (Level.Win);
			} else {
				updateUI ();
				SetFeedbackPanelVisibility (false);
			}
		} else {
			m_levelManager.LoadLevel (Level.Lose);
		}
	}
}
