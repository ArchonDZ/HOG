using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

[Serializable]
public class Content
{
    public List<LevelData> levels = new List<LevelData>(20);
}

[Serializable]
public class LevelData
{
    public int id;
    public string url;
    public string title;
    public int counter;
    public int counterMax;

    public LevelData(int _id, string _url, string _title, int _counter, int _counterMax)
    {
        id = _id;
        url = _url;
        title = _title;
        counter = _counter;
        counterMax = _counterMax;
    }
}

public class ContentLoader : IInitializable
{
    public event Action OnCompleteLoadingEvent;
    public event Action OnAbortLoadingEvent;

    [Inject] private SaveSystem saveSystem;
    [Inject] private LoadingScreen loadingScreen;

    private const string pathToJson = "https://raw.githubusercontent.com/ArchonDZ/HOG/dev/Resources/hog_levels.json";
    private const string fileSave = "saves.dat";

    private Content content;
    private Dictionary<int, Sprite> loadedSpritesDictionary = new Dictionary<int, Sprite>(20);
    private SaveData saveData;
    private List<AsyncOperation> asyncOperations = new List<AsyncOperation>(20);

    public List<LevelData> Levels => content.levels;
    public Dictionary<int, Sprite> LoadedSpritesDictionary => loadedSpritesDictionary;

    public async void Initialize()
    {
        using (UnityWebRequest levelConfigWebRequest = UnityWebRequest.Get(pathToJson))
        {
            UnityWebRequestAsyncOperation asyncOperation = levelConfigWebRequest.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                loadingScreen.ScreenLoading(asyncOperation.progress);
                await Task.Yield();
            }

            if (levelConfigWebRequest.result != UnityWebRequest.Result.Success)
            {
                loadingScreen.SetTextStatus(levelConfigWebRequest.result.ToString());
                OnAbortLoadingEvent?.Invoke();
            }
            else
            {
                content = JsonUtility.FromJson<Content>(levelConfigWebRequest.downloadHandler.text);
                loadingScreen.SetFixedStepLoading(1f / content.levels.Count);
                for (int i = 0; i < content.levels.Count; i++)
                {
                    CreateRequestTexture(i);
                }

                while (!asyncOperations.TrueForAll(x => x.isDone))
                {
                    await Task.Yield();
                }

                LoadProgress();
                asyncOperations.Clear();
                loadingScreen.ScreenHide();
                OnCompleteLoadingEvent?.Invoke();
            }
        }
    }

    #region SaveSystem
    public void SaveProgress(int id)
    {
        saveData ??= new SaveData();
        LevelData levelData = content.levels.Find(x => x.id.Equals(id));
        if (levelData != null)
        {
            Data data = saveData.datas.Find(x => x.id.Equals(id));
            if (data != null)
            {
                data.counter = levelData.counter;
            }
            else
            {
                saveData.datas.Add(new Data(id, levelData.counter));
            }
            saveSystem.Save(fileSave, saveData);
        }
    }

    private void LoadProgress()
    {
        saveData = saveSystem.Load(fileSave);
        if (saveData != null)
        {
            for (int i = 0; i < saveData.datas.Count; i++)
            {
                LevelData levelData = content.levels.Find(x => x.id.Equals(saveData.datas[i].id));
                if (levelData != null)
                {
                    levelData.counter = saveData.datas[i].counter;
                }
            }
        }
    }
    #endregion

    private async void CreateRequestTexture(int index)
    {
        using (UnityWebRequest textureWebRequest = UnityWebRequestTexture.GetTexture(content.levels[index].url))
        {
            UnityWebRequestAsyncOperation asyncOperation = textureWebRequest.SendWebRequest();
            asyncOperation.completed += (_) => loadingScreen.IncrementLoadingByStep();
            asyncOperations.Add(asyncOperation);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (textureWebRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)textureWebRequest.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                loadedSpritesDictionary.Add(content.levels[index].id, sprite);
            }
        }
    }
}