using System;

public class Challenge
{
	public String[] AnswerOptions { get; private set; }

	public int AnswerIndex { get; private set; }

	public String Question { get; private set; }

	public int Level {get; private set; }

	public Challenge (String[] answerOptions,
		                 int answerIndex,
		                 String question,
	                  	 int level)
	{
		this.AnswerOptions = answerOptions;
		this.AnswerIndex = answerIndex;
		this.Question = question;
		this.Level = level;
	}
}
