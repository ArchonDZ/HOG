using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
public class Screen : MonoBehaviour
{
    [SerializeField] private float durationTime = 0.5f;

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public Sequence DoScreenShow()
    {
        return DOTween.Sequence().Append(canvasGroup.DOFade(1f, durationTime)).PrependCallback(() => canvas.enabled = true);
    }

    public Sequence DoScreenHide()
    {
        return DOTween.Sequence().Append(canvasGroup.DOFade(0f, durationTime)).AppendCallback(() => canvas.enabled = false);
    }
}
