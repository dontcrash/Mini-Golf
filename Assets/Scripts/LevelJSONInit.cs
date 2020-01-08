﻿using System.Collections;
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

        LevelData.SetLevelData(new LevelData("Level_1", "Desert", true, 2));
        LevelData.SetLevelData(new LevelData("Level_2", "Heroic Minority", false, 3));
        LevelData.SetLevelData(new LevelData("Level_3", "Winds Of Stories", false, 3));
        LevelData.SetLevelData(new LevelData("Level_4", "Observing The Stars", false, 3));
        LevelData.SetLevelData(new LevelData("Level_5", "Winds Of Stories", false, 3));
        LevelData.SetLevelData(new LevelData("Level_6", "Winds Of Stories", false, 3));
        LevelData.SetLevelData(new LevelData("Level_7", "Winds Of Stories", false, 3));
        LevelData.SetLevelData(new LevelData("Level_8", "Winds Of Stories", false, 3));
        LevelData.SetLevelData(new LevelData("Level_9", "Winds Of Stories", false, 3));
        LevelData.SetLevelData(new LevelData("Level_10", "Winds Of Stories", false, 3));

    }

}
