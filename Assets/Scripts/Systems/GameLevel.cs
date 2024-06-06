using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLevel : MonoBehaviour
{
    public event Action<int> OnUpdateDataEvent;

    [SerializeField] private Screen screen;
    [SerializeField] private Button gameMainButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    private LevelData level;

    public Screen Screen => screen;

    void Awake()
    {
        gameMainButton.onClick.AddListener(PlayerClick);
        quitButton.onClick.AddListener(Quit);
    }

    public void StartLevel(LevelData _level, Sprite _sprite)
    {
        level = _level;
        gameMainButton.image.sprite = _sprite;
        UpdateCanvas();
    }

    private void PlayerClick()
    {
        if (level.counter + 1 <= level.counterMax)
        {
            level.counter++;
            UpdateCanvas();
            if (level.counter == level.counterMax)
            {
                OnUpdateDataEvent?.Invoke(level.id);
            }
        }
    }

    private void UpdateCanvas()
    {
        progressText.text = level.counter.ToString() + "/" + level.counterMax.ToString();
        progressBar.SetFillAmount((float)level.counter / level.counterMax);
    }

    private void Quit()
    {
        OnUpdateDataEvent?.Invoke(level.id);
    }
}
