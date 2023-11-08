using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSettings : MonoBehaviour
{

    public static GameSettings Instance;
    public int score;
    public int enemyIn;
    public int highScore;
    public int mathLevel;

    public float timer;

    public int enemySpeed;
    public bool frugality;

    private void Awake() //awake is before start?
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        Instance.LoadHighestScore();
        Instance.LoadSetting();
        DontDestroyOnLoad(gameObject);
    }

    public static void ResetState(){
       Instance.score = 0;
       Instance.enemyIn = 0;
    }

    [Serializable]
    public class PlayerSettings{
        public int mathLevel;
        public int enemySpeed;
        public float timer;
        public bool frugality;
    }
    [Serializable]
    public class HighestScore{
        public int highScore;
    }
    public void SaveHighestScore(){
        HighestScore data = new HighestScore();
        data.highScore = score;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/HighScore.json", json);
    }
    public bool LoadHighestScore(){
        string path = Application.persistentDataPath + "/HighScore.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighestScore data = JsonUtility.FromJson<HighestScore>(json);
            highScore = data.highScore;
            return true;
        }
        return false;
    }
    public void resetScore(){
        score = 0;
        SaveHighestScore();
        LoadHighestScore();
    }
    public void SaveSetting () {
        PlayerSettings data = new PlayerSettings();
        data.enemySpeed = enemySpeed;
        data.mathLevel = mathLevel;
        data.timer = timer;
        data.frugality = frugality;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/PlayerSetting.json", json);
    }
    public void LoadSetting(){
        string path = Application.persistentDataPath + "/PlayerSetting.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //Debug.Log(json);
            PlayerSettings data = JsonUtility.FromJson<PlayerSettings>(json);
            enemySpeed = data.enemySpeed;
            mathLevel = data.mathLevel;
            timer = data.timer;
            frugality = data.frugality;
           // Debug.Log(frugality);
        } else {
            timer = 60.0f;
            enemySpeed = 150;
            mathLevel = 10;
            frugality = false;
            SaveSetting();
        }
    }

    


}
