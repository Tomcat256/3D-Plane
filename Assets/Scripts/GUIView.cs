using UnityEngine;
using System.Collections;

public class GUIView : MonoBehaviour {
	public UnityEngine.UI.Text TimeBox;
	public UnityEngine.UI.Text ScoreBox;
	public UnityEngine.UI.Text MissilesLeftBox;
	public UnityEngine.UI.Text FinalScoreBox;
	public GameObject LevelCompleteBox;
	public GameObject GameOverBox;
	// Use this for initialization
	void Start () {
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset(){
		SetScore (0);
		SetMissilesLeft (0);
		SetSecondsLeft (0);
		GameOverBox.SetActive (false);
		LevelCompleteBox.SetActive (false);
	}

	void SetScore(uint newScore)
	{
		ScoreBox.text = newScore.ToString();
	}

	void SetMissilesLeft(uint missiles)
	{
		MissilesLeftBox.text = missiles.ToString();
	}

	void SetSecondsLeft(uint secondsLeft)
	{
		string minutes = (secondsLeft / 60).ToString();
		string seconds = (secondsLeft % 60).ToString();

		if (seconds.Length == 1)
			seconds = "0" + seconds;

		TimeBox.text = minutes + ":" + seconds;
	}

	void ShowGameOver()
	{
		GameOverBox.SetActive(true);
	}

	void ShowLevelComplete(LevelStatistics stats)
	{
		LevelCompleteBox.SetActive (true);
		FinalScoreBox.text = stats.ToString();
	}
}
