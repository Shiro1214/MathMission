using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class OptionMenuHandler : MonoBehaviour
{
    //TMPro.TMP_Dropdown dropdown
    private TextMeshProUGUI sliderLabel;
    private Slider slider;
    private TMPro.TMP_Dropdown dropdown;
    private TMPro.TMP_Dropdown timerDropdown;

    private Toggle frugalityToggle;
    void Start(){
        sliderLabel = GameObject.Find("SliderLabel").GetComponent<TextMeshProUGUI>();
        dropdown = GameObject.Find("MathLevel").GetComponent<TMPro.TMP_Dropdown>();
        timerDropdown = GameObject.Find("Timer").GetComponent<TMPro.TMP_Dropdown>();
        frugalityToggle = GameObject.Find("FrugalityMode").GetComponent<Toggle>();
        slider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
        if (GameSettings.Instance != null) {
            updateMathLevelDrops();
            updateTimerDrops();
            updateFrugalityUI();
        }
    }

    void updateFrugalityUI(){
        switch (GameSettings.Instance.frugality){
            case true: frugalityToggle.SetIsOnWithoutNotify(true); break;
            case false: frugalityToggle.SetIsOnWithoutNotify(false); break;
        }
    }

    void updateTimerDrops(){
        switch (GameSettings.Instance.timer){
            case 30: timerDropdown.value = 0; break;
            case 60: timerDropdown.value = 1; break;
            case 90: timerDropdown.value = 2; break;
            default: timerDropdown.value = 1; break;
        }
    }
    void updateMathLevelDrops(){
        sliderLabel.text = "Enemy Speed: " + GameSettings.Instance.enemySpeed;
        slider.value = GameSettings.Instance.enemySpeed;

        switch (GameSettings.Instance.mathLevel){
            case 5: dropdown.value = 0; break;
            case 10: dropdown.value = 1; break;
            case 15: dropdown.value = 2; break;
            default: dropdown.value = 1; break;
        }
    }
    public void dropDownSetting(){

        int index = dropdown.value;
        switch (index){
            case 0: GameSettings.Instance.mathLevel = 5; break;
            case 1: GameSettings.Instance.mathLevel = 10; break;
            case 2: GameSettings.Instance.mathLevel = 15; break;
            default: GameSettings.Instance.mathLevel = 15; break;
        }
    }

    public void sliderSetting(){
        GameSettings.Instance.enemySpeed = (int)slider.value;
        sliderLabel.text = "Enemy Speed: " + GameSettings.Instance.enemySpeed;
    }

    public void SaveGameSetting(){
        GameSettings.Instance.SaveSetting();
    }
    public void ResetScore(){
        GameSettings.Instance.resetScore();
    }
    public void LoadGameSetting(){
        GameSettings.Instance.LoadSetting();
        updateMathLevelDrops();
        updateTimerDrops();
        updateFrugalityUI();
    }

    public void FrugalityToggle() {
        Debug.Log("toggled");
        if (GameSettings.Instance.frugality) {
            GameSettings.Instance.frugality = false;
        } else {
            GameSettings.Instance.frugality = true;
        }
    }
    public void setTimer(){
        int index = timerDropdown.value;
        switch (index){
            case 0: GameSettings.Instance.timer = 30; break;
            case 1: GameSettings.Instance.timer = 60; break;
            case 2: GameSettings.Instance.timer = 90; break;
        }
    }

}
