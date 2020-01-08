using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LevelData;

public class LevelCompleteButtons : MonoBehaviour{

    Button MenuButton;
    Button RetryButton;
    Button NextLevelButton;
    LevelName levelName;

    private void Awake() {
        levelName = LevelData.GetLevelInfo(GameController.lastLevel);

        MenuButton = GameObject.Find("Menu").GetComponent<Button>();
        RetryButton = GameObject.Find("Retry").GetComponent<Button>();
        NextLevelButton = GameObject.Find("Next Level").GetComponent<Button>();

        MenuButton.onClick.AddListener(MenuButtonClicked);
        RetryButton.onClick.AddListener(RetryButtonClicked);
        NextLevelButton.onClick.AddListener(NextLevelButtonClicked);
    }

    private void MenuButtonClicked() {
        Debug.Log("Menu button");
    }

    private void RetryButtonClicked() {
        Initiate.Fade("Level_" + levelName.level, Color.black, GameController.fadeSceneTime);
    }

    private void NextLevelButtonClicked() {
        Initiate.Fade(levelName.nextLevel, Color.black, GameController.fadeSceneTime);
    }

}
