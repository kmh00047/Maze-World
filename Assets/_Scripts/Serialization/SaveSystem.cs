using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    #region Save System Logic | Universal

    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player_Data.scav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player_Data.scav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream.Length == 0)
            {
                Debug.LogWarning("The stream is empty. Nothing Loaded.");
                stream.Close();
                return null;
            }
            else
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
                
            }

        }
        else
        {
            Debug.Log("File not found at " + path);
            return null;
        }
    }
    #endregion

}
