using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuList : BaseMenuItem
{
    [SerializeField] private CanvasGroup _parentForElements;
    [SerializeField] private List<BaseMenuItem> _childrenElements = new List<BaseMenuItem>();
    
    
    private Sequence _cachedExpandSequence;

    public event Action<bool> ExpandToggled;
    public CanvasGroup ParentForElements => _parentForElements;
    public List<BaseMenuItem> ChildrenElements => _childrenElements; 
    public bool IsExpanded { get; protected set; }


    private void ExpandToggle()
    {
        if (!_cachedExpandSequence.IsActive())
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                ParentForElements.gameObject.SetActive(IsExpanded);
            });           
            sequence.Append(ParentForElements.transform.DOScaleY(1f, 0.25f));
            sequence.AppendCallback(() => { ParentForElements.interactable = IsExpanded; });

            if (transform is RectTransform rectTransform)
            {
                sequence.OnUpdate(() =>
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform));
            }
            _cachedExpandSequence = sequence;
            
        }

        if (IsExpanded)
        {
            var expandableChildrenElements = ChildrenElements.OfType<MenuList>();
            foreach (var expandableChildrenElement in expandableChildrenElements)
            {
                if (expandableChildrenElement.IsExpanded)
                    expandableChildrenElement.ExpandToggle();
            }
            _cachedExpandSequence.PlayBackwards();
        }
        else
        {
            _cachedExpandSequence.PlayForward();
        }

        IsExpanded = !IsExpanded;
        ExpandToggled?.Invoke(IsExpanded);
    }

    protected sealed override async void OnClick()
    {
        bool isListItemsLoaded;
        if (!_childrenElements.Any())
            isListItemsLoaded = await TryLoadListItems();
        else
            isListItemsLoaded = true;
        
        if(isListItemsLoaded)
            ExpandToggle();
    }

    protected abstract UniTask<bool> TryLoadListItems();
}