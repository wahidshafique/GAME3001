using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    //Heads up display to show score and highscore 
    public Text score;
    public Text highScore;
    public Text health;
    public Text costText;
    private int cost;
    private static HUD instance;
    public GameObject[] upgrades;
    bool anyUpgradesLeft = true;
    private void Awake() {
        cost = 10;
        PlayerData.GetInstance.myUpgradeLevel = 0;
        PlayerData.GetInstance.health = 100;
        if (instance == null) {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        } else Destroy(this.gameObject);
    }

    private void Update() {
        if (Application.loadedLevelName == "MainMenu") {
            GameObject.Destroy(this.gameObject);
            PlayerData.GetInstance.score = 0;
        }
        this.score.text = "Score: " + PlayerData.GetInstance.score;
        this.highScore.text = "HighScore: " + PlayerData.GetInstance.highScore;
        this.health.text = "Health: " + PlayerData.GetInstance.health;
        if (anyUpgradesLeft)
            this.costText.text = "Upgrade(cost: " + cost + ")";
    }

    public void upgrade() {
        int upgradeLevel = PlayerData.GetInstance.myUpgradeLevel;
        if (PlayerData.GetInstance.score >= cost) {
            PlayerData.GetInstance.myUpgradeLevel++;
            print(upgradeLevel);
            if (upgradeLevel == 0) {
                PlayerData.GetInstance.score -= cost;
                //PlayerData.GetInstance.health += 20;
                GameObject.Find("Emitter").GetComponent<TurretShoot>().fireRate = 0.25f;
                cost += 10;
                
            }
            if (upgradeLevel == 1) {
                PlayerData.GetInstance.score -= cost;
                //PlayerData.GetInstance.health += 60;
                upgrades[upgradeLevel].gameObject.SetActive(true);
                cost += 100;
            }
            if (upgradeLevel == 2) {
                PlayerData.GetInstance.score -= cost;
                upgrades[0].gameObject.SetActive(false);
                upgrades[1].gameObject.SetActive(false);
                upgrades[upgradeLevel].gameObject.SetActive(true);
                anyUpgradesLeft = false;
                this.costText.text = "";
                
            }
            
            //cost += 1;
        }
    }
}
