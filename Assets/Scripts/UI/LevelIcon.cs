using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelIcon : MonoBehaviour
{
    [Header("Components")]
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

    [Inject] private ContentLoader loader;

    public void Initialize(int number, Level level)
    {
        numberText.text = number.ToString();
        titleText.text = level.title;
        progressText.text = level.counter.ToString() + "/" + level.counterMax.ToString();
        progressBar.SetFillAmount((float)level.counter / level.counterMax);

        if (level.counter == level.counterMax)
        {
            statusImage.sprite = completeSprite;
        }
        else if (loader.LoadedSpritesDictionary.ContainsKey(level.id))
        {
            statusImage.sprite = playSprite;
        }
        else
        {
            statusImage.sprite = lockSprite;
            playButton.interactable = false;
        }
    }
}
