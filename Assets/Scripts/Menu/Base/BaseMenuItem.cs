using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenuItem : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    
    public TMP_Text Text => _text;
    public Button Button => _button;

    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();
}