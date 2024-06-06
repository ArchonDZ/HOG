using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelIcon levelIconPrefab;
    [SerializeField] private Transform levelIconParent;

    [Inject] private DiContainer diContainer;
    [Inject] private ContentLoader contentLoader;
    [Inject] private GameLevel gameLevel;

    private List<LevelIcon> levelIcons = new List<LevelIcon>();

    void Start()
    {
        contentLoader.OnCompleteLoadingEvent += ContentLoader_OnCompleteLoadingEvent;
        gameLevel.OnUpdateDataEvent += GameLevel_OnUpdateDataEvent;
    }

    private void ContentLoader_OnCompleteLoadingEvent()
    {
        for (int i = 0; i < contentLoader.Levels.Count; i++)
        {
            levelIcons.Add(diContainer.InstantiatePrefabForComponent<LevelIcon>(levelIconPrefab, levelIconParent));
            levelIcons[^1].Initialize(i + 1, contentLoader.Levels[i], contentLoader.LoadedSpritesDictionary.ContainsKey(contentLoader.Levels[i].id));
            levelIcons[^1].OnChooseLevelEvent += LevelIcon_OnChooseLevelEvent;
        }
    }

    private void LevelIcon_OnChooseLevelEvent(LevelData chooseLevelData)
    {
        gameLevel.StartLevel(chooseLevelData, contentLoader.LoadedSpritesDictionary[chooseLevelData.id]);
    }

    private void GameLevel_OnUpdateDataEvent(int id)
    {
        levelIcons.Find(x => x.Id.Equals(id)).UpdateProgressData();
    }
}
