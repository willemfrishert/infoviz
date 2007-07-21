using System;
using System.Collections.Generic;
using System.Text;

using Gav.Data;


namespace GAVExtra
{ 
    public class TableLensFilter : Filter<float, float>
    {
        private List<int> iSelectedRows;

        private bool iColumnOrderingAscending;
        private int iColumnNumberToOrder;

        private static int KNoColumnSelected = -1;

        public List<int> SelectedRows
        {
            get
            {
                return iSelectedRows;
            }
            set
            {
                iSelectedRows = value;
            }
        }

        public int ColumnToOrder
        {
            get
            {
                return iColumnNumberToOrder;
            }
            set
            {
                if (value == iColumnNumberToOrder)
                {
                    iColumnOrderingAscending = !iColumnOrderingAscending;
                }
                else
                {
                    iColumnOrderingAscending = true;
                }
                iColumnNumberToOrder = value;
            }
        }

        public TableLensFilter()
        {
            iSelectedRows = new List<int>();
            iColumnOrderingAscending = true;
            iColumnNumberToOrder = 0;
        }

        protected override void ProcessData()
        {
            // The filter's input data.
            float[,,] data = _input.GetData().Data;
            float[, ,] outdata = new float[data.GetLength(0)+3,data.GetLength(1), data.GetLength(2)];

            for (int rowCounter = 0; rowCounter < data.GetLength(1); rowCounter++)
            {
                for (int sheetCounter = 0; sheetCounter < data.GetLength(2); sheetCounter++)
                {
                    outdata[0, rowCounter, sheetCounter] = rowCounter;
                }
            }

            for (int columnCounter = 0; columnCounter < data.GetLength(0); columnCounter++)
            {
                for (int rowCounter = 0; rowCounter < data.GetLength(1); rowCounter++)
                {
                    for (int sheetCounter=0;sheetCounter<data.GetLength(2);sheetCounter++)
                    {
                        outdata[columnCounter+1, rowCounter, sheetCounter] = data[columnCounter, rowCounter, sheetCounter];
                    }
                }
            }
            for (int rowCounter = 0; rowCounter < data.GetLength(1); rowCounter++)
            {
                for (int sheetCounter = 0; sheetCounter < data.GetLength(2); sheetCounter++)
                {
                    outdata[outdata.GetLength(0) - 2, rowCounter, sheetCounter] = rowCounter;
                    outdata[outdata.GetLength(0) - 1, rowCounter, sheetCounter] = 0.0f;
                }
            }

            for (int i = 0; i < iSelectedRows.Count; i++ )
            {
                outdata[outdata.GetLength(0) - 1, iSelectedRows[i], 0] = 1.0f;
            }

            RandomDataByColumn(ref outdata);

            for (int i = 0; i < outdata.GetLength(1); i++)
            {
                if ( outdata[data.GetLength(0) + 1, i, 0] > 0.0f )
                {
                    Console.WriteLine(i);
                }
            }

            // The output data should be set to the _dataCube.Data array.
            _dataCube.Data = outdata;
        }

        private void RandomDataByColumn(ref float[, ,] outdata)
        {
            if (iColumnNumberToOrder != KNoColumnSelected)
            {
                BubbleSort(ref outdata);
            }
        }

        private void BubbleSort(ref float[,,] aData)
        {
            bool swapped = false;

            do
            {
                swapped = false;
                        
                if (iColumnOrderingAscending)
                {
                    for (int rowNumber = 0; rowNumber < aData.GetLength(1) - 1; rowNumber++)
                    {
                        float dataValue1 = aData[iColumnNumberToOrder, rowNumber, 0];
                        float dataValue2 = aData[iColumnNumberToOrder, rowNumber + 1, 0];

                        if (float.IsNaN(dataValue1))
                        {
                            dataValue1 = 0.0f;
                        }
                        if (float.IsNaN(dataValue2))
                        {
                            dataValue2 = 0.0f;
                        }

                        if (dataValue1 > dataValue2)
                        {
                            SwapRows(ref aData, rowNumber, rowNumber + 1);
                            swapped = true;
                        }
                    }
                }
                else
                {
                    for (int rowNumber = 0; rowNumber < aData.GetLength(1) - 1; rowNumber++)
                    {
                        float dataValue1 = aData[iColumnNumberToOrder, rowNumber, 0];
                        float dataValue2 = aData[iColumnNumberToOrder, rowNumber + 1, 0];

                        if (float.IsNaN(dataValue1))
                        {
                            dataValue1 = 0.0f;
                        }
                        if (float.IsNaN(dataValue2))
                        {
                            dataValue2 = 0.0f;
                        }
                        if (dataValue1 < dataValue2)
                        {
                            SwapRows(ref aData, rowNumber, rowNumber + 1);
                            swapped = true;
                        }
                    }
                }

            } while (swapped);

        }

        private void SwapRows(ref float[, ,] aData, int aRowNumber, int aRowNumber2)
        {
            float temp;
            for (int columnNumber = 0; columnNumber < aData.GetLength(0); columnNumber++)
            {
                temp = aData[columnNumber, aRowNumber, 0];
                aData[columnNumber, aRowNumber, 0] = aData[columnNumber, aRowNumber2, 0];
                aData[columnNumber, aRowNumber2, 0] = temp;
            }

        }
    }
}
