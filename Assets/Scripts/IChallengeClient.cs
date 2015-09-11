using System;
using System.Collections.Generic;
public interface IChallengeClient
{
	Challenge GetQuestion (int level, int index);

	int GetCountQuestionsForLevel(int level);
}
