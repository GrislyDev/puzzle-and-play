using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SettingButtonAnimToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private Image _toggleImage;
	[SerializeField] private Sprite _onToggleSprite;
	[SerializeField] private Sprite _pressedSprite;
	[SerializeField] private Sprite _offToggleSprite;

	private bool _isActive = false;

	public void Initialize(bool enable)
	{
		_isActive = enable;
		_toggleImage.sprite = enable ? _onToggleSprite : _offToggleSprite;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_toggleImage.sprite = _pressedSprite;
		_isActive = !_isActive;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_toggleImage.sprite = _isActive ? _onToggleSprite : _offToggleSprite;
	}
}
