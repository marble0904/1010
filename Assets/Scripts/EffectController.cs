﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour {

    public ParticleSystem[] lineR = new ParticleSystem[4];//行のラインエフェクト
    public ParticleSystem[] lineC = new ParticleSystem[4];//列のラインエフェクト

    ParticleSystem[,] zero = new ParticleSystem[4, 4];//0になった時のエフェクト

    public ParticleSystem[] zeroChainR = new ParticleSystem[4];//0の連鎖エフェクト行
    public ParticleSystem[] zeroChainC = new ParticleSystem[4];//0の連鎖エフェクト列

    public ParticleSystem bitEffect;

    public SoundController sc;

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

    public void LineEffect(int num, bool row, float waitTime)
    {
        sc.Line();

        float speed;

        if (row)
        {
            speed = (1.0f - waitTime) * 80f;
            lineR[num].startSpeed = speed;
            lineR[num].startLifetime = waitTime * 2f;
            lineR[num].Emit(1);
        }
        else
        {
            speed = (1.0f - waitTime) * 80f;
            lineC[num].startSpeed = speed;
            lineR[num].startLifetime = waitTime * 0.7f;
            lineC[num].Emit(1);
        }
    }

    public void ZeroEffect(int r,int c)
    {
        sc.Zero();
        zero[r, c].Emit(10);
    }

    public void ZeroChain(int num,bool row)
    {
        sc.ZeroLine();

        if (row)
        {
            zeroChainR[num].Emit(1);
        }
        else
        {
            zeroChainC[num].Emit(1);
        }

    }

    public void BitEffect()
    {
        sc.BitMode();
        bitEffect.Emit(30);
    }
}
