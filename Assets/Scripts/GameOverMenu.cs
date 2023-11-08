using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameOverMenu : MonoBehaviour
{
    private TextMeshProUGUI yourScore;
    private TextMeshProUGUI newRecord;
    private TextMeshProUGUI bestScore;
    // Start is called before the first frame update
    void Start()
    {
        yourScore = GameObject.Find("YourScore").GetComponent<TextMeshProUGUI>();
        newRecord = GameObject.Find("NewRecord").GetComponent<TextMeshProUGUI>();
        bestScore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
        yourScore.text = "Your Score : " + GameSettings.Instance.score.ToString();
        bestScore.text = "Highest Score : " + GameSettings.Instance.highScore.ToString();
        if (GameSettings.Instance.LoadHighestScore()){
            if (GameSettings.Instance.score > GameSettings.Instance.highScore){
                GameSettings.Instance.SaveHighestScore();
                newRecord.text = "Congrats! New score unlocked!";
            }
        }else {
            GameSettings.Instance.SaveHighestScore();
            newRecord.text = "First Best Record!";
        }
    }


    public void Restart() {
        GameSettings.ResetState();
        SceneManager.LoadScene(1);

    }

    public void MainButton(){
        GameSettings.ResetState();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
       #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit(); // original code to quit Unity player
    #endif
    }
}
