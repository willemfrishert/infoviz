using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationSweden
{
    class DataContainerMigration : DataContainer
    {
        protected override void CopyColumnHeaders(string[] aHeaders)
        {
            for (int i = 4; i < aHeaders.GetLength(0); i++)
            {
                iColumnHeaders.Add(aHeaders[i]);
            }
        }

        protected override void CleanData(object[,] aRawData, out object[,] aCleanData)
        {
            //reserve space for columns without municipalities
            aCleanData = new object[aRawData.GetLength(0) - 4, aRawData.GetLength(1)];

            //copy the multivariate data
            for (int rowNumber = 0; rowNumber < aCleanData.GetLength(1); rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < aCleanData.GetLength(0); columnNumber++)
                {
                    aCleanData[columnNumber, rowNumber] = aRawData[columnNumber + 4, rowNumber];
                }
            }
        }

    }
}
