using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreUI : MonoBehaviour
{
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (ScoreManager.instance != null)
        {
            text.text = "Score: " + ScoreManager.instance.amount;
        }
    }
}
