using UnityEngine.UI;
using UnityEngine;
using GrislyTools;

public class SettingsController : MonoBehaviour
{
	[SerializeField] private Button _openButton;
	[SerializeField] private Button _closeButton;
	[SerializeField] private PuzzleTimer _timer;

	[Header("Buttons")]
	[SerializeField] private Button _musicButton;
	[SerializeField] private Button _soundButton;
	[SerializeField] private Button _tapticButton;

	[Header("Toggle anim")]
	[SerializeField] private SettingButtonAnimToggle _musicToggle;
	[SerializeField] private SettingButtonAnimToggle _soundToggle;
	[SerializeField] private SettingButtonAnimToggle _tapticToggle;

	private void Awake()
	{
		Initialize();
		_openButton.onClick.AddListener(OpenButtonHandler);
		_closeButton.onClick.AddListener(CloseButtonHandler);
		gameObject.SetActive(false);
	}
	private void Initialize()
	{
		_musicButton.onClick.AddListener(OnClickMusicHandler);
		_soundButton.onClick.AddListener(OnClickSoundHandler);
		_tapticButton.onClick.AddListener(OnClickTapticHandler);

		DataManager.Data.GetValue("Music", out bool music);
		DataManager.Data.GetValue("Sound", out bool sound);
		DataManager.Data.GetValue("Taptic", out bool taptic);

		_musicToggle.Initialize(music);
		_soundToggle.Initialize(sound);
		_tapticToggle.Initialize(taptic);

		SetMusicActivity(music);
		SetSoundActivity(sound);
	}
	#region HANDLERS
	private void OnClickMusicHandler()
	{
		DataManager.Data.GetValue("Music", out bool music);
		DataManager.Data.SetValue("Music", !music);

		SetMusicActivity(!music);
	}
	private void OnClickSoundHandler()
	{
		DataManager.Data.GetValue("Sound", out bool sound);
		DataManager.Data.SetValue("Sound", !sound);

		SetSoundActivity(!sound);

	}
	private void OnClickTapticHandler()
	{
		DataManager.Data.GetValue("Taptic", out bool taptic);
		DataManager.Data.SetValue("Taptic", !taptic);
	}
	#endregion

	private void SetMusicActivity(bool enable)
	{
		if (enable)
			GlobalSettings.SoundController.UnmuteMusic();
		else
			GlobalSettings.SoundController.MuteMusic();
	}
	private void SetSoundActivity(bool enable)
	{
		if (enable)
			GlobalSettings.SoundController.UnmuteAllSFXSources();
		else
			GlobalSettings.SoundController.MuteAllSFXSources();
	}
	private void OpenButtonHandler()
	{
		if (_timer != null)
			_timer.IsTimerActive = false;

		Time.timeScale = 0;
		gameObject.SetActive(true);
	}
	private void CloseButtonHandler()
	{
		if (_timer != null)
			_timer.IsTimerActive = true;

		Time.timeScale = 1;
		gameObject.SetActive(false);
	}
	private void OnDestroy()
	{
		_openButton.onClick.RemoveAllListeners();
		_closeButton.onClick.RemoveAllListeners();
	}
}
