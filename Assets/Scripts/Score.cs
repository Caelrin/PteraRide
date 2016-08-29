using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

  private long currentScore = 0;
  private Text textObj;

	// Use this for initialization
	void Start () {
	  textObj = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	  textObj.text = "Score: " + currentScore.ToString("D4");

	}

  public void addPoints(long additionalScore) {
    currentScore += additionalScore;
  }
}
