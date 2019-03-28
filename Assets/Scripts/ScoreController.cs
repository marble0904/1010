using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text ScoreText;
    static int score = 0;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int point)
    {
        score += point;
        ScoreText.text = "SCORE:" + score;
    }

    public static int GetScore()
    {
        return score;
    }
}
