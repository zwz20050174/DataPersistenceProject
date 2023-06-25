using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText, bestScoreText, backToMenuText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;
    string playerName;
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if (GameObject.Find("PlayerData") != null)
        {
            int bestScore = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestScore;
            List<string> bestPlayer = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestPlayer;
            int language = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language;
            if (language == 0)
            {
                if (bestPlayer.Count == 0) { bestScoreText.text = "没有最高分纪录。"; }
                else if (bestPlayer.Count > 0)
                {
                    int i = 0;
                    foreach (string recordPlayer in bestPlayer)
                    {
                        bestScoreText.text += recordPlayer;
                        i++;
                        if (i < bestPlayer.Count) { bestScoreText.text += "，"; }
                    }
                    bestScoreText.text += "目前以" + bestScore + "的高分荣登榜首。";
                }
                ScoreText.text = playerName + "，分数：0";
                backToMenuText.text = "返回至菜单";
            }
            else if (language == 1)
            {
                if (bestPlayer.Count == 0) { bestScoreText.text = "No best score record. "; }
                else if (bestPlayer.Count > 0)
                {
                    int i = 0;
                    foreach (string recordPlayer in bestPlayer)
                    {
                        bestScoreText.text += recordPlayer;
                        i++;
                        if (i < bestPlayer.Count) { bestScoreText.text += ", "; }
                    }
                    bestScoreText.text += "hold the record at " + bestScore + " points. ";
                }
                ScoreText.text = playerName + ", Score : 0";
                backToMenuText.text = "Back to menu";
            }
        }
        else { bestScoreText.text = " 没有最高分纪录。"; ScoreText.text = playerName + "，分数：0"; backToMenuText.text = "返回至菜单"; }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        if (GameObject.Find("PlayerData") != null)
        {
            int language = GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language;
            if (language == 0) { ScoreText.text = playerName + $"，分数 : {m_Points}"; }
            else if (language == 1) { ScoreText.text = playerName + $", Score : {m_Points}"; }
        }
        else { ScoreText.text = $"，分数：{m_Points}"; }
    }
    public void BackToMenu() { SceneManager.LoadScene(0); }
    public void GameOver()
    {
        m_GameOver = true;
        if (GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language == 0) { GameOverText.GetComponent<TextMeshProUGUI>().text = "游戏结束\r\n按下空格键重新开始\r\n"; }
        else if (GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().language == 1) { GameOverText.GetComponent<TextMeshProUGUI>().text = "GAME OVER\r\nPress Space to Restart\r\n"; }
        else { GameOverText.GetComponent<TextMeshProUGUI>().text = "游戏结束\r\n按下空格键重新开始\r\n"; }
        GameOverText.SetActive(true);
        if (m_Points == GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestScore) { GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestPlayer.Add(playerName); }
        if (m_Points > GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestScore)
        {
            GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestPlayer.Clear();
            GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestScore = m_Points;
            GameObject.Find("PlayerData").GetComponent<PlayerDataHolder>().bestPlayer.Add(playerName);
        }
    }
}
