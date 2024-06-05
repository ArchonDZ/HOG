using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
}
