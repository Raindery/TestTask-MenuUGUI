using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : BaseMenuItem
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    
    public override TMP_Text Text => _text;
    public override Button Button => _button;
}