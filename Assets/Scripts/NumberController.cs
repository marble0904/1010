using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberController : MonoBehaviour {

    int[,] intValues = new int[4, 4];
    Text[,] numberText = new Text[4, 4];
    int nowNumber;
    int nextNumber;
    public Text nowNumberText;
    public Text nextNumberText;

    public ScoreController sc;
    public EffectController ec;

	// Use this for initialization
	void Start () {
        //nowNumberText = GameObject.Find("/Canvas/now").GetComponent<Text>();
        //nextNumberText = GameObject.Find("/Canvas/next").GetComponent<Text>();
        nowNumber = Random.Range(1, 10);
        nextNumber = Random.Range(1, 10);
        nowNumberText.text = nowNumber.ToString();
        nextNumberText.text = nextNumber.ToString();
        

        //テキストを配列に入れる
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                numberText[i,j] = GameObject.Find("/Canvas/r"+i+"/c"+j).GetComponent<Text>();
            }
        }


        //ランダムに初期値を代入
		for(int i=0; i<4; i++)//行
        {
            for(int j = 0; j < 4; j++)//列
            {
                intValues[i, j] = Random.Range(0, 10);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                numberText[i, j].text = intValues[i, j].ToString();
            }
        }
	}

    public void InputKey(string key)
    {
        switch (key)//キーに対応した計算を行う
        {
            case "A":
                Calculate(0,false);
                break;
            case "S":
                Calculate(1, false);
                break;
            case "D":
                Calculate(2, false);
                break;
            case "F":
                Calculate(3, false);
                break;
            case "J":
                Calculate(0, true);
                break;
            case "K":
                Calculate(1, true);
                break;
            case "L":
                Calculate(2, true);
                break;
            case ";":
                //Debug.Log("InputKey.;");
                Calculate(3, true);
                break;
            default:
                break;
        }
        //現在の数を更新
        nowNumber = nextNumber;
        nowNumberText.text = nowNumber.ToString();
        nextNumber = Random.Range(1, 10);
        nextNumberText.text = nextNumber.ToString();
    }

    //対応した行または列に値を足す
    //num:行または列の番号
    //row:行であればtrue
    void Calculate(int num,bool row)
    {
        if (row)
        {
            for (int i = 0; i < 4; i++)
            {
                intValues[num, i] = (intValues[num,i] + nowNumber)%10;//現在と足す値の合計を10で割った値
                if(intValues[num,i] == 0)//0になったらスコアを加算
                {
                    sc.AddScore(100);
                    ec.ZeroEffect(num, i);
                    //ゼロの連鎖
                    Chain(num, i,true);
                    ec.ZeroChain(i, false);
                }

                if ((i == 0 || i == 2)&& intValues[num, i] == 1)//c0が1のとき
                {
                    if (BitCheck(num))
                    {
                        Debug.Log("BitMode");
                    }
                    else
                    {
                        Debug.Log("NotBitMode");
                    }
                }else if ((i == 1 || i ==3) && intValues[num, i] == 0)
                {
                    if (BitCheck(num))
                    {
                        Debug.Log("BitMode");
                    }
                    else
                    {
                        Debug.Log("NotBitMode");
                    }
                }

                numberText[num, i].text = intValues[num, i].ToString();//テキストを更新

            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                intValues[i, num] = (intValues[i, num] + nowNumber) % 10;//現在と足す値の合計を10で割った値

                if (intValues[i, num] == 0)//0になったらスコアを加算
                {
                    sc.AddScore(100);
                    ec.ZeroEffect(i,num);
                    Chain(i, num, false);
                    ec.ZeroChain(i,true);
                }

                if((num == 0 || num == 2)&& intValues[i,num] == 1)//c0が1のとき
                {
                    if (BitCheck(i))
                    {
                        Debug.Log("BitMode");
                    }
                    else
                    {
                        Debug.Log("NotBitMode");
                    }
                }else if ((num == 1 || num ==3) && intValues[i,num] == 0)
                {
                    if (BitCheck(i))
                    {
                        Debug.Log("BitMode");
                    }
                    else
                    {
                        Debug.Log("NotBitMode");
                    }
                }

                numberText[i, num].text = intValues[i,num].ToString();//テキストを更新

            }
        }

    }

    bool BitCheck(int r)
    {
        if (intValues[r,1] == 0)
        {
            if(intValues[r,2] == 1)
            {
                if(intValues[r,3] == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void Chain(int r,int c,bool row)//列で連鎖
    {
        if (row) {
            for (int i = 0; i < 4; i++)
            {
                if (i != r)
                {
                    intValues[i, c] = (intValues[i, c] + 1) % 10;
                    if (intValues[i, c] == 0)
                    {
                        ec.ZeroEffect(i, c);
                        sc.AddScore(100);
                    }
                }
            }


        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != c)
                {
                    intValues[r, i] = (intValues[r, i] + 1) % 10;
                    if (intValues[r, i] == 0)
                    {
                        ec.ZeroEffect(r, i);
                        sc.AddScore(100);
                    }
                }
            }
        }

    }
}
