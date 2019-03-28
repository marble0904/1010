using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject[] arrow = new GameObject[3];
    int mode = 0;

    public GameObject menuText;
    public GameObject ruleText;
    bool menu = true;//ルール説明画面ならfalse
    public AudioSource selectSE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (menu)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ArrowUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ArrowDown();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModeSelect();
            //SceneManager.LoadScene("Stage");
        }
	}

    private void ModeSelect()
    {
        switch (mode)
        {
            case 0:
                GameController.SetGameMode(true);
                SceneManager.LoadScene("Stage");
                break;
            case 1:
                GameController.SetGameMode(false);
                SceneManager.LoadScene("Stage");
                break;
            case 2:
                ShowRule();
                //SceneManager.LoadScene("Stage");
                break;
            default:
                break;
        }
    }

    private void ArrowUp()
    {
        selectSE.Play();
        arrow[mode].SetActive(false);
        if(mode == 0)
        {
            mode = 2;
        }
        else
        {
            mode--;
        }
        arrow[mode].SetActive(true);
    }

    private void ArrowDown()
    {
        selectSE.Play();
        arrow[mode].SetActive(false);
        if (mode == 2)
        {
            mode = 0;
        }
        else
        {
            mode++;
        }
        arrow[mode].SetActive(true);
    }

    private void ShowRule()
    {
        if (menu)
        {
            menu = false;
            menuText.SetActive(false);
            ruleText.SetActive(true);
        }
        else
        {
            menu = true;
            menuText.SetActive(true);
            ruleText.SetActive(false);
        }
    }
}
