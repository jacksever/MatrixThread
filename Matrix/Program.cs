using System;
using System.Threading;

namespace Matrix
{
	public class Program
	{
		private const int Dimension = 100;

		private static int[,] FirstMatrix;
		private static int[,] SecondMatrix;
		private static MatrixThread Matrix;

		public static int[,] ResultMatrix = new int[Dimension, Dimension];
		public static int[,] ResultMatrix2 = new int[Dimension, Dimension];

		public static void Main(string[] args)
		{
			FirstMatrix = InitializationMatrix();
			SecondMatrix = InitializationMatrix();

			int firstIndex = 0;
			Thread[] ThreadArray = new Thread[Dimension];
			Matrix = new MatrixThread(FirstMatrix, SecondMatrix, firstIndex, Dimension);

			for (int index = Dimension - 1; index >= 0; --index)
			{
				int lastIndex = firstIndex + Dimension;
				if (index == 0)
					lastIndex = Dimension * Dimension;

				ThreadArray[index] = new Thread(new ParameterizedThreadStart(SetMartixThread));
				ThreadArray[index].Start(lastIndex);
				firstIndex = lastIndex;
			}

			var watch = System.Diagnostics.Stopwatch.StartNew();

			foreach (var item in ThreadArray)
				item.Join();

			watch.Stop();
			Console.WriteLine("Время потоковое: " + watch.ElapsedMilliseconds);

			ResultMatrix2 = MultiplicationMatrix();

			Console.Write("Матрицы совпадают: ");
			bool flag = true;

			for (int row = 0; row < Dimension; row++)
				for (int col = 0; col < Dimension; col++)
					if (ResultMatrix[row, col] != ResultMatrix2[row, col])
						flag = false;

			Console.WriteLine(flag);
			Console.ReadLine();
		}

		public static void SetMartixThread(object items)
		{
			int index = (int)items;
			Matrix.CalculateItem(index);
		}

		private static int[,] InitializationMatrix()
		{
			var matrix = new int[Dimension, Dimension];
			var random = new Random();

			for (int i = 0; i < Dimension; i++)
				for (int j = 0; j < Dimension; j++)
					matrix[i, j] = random.Next(1, 15);

			return matrix;
		}

		private static int[,] MultiplicationMatrix()
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();
			int[,] result = new int[Dimension, Dimension];

			for (int i = 0; i < FirstMatrix.GetLength(0); i++)
				for (int j = 0; j < SecondMatrix.GetLength(1); j++)
					for (int k = 0; k < SecondMatrix.GetLength(0); k++)
						result[i, j] += FirstMatrix[i, k] * SecondMatrix[k, j];

			watch.Stop();
			Console.WriteLine("Время обычное: " + watch.ElapsedMilliseconds);

			return result;
		}

	}
}
