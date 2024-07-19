using UnityEngine;
using System;

public interface IPuzzleModel
{
	event Action OnPuzzleSolved;
	int PuzzleSize { get; }
	Sprite[] Sprites { get; }
	Sprite EmptySprite { get; }
	void Move(int row, int col);
}