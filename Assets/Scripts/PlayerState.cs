using UnityEngine;
using System;
public static class PlayerState {
	public static int CurrentScore;
	public static int CurrentLevel;

	private static string HIGH_SCORE_KEY = "HIGH_SCORE";
	private static string STREAK_KEY = "STREAK";
	private static string LASTPLAYED_KEY = "LAST_PLAYED";
	private static string HIGH_STREAK_KEY = "HIGH_STREAK";

	public static int GetHighScore() {
		return PlayerPrefs.GetInt (HIGH_SCORE_KEY);
	}

	public static void SetHighScore(int value) {
		PlayerPrefs.SetInt (HIGH_SCORE_KEY, value);
	}

	public static int GetHighestStreak() {
		return PlayerPrefs.GetInt (HIGH_STREAK_KEY);
	}

	public static void SetHighestStreak(int value) {
		PlayerPrefs.SetInt(HIGH_STREAK_KEY, value);
	}

	public static int GetStreak() {

		return PlayerPrefs.GetInt(STREAK_KEY);
	}

	public static void SetStreak(int value) {
		PlayerPrefs.SetInt(STREAK_KEY, value);
	}

	public static void UpdateHighScore() {
		int highScore = GetHighScore ();
		if (CurrentScore > highScore) {
			SetHighScore (CurrentScore);
		}
	}

	public static void IncrementScore() {
		PlayerState.CurrentScore += 10;
	}
	
	public static void UpdateStreak() {
		if (CurrentScore > 50) {
			DateTime now = DateTime.Now;
			DateTime epoch = DateTime.FromBinary (0);
			string lastUpdateStr = PlayerPrefs.GetString (LASTPLAYED_KEY, defaultValue: epoch.ToString());
			DateTime lastUpdate = DateTime.Parse (lastUpdateStr);

			int dayDelta = now.DayOfYear - lastUpdate.DayOfYear;
			int currentStreak = GetStreak();
			if( dayDelta == 1) {
				currentStreak++;
			} else if (dayDelta > 1) {
				currentStreak = 0;
			}

			SetStreak(currentStreak);
			PlayerPrefs.SetString(LASTPLAYED_KEY, now.ToString());

			int highStreak = GetHighestStreak();
			if(currentStreak > highStreak) {
				SetHighestStreak(currentStreak);
			}

		}
	}

}
