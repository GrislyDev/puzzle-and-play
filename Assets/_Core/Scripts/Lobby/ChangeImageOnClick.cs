using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeImageOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private Image _targetImage;
	[SerializeField] private Sprite _onDownImage;
	[SerializeField] private Sprite _onUpImage;

	public void OnPointerDown(PointerEventData eventData)
	{
		_targetImage.sprite = _onDownImage;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_targetImage.sprite = _onUpImage; 
	}
}
