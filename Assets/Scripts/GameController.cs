using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public float span = 2f;
    float delta = 0;
    bool wait = true;
    public NumberController nc;
    public EffectController ec;
    public SoundController sc;

    public float time;
    public Text timeText;
    bool bitMode;

    int zeroCount = 0;
    float waitTime;

    static bool scoreAttack = true;//スコアアタックかどうか


	// Use this for initialization
	void Start () {
        if (scoreAttack)
        {
            time = 60f;
        }
        else
        {
            time = 0;
            timeText.text = "TIME:" + time.ToString("--");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }

        waitTime = span / 2.0f;

        if (scoreAttack)
        {
            if (!bitMode)
            {
                time -= Time.deltaTime;
                timeText.text = "TIME:" + time.ToString("0.0");
                if (time <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }


        delta += Time.deltaTime;
        if(delta >= span)//一定時間たつと
        {
            wait = false;//待ちを解除
            delta = 0;
        }

        if (!wait) {
            if (Input.GetKeyDown(KeyCode.A))
            {
                nc.InputKey("A",waitTime);//計算
                ec.LineEffect(0,false,waitTime);//ラインエフェクトを出す
                wait = true;//待ち状態に
            }else if (Input.GetKeyDown(KeyCode.S))
            {
                nc.InputKey("S",waitTime);
                ec.LineEffect(1, false,waitTime);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.D))
            {
                nc.InputKey("D",waitTime);
                ec.LineEffect(2, false,waitTime);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.F))
            {
                nc.InputKey("F",waitTime);
                ec.LineEffect(3, false,waitTime);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.J))
            {
                nc.InputKey("J",waitTime);
                ec.LineEffect(0, true,waitTime);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.K))
            {
                nc.InputKey("K",waitTime);
                ec.LineEffect(1, true,waitTime);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.L))
            {
                nc.InputKey("L",waitTime);
                ec.LineEffect(2, true,waitTime);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.Equals))
            {
                //Debug.Log("GetKeyDown.;");
                nc.InputKey(";",waitTime);
                ec.LineEffect(3, true,waitTime);
                wait = true;
            }
        }
	}

    public void SetBitMode(bool start)
    {
        if (start)
        {
            if (scoreAttack)
            {
                time += 20.0f;
                timeText.text = "TIME:" + time.ToString("0.0");
            }
            bitMode = true;
            sc.SetBGM1();
        }
        else
        {
            bitMode = false;
            sc.SetBGM0();
        }
    }

    public void ZeroCount()
    {
        zeroCount++;
        if (zeroCount == 6)
        {
            SetSpan(0.5f);
        }
        else if (zeroCount == 14)
        {
            SetSpan(0.3f);
        }
    }

    public void SetSpan(float span)
    {
        this.span = span;
    }

    public void ResetZeroCount()
    {
        zeroCount = 0;
        SetSpan(1.0f);
    }

    public float GetSpan()
    {
        return span;
    }


    public static void SetGameMode(bool gameMode)
    {
        scoreAttack = gameMode;
    }
}
