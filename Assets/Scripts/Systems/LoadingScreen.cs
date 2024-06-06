using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Screen screen;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private TextMeshProUGUI statusTextMeshPro;

    private float step = 1f;

    public void ScreenShow()
    {
        screen.DoScreenShow();
        ResetProgress();
    }

    public void ScreenHide()
    {
        screen.DoScreenHide();
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
}
