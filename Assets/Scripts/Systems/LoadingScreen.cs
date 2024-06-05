using DG.Tweening;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float durationTime = 0.5f;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI statusTextMeshPro;

    private float step = 1f;

    public void ScreenShow()
    {
        DoScreenShow();
        ResetProgress();
    }

    public void ScreenHide()
    {
        DoScreenHide();
    }

    public void SetFixedStepLoading(float _step)
    {
        step = _step;
    }

    public void IncrementLoadingByStep()
    {
        progressBar.IncrementFillAmount(step);
    }

    public void ScreenLoading(float progress)
    {
        progressBar.SetFillAmount(progress);
    }

    public void SetTextStatus(string status)
    {
        statusTextMeshPro.text = status;
    }

    private void ResetProgress()
    {
        progressBar.SetFillAmount(0f);
        statusTextMeshPro.text = string.Empty;
        step = 1f;
    }

    private Sequence DoScreenShow()
    {
        return DOTween.Sequence().Append(canvasGroup.DOFade(1f, durationTime)).PrependCallback(() => canvas.enabled = true);
    }

    private Sequence DoScreenHide()
    {
        return DOTween.Sequence().Append(canvasGroup.DOFade(0f, durationTime)).AppendCallback(() => canvas.enabled = false);
    }
}
