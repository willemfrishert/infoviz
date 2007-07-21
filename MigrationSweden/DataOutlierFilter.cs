using System;
using System.Collections.Generic;
using System.Text;

using Gav.Data;
using Redhotglue.Utilities.Extra;

namespace MigrationSweden
{
    public class DataOutlierFilter : Filter<float, float>
    {
        public const float KPositiveOutLier = 100.0f;
        public const float KPositive        = 50.0f;
        public const float KNegative        = -50.0f;
        public const float KNegativeOutLier = -100.0f;

        private List<float> iEdge0;
        private List<float> iEdge1;
        private List<float> iEdge2;

        private List<TDataValueRange> iRange;

        public List<float> Edge0
        {
            get
            {
                return iEdge0;
            }
        }

        public List<float> Edge1
        {
            get
            {
                return iEdge1;
            }
        }

        public List<float> Edge2
        {
            get
            {
                return iEdge2;
            }
        }

        public List<TDataValueRange> Range
        {
            get
            {
                return iRange;
            }
        }

        public enum TDataValueRange
        {
            ENone = 0,
            EFull,
            EPositive,
            ENegative
        }

        public DataOutlierFilter()
        {
            iRange = new List<TDataValueRange>();
            iEdge0 = new List<float>();
            iEdge1 = new List<float>();
            iEdge2 = new List<float>();
        }

        protected override void ProcessData()
        {
            // The filter's input data.
            float[, ,] outlierData;

            iRange = new List<TDataValueRange>();
            iEdge0 = new List<float>();
            iEdge1 = new List<float>();
            iEdge2 = new List<float>();

            CalculateOutliers(_input.GetData(), out outlierData);

            // The output data should be set to the _dataCube.Data array.
            _dataCube.Data = outlierData;
        }

        private void CalculateOutliers(DataCube aDataCube, out float[, ,] aMarkedOutliers)
        {
            float[, ,] inputData = _input.GetData().Data;
            aMarkedOutliers = new float[inputData.GetLength(0), inputData.GetLength(1), inputData.GetLength(2)];

            for (int columnCounter = 0; columnCounter < inputData.GetLength(0); columnCounter++)
            {
                //for (int sheetCounter = 0; sheetCounter < inputData.GetLength(2); sheetCounter++)
                //{
                    CalculateOutliersPerColumn(aDataCube, columnCounter, 0, ref aMarkedOutliers);
                //}
            }
        }

        private void CalculateOutliersPerColumn(DataCube aDataCube, int aSelectedColumn, int aSelectedSheet, ref float[, ,] aMarkedOutliers)
        {

            float edge0, edge1, edge2;
            TDataValueRange range = CalculateOutlierEdgePerColumn(aDataCube, aSelectedColumn, aSelectedSheet, out edge0, out edge1, out edge2);
            MarkOutLiers(aDataCube, aSelectedColumn, aSelectedSheet, ref aMarkedOutliers, edge0, edge1, edge2);
            iRange.Add(range);
            iEdge0.Add(edge0);
            iEdge1.Add(edge1);
            iEdge2.Add(edge2);

        }

        private void MarkOutLiers(DataCube aDataCube, int aSelectedColumn, int aSelectedSheet, ref float[, ,] aMarkedOutliers, float aEdge0, float aEdge1, float aEdge2)
        {
            float[,,] inputData = aDataCube.GetData().Data;

            for (int rowCounter=0; rowCounter < inputData.GetLength(1); rowCounter++ )
            {
                float value = inputData[aSelectedColumn, rowCounter, aSelectedSheet];
                if (float.IsNaN(value))
                {
                    aMarkedOutliers[aSelectedColumn, rowCounter, aSelectedSheet] = float.NaN;
                }
                else
                {
                    if (aEdge1 <= value)
                    {
                        aMarkedOutliers[aSelectedColumn, rowCounter, aSelectedSheet] = KPositive;

                        if (aEdge2 <= value)
                        {
                            aMarkedOutliers[aSelectedColumn, rowCounter, aSelectedSheet] = KPositiveOutLier;
                        }
                    }
                    else
                    {
                        aMarkedOutliers[aSelectedColumn, rowCounter, aSelectedSheet] = KNegative;
                        if (aEdge0 >= value)
                        {
                            aMarkedOutliers[aSelectedColumn, rowCounter, aSelectedSheet] = KNegativeOutLier;
                        }
                    }
                }
            }
        }

        public TDataValueRange CalculateOutlierEdgePerColumn(IDataCubeProvider<float> aDataCube, int aSelectedColumn, int aSelectedSheet, out float aEdge0, out float aEdge1, out float aEdge2)
        {
            float[] positiveValues = new float[1];
            float[] negativeValues = new float[1];
            float minValue = float.MinValue;
            float maxValue = float.MaxValue;

            TDataValueRange range = TDataValueRange.ENone;
            aEdge0 = 0.0f;
            aEdge1 = 0.0f;
            aEdge2 = 0.0f;

            float[] dataColumn;
            if (GetSortedColumn(aDataCube, aSelectedColumn, aSelectedSheet, out dataColumn))
            {
                minValue = dataColumn[0];
                maxValue = dataColumn[dataColumn.GetLength(0) - 1];
                range = SplitPositiveNegativeValues(dataColumn, out positiveValues, out negativeValues);
            }

            switch (range)
            {
                case TDataValueRange.ENegative:
                    {
                        CalulateNegativeEdgeValues(negativeValues, maxValue, minValue, out aEdge0, out aEdge1, out aEdge2);
                        break;
                    }
                case TDataValueRange.EPositive:
                    {
                        CalculatePostiveEdgeValues(positiveValues, maxValue, minValue, out aEdge0, out aEdge1, out aEdge2);
                        break;
                    }
                case TDataValueRange.ENone:
                    {
                        aEdge0 = 0.0f;
                        aEdge1 = 0.0f;
                        aEdge2 = 0.0f;
                        break;
                    }
                default:
                    {
                        CalculateFullEdgeValues(positiveValues, negativeValues, maxValue, minValue, out aEdge0, out aEdge1, out aEdge2);
                        break;
                    }
            }

            return range;
        }
        private void CalculateFullEdgeValues(float[] positiveValues, float[] negativeValues, float maxValue, float minValue, out float aEdge0, out float aEdge1, out float aEdge2)
        {
            float Q1Negative = 0;
            float Q3Negative = 0;
            if (negativeValues.GetLength(0) > 0)
            {
                CalculateIQR(negativeValues, out Q1Negative, out Q3Negative);
            }
            float IQRNegative = Q3Negative - Q1Negative;


            float Q1Positive = 0;
            float Q3Positive = 0;
            if (positiveValues.GetLength(0) > 0)
            {
                CalculateIQR(positiveValues, out Q1Positive, out Q3Positive);
            }

            float IQRPositive = Q3Positive - Q1Positive;

            aEdge0 = Q1Negative - 1.5f * IQRNegative;
            aEdge1 = 0.0f;
            aEdge2 = Q3Positive + 1.5f * IQRPositive;
        }

        private void CalculatePostiveEdgeValues(float[] positiveValues, float maxValue, float minValue, out float aEdge0, out float aEdge1, out float aEdge2)
        {
            float Q1Positive = 0;
            float Q3Positive = 0;
            if (positiveValues.GetLength(0) > 0)
            {
                CalculateIQR(positiveValues, out Q1Positive, out Q3Positive);
            }

            float IQRPositive = Q3Positive - Q1Positive;

            aEdge0 = Q1Positive - 1.5f * IQRPositive;
            aEdge2 = Q3Positive + 1.5f * IQRPositive;

            if (aEdge0 < minValue)
            {
                aEdge0 = minValue;
            }

            aEdge1 = (aEdge2 + aEdge0) / 2.0f;
        }

        private void CalulateNegativeEdgeValues(float[] negativeValues, float maxValue, float minValue, out float aEdge0, out float aEdge1, out float aEdge2)
        {
            float Q1Negative = 0;
            float Q3Negative = 0;
            if (negativeValues.GetLength(0) > 0)
            {
                CalculateIQR(negativeValues, out Q1Negative, out Q3Negative);
            }
            float IQRNegative = Q3Negative - Q1Negative;

            aEdge0 = Q1Negative - 1.5f * IQRNegative;
            aEdge2 = Q3Negative + 1.5f * IQRNegative;

            if (aEdge2 > maxValue)
            {
                aEdge2 = maxValue;
            }

            aEdge1 = (aEdge2 + aEdge0) / 2.0f;
        }

        public void CalculateIQR(float[] aValues, out float aQ1, out float aQ3)
        {
            aQ1 = new float();
            aQ3 = new float();

            int indexQ1 = (int)(0.25f * aValues.GetLength(0));
            int indexQ3 = (int)(0.75f * aValues.GetLength(0));
            aQ1 = aValues[indexQ1];
            aQ3 = aValues[indexQ3];
        }

        public bool GetSortedColumn(IDataCubeProvider<float> aDataCube, int aColumnNumber, int aSheetNumber, out float[] aData)
        {
            bool columnSorted = false;
            float[, ,] sourceData = aDataCube.GetData().Data;

            // reserve memory for n rows (for index & value)
            aData = new float[sourceData.GetLength(1)];

            int numberOfValidRows = 0;

            // Count the amount of valid numbers in one column and store them in data
            for (int rowNumber = 0; rowNumber < sourceData.GetLength(1); rowNumber++)
            {
                if (!float.IsNaN(sourceData[aColumnNumber, rowNumber, aSheetNumber]))
                {
                    aData[rowNumber] = sourceData[aColumnNumber, rowNumber, aSheetNumber];
                    numberOfValidRows++;
                    columnSorted = true;
                }
            }

            ArrayObject arrayObject = new ArrayObject();
            aData = arrayObject.ReDimension(aData, numberOfValidRows);

            Array.Sort(aData);

            return columnSorted;
        }

        public TDataValueRange SplitPositiveNegativeValues(float[] aSortedDataColumn, out float[] aPositive, out float[] aNegative)
        {
            int counter;

            // go through the sorted list until we find 0
            for (counter=0; counter < aSortedDataColumn.GetLength(0); counter++)
            {
                if (aSortedDataColumn[counter] >= 0)
                {
                    break;
                }
            }

            // reserve memory
            aNegative = new float[counter];
            aPositive = new float[aSortedDataColumn.GetLength(0) - counter];

            // copy negative values going from (0, max negative value]
            for (int i = 0; i < counter; i++)
            {
                aNegative[i] = aSortedDataColumn[i];
            }

            // copy positive values going from [0, max positive value]
            for (int i = counter; i < aSortedDataColumn.GetLength(0); i++)
            {
                aPositive[i - counter] = aSortedDataColumn[i];
            }

            if ( (aNegative.GetLength(0) > 0) && (aPositive.GetLength(0) > 0) )
            {
                return TDataValueRange.EFull;
            }
            if (aPositive.GetLength(0) > 0)
            {
                return TDataValueRange.EPositive;
            }
            if (aNegative.GetLength(0) > 0)
            {
                return TDataValueRange.ENegative;
            }

            return TDataValueRange.ENone;
        }
    }
}
