using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[Serializable]
public class Data
{
    public int id;
    public int counter;

    public Data(int _id, int _counter)
    {
        id = _id;
        counter = _counter;
    }
}

[Serializable]
public class SaveData
{
    public List<Data> datas = new List<Data>(20);
}

public class SaveSystem
{
    public SaveData Load(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath))
            return null;

        byte[] jsonData = Convert.FromBase64String(File.ReadAllText(filePath));
        return JsonUtility.FromJson<SaveData>(Encoding.UTF8.GetString(jsonData));
    }

    public void Save(string fileName, SaveData objToJson)
    {
        byte[] jsonData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(objToJson));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, fileName), Convert.ToBase64String(jsonData));
    }
}
