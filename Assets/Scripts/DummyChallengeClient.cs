using System;
using System.Collections.Generic;
/// <summary>
/// Class that returns dummy challenge data for a lesson
/// </summary>
public class DummyChallengeClient : IChallengeClient
{
	private List<Challenge> m_challenges;

	public DummyChallengeClient() {
		m_challenges = new List<Challenge> ();
		m_challenges.Add(new Challenge( new string[] {"он", "она", "оно", "они"}, 0, "стол", 1));
		m_challenges.Add(new Challenge( new string[] {"он", "она", "оно", "они"}, 0, "малчик", 1));
		m_challenges.Add(new Challenge( new string[] {"он", "она", "оно", "они"}, 0, "человек", 1));
		m_challenges.Add(new Challenge( new string[] {"он", "она", "оно", "они"}, 2, "время", 1));
	}

	public Challenge GetQuestion(int level, int index) {
		return m_challenges[index];
	}

	public int GetCountQuestionsForLevel(int level) {
		return m_challenges.Count;
	}
}

