using System;

public static class ArrayExtensions
{
	public static (int,int) IndexOf(this int[,] array, int value)
	{
		for (int i = 0; i < array.GetLength(0); i++)
		{
			for (int j = 0; j < array.GetLength(1); j++)
			{
				if(array[i,j] == value)
					return (i,j);
			}
		}

		throw new Exception("Value not found");
	}
}
