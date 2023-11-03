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
    // Start is called before the first frame update
    void Start()
    {
        yourScore = GameObject.Find("YourScore").GetComponent<TextMeshProUGUI>();
        newRecord = GameObject.Find("NewRecord").GetComponent<TextMeshProUGUI>();
        yourScore.text = "Your Score : " + GameSettings.Instance.score.ToString();
        //Debug.Log("Your score: " + GameSettings.Instance.score);
        if (GameSettings.Instance.LoadHighestScore()){
            if (GameSettings.Instance.score > GameSettings.Instance.highScore){
                GameSettings.Instance.SaveHighestScore();
                newRecord.text = "New Record! compared to " + GameSettings.Instance.highScore.ToString();
                //Debug.Log("Previous High Score: " + GameSettings.Instance.highScore);
                //Debug.Log("New High Score Unlocked!  ");
            }
        }else {
            GameSettings.Instance.SaveHighestScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
