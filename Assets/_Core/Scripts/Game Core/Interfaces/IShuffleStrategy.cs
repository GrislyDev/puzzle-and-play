using System;
using System.Collections;

public interface IShuffleStrategy
{
	IEnumerator Shuffle(int movesCount, int[,] playerMatrix, Action<int, int> move, float animDuration, Action enableController);
}