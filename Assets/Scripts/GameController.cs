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

    public float time;
    public Text timeText;
    bool bitMode;

    int zeroCount = 0;

	// Use this for initialization
	void Start () {
        time = 60f;
	}
	
	// Update is called once per frame
	void Update () {

        if (!bitMode)
        {
            time -= Time.deltaTime;
            timeText.text = "TIME:" + time.ToString("0.0");
            if(time <= 0)
            {
                SceneManager.LoadScene("GameOver");
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
                nc.InputKey("A");//計算
                ec.LineEffect(0,false);//ラインエフェクトを出す
                wait = true;//待ち状態に
            }else if (Input.GetKeyDown(KeyCode.S))
            {
                nc.InputKey("S");
                ec.LineEffect(1, false);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.D))
            {
                nc.InputKey("D");
                ec.LineEffect(2, false);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.F))
            {
                nc.InputKey("F");
                ec.LineEffect(3, false);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.J))
            {
                nc.InputKey("J");
                ec.LineEffect(0, true);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.K))
            {
                nc.InputKey("K");
                ec.LineEffect(1, true);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.L))
            {
                nc.InputKey("L");
                ec.LineEffect(2, true);
                wait = true;
            }else if (Input.GetKeyDown(KeyCode.Equals))
            {
                //Debug.Log("GetKeyDown.;");
                nc.InputKey(";");
                ec.LineEffect(3, true);
                wait = true;
            }
        }
	}

    public void SetBitMode(bool start)
    {
        if (start)
        {
            bitMode = true;
        }
        else
        {
            bitMode = false;
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
}
