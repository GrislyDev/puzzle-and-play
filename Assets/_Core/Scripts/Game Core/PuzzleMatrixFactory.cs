using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMatrixFactory : IPuzzleMatrixFactory
{
	private const int EMPTY = -1;

	public int[,] CreateTargetMatrix(int puzzleSize, Sprite[] sprites, out List<int> availableChips)
	{
		int totalCells = puzzleSize * puzzleSize;
		List<int> spriteIndices = new();
		availableChips = new List<int>();

		for (int i = 0; i < sprites.Length; i++)
		{
			spriteIndices.Add(i + 1);
		}

		while (spriteIndices.Count < totalCells - 1)
		{
			int randomValue = UnityEngine.Random.Range(1, sprites.Length + 1);
			spriteIndices.Add(randomValue);
		}

		System.Random rand = new System.Random();
		spriteIndices = ShuffleList(spriteIndices, rand);

		int[,] matrixArray = new int[puzzleSize, puzzleSize];
		int index = 0;
		for (int i = 0; i < puzzleSize; i++)
		{
			for (int j = 0; j < puzzleSize; j++)
			{
				if (index < spriteIndices.Count)
				{
					matrixArray[i, j] = spriteIndices[index];
					index++;
				}
			}
		}

		matrixArray[puzzleSize - 1, puzzleSize - 1] = EMPTY;


		availableChips.AddRange(spriteIndices);
		return matrixArray;
	}

	public int[,] CreateRandomPlayerMatrix(int puzzleSize, List<int> availableChips)
	{
		availableChips.Add(EMPTY);

		int[,] matrixArray = new int[puzzleSize, puzzleSize];
		System.Random rand = new System.Random();
		for (int i = 0; i < puzzleSize; i++)
		{
			for (int j = 0; j < puzzleSize; j++)
			{
				if (availableChips.Count > 0)
				{
					int randomIndex = rand.Next(availableChips.Count);
					matrixArray[i, j] = availableChips[randomIndex];
					availableChips.RemoveAt(randomIndex);
				}
				else
				{
					throw new InvalidOperationException("No more available chips to assign.");
				}
			}
		}
		return matrixArray;
	}

	private List<int> ShuffleList(List<int> list, System.Random rand)
	{
		for (int i = list.Count - 1; i > 0; i--)
		{
			int randomIndex = rand.Next(i + 1);
			int temp = list[i];
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
		return list;
	}
}
