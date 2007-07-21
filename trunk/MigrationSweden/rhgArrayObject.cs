// Provides extra fuctionality for Arrays

namespace Redhotglue.Utilities.Extra
{
	public class ArrayObject
	{
// ----------------------------------------------------------------------------
		// (sbyte) ReDimension a one dimensional array
		public sbyte[] ReDimension(sbyte[] OldArray, int arrNewLength)
		{
			// declare a larger array
			sbyte[] NewArray = new sbyte[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (byte) ReDimension a one dimensional array
		public byte[] ReDimension(byte[] OldArray, int arrNewLength)
		{
			// declare a larger array
			byte[] NewArray = new byte[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (short) ReDimension a one dimensional array
		public short[] ReDimension(short[] OldArray, int arrNewLength)
		{
			// declare a larger array
			short[] NewArray = new short[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (ushort) ReDimension a one dimensional array
		public ushort[] ReDimension(ushort[] OldArray, int arrNewLength)
		{
			// declare a larger array
			ushort[] NewArray = new ushort[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (int) ReDimension a one dimensional array
		public int[] ReDimension(int[] OldArray, int arrNewLength)
		{
			// declare a larger array
			int[] NewArray = new int[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (uint) ReDimension a one dimensional array
		public uint[] ReDimension(uint[] OldArray, int arrNewLength)
		{
			// declare a larger array
			uint[] NewArray = new uint[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (long) ReDimension a one dimensional array
		public long[] ReDimension(long[] OldArray, int arrNewLength)
		{
			// declare a larger array
			long[] NewArray = new long[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (ulong) ReDimension a one dimensional array
		public ulong[] ReDimension(ulong[] OldArray, int arrNewLength)
		{
			// declare a larger array
			ulong[] NewArray = new ulong[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (char) ReDimension a one dimensional array
		public char[] ReDimension(char[] OldArray, int arrNewLength)
		{
			// declare a larger array
			char[] NewArray = new char[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (float) ReDimension a one dimensional array
		public float[] ReDimension(float[] OldArray, int arrNewLength)
		{
			// declare a larger array
			float[] NewArray = new float[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (double) ReDimension a one dimensional array
		public double[] ReDimension(double[] OldArray, int arrNewLength)
		{
			// declare a larger array
			double[] NewArray = new double[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (bool) ReDimension a one dimensional array
		public bool[] ReDimension(bool[] OldArray, int arrNewLength)
		{
			// declare a larger array
			bool[] NewArray = new bool[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (decimal) ReDimension a one dimensional array
		public decimal[] ReDimension(decimal[] OldArray, int arrNewLength)
		{
			// declare a larger array
			decimal[] NewArray = new decimal[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (string) ReDimension a one dimensional array
		public string[] ReDimension(string[] OldArray, int arrNewLength)
		{
			// declare a larger array
			string[] NewArray = new string[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (object) ReDimension a one dimensional array
		public object[] ReDimension(object[] OldArray, int arrNewLength)
		{
			// declare a larger array
			object[] NewArray = new object[arrNewLength];
			
			// determine if we are shrinking or enlarging
			int rowMax = 0;
			if (OldArray.Length < arrNewLength)
				rowMax = OldArray.Length;
			else
				rowMax = arrNewLength;
			
			// place values of old array into new array
			for(int row = 0; row < rowMax; row++)
			{
				NewArray[row] = OldArray[row];
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (sbyte) ReDimension a 2D rectangular array
		public sbyte[,] ReDimension(sbyte[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			sbyte[,] NewArray = new sbyte[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (byte) ReDimension a 2D rectangular array
		public byte[,] ReDimension(byte[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			byte[,] NewArray = new byte[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (short) ReDimension a 2D rectangular array
		public short[,] ReDimension(short[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			short[,] NewArray = new short[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (ushort) ReDimension a 2D rectangular array
		public ushort[,] ReDimension(ushort[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			ushort[,] NewArray = new ushort[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (int) ReDimension a 2D rectangular array
		public int[,] ReDimension(int[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			int[,] NewArray = new int[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (uint) ReDimension a 2D rectangular array
		public uint[,] ReDimension(uint[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			uint[,] NewArray = new uint[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (long) ReDimension a 2D rectangular array
		public long[,] ReDimension(long[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			long[,] NewArray = new long[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (ulong) ReDimension a 2D rectangular array
		public ulong[,] ReDimension(ulong[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			ulong[,] NewArray = new ulong[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (char) ReDimension a 2D rectangular array
		public char[,] ReDimension(char[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			char[,] NewArray = new char[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (float) ReDimension a 2D rectangular array
		public float[,] ReDimension(float[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			float[,] NewArray = new float[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (double) ReDimension a 2D rectangular array
		public double[,] ReDimension(double[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			double[,] NewArray = new double[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (bool) ReDimension a 2D rectangular array
		public bool[,] ReDimension(bool[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			bool[,] NewArray = new bool[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (decimal) ReDimension a 2D rectangular array
		public decimal[,] ReDimension(decimal[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			decimal[,] NewArray = new decimal[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (string) ReDimension a 2D rectangular array
		public string[,] ReDimension(string[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			string[,] NewArray = new string[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (object) ReDimension a 2D rectangular array
		public object[,] ReDimension(object[,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength)
		{
			// declare a larger array
			object[,] NewArray = new object[arr1stDimLength, arr2ndDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			int xMax = 0;
			int yMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// place values of old array into new array
			for(int x = 0; x < xMax; x++)
			{
				for(int y = 0; y < yMax; y++)
				{
					NewArray[x, y] = OldArray[x, y];
					//Console.Write("[{0}]", NewArray[x, y]);
				}
				//Console.Write("\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (sbyte) ReDimension a 3D rectangular array
		public sbyte[,,] ReDimension(sbyte[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			sbyte[,,] NewArray = new sbyte[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (byte) ReDimension a 3D rectangular array
		public byte[,,] ReDimension(byte[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			byte[,,] NewArray = new byte[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (short) ReDimension a 3D rectangular array
		public short[,,] ReDimension(short[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			short[,,] NewArray = new short[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (ushort) ReDimension a 3D rectangular array
		public ushort[,,] ReDimension(ushort[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			ushort[,,] NewArray = new ushort[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (int) ReDimension a 3D rectangular array
		public int[,,] ReDimension(int[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			int[,,] NewArray = new int[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (uint) ReDimension a 3D rectangular array
		public uint[,,] ReDimension(uint[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			uint[,,] NewArray = new uint[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (long) ReDimension a 3D rectangular array
		public long[,,] ReDimension(long[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			long[,,] NewArray = new long[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (ulong) ReDimension a 3D rectangular array
		public ulong[,,] ReDimension(ulong[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			ulong[,,] NewArray = new ulong[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (char) ReDimension a 3D rectangular array
		public char[,,] ReDimension(char[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			char[,,] NewArray = new char[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (float) ReDimension a 3D rectangular array
		public float[,,] ReDimension(float[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			float[,,] NewArray = new float[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (double) ReDimension a 3D rectangular array
		public double[,,] ReDimension(double[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			double[,,] NewArray = new double[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (bool) ReDimension a 3D rectangular array
		public bool[,,] ReDimension(bool[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			bool[,,] NewArray = new bool[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (decimal) ReDimension a 3D rectangular array
		public decimal[,,] ReDimension(decimal[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			decimal[,,] NewArray = new decimal[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (string) ReDimension a 3D rectangular array
		public string[,,] ReDimension(string[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			string[,,] NewArray = new string[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
		// (object) ReDimension a 3D rectangular array
		public object[,,] ReDimension(object[,,] OldArray,
		                             int arr1stDimLength,
		                             int arr2ndDimLength,
		                             int arr3rdDimLength)
		{
			// declare a larger array
			object[,,] NewArray = new object[arr1stDimLength,
											arr2ndDimLength,
											arr3rdDimLength];
			
			// determine if we are shrinking or enlarging
			const int FirstDimension = 0;
			const int SecondDimension = 1;
			const int ThirdDimension = 2;
			int xMax = 0;
			int yMax = 0;
			int zMax = 0;
			// determine if we are shrinking or enlarging columns
			if (OldArray.GetUpperBound(FirstDimension) < (arr1stDimLength - 1))
				xMax = OldArray.GetUpperBound(FirstDimension) + 1;
			else
				xMax = arr1stDimLength;
			
			// determine if we are shrinking or enlarging rows
			if (OldArray.GetUpperBound(SecondDimension) < (arr2ndDimLength - 1))
				yMax = OldArray.GetUpperBound(SecondDimension) + 1;
			else
				yMax = arr2ndDimLength;
			
			// determine if we are shrinking or enlarging depth
			if (OldArray.GetUpperBound(ThirdDimension) < (arr3rdDimLength - 1))
				zMax = OldArray.GetUpperBound(ThirdDimension) + 1;
			else
				zMax = arr3rdDimLength;
			
			// place values of old array into new array
			for(int z = 0; z < zMax; z++)
			{
				for(int x = 0; x < xMax; x++)
				{
					for(int y = 0; y < yMax; y++)
					{
						NewArray[x, y, z] = OldArray[x, y, z];
						//Console.Write("[{0}]", NewArray[x, y, z]);
					}
					//Console.Write("\n");
				}
				//Console.Write("\n\n");
			}
			
			return NewArray;
		}
// ----------------------------------------------------------------------------
	}
}
