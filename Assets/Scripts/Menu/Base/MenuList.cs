using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuList : BaseMenuItem
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    [SerializeField] private CanvasGroup _parentForElements;

    protected readonly List<BaseMenuItem> _childrenElements = new List<BaseMenuItem>();
    
    public override TMP_Text Text => _text;
    public override Button Button => _button;
    public CanvasGroup ParentForElements => _parentForElements;
    public IReadOnlyList<BaseMenuItem> ChildrenElements => _childrenElements; 
    public bool IsExpanded { get; protected set; }


    private void OnEnable()
    {
        _button.onClick.AddListener(ExpandToggle);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ExpandToggle);
    }


    public abstract void ExpandToggle();
}