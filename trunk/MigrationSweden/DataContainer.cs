using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gav.Data;
using LabExcelReader;
using Redhotglue.Utilities.Extra;


namespace MigrationSweden
{
    /// <summary>
    /// Summary description for DataContainer
    /// </summary>
    public class DataContainer
    {
        //contains excel information
        protected DataCube iDataCube;

        //filter based on year
        private DataCubeYearFilter iDataCubeYearFilter;

        //filter to check for outliers
        private DataOutlierFilter iDataOutlierFilter;

        //contains the column headers
        protected List<string> iColumnHeaders;

        // the title of the excel file
        protected string iTitle;

        // an ID list of the municipalities
        protected List<int> iMunicipalitiesId;
        // a string list of the municipalities
        protected List<string> iMunicipalitiesNames;

        // a list of the sheet names 
        protected List<string> iSheetName;

        //public DataCube DataCube
        //{
        //    get
        //    {
        //        return iDataCube;
        //    }
        //}

        public DataCubeYearFilter DataCubeYearFilter
        {
            get
            {
                return iDataCubeYearFilter;
            }
        }

        public DataOutlierFilter DataOutlierFilter
        {
            get
            {
                return iDataOutlierFilter;
            }
        }

        public List<string> ColumnHeaders
        {
            get
            {
                return iColumnHeaders;
            }
        }

        public List<int> MunicipalitiesIds
        {
            get
            {
                return iMunicipalitiesId;
            }
        }

        public List<string> MunicipalitiesNames
        {
            get
            {
                return iMunicipalitiesNames;
            }
        }

        public string Title
        {
            get
            {
                return iTitle;
            }
        }

        public List<string> SheetName
        {
            get
            {
                return iSheetName;
            }
        }


        public DataContainer()
        {
            iDataCube = new DataCube();
            iDataCube.Data = new float[1, 1, 1];
            iColumnHeaders = new List<string>();
            iMunicipalitiesId = new List<int>();
            iMunicipalitiesNames = new List<string>();
            iSheetName = new List<string>();
            iDataOutlierFilter = new DataOutlierFilter();
        }

        public void LoadData(string aFileName, List<string> aSheet)
        {
            CopySheetNames(aSheet);

            try
            {
                // create an excel reader
                ExcelReader excelReader = new ExcelReader();

                // reserve space in data cube by checking the first sheet of the excel table
                string[] headers;
                object[,] data;
                excelReader.GetArrayFromExcel(aFileName, iSheetName[0], out headers, out data);
                //iDataCube.Data = new float[data.GetLength(0) - 1, data.GetLength(1), iSheet.Count];

                //copy the title from the top left cell
                iTitle = headers[0];

                // copy the multi-variate data names
                CopyColumnHeaders(headers);

                // copy the first column
                CopyMunicipalities(data, 0);

                // copy data from all sheet numbers
                for (int sheetNumber = 0; sheetNumber < iSheetName.Count; sheetNumber++)
                {
                    string[] excelHeader;
                    object[,] excelData;
                    excelReader.GetArrayFromExcel(aFileName, iSheetName[sheetNumber], out excelHeader, out excelData);

                    object[,] cleanData;
                    // clean up data (remove municipalities)
                    CleanData(excelData, out cleanData);

                    CopyDataToDataCube(cleanData, sheetNumber);
                }

                iDataCubeYearFilter = new DataCubeYearFilter(iSheetName);
                iDataCubeYearFilter.Input = iDataCube;
                iDataCubeYearFilter.CommitChanges();

                iDataOutlierFilter.Input = iDataCubeYearFilter;
                iDataOutlierFilter.CommitChanges();
            }
            catch
            {
                Console.WriteLine("Oops! Error opening file or opening worksheet.");
            }

        }

        protected void CopySheetNames(List<string> aSheet)
        {
            for (int i = 0; i < aSheet.Count; i++)
            {
                iSheetName.Add(aSheet[i]);
            }
        }

        //copies the municipality name
        protected void CopyMunicipalities(object[,] aData, int aColumnNumber)
        {
            String[] stringSeperator = new String[1];
            stringSeperator[0] = " ";
            for (int rowNumber = 0; rowNumber < aData.GetLength(1); rowNumber++)
            {
                object municipality = aData[aColumnNumber, rowNumber];


                String[] splittedString = municipality.ToString().Split(stringSeperator, 2, StringSplitOptions.RemoveEmptyEntries);

                iMunicipalitiesId.Add(int.Parse(splittedString[0]));
                iMunicipalitiesNames.Add(splittedString[1]);
            }
        }

        //copies the headers from the multivariate data
        protected virtual void CopyColumnHeaders(string[] aHeaders)
        {
            for (int i = 1; i < aHeaders.GetLength(0); i++)
            {
                iColumnHeaders.Add(aHeaders[i]);
            }
        }

        protected void CopyDataToDataCube(object[,] aData, int aSheetNumber)
        {
            ArrayObject arrayObject = new ArrayObject();
            iDataCube.Data = arrayObject.ReDimension(iDataCube.Data, aData.GetLength(0), aData.GetLength(1), aSheetNumber + 1);

            float value;

            // add the data to the 3d matrix
            for (int rowNumber = 0; rowNumber < aData.GetLength(1); rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < aData.GetLength(0); columnNumber++)
                {
                    if (float.TryParse(aData[columnNumber, rowNumber].ToString(), out value))
                    {
                        iDataCube.Data[columnNumber, rowNumber, aSheetNumber] = value;
                    }
                    else
                    {
                        iDataCube.Data[columnNumber, rowNumber, aSheetNumber] = float.NaN;
                    }
                }
            }
        }

        // clean up data (remove municipalities)
        protected virtual void CleanData(object[,] aRawData, out object[,] aCleanData)
        {
            //reserve space for columns without municipalities
            aCleanData = new object[aRawData.GetLength(0) - 1, aRawData.GetLength(1)];

            //copy the multivariate data
            for (int rowNumber = 0; rowNumber < aCleanData.GetLength(1); rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < aCleanData.GetLength(0); columnNumber++)
                {
                    aCleanData[columnNumber, rowNumber] = aRawData[columnNumber + 1, rowNumber];
                }
            }
        }

        public void GetSortedColumn(int aColumnNumber, int aSheetNumber, out float[] aData, out int aCount)
        {
            float[, ,] sourceData = iDataCube.Data;

            // reserve memory for n rows
            aData = new float[sourceData.GetLength(1)];

            aCount = 0;

            // Count the amount of valid numbers in one column and store them in data
            for (int rowNumber = 0; rowNumber < sourceData.GetLength(1); rowNumber++)
            {
                if (!float.IsNaN(sourceData[aColumnNumber, rowNumber, aSheetNumber]))
                {
                    aData[rowNumber] = sourceData[aColumnNumber, rowNumber, aSheetNumber];
                    aCount++;
                }
            }

            Array.Sort(aData);
        }

        public float GetColumnMinimum(int aColumnNumber, int aSheetNumber)
        {
            float[] data;
            int numberOfValidRows;
            GetSortedColumn(aColumnNumber, aSheetNumber, out data, out numberOfValidRows);

            float minimum = data[0];

            return minimum;
        }

        public float GetColumnMaximum(int aColumnNumber, int aSheetNumber)
        {
            float[] data;
            int numberOfValidRows;
            GetSortedColumn(aColumnNumber, aSheetNumber, out data, out numberOfValidRows);

            float maximum = data[(numberOfValidRows - 1)];

            return maximum;
        }

        public float GetColumnMedian(int aColumnNumber, int aSheetNumber)
        {
            float[] data;
            int numberOfValidRows;
            GetSortedColumn(aColumnNumber, aSheetNumber, out data, out numberOfValidRows);

            float median = data[(numberOfValidRows - 1) / 2];

            return median;
        }


        public void GetAllColumnMaxMin(out List<float> max, out List<float> min)
        {
            max = new List<float>();
            min = new List<float>();

            float[, ,] data = iDataCube.GetData().Data;
            for (int columnNumber = 0; columnNumber < data.GetLength(0); columnNumber++)
            {
                max.Add(float.MinValue);
                min.Add(float.MaxValue);
            }

            for (int rowNumber = 0; rowNumber < data.GetLength(1); rowNumber++)
            {
                for (int sheetNumber = 0; sheetNumber < data.GetLength(2); sheetNumber++ )
                {
                    for (int columnNumber = 0; columnNumber < data.GetLength(0); columnNumber++)
                    {
                        max[columnNumber] = (data[columnNumber, rowNumber, sheetNumber] > max[columnNumber]) ?
                            data[columnNumber, rowNumber, sheetNumber] : max[columnNumber];

                        min[columnNumber] = (data[columnNumber, rowNumber, sheetNumber] < min[columnNumber]) ?
                            data[columnNumber, rowNumber, sheetNumber] : min[columnNumber];

                    }
                }
            }
        }
    }
}