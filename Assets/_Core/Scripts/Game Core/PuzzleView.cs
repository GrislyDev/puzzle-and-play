using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PuzzleView : MonoBehaviour, IPuzzleView
{
	public RectTransform PuzzleField => _puzzleField;
	public bool IsMoving => _isMoving;

	[SerializeField] private GridLayoutGroup _playerMatrix;
	[SerializeField] private GridLayoutGroup _targetMatrix;
	[SerializeField] private RectTransform _puzzleField;
	[SerializeField] private RectTransform _targetField;
	[SerializeField] private int _playerOffset;
	[SerializeField] private int _playerSpacing;
	[SerializeField] private int _matchSpacing;
	[SerializeField] private GameObject _cellPrefab;

	private IPuzzleModel _model;
	private List<GameObject> _cells;
	private List<GameObject> _targetCells;
	private Tween _tween;
	private bool _isMoving = false;

	public void Init()
	{
		_model = DependencyInjector.Resolve<IPuzzleModel>();
	}

	public void CreatePlayerPuzzleField(int size)
	{
		ConfigureGrid(_playerMatrix, _puzzleField, size, _playerOffset, _playerSpacing);

		_cells = new List<GameObject>();
		for (int i = 0; i < size * size; i++)
		{
			GameObject cell = Instantiate(_cellPrefab, _puzzleField);
			_cells.Add(cell);
		}
	}

	public void CreateAndDisplayTargetPuzzleField(int size, int[,] matrix)
	{
		ConfigureGrid(_targetMatrix, _targetField, size, _matchSpacing, _matchSpacing);

		_targetCells = new List<GameObject>();
		for (int i = 0; i < size * size; i++)
		{
			GameObject cell = Instantiate(_cellPrefab, _targetField);
			_targetCells.Add(cell);
		}

		UpdateCells(matrix, _targetCells);
	}

	public void Display(int[,] matrix)
	{
		UpdateCells(matrix, _cells);
	}

	private void ConfigureGrid(GridLayoutGroup grid, RectTransform field, int size, int offset, int spacing)
	{
		float maxFieldSize = Mathf.Max(field.rect.width, field.rect.height);
		float cellSize = Mathf.Floor((maxFieldSize - (offset * (size - 1))) / size);

		grid.cellSize = new Vector2(cellSize, cellSize);
		grid.spacing = new Vector2(spacing, spacing);
		grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		grid.constraintCount = size;
	}

	private void UpdateCells(int[,] matrix, List<GameObject> cells)
	{
		int size = matrix.GetLength(0);
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				int index = i * size + j;
				Image cellImage = cells[index].GetComponent<Image>();
				int value = matrix[i, j];
				cellImage.sprite = value == -1 ? _model.EmptySprite : _model.Sprites[value - 1];
			}
		}
	}

	public void AnimateMove(int fromRow, int fromCol, int toRow, int toCol, int movedValue, float animDuration)
	{
		int size = _model.PuzzleSize;
		int fromIndex = fromRow * size + fromCol;
		int toIndex = toRow * size + toCol;

		GameObject fromCell = _cells[fromIndex];
		GameObject toCell = _cells[toIndex];

		Vector3 fromPosition = fromCell.transform.position;
		Vector3 toPosition = toCell.transform.position;

		_tween?.Kill();
		_tween = fromCell.transform.DOMove(toPosition, animDuration)
			.OnStart(() => _isMoving = true)
			.OnKill(() =>
			{
				Image fromImage = fromCell.GetComponent<Image>();
				Image toImage = toCell.GetComponent<Image>();

				fromImage.sprite = _model.EmptySprite;
				toImage.sprite = _model.Sprites[movedValue - 1];
				fromCell.transform.position = fromPosition;
				_isMoving = false;
			});
	}

	private void OnDestroy()
	{
		_tween?.Kill();
	}
}
