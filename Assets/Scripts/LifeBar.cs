using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBar : MonoBehaviour
{
    public Life targetLife;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void LateUpdate()
    {
        image.fillAmount = targetLife != null && targetLife.MaxAmount > 0f
            ? Mathf.Clamp01(targetLife.amount / targetLife.MaxAmount)
            : 0f;
    }
}
