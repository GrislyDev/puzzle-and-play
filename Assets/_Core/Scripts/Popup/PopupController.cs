using DG.Tweening;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] private Transform _popupTransform;
    [SerializeField] private Transform _popupImage;
    [SerializeField] private AudioClip _popupClip;
    [SerializeField] private float _popupVolume;
    [SerializeField] private float _startScale = 0.2f;

    private Tween _tween;

    private void Awake()
    {
        ClosePopup();
    }

    public void StartPopup(float animDuration, float endScale)
    {
        _popupTransform.gameObject.SetActive(true);
        GlobalSettings.SoundController.PauseMusic();
        GlobalSettings.SoundController.PlaySFX(_popupClip, _popupVolume, true);

        _tween?.Kill();

        _tween = _popupImage.DOScale(endScale, animDuration);
    }
    public void ClosePopup()
    {
        GlobalSettings.SoundController.UnpauseMusic();
        _tween?.Kill();
        _tween = _popupImage.DOScale(_startScale, 0);

        _popupTransform.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _tween?.Kill();
        _tween = null;
    }
}