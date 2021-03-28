
namespace Matrix
{
	public class MatrixThread
	{
		private int[,] FirstMatrix, SecondMatrix;
		private int FirstIndex, Dimension;

		public MatrixThread(int[,] firstMatrix, int[,] secondMatrix, int firstIndex, int dimension)
		{
			this.FirstMatrix = firstMatrix;
			this.SecondMatrix = secondMatrix;
			this.FirstIndex = firstIndex;
			this.Dimension = dimension;
		}

		public void SetValueToMatrix(int row, int col)
		{
			int result = 0;
			for (int i = 0; i < Dimension; ++i)
				result += FirstMatrix[row, i] * SecondMatrix[i, col];

			Program.ResultMatrix[row, col] = result;
		}

		public void CalculateItem(int lastIndex)
		{
			for (int index = FirstIndex; index < lastIndex; ++index)
				SetValueToMatrix(index / Dimension, index % Dimension);
		}
	}
}
