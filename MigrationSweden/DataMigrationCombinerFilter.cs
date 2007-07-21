using System;
using System.Collections.Generic;
using System.Text;

using Gav.Data;

namespace MigrationSweden
{
    public class DataMigrationCombinerFilter : Filter<float, float>
    {
        private List<DataContainer> iDataContainerList;

        private string iSelectedYear;

        public List<string> ColumnHeaders
        {
            get
            {
                List<string> headers = new List<string>();
                for (int i=0; i < iDataContainerList.Count; i++)
                {
                    headers.AddRange(iDataContainerList[i].ColumnHeaders);
                }

                return headers;
            }
        }

        public List<DataContainer> DataContainerList
        {
            set
            {
                iDataContainerList = value;
            }
            get
            {
                return iDataContainerList;
            }
        }

        public string SelectedYear
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

        public DataMigrationCombinerFilter(string aSelectedYear)
        {
            iSelectedYear = aSelectedYear;
            iDataContainerList = new List<DataContainer>();
        }

        protected override void ProcessData()
        {
            int numberOfTotalColumns = 0;
            int numberOfTotalRows = 0;

            for (int i = 0; i < iDataContainerList.Count; i++)
            {
                iDataContainerList[i].DataCubeYearFilter.Year = iSelectedYear;
                iDataContainerList[i].DataCubeYearFilter.CommitChanges();
                float[, ,] inputData = iDataContainerList[i].DataCubeYearFilter.GetData().Data;
                numberOfTotalColumns += inputData.GetLength(0);

                if (numberOfTotalRows < inputData.GetLength(1))
                {
                    numberOfTotalRows = inputData.GetLength(1);
                }
            }

            float[, ,] outputData = new float[numberOfTotalColumns, numberOfTotalRows, 1];

            for (int rowNumber = 0; rowNumber < numberOfTotalRows; rowNumber++)
            {
                for (int columnNumber=0; columnNumber < numberOfTotalColumns; columnNumber++)
                {
                    outputData[columnNumber, rowNumber, 0] = float.NaN;
                }
            }

            int totalColumns = 0;
            for (int i = 0; i < iDataContainerList.Count; i++)
            {
                float[, ,] inputData = iDataContainerList[i].DataCubeYearFilter.GetData().Data;

                for (int rowNumber = 0; rowNumber < numberOfTotalRows; rowNumber++)
                {
                    for (int columnNumber = 0; columnNumber < inputData.GetLength(0); columnNumber++)
                    {
                        outputData[columnNumber+totalColumns, rowNumber, 0] = inputData[columnNumber, rowNumber, 0];
                    }
                }

                totalColumns += inputData.GetLength(0);
            }

            this._dataCube.Data = outputData;
        }
    }
}
