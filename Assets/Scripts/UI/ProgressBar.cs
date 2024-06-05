using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressFillImage;

    public float FillAmount => progressFillImage.fillAmount;

    public void SetFillAmount(float amount)
    {
        progressFillImage.fillAmount = amount;
    }

    public void IncrementFillAmount(float amount)
    {
        progressFillImage.fillAmount += amount;
    }
}
