using System;
using System.IO;
using UnityEngine;

[Serializable]
public class LevelData {

    public string level_name;
    public string music_name;
    public bool unlocked;
    public int best_score;
    public int par;

    public LevelData(string ln, string mn, bool u, int p) {
        level_name = ln;
        music_name = mn;
        unlocked = u;
        best_score = 0;
        par = p;
    }

    public static LevelData GetLevelData(string level) {
        return JsonUtility.FromJson<LevelData>(PlayerPrefs.GetString(level));
    }

    public static void SetLevelData(LevelData level) {
        PlayerPrefs.SetString(level.level_name, JsonUtility.ToJson(level));
        PlayerPrefs.Save();
    }

    public static AudioClip GetAudioClip(string clip) {
        return (AudioClip)Resources.Load("Sounds/Music/" + clip, typeof(AudioClip));
    }

    public static LevelName GetLevelInfo(LevelData ld) {
        return new LevelName(ld);
    }

    public class LevelName {

        public int level;
        public string nextLevel;

        public LevelName(LevelData ld) {
            level = int.Parse(ld.level_name.Split('_')[1]);
            nextLevel = "Level_" + (level + 1);
        }

    }
        
}
