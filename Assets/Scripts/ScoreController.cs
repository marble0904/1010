using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text ScoreText;
    static int score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int point)
    {
        score += point;
        ScoreText.text = "SCORE:" + score;
    }

    public int GetScore()
    {
        return score;
    }
}
