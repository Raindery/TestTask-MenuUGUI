using System.Linq;
using DG.Tweening;
using UnityEngine;

public class RootMenuItem: MenuList
{
    [SerializeField] private BaseMenuItem[] _rootMenuItems;
    private Sequence _cachedSequence;


    private void Awake()
    {
        _childrenElements.AddRange(_rootMenuItems);
    }
    

    public override void ExpandToggle()
    {
        if (!_cachedSequence.IsActive())
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                ParentForElements.gameObject.SetActive(IsExpanded);
            });           
            sequence.Append(ParentForElements.transform.DOScaleY(1f, 0.25f));
            for (int i = 0; i < _rootMenuItems.Length; i++)
            {
                var rootMenuItem = _rootMenuItems[i];
            
                rootMenuItem.transform.localScale = new Vector3(0, 1);
                rootMenuItem.gameObject.SetActive(true);
                if (i == 0)
                    sequence.Append(rootMenuItem.transform.DOScaleX(1f, 0.3f));
                else
                    sequence.Join(rootMenuItem.transform.DOScaleX(1f, 0.3f));
            }

            _cachedSequence = sequence;
        }

        if (IsExpanded)
        {
            var expandableChildrenElements = ChildrenElements.OfType<MenuList>();
            foreach (var expandableChildrenElement in expandableChildrenElements)
            {
                if (expandableChildrenElement.IsExpanded)
                    expandableChildrenElement.ExpandToggle();
            }
            _cachedSequence.PlayBackwards();
        }
        else
        {
            _cachedSequence.PlayForward();
        }

        IsExpanded = !IsExpanded;
    }
}