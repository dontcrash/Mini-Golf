using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SettingsData {

    public float music_volume;
    public float sfx_volume;
    public string last_level;
    public string max_level;

    public static SettingsData GetSaveData() {
        return JsonUtility.FromJson<SettingsData>(PlayerPrefs.GetString("Settings"));
    }

    public static void SetSaveData(SettingsData settings) {
        PlayerPrefs.SetString("Settings", JsonUtility.ToJson(settings));
        PlayerPrefs.Save();
    }

}
