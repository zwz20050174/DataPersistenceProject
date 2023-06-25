using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHolder : MonoBehaviour
{
    public string playerName;
    public int language, bestScore;
    public List<string> bestPlayer;
    public static PlayerDataHolder instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        string path = Application.persistentDataPath + "/userprofile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.playerName;
            language = data.language;
            bestScore = data.bestScore;
            bestPlayer = data.bestPlayer;
        }

    }
    private void Start()
    {

    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int language, bestScore;
        public List<string> bestPlayer;
    }
    public void SaveUserProfile()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.language = language;
        data.bestScore = bestScore;
        data.bestPlayer = bestPlayer;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/userprofile.json", json);
    }
}

