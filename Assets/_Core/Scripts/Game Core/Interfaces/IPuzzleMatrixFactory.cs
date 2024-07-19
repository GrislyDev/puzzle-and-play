using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleMatrixFactory
{
	int[,] CreateTargetMatrix(int puzzleSize, Sprite[] sprites, out List<int> availableChips);
	int[,] CreateRandomPlayerMatrix(int puzzleSize, List<int> availableChips);
}