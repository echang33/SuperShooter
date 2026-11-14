using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WavesUI : MonoBehaviour
{
    private TMP_Text text;
    private WaveManager manager;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        // All managers have completed Awake before Start runs.
        manager = WaveManager.instance;
        OnEnable();
    }

    void OnEnable()
    {
        if (manager != null)
        {
            manager.onChanged.AddListener(RefreshText);
            RefreshText();
        }
    }

    void OnDisable()
    {
        if (manager != null)
        {
            manager.onChanged.RemoveListener(RefreshText);
        }
    }

    void RefreshText()
    {
        text.text = "Remaining Waves: " + manager.waves.Count;
    }
}
