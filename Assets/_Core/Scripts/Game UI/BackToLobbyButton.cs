using DG.Tweening;
using GrislyTools;
using UnityEngine;
using UnityEngine.UI;

public class BackToLobbyButton : MonoBehaviour
{
	[SerializeField] private Button _button;

	private const string LOBBY_SCENE = "Lobby";

	private void Start()
	{
		_button.onClick.AddListener(BackToLobby);
	}
	private void BackToLobby()
	{
		DOTween.KillAll();
		DataManager.Data.GetValue("CurrentScene", out string scene);
		GlobalSettings.SceneLoader.UnloadScene(scene);
		GlobalSettings.SceneLoader.LoadScene(LOBBY_SCENE);
		GlobalSettings.SceneLoader.AllowSceneActivation();
	}
	private void OnDestroy()
	{
		_button.onClick?.RemoveListener(BackToLobby); 
	}
}
