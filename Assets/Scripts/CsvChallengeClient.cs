using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class CsvChallengeClient : IChallengeClient {
	/// <summary>
	/// Location, relative to game data folder, of data file
	/// </summary>
	const string CSV_DATA_LOCATION = "/вопроси-в1.csv";

	Dictionary<int,List<Challenge>> m_levels;

	public CsvChallengeClient() {
		m_levels = new Dictionary<int, List<Challenge>> ();
		// parse the questions
		string[] lines = File.ReadAllLines (Application.dataPath + CSV_DATA_LOCATION);
		for (int i = 1; i < lines.Length; i++) {
			string line = lines[i];
			line = line.Trim();
			Challenge challengeForLine = parseChallengeFromLine(line);
			if (!m_levels.ContainsKey(challengeForLine.Level)) {
				m_levels[challengeForLine.Level] = new List<Challenge>();
			}
			m_levels[challengeForLine.Level].Add(challengeForLine);
		}
		shuffleQuestionsInLevel ();

	}

	private void shuffleQuestionsInLevel() {
		foreach (int key in m_levels.Keys) {
			m_levels[key].Shuffle();
		}
	}

	private Challenge parseChallengeFromLine(string line) {
		string[] tokens = line.Split (new char[]{','});
		string word = tokens[0];
		int answerIndex = int.Parse(tokens[1]);
		int level = int.Parse (tokens[2]);
		string[] options = tokens[3].Split(new char[]{' '});
		return new Challenge(options, answerIndex, word, level);
	}

	public Challenge GetQuestion (int level, int index) {
		return m_levels [level] [index];
	}
	
	public int GetCountQuestionsForLevel(int level) {
		return m_levels [level].Count;
	}
}
