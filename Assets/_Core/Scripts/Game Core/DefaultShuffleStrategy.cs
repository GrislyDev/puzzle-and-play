using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class DefaultShuffleStrategy : IShuffleStrategy
{
	private const int EMPTY = -1;

	public IEnumerator Shuffle(int movesCount, int[,] playerMatrix, Action<int, int> move, float animDuration, Action enableController)
	{
		float animDurationCoef = 1.3f;
		float startDelay = 0.5f;
		(int, int) lastMovedIndex = (-1, -1);

		yield return new WaitForSeconds(startDelay);

		for (int i = 0; i < movesCount; i++)
		{
			bool isMoved = false;
			while (!isMoved)
			{
				(int, int) emptyIndex = playerMatrix.IndexOf(EMPTY);

				List<(int, int)> possibleMoves = new()
				{
					(emptyIndex.Item1 + 1, emptyIndex.Item2),
					(emptyIndex.Item1 - 1, emptyIndex.Item2),
					(emptyIndex.Item1, emptyIndex.Item2 + 1),
					(emptyIndex.Item1, emptyIndex.Item2 - 1)
				};

				possibleMoves = possibleMoves
					.Where(move => move.Item1 >= 0 && move.Item1 < playerMatrix.GetLength(0) && move.Item2 >= 0 && move.Item2 < playerMatrix.GetLength(1))
					.Where(move => move != lastMovedIndex)
					.ToList();

				if (possibleMoves.Count == 0) continue;

				(int, int) randomMove = possibleMoves[UnityEngine.Random.Range(0, possibleMoves.Count)];

				move(randomMove.Item1, randomMove.Item2);
				lastMovedIndex = emptyIndex;
				isMoved = true;
				yield return new WaitForSeconds(animDuration * animDurationCoef);
			}
		}

		enableController?.Invoke();
	}
}