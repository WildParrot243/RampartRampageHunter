using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public struct GameData
{
    //Add variables to save here
    public int waveNumber;
    public float castleHealth;
    public float money;
    public int gameStats;
}
public static class GameSaver
{

    public static readonly string SaveDirectory = Application.persistentDataPath + "/Saves";
    public static readonly string SaveFile = SaveDirectory + "/save.dat";
    public static GameData SaveData;

    static GameSaver()
    {
        Load();
    }

    public static async void Save()
    {
        if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        await using var x = File.OpenWrite(SaveFile);
        Debug.Log(SaveFile);
        serializer.Serialize(x, SaveData);
    }

    public static async void Load()
    {
        if (!Directory.Exists(SaveDirectory))
        {
            Reset();
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        await using var x = File.OpenRead(SaveFile);
        SaveData = (GameData)serializer.Deserialize(x);
    }
    public static void Reset(float startingMoney = 0)
    {
        SaveData = new GameData()
        {
            castleHealth = 0,
            waveNumber = 0, 
            money = startingMoney
        };
    }
}
