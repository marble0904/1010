using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontController : MonoBehaviour {

    Font font1;//ビットフォント
    Font font2;//通常フォント
    Text[,] numberText = new Text[4, 4];
    public Text nowNumberText;
    public Text nextNumberText;
    public Text nowText;
    public Text nextText;
    public Text timeText;
    public Text scoreText;

    // Use this for initialization
    void Start () {
        font1 = Resources.Load<Font>("Fonts/FINGFA");
        font2 = Resources.Load<Font>("Fonts/ALTERNATE");

        //テキストを配列に入れる
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                numberText[i, j] = GameObject.Find("/Canvas/r" + i + "/c" + j).GetComponent<Text>();
            }
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetFont(bool bit)
    {
        Font font;
        if (bit)
        {
            font = font1;
            nowText.fontSize = 20;
            nextText.fontSize = 20;
        }
        else
        {
            font = font2;
            nowText.fontSize = 50;
            nextText.fontSize = 50;
        }

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                numberText[i, j].font = font;
            }
        }
        nowNumberText.font = font;

        nextNumberText.font = font;
        nowText.font = font;
        nextText.font = font;
        timeText.font = font;
        scoreText.font = font;

    }
}
