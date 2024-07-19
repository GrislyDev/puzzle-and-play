using UnityEngine;
using UnityEngine.UI;

public class ReloadCurrentSceneButton : MonoBehaviour
{
	[SerializeField] private Button _button;

	private void Start()
	{
		_button.onClick.AddListener(ReloadScene);
	}
	private void ReloadScene()
	{
		GlobalSettings.SceneLoader.ReloadScene();
	}
	private void OnDestroy()
	{
		_button.onClick.RemoveAllListeners();
	}
}
