using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText, namePlaceHolder, playerNameText, submitText, startText, exitText;
    public string playerName;
    public int language = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (language == 0)
        {
            bestScoreText.text = "没人玩过！";
            namePlaceHolder.text = "请在此输入你的名字……";
            submitText.text = "提交";
            startText.text = "开始游戏";
            exitText.text = "退出";
        }
        else if (language == 1)
        {
            bestScoreText.text = "No player have played the game!";
            namePlaceHolder.text = "Enter your name...";
            submitText.text = "Submit";
            startText.text = "Start";
            exitText.text = "Exit";
        }
    }

    public void SubmitPlayerName()
    {
        playerName = playerNameText.text;
        GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().playerName = playerName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeLanguage()
    {
        if (language == 0) { language = 1; }
        else if (language == 1) { language = 0; }
        GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language = language;
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
