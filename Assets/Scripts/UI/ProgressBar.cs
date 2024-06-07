using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressFillImage;
    [SerializeField] private float durationEffect = 0.5f;

    public float FillAmount => progressFillImage.fillAmount;

    public void SetFillAmount(float amount)
    {
        DOVirtual.Float(FillAmount, amount, durationEffect, x => progressFillImage.fillAmount = x);
    }

    public void IncrementFillAmount(float amount)
    {
        DOVirtual.Float(FillAmount, progressFillImage.fillAmount + amount, durationEffect, x => progressFillImage.fillAmount = x);
    }
}
