using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Gav.Data;
using Gav.Graphics;

namespace MigrationSweden
{
    public class ColorMapOutLierFilter : ColorMap
    {
        private Color iColorPositiveOutlier;
        private Color iColorPositive;
        private Color iColorNegative;
        private Color iColorNegativeOutlier;

        List<IColorMapPart> iColorMapPartNegativeList;
        List<IColorMapPart> iColorMapPartPositiveList;
        List<IColorMapPart> iColorMapPartFullList;

        public ColorMapOutLierFilter()
        {
            iColorPositiveOutlier = Color.FromArgb(217, 35, 35);
            iColorPositive = Color.FromArgb(251, 168, 94);
            //iColorPositive = Color.FromArgb(241, 234, 57);
            iColorNegative = Color.FromArgb(162, 209, 229);
            iColorNegativeOutlier = Color.FromArgb(37, 106, 173);

            iColorMapPartNegativeList = new List<IColorMapPart>();
            iColorMapPartNegativeList.Add(new LinearColorMapPart(iColorNegative, iColorNegativeOutlier));
            iColorMapPartNegativeList.Add(new SimpleColorMapPart(iColorNegativeOutlier));

            iColorMapPartPositiveList = new List<IColorMapPart>();
            iColorMapPartPositiveList.Add(new LinearColorMapPart(iColorNegative, iColorPositiveOutlier));
            iColorMapPartPositiveList.Add(new SimpleColorMapPart(iColorPositiveOutlier));

            iColorMapPartFullList = new List<IColorMapPart>();
            iColorMapPartFullList.Add(new SimpleColorMapPart(iColorNegativeOutlier));
            iColorMapPartFullList.Add(new SimpleColorMapPart(iColorNegative));
            iColorMapPartFullList.Add(new SimpleColorMapPart(iColorPositive));
            iColorMapPartFullList.Add(new SimpleColorMapPart(iColorPositiveOutlier));

            ResetColorMapPart(iColorMapPartFullList);
            
        }

        private void RemoveAllColorMapPartsButOne()
        {
            int numberOfParts = this.NumberOfParts;
            for (int i = 0; i < numberOfParts; i++)
            {
                this.RemoveColorMapPart(0);
            }
        }

        private void ResetColorMapPart(List<IColorMapPart> aColorMapPartFullList)
        {
            RemoveAllColorMapPartsButOne();
            int initialNumerOfParts = this.NumberOfParts;

            for (int i = 0; i < aColorMapPartFullList.Count; i++)
            {
                this.AddColorMapPart(aColorMapPartFullList[i]);
            }

            if (initialNumerOfParts > 0)
            {
                RemoveColorMapPart(0);
            }
            CalculateEdges();
        }

        public void CalculateColorMapShowingOutliers(DataContainer aDataContainer, int aSelectedColumn, int aSelectedSheet)
        {
            float edge0 = 0.0f;
            float edge1 = 0.0f;
            float edge2 = 0.0f;

            Input = aDataContainer.DataCubeYearFilter;
            Index = aSelectedColumn;

            edge0 = aDataContainer.DataOutlierFilter.Edge0[aSelectedColumn];
            edge1 = aDataContainer.DataOutlierFilter.Edge1[aSelectedColumn];
            edge2 = aDataContainer.DataOutlierFilter.Edge2[aSelectedColumn];
            DataOutlierFilter.TDataValueRange range = aDataContainer.DataOutlierFilter.Range[aSelectedColumn];

            DataOutlierFilter dataOutlierFilter = new DataOutlierFilter();

            float[] sortedDataColumn;
            dataOutlierFilter.GetSortedColumn(Input, aSelectedColumn, aSelectedSheet, out sortedDataColumn);

            float minValue = sortedDataColumn[0];
            float maxValue = sortedDataColumn[sortedDataColumn.GetLength(0) - 1];

            float[] positiveValues;
            float[] negativeValues;

            dataOutlierFilter.SplitPositiveNegativeValues(sortedDataColumn, out positiveValues, out negativeValues);

            switch (range)
            {
                case DataOutlierFilter.TDataValueRange.ENegative:
                    {
                        CalulateNegativeEdgeValues(negativeValues, maxValue, minValue, edge0, edge1, edge2);
                        break;
                    }
                case DataOutlierFilter.TDataValueRange.EPositive:
                    {
                        CalculatePostiveEdgeValues(positiveValues, maxValue, minValue, edge0, edge1, edge2);
                        break;
                    }
                case DataOutlierFilter.TDataValueRange.ENone:
                    {
                        break;
                    }
                default:
                    {
                        CalculateFullEdgeValues(positiveValues, negativeValues, maxValue, minValue, edge0, edge1, edge2);
                        break;
                    }
            }
        }

        private void CalculateFullEdgeValues(float[] positiveValues, float[] negativeValues, float maxValue, float minValue, float aEdge0, float aEdge1, float aEdge2)
        {
            float PositiveOutlierEdge = (aEdge2 - minValue) / (maxValue - minValue);
            float Zero = (aEdge1 - minValue) / (maxValue - minValue);
            float NegativeOutlierEdge = (aEdge0 - minValue) / (maxValue - minValue);

            ResetColorMapPart(iColorMapPartFullList);

            SetEdgeValue(0, NegativeOutlierEdge);
            SetEdgeValue(1, Zero);
            SetEdgeValue(2, PositiveOutlierEdge);
        }

        private void CalculatePostiveEdgeValues(float[] positiveValues, float maxValue, float minValue, float aEdge0, float aEdge1, float aEdge2)
        {
            float PositiveOutlierEdge = (aEdge2 - minValue) / (maxValue - minValue);

            ResetColorMapPart(iColorMapPartPositiveList);

            SetEdgeValue(0, PositiveOutlierEdge);
        }

        private void CalulateNegativeEdgeValues(float[] negativeValues, float maxValue, float minValue, float aEdge0, float aEdge1, float aEdge2)
        {
            float NegativeOutlierEdge = (aEdge0 - minValue) / (maxValue - minValue);

            ResetColorMapPart(iColorMapPartNegativeList);

            SetEdgeValue(0, NegativeOutlierEdge);
        }
    }
}
