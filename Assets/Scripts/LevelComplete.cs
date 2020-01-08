using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour{

    private void Awake() {
        TextMeshProUGUI LevelCompleteText = GameObject.Find("LevelCompleteText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI shotText = GameObject.Find("ShotCount").GetComponent<TextMeshProUGUI>();
        GameObject bronze = GameObject.Find("Bronze Medal");
        GameObject silver = GameObject.Find("Silver Medal");
        GameObject gold = GameObject.Find("Gold Medal");
        
        LevelData last = GameController.lastLevel;
        LevelData.LevelName ln = new LevelData.LevelName(GameController.lastLevel);
        string score = "Nice";
        int lastShots = GameController.lastShots;
        shotText.text = last.par + "\n" + last.best_score + "\n" + lastShots;
        bronze.SetActive(false);
        silver.SetActive(false);
        gold.SetActive(false);
        if (lastShots == last.par - 2) {
            score = "Eagle";
            gold.SetActive(true);
        }
        if (lastShots == last.par - 1) {
            score = "Birdie";
            gold.SetActive(true);
        }
        if(lastShots == last.par) {
            score = "Par";
            gold.SetActive(true);
        }
        if (lastShots == last.par + 1) {
            score = "Bogey";
            silver.SetActive(true);
        }
        //Sad face
        if (lastShots >= last.par + 2) {
            bronze.SetActive(true);
        }
        LevelCompleteText.text = "Course " + ln.level + " - " + score + "!";
    }

}
