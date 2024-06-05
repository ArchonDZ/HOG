using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressFillImage;

    public void SetFillAmount(float amount)
    {
        progressFillImage.fillAmount = amount;
    }
}
