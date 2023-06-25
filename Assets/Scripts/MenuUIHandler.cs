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
        language = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language;
        List<string> bestPlayer = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestPlayer;
        if (language == 0)
        {
            if (bestPlayer.Count == 0) { bestScoreText.text = "没人玩过！"; }
            else if (bestPlayer.Count > 0)
            {
                int i = 0, bestScore = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestScore;
                foreach (string recordPlayer in bestPlayer)
                {
                    bestScoreText.text += recordPlayer;
                    i++;
                    if (i < bestPlayer.Count) { bestScoreText.text += "，"; }
                }
                bestScoreText.text += "目前以" + bestScore + "分的高分荣登榜首。";
            }
            namePlaceHolder.text = "请在此输入你的名字……";
            submitText.text = "提交";
            startText.text = "开始游戏";
            exitText.text = "退出";
        }
        else if (language == 1)
        {
            if (bestPlayer.Count == 0) { bestScoreText.text = "No player have played the game!"; }
            else if (bestPlayer.Count > 0)
            {
                int i = 0, bestScore = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestScore;
                foreach (string recordPlayer in bestPlayer)
                {
                    bestScoreText.text += recordPlayer;
                    i++;
                    if (i < bestPlayer.Count) { bestScoreText.text += ", "; }
                }
                bestScoreText.text += " hold the record at " + bestScore + " points. ";
            }
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
        if (playerName == "") { GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().playerName = "Anonymous"; }
        SceneManager.LoadScene(1);
    }

    public void ChangeLanguage()
    {
        if (language == 0) { language = 1; }
        else if (language == 1) { language = 0; }
        GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language = language;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitApplication()
    {
        if (GameObject.Find("PlayerData") != null) { GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().SaveUserProfile(); }
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
