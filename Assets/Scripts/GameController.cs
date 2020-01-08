using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour{

    [SerializeField] GameObject backgroundMusicObject;
    [SerializeField] GameObject powerBarContainer;
    [SerializeField] GameObject shotCountLeft;
    public static float fadeSceneTime = 3f;
    [SerializeField] GameObject shotCount;
    [SerializeField] AudioClip puttSound;
    [SerializeField] AudioClip holeSound;
    [SerializeField] bool showUIOnStart;
    [SerializeField] Golf_Ball golfBall;
    public static SettingsData settings;
    static AudioSource backgroundMusic;
    TextMeshProUGUI shotCountLeftText;
    public static LevelData lastLevel;
    TextMeshProUGUI shotCountText;
    public static int lastShots;
    private string this_level;
    AudioSource audioSource;
    RectTransform powerBar;
    public int finishTimer;
    Image powerBarImage;
    LevelData levelData;
    CanvasGroup GameUI;
    public int shots;
    bool showUI;

    private void Awake() {
        this_level = SceneManager.GetActiveScene().name;
        settings = SettingsData.GetSaveData();
        settings.last_level = this_level;
        SettingsData.SetSaveData(settings);
        levelData = LevelData.GetLevelData(this_level);
        if (backgroundMusic == null) {
            GameObject newBGMusic = Instantiate(backgroundMusicObject);
            backgroundMusic = newBGMusic.GetComponent<AudioSource>();
            DontDestroyOnLoad(newBGMusic);
        }
        if (backgroundMusic.clip != LevelData.GetAudioClip(levelData.music_name)){
            backgroundMusic.clip = LevelData.GetAudioClip(levelData.music_name);
            backgroundMusic.Play();
        }
        powerBar = powerBarContainer.GetComponent<RectTransform>();
        shotCountLeftText = shotCountLeft.GetComponent<TextMeshProUGUI>();
        shotCountText = shotCount.GetComponent<TextMeshProUGUI>();
        powerBarImage = powerBarContainer.GetComponent<Image>();
        audioSource = gameObject.GetComponent<AudioSource>();
        GameObject gameUI = GameObject.Find("Game UI");
        audioSource.volume = settings.sfx_volume;
        gameUI.SetActive(true);
        GameUI = gameUI.GetComponent<CanvasGroup>();
        ShowUI(showUIOnStart);
    }

    private void Update() {
        if (showUI) {
            GameUI.alpha = Mathf.MoveTowards(GameUI.alpha, 0.7f, 0.025f);
        } else {
            GameUI.alpha = Mathf.MoveTowards(GameUI.alpha, 0f, 0.025f);
        }
        //Save data
        if(finishTimer == 1) {
            lastShots = shots;
            lastLevel = levelData;
            if (levelData.best_score == 0 || shots < levelData.best_score) {
                levelData.best_score = shots;
                LevelData.SetLevelData(levelData);
            }
        }
        if (finishTimer > 0) {
            finishTimer++;
            FadeMusicOut();
            if (finishTimer > 100) {
                NextLevel();
            }
        } else {
            FadeMusicIn();
        }
        if (golfBall.isMoving) {
            showUI = false;
        }
        if (golfBall.canShoot) {
            float barHeight = (golfBall.power / golfBall.maxPower) * 300;
            powerBarImage.color = Color.green;
            if (barHeight > 150) {
                powerBarImage.color = Color.yellow;
            }
            if (barHeight > 250) {
                powerBarImage.color = Color.red;
            }
            powerBar.sizeDelta = new Vector2(50, barHeight);
        }
        if (golfBall.canShoot) {
            if (levelData.best_score != 0) {
                shotCountLeftText.text = "Par\nBest\nStroke";
                shotCountText.text = levelData.par + "\n" + levelData.best_score + "\n" + shots.ToString();
            } else {
                shotCountLeftText.text = "Par\nStroke";
                shotCountText.text = levelData.par + "\n" + shots.ToString();
            }
        }
    }

    public void PuttSound() {
        audioSource.PlayOneShot(puttSound);
    }

    public void HoleSound() {
        audioSource.PlayOneShot(holeSound);
    }

    public void ShowUI(bool v) {
        showUI = v;
    }

    public void NextLevel() {
        Initiate.Fade("LevelComplete", Color.black, GameController.fadeSceneTime);
    }

    public void FadeMusicOut() {
        if(backgroundMusic.volume > 0) {
            backgroundMusic.volume = Mathf.MoveTowards(backgroundMusic.volume, 0, 0.002f);
        }
    }

    public void FadeMusicIn() {
        if (backgroundMusic.volume < settings.music_volume) {
            backgroundMusic.volume = Mathf.MoveTowards(backgroundMusic.volume, settings.music_volume, 0.002f);
        }
    }

}
