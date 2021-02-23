﻿using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/game.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(GameManager.instance.playerLevel, GameManager.instance.playedLevel);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/game.fun";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
