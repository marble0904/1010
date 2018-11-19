using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour {

    public ParticleSystem[] lineR = new ParticleSystem[4];//行のラインエフェクト
    public ParticleSystem[] lineC = new ParticleSystem[4];//列のラインエフェクト

    ParticleSystem[,] zero = new ParticleSystem[4, 4];//0になった時のエフェクト

    public ParticleSystem[] zeroChainR = new ParticleSystem[4];//0の連鎖エフェクト行
    public ParticleSystem[] zeroChainC = new ParticleSystem[4];//0の連鎖エフェクト列

	// Use this for initialization
	void Start () {
        for (int i=0;i<4;i++)
        {
            for(int j = 0; j < 4; j++)
            {
                zero[i, j] = GameObject.Find("/Effect/ZeroEffect/r"+i+"/c"+j).GetComponent<ParticleSystem>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LineEffect(int num,bool row)
    {
        if (row)
        {
            lineR[num].Emit(1);
        }
        else
        {
            lineC[num].Emit(1);
        }
    }

    public void ZeroEffect(int r,int c)
    {
        zero[r, c].Emit(10);
    }

    public void ZeroChain(int num,bool row)
    {
        if (row)
        {
            zeroChainR[num].Emit(1);
        }
        else
        {
            zeroChainC[num].Emit(1);
        }
    }
}
