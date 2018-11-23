using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioSource bgmSource;
    public AudioClip bgm0;
    public AudioClip bgm1;

    public AudioSource line;
    public AudioSource zeroLine;
    public AudioSource bitMode;
    public AudioSource zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBGM0()
    {
        bgmSource.clip = bgm0;
        bgmSource.Play();
    }

    public void SetBGM1()
    {
        bgmSource.clip = bgm1;
        bgmSource.Play();
    }

    public void Line()
    {
        line.Play();
    }

    public void ZeroLine()
    {
        zeroLine.Play();
    }

    public void BitMode()
    {
        bitMode.Play();
    }

    public void Zero()
    {
        zero.Play();
    }
}
