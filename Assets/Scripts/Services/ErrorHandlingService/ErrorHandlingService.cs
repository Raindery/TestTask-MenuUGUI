using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorHandlingService : MonoBehaviour, IErrorHandlingService
{
    [Header("Main")]
    [SerializeField] private CanvasGroup _errorDisplay;
    [SerializeField] private Button _closeButton;
    [Header("Popup")]
    [SerializeField] private RectTransform _popup;
    [SerializeField] private TMP_Text _popupText;

    private Sequence _cachedSequence;
    private bool _isErrorShowed;

    
    private void OnEnable()
    {
        _closeButton.onClick.AddListener(OnClickClose);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnClickClose);
    }
    

    public void ShowError(string text = null)
    {
        if (!_cachedSequence.IsActive())
        {
            var closeButtonTransform = _closeButton.transform;
            _popup.transform.localScale = Vector3.zero;
            closeButtonTransform.localScale = Vector3.zero;
            _errorDisplay.alpha = 0f;

            _cachedSequence = DOTween.Sequence()
                .AppendCallback(() => _errorDisplay.gameObject.SetActive(_isErrorShowed))
                .Append(_errorDisplay.DOFade(1f, 0.3f))
                .Append(_popup.DOScale(1f, 0.25f))
                .AppendInterval(0.5f)
                .Append(closeButtonTransform.DOScale(1f, 0.2f));
        }
        
        if (_popupText != null && text != null)
            _popupText.text = text;

        if (_isErrorShowed) 
            return;
        _isErrorShowed = !_isErrorShowed;
        _cachedSequence.PlayForward();
    }

    private void OnClickClose()
    {
        if(!_cachedSequence.IsActive())
            return;

        _isErrorShowed = !_isErrorShowed;
        _cachedSequence.PlayBackwards();
    }
}