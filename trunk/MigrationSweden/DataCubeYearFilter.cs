using System;
using System.Collections.Generic;
using System.Text;

using Gav.Data;
using Redhotglue.Utilities.Extra;

namespace MigrationSweden
{
    public class DataCubeYearFilter : Filter<float, float>
    {
        //public DataCubeYearFilter()
        //{

        //}

         //a list of the years the filter knows
        private List<string> iYears;

        private string iSelectedYear;

        public string Year
        {
            set
            {
                iSelectedYear = value;
            }
            get
            {
                return iSelectedYear;
            }
        }

        public DataCubeYearFilter(List<string> aYears)
        {
            iYears = aYears;
            iSelectedYear = iYears[0];
        }

        protected override void ProcessData()
        {
            // The filter's input data.
            float[, ,] inputData = _input.GetData().Data;
            float[, ,] outputData;

            CopyData(inputData, out outputData);

            // The output data should be set to the _dataCube.Data array.
            _dataCube.Data = outputData;
        }

        private void CopyData(float[, ,] inputData, out float[, ,] outputData)
        {
            int sheetNumber = -1;

            outputData = new float[inputData.GetLength(0), inputData.GetLength(1), 1];

            if (iYears.Contains(iSelectedYear))
            {
                sheetNumber = iYears.IndexOf(iSelectedYear);
            }
            //copy the multivariate data
            for (int rowNumber = 0; rowNumber < inputData.GetLength(1); rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < inputData.GetLength(0); columnNumber++)
                {
                    if (sheetNumber == -1)
                    {
                        outputData[columnNumber, rowNumber, 0] = float.NaN;
                    }
                    else
                    {
                        outputData[columnNumber, rowNumber, 0] = inputData[columnNumber, rowNumber, sheetNumber];
                    }
                }
            }

        }
    }
}
