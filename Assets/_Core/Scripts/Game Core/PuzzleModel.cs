using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleModel : MonoBehaviour, IPuzzleModel
{
	public event Action OnPuzzleSolved;

	public int PuzzleSize => _puzzleSize;
	public Sprite[] Sprites => _sprites;
	public Sprite EmptySprite => _emptySprite;

	[SerializeField] private IPuzzleView _view;
	[SerializeField] private IPuzzleController _controller;
	[SerializeField, Min(3)] private int _puzzleSize;
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private Sprite _emptySprite;
	[SerializeField] private AudioClip _socketMoveAudio;
	[SerializeField] private float _socketMoveVolume;
	[SerializeField] private AudioClip _uiClickAudio;
	[SerializeField] private float _uiClickVolume;
	[SerializeField] private AudioClip _shuffleAudio;
	[SerializeField] private float _shuffleVolume;

	private int[,] _targetMatrix;
	private int[,] _playerMatrix;
	private float _animDuration = 0.1f;
	private List<int> _availableChips = new();
	private bool _hasShuffled = false;

	private IPuzzleMatrixFactory _matrixFactory;
	private IShuffleStrategy _shuffleStrategy;

	public void Init()
	{
		_view = DependencyInjector.Resolve<IPuzzleView>();
		_controller = DependencyInjector.Resolve<IPuzzleController>();
		_matrixFactory = DependencyInjector.Resolve<IPuzzleMatrixFactory>();
		_shuffleStrategy = DependencyInjector.Resolve<IShuffleStrategy>();

		_controller.IsEnabled = false;
		_targetMatrix = _matrixFactory.CreateTargetMatrix(_puzzleSize, _sprites, out _availableChips);
		_playerMatrix = _matrixFactory.CreateRandomPlayerMatrix(_puzzleSize, _availableChips);

		_view.CreatePlayerPuzzleField(_puzzleSize);
		_view.CreateAndDisplayTargetPuzzleField(_puzzleSize, _targetMatrix);
		_view.Display(_playerMatrix);

		StartCoroutine(_shuffleStrategy.Shuffle(15, _playerMatrix, Move, _animDuration, () =>
		{
			_animDuration = 0.2f;
			_controller.IsEnabled = true;
			_hasShuffled = true;
		}));
	}

	public void Move(int row, int col)
	{
		if (PuzzleUtils.IsAdjacentToEmptyCell(_playerMatrix, row, col, out int emptyRow, out int emptyCol))
		{
			SwapTiles(row, col, emptyRow, emptyCol);

			_view.AnimateMove(row, col, emptyRow, emptyCol, _playerMatrix[emptyRow, emptyCol], _animDuration);

			PlayMoveSound();

			if (IsPuzzleSolved())
			{
				OnPuzzleSolved?.Invoke();
				Debug.Log("Puzzle solved!");
			}
		}
		else
		{
			PlayClickSound();
		}
	}
	private void SwapTiles(int row, int col, int emptyRow, int emptyCol)
	{
		int temp = _playerMatrix[row, col];
		_playerMatrix[row, col] = _playerMatrix[emptyRow, emptyCol];
		_playerMatrix[emptyRow, emptyCol] = temp;
	}
	private void PlayMoveSound()
	{
		if (!_hasShuffled)
			GlobalSettings.SoundController.PlaySFX(_shuffleAudio, _shuffleVolume);
		else
			GlobalSettings.SoundController.PlaySFX(_socketMoveAudio, _socketMoveVolume, true);

	}
	private void PlayClickSound()
	{
		GlobalSettings.SoundController.PlaySFX(_uiClickAudio, _uiClickVolume, true);
	}
	private bool IsPuzzleSolved()
	{
		if (_playerMatrix.GetLength(0) != _targetMatrix.GetLength(0) || _playerMatrix.GetLength(1) != _targetMatrix.GetLength(1))
		{
			return false;
		}

		for (int i = 0; i < _playerMatrix.GetLength(0); i++)
		{
			for (int j = 0; j < _playerMatrix.GetLength(1); j++)
			{
				if (_playerMatrix[i, j] != _targetMatrix[i, j])
				{
					return false;
				}
			}
		}

		return true;
	}
}