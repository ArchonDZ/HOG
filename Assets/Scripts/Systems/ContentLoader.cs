using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public class ContentLoader : IInitializable
{
    private Content content = new Content();

    public void Initialize()
    {
        for (int i = 0; i < 12; i++)
        {
            content.levels.Add(new Level(i, "null", "null", 0, 10));
        }

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "hog_levels.json"), JsonUtility.ToJson(content));
    }

    [Serializable]
    private class Content
    {
        public List<Level> levels = new List<Level>();
    }

    [Serializable]
    private class Level
    {
        public int id;
        public string url;
        public string title;
        public int counter;
        public int counterMax;

        public Level(int _id, string _url, string _title, int _counter, int _counterMax)
        {
            id = _id;
            url = _url;
            title = _title;
            counter = _counter;
            counterMax = _counterMax;
        }
    }
}
