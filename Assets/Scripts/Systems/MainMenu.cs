using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Screen screen;
    [SerializeField] private LevelIcon levelIconPrefab;
    [SerializeField] private Transform levelIconParent;

    [Inject] private DiContainer diContainer;
    [Inject] private ContentLoader contentLoader;
    [Inject] private GameLevel gameLevel;

    private List<LevelIcon> levelIcons = new List<LevelIcon>();

    public Screen Screen => screen;

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
        screen.DoScreenHide().Append(gameLevel.Screen.DoScreenShow());
        gameLevel.StartLevel(chooseLevelData, contentLoader.LoadedSpritesDictionary[chooseLevelData.id]);
    }

    private void GameLevel_OnUpdateDataEvent(int id)
    {
        gameLevel.Screen.DoScreenHide().Append(screen.DoScreenShow());
        levelIcons.Find(x => x.Id.Equals(id)).UpdateProgressData();
        contentLoader.SaveProgress(id);
    }
}
