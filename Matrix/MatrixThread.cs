namespace Matrix
{
	public class MatrixThread
	{
		private int[,] FirstMatrix, SecondMatrix;
		private int Dimension;

		public MatrixThread(int[,] firstMatrix, int[,] secondMatrix, int dimension)
		{
			this.FirstMatrix = firstMatrix;
			this.SecondMatrix = secondMatrix;
			this.Dimension = dimension;
		}

		public void MultipleMatrixThread(int firstIndex, int lastIndex)
		{
			for (int index = firstIndex; index < lastIndex; index++)
			{
				int i = index / Dimension;
				int j = index % Dimension;

				Program.ResultMatrix[i, j] = 0;

				for (int k = 0; k < Dimension; k++)
					Program.ResultMatrix[i, j] += FirstMatrix[i, k] * SecondMatrix[k, j];
			}
		}
	}
}
