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

    public void ScreenShow()
    {
        DoScreenShow();
    }

    public void ScreenHide()
    {
        DoScreenHide();
    }

    protected Sequence DoScreenShow()
    {
        return DOTween.Sequence().Append(canvasGroup.DOFade(1f, durationTime)).PrependCallback(() => canvas.enabled = true);
    }

    protected Sequence DoScreenHide()
    {
        return DOTween.Sequence().Append(canvasGroup.DOFade(0f, durationTime)).AppendCallback(() => canvas.enabled = false);
    }
}
