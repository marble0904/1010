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

    bool[,] zeroFlag = new bool[4, 4];//ゼロになったか

    public ScoreController sc;
    public EffectController ec;
    public GameController gc;

    bool bitMode = false;

    public Text bitTimeText;
    float bitTime = 0;

	// Use this for initialization
	void Start () {
        nowNumber = Random.Range(1, 10);
        nextNumber = Random.Range(1, 10);
        nowNumberText.text = nowNumber.ToString();
        nextNumberText.text = nextNumber.ToString();

        bitTimeText.text = "";

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
                intValues[i, j] = Random.Range(1, 10);//1~9の値を入れる
                numberText[i, j].text = intValues[i, j].ToString();
            }
        }

        //フラグを初期化
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                zeroFlag[i, j] = false;
            }
        }


    }
	
	// Update is called once per frame
	void Update () {

        if (bitMode)
        {
            if(bitTime <= 0)
            {
                ResetBitMode();
            }
            else
            {
                bitTime -= Time.deltaTime;
                bitTimeText.text = "BITTIME:" + bitTime.ToString("0.0");
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("number=["+ intValues[0,0] + intValues[0,1] + intValues[0,2] + intValues[0,3]+
                      ";" + intValues[1,0] + intValues[1,1] + intValues[1,2] + intValues[1,3]+
                      ";" + intValues[2,0] + intValues[2,1] + intValues[2,2] + intValues[2,3]+
                      ";" + intValues[3,0] + intValues[3,1] + intValues[3,2] + intValues[3,3] + "]");
            Debug.Log("flag =[" + zeroFlag[0,0] + zeroFlag[0,1] + zeroFlag[0,2] + zeroFlag[0,3] +
                       ";" +zeroFlag[1, 0] + zeroFlag[1, 1] + zeroFlag[1, 2] + zeroFlag[1, 3] +
                       ";" + zeroFlag[2, 0] + zeroFlag[2, 1] + zeroFlag[2, 2] + zeroFlag[2, 3] +
                       ";" + zeroFlag[3, 0] + zeroFlag[3, 1] + zeroFlag[3, 2] + zeroFlag[3, 3] + "]");
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
                Calculate(3, true);
                break;
            default:
                break;
        }
        //現在の数を更新
        if (!bitMode)
        {
            nowNumber = nextNumber;
            nextNumber = Random.Range(1, 10);
        }else
        {
            nowNumber = 1;
            nextNumber = 1;
        }
        nowNumberText.text = nowNumber.ToString();
        nextNumberText.text = nextNumber.ToString();
    }

    //対応した行または列に値を足す
    //num:行または列の番号
    //row:行であればtrue
    void Calculate(int num,bool row)
    {
        if (!bitMode)
        {
            if (row)//行のとき
            {

                for (int i = 0; i < 4; i++)
                {
                    if (!zeroFlag[num, i])//0ではないマスの場合
                    {
                        SetValue(num, i, false);
                        if(intValues[num,i] == 0)
                        {
                            Chain(num, i, true);
                        }
                    }
                }
            }
            else//列のとき
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!zeroFlag[i, num])//ゼロではない場合
                    {
                        SetValue(i, num, false);//マスの値を計算
                        if(intValues[i,num] == 0)
                        {
                            Chain(i, num, false);//連鎖処理
                        }
                    }

                }
            }
        }
        else//ビットモードのとき
        {
            if (row)//行のとき
            {
                for(int i=0;i<4;i++)
                {
                    if(!zeroFlag[num,i])//0ではないとき
                    {
                        SetValue(num, i, true);
                        Chain(num, i, true);
                    }
                }

            }
            else//列のとき
            {
                for(int i = 0; i < 4; i++)
                {
                    if (!zeroFlag[i, num])//0ではないとき
                    {
                        SetValue(i,num,true);
                        Chain(i, num, false);
                    }
                }
            }
        }

    }

    bool BitCheck()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (!zeroFlag[i,j])
                {
                    return false;
                }
            }
        }
        if (bitMode)
        {
            bitTime+= 1.0f;
        }
        ec.BitEffect();
        return true;
    }

    void Chain(int r,int c,bool row)//列で連鎖
    {
        if (row)//連鎖エフェクト
        {
            ec.ZeroChain(c,false);
        }
        else
        {
            ec.ZeroChain(r, true);
        }

        if (!bitMode)//通常モードのとき
        {
            if (row)//行のとき
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != r)
                    {
                        if (!zeroFlag[i, c])//ゼロではない場合
                        {
                            SetValue(i, c, false);
                        }
                    }
                }
            }
            else//列のとき
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != c)
                    {
                        if (!zeroFlag[r, i])//ゼロではない場合
                        {
                            SetValue(r, i,false);
                        }
                    }
                }
            }
        }
        else//bitModeのとき
        {
            if (row)//行のとき
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != r)
                    {
                        if (!zeroFlag[i, c])//ゼロではない場合
                        {
                            SetValue(i, c, true);
                        }
                    }
                }
            }
            else//列のとき
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i != c)
                    {
                        if (!zeroFlag[r, i])//ゼロではない場合
                        {
                            SetValue(r, i, true);
                        }
                    }
                }
            }
        }
    }

    void Zero(int r,int c)
    {
        zeroFlag[r, c] = true;//フラグを立てる
        ec.ZeroEffect(r, c);//ゼロになった時のエフェクトを出す
        sc.AddScore(100);//スコアを加算
        if (BitCheck())
        {
            SetBitMode();
            Debug.Log("BitMode");
        }
        else
        {
            Debug.Log("NotBitMode");
        }
    }

    void SetBitMode()
    {
        if (!bitMode)
        {
            bitTime = 10f;
            bitTimeText.text = "BITTIME:" + bitTime.ToString("0.0");

            bitMode = true;
            gc.SetBitMode(true);
            nowNumber = 1;
            nextNumber = 1;
            nowNumberText.text = nowNumber.ToString();
            nextNumberText.text = nextNumber.ToString();
            //ランダムに初期値を代入
            for (int i = 0; i < 4; i++)//行
            {
                for (int j = 0; j < 4; j++)//列
                {
                    intValues[i, j] = Random.Range(0, 2);//0,1の値を入れる 
                    numberText[i, j].text = intValues[i, j].ToString();
                    if (intValues[i, j] == 1)
                    {
                        zeroFlag[i, j] = false; //1であればフラグを外す
                    }
                }
            }
            Debug.Log("number=[" + intValues[0, 0] + intValues[0, 1] + intValues[0, 2] + intValues[0, 3] +
";" + intValues[1, 0] + intValues[1, 1] + intValues[1, 2] + intValues[1, 3] +
";" + intValues[2, 0] + intValues[2, 1] + intValues[2, 2] + intValues[2, 3] +
";" + intValues[3, 0] + intValues[3, 1] + intValues[3, 2] + intValues[3, 3] + "]");
        }
        else
        {
            //ランダムに初期値を代入
            for (int i = 0; i < 4; i++)//行
            {
                for (int j = 0; j < 4; j++)//列
                {
                    intValues[i, j] = Random.Range(0, 2);//0,1の値を入れる 
                    numberText[i, j].text = intValues[i, j].ToString();
                    if (intValues[i, j] == 1)
                    {
                        zeroFlag[i, j] = false; //1であればフラグを外す
                    }
                }
            }
        }


    }

    void ResetBitMode()
    {
        bitMode = false;
        gc.SetBitMode(false);

        nowNumber = Random.Range(1, 10);
        nextNumber = Random.Range(1, 10);
        nowNumberText.text = nowNumber.ToString();
        nextNumberText.text = nextNumber.ToString();

        bitTimeText.text = "";

        //ランダムに初期値を代入
        for (int i = 0; i < 4; i++)//行
        {
            for (int j = 0; j < 4; j++)//列
            {
                intValues[i, j] = Random.Range(1, 10);//1~9の値を入れる
                numberText[i, j].text = intValues[i, j].ToString();
            }
        }

        //フラグを初期化
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                zeroFlag[i, j] = false;
            }
        }

    }

    void SetValue(int r,int c,bool bit)
    {
        if (!bit)
        {
            intValues[r, c] = (intValues[r, c] + nowNumber) % 10;//加算する値との和の下一桁
            if(intValues[r,c] == 0)//計算後に0になったとき
            {
                Zero(r, c);
            }
        }
        else
        {
            intValues[r, c] = 0;
            Zero(r, c);
        }

        numberText[r, c].text = intValues[r, c].ToString();//値を更新
    }
}
