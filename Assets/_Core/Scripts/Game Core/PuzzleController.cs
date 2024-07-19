using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleController : MonoBehaviour, IPuzzleController, IPointerClickHandler
{
	public bool IsEnabled { get; set; }

	private IPuzzleView _view;
	private IPuzzleModel _model;

	public void Init()
	{
		_view = DependencyInjector.Resolve<IPuzzleView>();
		_model = DependencyInjector.Resolve<IPuzzleModel>();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_view.IsMoving || !IsEnabled)
			return;

		Vector2 localPoint;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_view.PuzzleField, eventData.position, eventData.pressEventCamera, out localPoint))
		{
			int size = _model.PuzzleSize;
			float cellSize = _view.PuzzleField.rect.width / size;

			int col = Mathf.Clamp(Mathf.FloorToInt(localPoint.x / cellSize), 0, size - 1);
			int row = Mathf.Clamp(Mathf.FloorToInt(-localPoint.y / cellSize), 0, size - 1);

			_model.Move(row, col);
		}
	}
}
