using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Transform _targetTransform;
	[SerializeField] private float _startScale = 1f;
	[SerializeField] private float _endScale = 1.1f;
	[SerializeField] private float _duration;

	private Tween _tween;

	public void OnPointerEnter(PointerEventData eventData)
	{
		_tween?.Kill();
		_tween = _targetTransform.DOScale(_endScale, _duration)
			.SetEase(Ease.InOutQuad);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_tween?.Kill();
		_tween = _targetTransform.DOScale(_startScale, _duration)
			.SetEase(Ease.InOutQuad);
	}
}
