using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelIcon levelIconPrefab;
    [SerializeField] private Transform levelIconParent;

    [Inject] private DiContainer diContainer;
    [Inject] private ContentLoader contentLoader;

    private List<LevelIcon> levelIcons = new List<LevelIcon>();

    void Start()
    {
        contentLoader.OnCompleteLoadingEvent += ContentLoader_OnCompleteLoadingEvent;
    }

    private void ContentLoader_OnCompleteLoadingEvent()
    {
        for (int i = 0; i < contentLoader.Levels.Count; i++)
        {
            levelIcons.Add(diContainer.InstantiatePrefabForComponent<LevelIcon>(levelIconPrefab, levelIconParent));
            levelIcons[^1].Initialize(i + 1, contentLoader.Levels[i]);
        }
    }
}
