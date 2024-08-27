using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public abstract class MenuList : BaseMenuItem
{
    [SerializeField] private CanvasGroup _parentForElements;
    [SerializeField] private List<BaseMenuItem> _childrenElements = new List<BaseMenuItem>();
    
    private Sequence _cachedExpandSequence;

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
            for (int i = 0; i < _childrenElements.Count; i++)
            {
                var rootMenuItem = _childrenElements[i];
            
                rootMenuItem.transform.localScale = new Vector3(0, 1);
                rootMenuItem.gameObject.SetActive(true);
                if (i == 0)
                    sequence.Append(rootMenuItem.transform.DOScaleX(1f, 0.3f));
                else
                    sequence.Join(rootMenuItem.transform.DOScaleX(1f, 0.3f));
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
    }

    protected override async void OnClick()
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