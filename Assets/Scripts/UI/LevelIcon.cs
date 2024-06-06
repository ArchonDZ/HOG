using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    public event Action<LevelData> OnChooseLevelEvent;

    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Button playButton;
    [SerializeField] private Image statusImage;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI progressText;

    [Header("References")]
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite completeSprite;

    [Header("Settings")]
    [SerializeField] private float endScaleClick = 1.05f;
    [SerializeField] private float durationEffects = 0.5f;

    private bool isInitialized;
    private bool isReady;
    private bool isShake;
    private LevelData level;

    public int Id => level.id;

    public void Initialize(int _number, LevelData _level, bool _ready)
    {
        isInitialized = true;
        level = _level;
        isReady = _ready;
        numberText.text = _number.ToString();
        titleText.text = level.title;
        UpdateProgressData();
    }

    public void UpdateProgressData()
    {
        if (!isInitialized) return;

        progressText.text = level.counter.ToString() + "/" + level.counterMax.ToString();
        progressBar.SetFillAmount((float)level.counter / level.counterMax);
        playButton.onClick.RemoveAllListeners();

        if (!isReady)
        {
            statusImage.sprite = lockSprite;
            playButton.onClick.AddListener(Shake);
        }
        else
        {
            if (level.counter == level.counterMax)
            {
                statusImage.sprite = completeSprite;
                playButton.onClick.AddListener(Shake);
            }
            else
            {
                statusImage.sprite = playSprite;
                playButton.onClick.AddListener(Play);
            }
        }
    }

    private void Play()
    {
        DOTween.Sequence()
            .Append(rectTransform.DOScale(Vector3.one * endScaleClick, durationEffects / 2))
            .Append(rectTransform.DOScale(Vector3.one, durationEffects / 2));

        OnChooseLevelEvent?.Invoke(level);
    }

    private void Shake()
    {
        if (!isShake)
        {
            isShake = true;
            rectTransform.DOShakeAnchorPos(durationEffects, 50f).OnComplete(() => isShake = false);
        }
    }
}
