using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelJSONInit : MonoBehaviour{

    [SerializeField] bool resetLevelData;

    public void Awake() {

        if(!resetLevelData) return;

        SettingsData settings = new SettingsData {
            music_volume = 0.5f,
            sfx_volume = 1f,
            last_level = "Level_1",
            max_level = "Level_1"
        };
        SettingsData.SetSaveData(settings);

        LevelData.SetLevelData(new LevelData("Level_1", "Desert", true, 1));
        LevelData.SetLevelData(new LevelData("Level_2", "Desert", false, 2));
        LevelData.SetLevelData(new LevelData("Level_3", "Desert", false, 3));
        LevelData.SetLevelData(new LevelData("Level_4", "Desert", false, 2));
        LevelData.SetLevelData(new LevelData("Level_5", "Desert", false, 99));
        LevelData.SetLevelData(new LevelData("Level_6", "Desert", false, 99));
        LevelData.SetLevelData(new LevelData("Level_7", "Desert", false, 99));
        LevelData.SetLevelData(new LevelData("Level_8", "Desert", false, 99));
        LevelData.SetLevelData(new LevelData("Level_9", "Desert", false, 99));
        LevelData.SetLevelData(new LevelData("Level_10", "Desert", false, 99));

        //Observing The Stars
        //Heroic Minority
        //Winds Of Stories

    }

}
