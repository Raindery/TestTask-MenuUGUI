using Cysharp.Threading.Tasks;
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
    
    
    protected override async UniTask<bool> TryLoadListItems()
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
                ChildrenElements.Add(textButton);
            }

            return true;
        }
        catch
        {
            _errorHandlingService.ShowError();
            return false;
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
}