using UnityEngine;
using UnityEngine.UI;


public class UIClickSound : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private float _volume;

    private void Awake()
    {
        _button.onClick.AddListener(PlaySoundOnClick);
    }

    private void PlaySoundOnClick()
    {
        GlobalSettings.SoundController.PlaySFX(_audioClip, _volume, true);
    }

    private void OnDestroy()
    {
        _button?.onClick.RemoveListener(PlaySoundOnClick);
    }
}
