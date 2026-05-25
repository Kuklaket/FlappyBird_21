using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private int _waveCount = 0;

    private void Start()
    {
        UpdateText();
    }

    public void AddPoints()
    {
        _waveCount++;
        UpdateText();
    }

    public void ResetCount()
    {
        _waveCount = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        if (_text != null)
            _text.text = _waveCount.ToString();
    }
}