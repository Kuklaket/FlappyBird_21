using System;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _button;

    public event Action ButtonClicked;

    protected CanvasGroup WindowGroup => _windowGroup;
    protected Button Button => _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(() => ButtonClicked?.Invoke());
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public virtual void Open()
    {
        WindowGroup.alpha = 1f;
        Button.interactable = true;
        WindowGroup.blocksRaycasts = true;
    }

    public virtual void Close()
    {
        WindowGroup.alpha = 0f;
        Button.interactable = false;
        WindowGroup.blocksRaycasts = false;
    }
}