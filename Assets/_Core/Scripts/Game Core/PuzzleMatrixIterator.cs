using System.Collections.Generic;
using System.Collections;

public class PuzzleMatrixIterator : IEnumerator<int>
{
	private readonly int[,] _matrix;
	private readonly int _size;
	private int _row;
	private int _col;

	public PuzzleMatrixIterator(int[,] matrix)
	{
		_matrix = matrix;
		_size = matrix.GetLength(0);
		_row = 0;
		_col = -1;
	}
	public int Current => _matrix[_row, _col];
	object IEnumerator.Current => Current;
	public bool MoveNext()
	{
		_col++;
		if (_col >= _size)
		{
			_col = 0;
			_row++;
		}
		return _row < _size;
	}
	public void Reset()
	{
		_row = 0;
		_col = -1;
	}
	public void Dispose()
	{
	}
}
