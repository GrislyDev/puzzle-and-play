using DG.Tweening;
using UnityEngine;

public class TittleEffect : MonoBehaviour
{
	[SerializeField] private Transform _effectTransform;
	[SerializeField] private Transform _tittleTransform;
	[SerializeField] private Transform _firstCharacter;
	[SerializeField] private Transform _secondCharacter;
	[SerializeField] private Transform _thirdCharacter;

	private Tween _tween;
	private Tween _tween2;
	private Sequence _sequence;
	private Vector3 _fullRotate = new Vector3(0, 0, 360f);
	private float _rotateSpeed = 10f;
	private float _movePosY;
	private float _offset = 25f;
	private float _moveDuration = 1f;
	private float _endScale = 1.3f;
	private float _scaleDuration = 0.8f;

	private void Start()
	{
		_movePosY = _tittleTransform.localPosition.y + _offset;
		_sequence = DOTween.Sequence();

		KillTween();

		_tween = _effectTransform.DOLocalRotate(_fullRotate, _rotateSpeed, RotateMode.FastBeyond360)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear);

		_tween2 = _tittleTransform.DOLocalMoveY(_movePosY, _moveDuration)
			.SetLoops(-1, LoopType.Yoyo);

		float normalScale = 1f;

		_sequence.Append(_firstCharacter.DOScale(_endScale, _scaleDuration));
		_sequence.Append(_firstCharacter.DOScale(normalScale, _scaleDuration));
		_sequence.Append(_secondCharacter.DOScale(_endScale, _scaleDuration));
		_sequence.Append(_secondCharacter.DOScale(normalScale, _scaleDuration));
		_sequence.Append(_thirdCharacter.DOScale(_endScale, _scaleDuration));
		_sequence.Append(_thirdCharacter.DOScale(normalScale, _scaleDuration));
		_sequence.SetLoops(-1, LoopType.Restart);
	}
	private void KillTween()
	{
		_tween?.Kill();
		_tween2?.Kill();

	}
	private void OnDestroy()
	{
		KillTween();
		_sequence?.Kill();
		_sequence = null;
	}
}
