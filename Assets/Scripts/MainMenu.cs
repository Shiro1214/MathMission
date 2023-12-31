using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MainMenu : MonoBehaviour
{
    public GameObject levelComplete;
    // Start is called before the first frame update
    void Start()
    {
        levelComplete = transform.Find("LevelComplete").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Invoke("loadNextLevel", 2f);
        levelComplete.SetActive(true);
    }
    private void loadNextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
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
