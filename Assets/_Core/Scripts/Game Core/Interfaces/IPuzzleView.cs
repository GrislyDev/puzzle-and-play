using UnityEngine;

public interface IPuzzleView
{
	RectTransform PuzzleField { get; }
	bool IsMoving { get; }
	void CreatePlayerPuzzleField(int size);
	void AnimateMove(int fromRow, int fromCol, int toRow, int toCol, int movedValue, float animDuration);
	public void CreateAndDisplayTargetPuzzleField(int size, int[,] matrix);
	public void Display(int[,] matrix);
}