using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/data.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = new SaveData();
            try
            {
                data = formatter.Deserialize(stream) as SaveData;
            }
            catch (System.Exception e)
            {
                Debug.Log("Exception: Failed to read save -> " + e);
                return data;
            }
            
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File not found at " + path);
            return null;
        }
    }
}
