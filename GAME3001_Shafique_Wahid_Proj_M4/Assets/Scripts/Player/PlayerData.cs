using UnityEngine;
using System.Collections;

public class PlayerData {
    //holds player data for scoring 
    private static PlayerData instance;
    private const string HighScoreKey = "HighScore: ";

    public static PlayerData GetInstance {
        get {
            if (instance == null) {
                instance = new PlayerData();
                instance.topScore = PlayerPrefs.GetInt("HighScore");
            }
            return instance;
        }
    }

    private PlayerData() { }

    private int topScore;
    private int presentScore;
    private int healthRemaining;
    private int upgradeLevel;
    private bool isGameOver = false;

    public int score {
        get { return presentScore; }
        set {
            presentScore = value;
            if (presentScore > topScore) {
                PlayerPrefs.SetInt(HighScoreKey, topScore);
                topScore = presentScore;
            }
        }
    }
    public int highScore {
        get { return topScore; }
    }
    public int health {
        get { return healthRemaining; }
        set {
            healthRemaining = value;
            if (healthRemaining <= 0 && !isGameOver) {
                isGameOver = true;
                Audio.instance.GameOver();
            }
        }
    }
    public int myUpgradeLevel {
        get { return upgradeLevel; }
        set {
            upgradeLevel = value;
        }
    }
}
