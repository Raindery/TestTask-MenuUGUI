using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ColorsSublistItem : MenuList
{
    [SerializeField] private ColorMenuButton _menuItemPrefab;
    [SerializeField] private Image _arrowImage;
    [SerializeField] private Image _loadImage;
    
    private IPseudoAPI _pseudoApi;
    private IErrorHandlingService _errorHandlingService;
    private DiContainer _diContainer;
    
    
    [Inject]
    private void Construct(IPseudoAPI pseudoApi, IErrorHandlingService errorHandlingService,  DiContainer diContainer)
    {
        _pseudoApi = pseudoApi;
        _errorHandlingService = errorHandlingService;
        _diContainer = diContainer;
    }

    
    protected override void OnEnable()
    {
        base.OnEnable();
        ExpandToggled += OnExpandToggled;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ExpandToggled -= OnExpandToggled;

    }

    
    private void OnExpandToggled(bool isExpanded)
    {
        Vector3 rotateValue;
        if (isExpanded)
            rotateValue = Vector3.back * 180f;
        else
            rotateValue = Vector3.zero;

        _arrowImage.transform
            .DORotate(rotateValue, 0.25f, RotateMode.FastBeyond360)
            .SetAutoKill(true)
            .Play();
    }

    protected override async UniTask<bool> TryLoadListItems()
    {
        Tween loadTween = null;
        try
        {
            _arrowImage.gameObject.SetActive(false);
            loadTween = _loadImage.transform
                .DORotate(Vector3.forward * 360, 1f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear)
                .Play();
            _loadImage.gameObject.SetActive(true);
            Button.interactable = false;
            var colors = await _pseudoApi.GetColors();

            for (int i = 0; i < colors.ColorArray.Count; i++)
            {
                var colorData = colors.ColorArray[i];
                var colorButton = _diContainer.InstantiatePrefabForComponent<ColorMenuButton>(_menuItemPrefab,
                    ParentForElements.transform);
                colorButton.SetData(colorData);
                ChildrenElements.Add(colorButton);
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
            _arrowImage.gameObject.SetActive(true);
            _loadImage.transform.rotation = Quaternion.identity;
            Button.interactable = true;
        }
    }
}