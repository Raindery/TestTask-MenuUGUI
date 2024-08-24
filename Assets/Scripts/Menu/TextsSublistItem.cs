using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TextsSublistItem : MenuList
{
    [SerializeField] private TextMenuButton _menuItemPrefab;
    [SerializeField] private Image _arrowImage;
    [SerializeField] private Image _loadImage;
    
    private Sequence _cachedSequence;
    private IPseudoAPI _pseudoApi;
    private IErrorHandlingService _errorHandlingService;
    private DiContainer _diContainer;
    
    
    [Inject]
    private void Construct(IPseudoAPI pseudoApi, IErrorHandlingService errorHandlingService, DiContainer diContainer)
    {
        _pseudoApi = pseudoApi;
        _errorHandlingService = errorHandlingService;
        _diContainer = diContainer;
    }


    public override async void ExpandToggle()
    {
        if (!ChildrenElements.Any())
        {
            Tween loadTween = null;
            try
            {
                Button.interactable = false;
                _arrowImage.gameObject.SetActive(false);
                loadTween = _loadImage.transform
                    .DORotate(Vector3.forward * 360, 1f, RotateMode.FastBeyond360)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear)
                    .Play();
                _loadImage.gameObject.SetActive(true);

                var texts = await _pseudoApi.GetTexts();
                for (int i = 0; i < texts.TextArray.Count; i++)
                {
                    var textData = texts.TextArray[i];
                    var textButton = _diContainer.InstantiatePrefabForComponent<TextMenuButton>(_menuItemPrefab,
                        ParentForElements.transform);
                    textButton.SetData(textData);
                    _childrenElements.Add(textButton);
                }
            }
            catch
            {
                _errorHandlingService.ShowError();
                return;
            }
            finally
            {
                _loadImage.gameObject.SetActive(false);
                if(loadTween.IsActive())
                    loadTween.Kill();
                _loadImage.transform.rotation = Quaternion.identity;
                _arrowImage.gameObject.SetActive(true);
                Button.interactable = true;
            }
        }
        
        if (!_cachedSequence.IsActive())
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                ParentForElements.gameObject.SetActive(IsExpanded);
            });           
            sequence.Append(ParentForElements.transform.DOScaleY(1f, 0.3f));
            sequence.Join(_arrowImage.transform.DORotate(Vector3.forward * 180f, 0.2f));
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

            _cachedSequence = sequence;
        }
        
        if(IsExpanded)
            _cachedSequence.PlayBackwards();
        else
            _cachedSequence.PlayForward();
        IsExpanded = !IsExpanded;
    }
}