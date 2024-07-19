public static class PuzzleUtils
{
	public const int EMPTY = -1;

	public static bool IsAdjacentToEmptyCell(int[,] matrix, int row, int col, out int emptyRow, out int emptyCol)
	{
		emptyRow = -1;
		emptyCol = -1;
		int size = matrix.GetLength(0);

		if (row > 0 && matrix[row - 1, col] == EMPTY)
		{
			emptyRow = row - 1;
			emptyCol = col;
			return true;
		}

		if (row < size - 1 && matrix[row + 1, col] == EMPTY)
		{
			emptyRow = row + 1;
			emptyCol = col;
			return true;
		}

		if (col > 0 && matrix[row, col - 1] == EMPTY)
		{
			emptyRow = row;
			emptyCol = col - 1;
			return true;
		}

		if (col < size - 1 && matrix[row, col + 1] == EMPTY)
		{
			emptyRow = row;
			emptyCol = col + 1;
			return true;
		}

		return false;
	}
}