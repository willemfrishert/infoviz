using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Gav.Graphics;
using Gav.Data;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Redhotglue.Utilities.Extra;

using MigrationSweden;

namespace GAVExtra
{
    public class TableLens : VizComponent 
    {
        private IDataCubeProvider<float> iInput;
        private TableLensFilter iTableLensFilter;

        private Device iDevice;

        private Graphics iGraphics;

        private bool iInited = false;

        private List<RectangleF> iHeaderCell;
        private List<TableLensRectangle> iRowCell;
        private List<RectangleF> iColumnCell;

        private RectangleF iHeaderArea;
        private RectangleF iDataArea;
        private RectangleF iArea;

        private List<float> iMinimumFloatValue;
        private List<float> iMaximumFloatValue;
        private List<float> iZeroPixelValue;
        private Line iLine;

        //private int iColumnPadding;
        private float iColumnHeaderHeight;
        private float iTextHeight;
        private float iRowCellSelectedHeight;

        private float[, ,] iValueData;
        private float[, ,] iMetaData;
        //private float[, ,] iMunicipalityData;

        private DataOutlierFilter iDataOutlierFilter;

        private List<string> iHeaders;
        private List<string> iMunicipalityNames;

        private Color iFocusColor;
        private Color iContextColor;
        private Color iSelectedColumnColor;
        private Color iFocusPositiveOutLierColor;
        private Color iFocusPositiveOutLierBackGroundColor;
        private Color iFocusNegativeOutLierColor;
        private Color iFocusNegativeOutLierBackGroundColor;

        private Point iMouseButtonDownLocation;

        public Color FocusColor
        {
            get
            {
                return iFocusColor;
            }
            set
            {
                iFocusColor = value;
            }
        }

        public Color ContextColor
        {
            get
            {
                return iContextColor;
            }
            set
            {
                iContextColor = value;
            }
        }

        public Color SelectedColumnColor
        {
            get
            {
                return iSelectedColumnColor;
            }
            set
            {
                iSelectedColumnColor = value;
            }
        }

        public Color FocusColorPositiveOutlier
        {
            get
            {
                return iFocusPositiveOutLierColor;
            }
            set
            {
                iFocusPositiveOutLierColor = value;
            }
        }

        public Color FocusColorNegativeOutlier
        {
            get
            {
                return iFocusNegativeOutLierColor;
            }
            set
            {
                iFocusNegativeOutLierColor = value;
            }
        }

        /// <summary>
        /// The system font used when creating the Direct3D font.
        /// </summary>
        private System.Drawing.Font iFont;
        /// <summary>
        /// Object used to render text.
        /// </summary>
        private Microsoft.DirectX.Direct3D.Font iD3DFont;

        public List<string> Headers
        {
            set
            {
                iHeaders = value;
            }
            get
            {
                return iHeaders;
            }
        }

        public Color FocusPositiveOutLierBackGroundColor
        {
            set
            {
                iFocusPositiveOutLierBackGroundColor = value;
            }
            get
            {
                return iFocusPositiveOutLierBackGroundColor;
            }
        }

        public Color FocusNegativeOutLierBackGroundColor
        {
            set
            {
                iFocusNegativeOutLierBackGroundColor = value;
            }
            get
            {
                return iFocusNegativeOutLierBackGroundColor;
            }
        }

        public event EventHandler<IndexesPickedEventArgs> Picked;

        // Constructor
        public TableLens(List<string> aMunicipalityNames) 
        {
            iMunicipalityNames = aMunicipalityNames;
            iMouseButtonDownLocation = new Point();
            //iColumnHeaderHeight = 20;
            //iColumnPadding = 10;
            iTableLensFilter = new TableLensFilter();
            //iTableLensFilter.SelectedRows.Add(0);
            //iTableLensFilter.SelectedRows.Add(40);
            //iTableLensFilter.SelectedRows.Add(288);
            //iTableLensFilter.SelectedRows.Add(289);
            iContextColor = Color.Blue;
            iFocusColor = Color.Red;
            iSelectedColumnColor = Color.Red;
            iDataOutlierFilter = new DataOutlierFilter();

            iFocusPositiveOutLierColor = Color.Green;
            iFocusNegativeOutLierColor = Color.Green;
            iFocusNegativeOutLierBackGroundColor = Color.Orange;
            iFocusPositiveOutLierBackGroundColor = Color.Orange;
        }

 


        /// <summary>
        /// Gets or sets the data input.
        /// </summary>
        public IDataCubeProvider<float> Input 
        {
            get 
            {
                return iInput; 
            }
            set 
            {
                SetInput(value);
            }
        }
        /// <summary>
        /// Helper method to set the datacube
        /// </summary>
        /// <param name="input"></param>
        private void SetInput(IDataCubeProvider<float> input)
        {
            // If the input already is set. Remove the event listener.
            if (iInput != null) 
            {
                iInput.Changed -= new EventHandler(InputChangedInternal);
            }

            iInput = input;
            // Listen to changes in the input data.
            iInput.Changed += new EventHandler(InputChangedInternal);

            iTableLensFilter.Input = iInput;
            iTableLensFilter.CommitChanges();

            iDataOutlierFilter.Input = iTableLensFilter;
            iDataOutlierFilter.CommitChanges();
        }

        /// <summary>
        /// This method returns true if the input is valid, else false.
        /// </summary>
        /// <returns></returns>
        private bool IsInputValid() 
        {
            return !(iInput == null || iInput.GetData() == null || iInput.GetData().Data == null);
        }

        /// <summary>
        /// This methos is called if the input data has changed. 
        /// </summary>
        void InputChangedInternal(object sender, EventArgs e)
        {
            // set something since the data has changed
        }

        /// <summary>
        /// This method is called when the component should be initialized.
        /// </summary>
        protected override void InternalInit(Device aDevice)
        {
            iInited = InitializeComponent(aDevice);
            if (iInited)
            {
                InternalInvalidate();
            }

        }

        private void CalculateZeroPixelValue(List<float> maximumFloatValue, List<float> minimumFloatValue)
        {
            iZeroPixelValue = new List<float>();

            for (int i = 0; i < iColumnCell.Count; i++)
            {
                if (minimumFloatValue[i] > 0.0f)
                {
                    minimumFloatValue[i] = 0.0f;
                }

                if (maximumFloatValue[i] < 0.0f)
                {
                    maximumFloatValue[i] = 0.0f;
                }

                float pixelX = (int)(((0 - minimumFloatValue[i]) / (maximumFloatValue[i] - minimumFloatValue[i])) * iColumnCell[i].Width + iColumnCell[i].X);
                //if (pixelX < iColumnCell[i].Left)
                //{
                //    pixelX = iColumnCell[i].Left;
                //}
                //if (pixelX > iColumnCell[i].Right)
                //{
                //    pixelX = iColumnCell[i].Right;
                //}

                iZeroPixelValue.Add(pixelX);
            }
        }

        private void DrawTableLensLine(float aValue, int aColumnCounter, float aRowPixelNumber, TableLensRectangle aTableLensRect, Color aColor)
        {
            float aMinimumFloatValue = iMinimumFloatValue[aColumnCounter];
            float aMaximumFloatValue = iMaximumFloatValue[aColumnCounter];
            float aZeroPixelX        = iZeroPixelValue[aColumnCounter];
            RectangleF columnRect    = iColumnCell[aColumnCounter];

            float valuePixelX = (int)(((aValue - aMinimumFloatValue) / (aMaximumFloatValue - aMinimumFloatValue)) * columnRect.Width + columnRect.X);

            iLine.Begin();

            // Create a new vector to hold the axis two positions.
            Vector2[] v = new Vector2[2];
            v[0].X = aZeroPixelX;
            v[0].Y = aRowPixelNumber;
            v[1].X = valuePixelX;
            v[1].Y = aRowPixelNumber;

            // Render the x-axis.
            iLine.Draw(v, aColor);

            // Tell DirectX that we are finished rendering lines. 
            iLine.End();
        }

        private void CreateHeaderCells(float[, ,] aData)
        {
            iHeaderCell = new List<RectangleF>();

            int numberOfColumns = aData.GetLength(0);

            RectangleF cellHeaderTemplate = new RectangleF();
            cellHeaderTemplate.Width = iHeaderArea.Right / (float)(numberOfColumns);
            cellHeaderTemplate.Height = iHeaderArea.Height;
            cellHeaderTemplate.X = 0.0f;
            cellHeaderTemplate.Y = iHeaderArea.Y;


            RectangleF municipalityCell = new RectangleF();
            municipalityCell.X = cellHeaderTemplate.Left;
            municipalityCell.Width = cellHeaderTemplate.Width * 1.2f;
            municipalityCell.Y = cellHeaderTemplate.Top;
            municipalityCell.Height = cellHeaderTemplate.Height;
            iHeaderCell.Add(municipalityCell);

            cellHeaderTemplate.X += municipalityCell.Width;
            cellHeaderTemplate.Width = (iHeaderArea.Right - municipalityCell.Width) / (float)(numberOfColumns-1);

            //float yOffset = iColumnHeaderHeight;
            //float xOffSet = 0.0f;

            //float maxTextWidth = iGraphics.MeasureString("Column Number One", iFont).Width;

            //for (int i = 0; i < numberOfColumns; i++)
            //{
            //    float textWidth = iGraphics.MeasureString(aHeaders[i], iFont).Width;
            //    float value = iGraphics.MeasureString(aHeaders[i], iFont).Height * (float)Math.Ceiling((maxTextWidth / width));
            //    if (yOffset < value)
            //    {
            //        yOffset = value;
            //    }
            //}


            for (int i = 1; i < numberOfColumns; i++)
            {
                RectangleF cell = new RectangleF();
                cell.X = cellHeaderTemplate.Left;
                cell.Width = cellHeaderTemplate.Width;
                cell.Y = cellHeaderTemplate.Top;
                cell.Height = cellHeaderTemplate.Height;

                // make the last cell of the headers equal a little bigger so all the screen space is used.
                //if (i == (numberOfColumns - 1))
                //{
                //    //cell.Width = cellHeaderTemplate.Width + (iHeaderArea.Width - ((i + 1) * cellHeaderTemplate.Width)) - 2;
                //}
                iHeaderCell.Add(cell);

                cellHeaderTemplate.X += cell.Width;
                //xOffSet += width;

            }

            //aDataRectangle = new RectangleF();
            //aDataRectangle.X = rectangle.X;
            //aDataRectangle.Y = yOffset;
            //aDataRectangle.Height = ((aDataRectangle.Y + aData.GetLength(1)) < iDevice.Viewport.Height) ? aData.GetLength(1) : iDevice.Viewport.Height - yOffset;
            //aDataRectangle.Width = rectangle.Width;

        }

        //private void CalculateColumnDataRectangle(float[, ,] aData, RectangleF aDataRegionRectangle, out List<RectangleF> aColumnRectangle)
        //{
        //    //Rectangle rectangle = new Rectangle(iDevice.Viewport.X, iDevice.Viewport.Y, iDevice.Viewport.Width, iDevice.Viewport.Height);
        //    int numberOfColumns = aData.GetLength(0);

        //    aColumnRectangle = new List<RectangleF>();

        //    float width = (float)(aDataRegionRectangle.Width) / (float)(numberOfColumns);
        //    float yOffset = aDataRegionRectangle.Y;
        //    float xOffSet = aDataRegionRectangle.X;

        //    for (int i = 0; i < numberOfColumns; i++)
        //    {
        //        RectangleF columnRectangle = new RectangleF();

        //        columnRectangle.Height = aDataRegionRectangle.Height;
        //        columnRectangle.Y = yOffset;
        //        columnRectangle.X = xOffSet;
        //        columnRectangle.Width = width;

        //        if (i == (numberOfColumns - 1))
        //        {
        //            columnRectangle.Width = width + (aDataRegionRectangle.Width - ((i + 1) * width)) - 1;
        //        }

        //        //columnRectangle.X += iColumnPadding/2.0f;
        //        //columnRectangle.Width -= iColumnPadding/2.0f;


        //        aColumnRectangle.Add(columnRectangle);

        //        xOffSet += width;
        //    }
        //}

        /// <summary>
        /// Intialize the component. We return false if the intialization fails.
        /// </summary>
        private bool InitializeComponent(Device aDevice)
        {

            // If the device we cannot initialize the component. 
            if (aDevice == null)
            {
                return false;
            }

            if (iInput == null)
            {
                return false;
            }
            iDevice = aDevice;
            iLine = new Line(iDevice);

            //iTableLensFilter.Input = iInput;
            //iTableLensFilter.CommitChanges();

            //iDataOutlierFilter.Input = iInput;
            //iDataOutlierFilter.CommitChanges();


            // Create the system font used for the Direct3D font. 
            iFont = new System.Drawing.Font("Verdana", 8, FontStyle.Regular);
            // Create the Direct3D font used when rendering text. 
            iD3DFont = new Microsoft.DirectX.Direct3D.Font(iDevice, iFont);

            iGraphics = RenderTarget.CreateGraphics();
            iTextHeight = iGraphics.MeasureString("Column", iFont).Height;
            iColumnHeaderHeight = iTextHeight * 5.0f;

            //iRowCellNotSelectedHeight = 1.0f;
            iRowCellSelectedHeight = iTextHeight*1.5f + 3.0f;


            //this.RenderTarget.KeyDown += new System.Windows.Forms.KeyEventHandler(OnKeyDown);
            //this.RenderTarget.KeyUp += new System.Windows.Forms.KeyEventHandler(OnKeyUp);
            // If we get this far the initialization has succeded an we can return true.
            return true;
        }

        //void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    iKeyboardEvent = e;
        //}

        //void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    iKeyboardEvent = e;
        //}

        /// <summary>
        /// This method is always called before a InternalRender call. 
        /// </summary>
        protected override void InternalInvalidate()
        {

            //If the input is invalid return.
            if (!IsInputValid())
            {
                return;
            }


            //// Invalidate the axis maps. 
            //InvalidateAxisMaps();
        }

        // VERY IMPORTANT METHOD! SETS UP EVERYTHING WHEN THE RENDERER ASKS FOR IT
        /// <summary>
        /// This method is called by GAV everytime the component needs to render.
        /// </summary>
        protected override void InternalRender(Device aDevice)
        {
            // Store the device in the local variable _device.
            iDevice = aDevice;

            // If the component is not initialize call the InternalInit method.
            if (!iInited)
            {
                InternalInit(aDevice);
            }

            // If the component input is invalid (not set) return.
            if (!IsInputValid())
            {
                return;
            }

            float[, ,] data = iTableLensFilter.GetData().Data;
            float[, ,] dataOutlier = iDataOutlierFilter.GetData().Data;


            ExtractMetaData(data, out iValueData, out iMetaData);

            List<string> headerNames;
            CreateHeaderNames(iValueData, out headerNames);

            CreateAreas(iValueData, iColumnHeaderHeight);

            CreateHeaderCells(iValueData);

            CreateColumnCells();

            CreateRowCells(iValueData, iMetaData);

            //CalculateColumnDataRectangle(valueData, iDataRegionRectangle, out iColumnDataRectangle);


            
            GetAllColumnMaxMin(iValueData, out iMaximumFloatValue, out iMinimumFloatValue);

            CalculateZeroPixelValue(iMaximumFloatValue, iMinimumFloatValue);

            //// Draw/Render stuff
            DrawData(iValueData, dataOutlier);
            DrawFrame();
            DrawHeaders(headerNames);

            aDevice.RenderState.CullMode = Cull.None;

            CustomVertex.PositionOnly[] rect = new CustomVertex.PositionOnly[4];

            rect[0].Position = new Vector3(0, 1, 0);
            rect[1].Position = new Vector3(1, 1, 0);
            rect[2].Position = new Vector3(0, 0, 0);
            rect[3].Position = new Vector3(1, 0, 0);


            //aDevice.VertexFormat = CustomVertex.PositionOnly.Format;
            //aDevice.RenderState.Lighting = true;
            //aDevice.RenderState.AlphaBlendEnable = false;

            //Material m = new Material();
            //m.Emissive = Color.Red;
            //m.Diffuse = Color.Red;
            //aDevice.Material = m;
            //aDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, rect);


        }

        private void GetAllColumnMaxMin(float[, ,] data, out List<float> max, out List<float> min)
        {
            max = new List<float>();
            min = new List<float>();

            for (int columnNumber = 0; columnNumber < data.GetLength(0); columnNumber++)
            {
                max.Add(float.MinValue);
                min.Add(float.MaxValue);
            }

            for (int rowNumber = 0; rowNumber < data.GetLength(1); rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < data.GetLength(0); columnNumber++)
                {
                    float valueData = data[columnNumber, rowNumber, 0];

                    if (float.IsNaN(valueData))
                    {
                        valueData = 0.0f;
                    }

                    max[columnNumber] = (valueData > max[columnNumber]) ?
                        valueData : max[columnNumber];

                    min[columnNumber] = (valueData < min[columnNumber]) ?
                        valueData : min[columnNumber];

                }
            }
        }

        private void CreateRowCells(float[,,] aData, float[,,] aMetaData)
        {
            iRowCell = new List<TableLensRectangle>();

            List<int> selectedRows;
            RetrieveSelectedRows(aMetaData, out selectedRows);

            CreateDataAreaRectangle(aData, selectedRows);
        }

        private void CreateColumnCells()
        {
            iColumnCell = new List<RectangleF>();

            for (int i = 0; i < iHeaderCell.Count; i++)
            {
                RectangleF cell = new RectangleF();
                cell.X = iHeaderCell[i].Left;
                cell.Y = iHeaderCell[i].Top;
                cell.Width = iHeaderCell[i].Width;
                cell.Height = iArea.Height;

                iColumnCell.Add(cell);
            }
        }

        private void CreateAreas(float[, ,] aValueData, float aColumnHeight)
        {
            iArea = new RectangleF();
            iArea.X = iDevice.Viewport.X;
            iArea.Y = iDevice.Viewport.Y;
            iArea.Width = iDevice.Viewport.Width;
            iArea.Height = aColumnHeight + aValueData.GetLength(1);
            
            if (iDevice.Viewport.Height < iArea.Height)
            {
                iArea.Height = iDevice.Viewport.Height;
            }
            
            iHeaderArea = new RectangleF(iArea.X, iArea.Y, iArea.Width, aColumnHeight);

            iDataArea = new RectangleF(iArea.X, iHeaderArea.Bottom, iArea.Width, aValueData.GetLength(1));
            if (iDataArea.Bottom > iArea.Bottom)
            {
                iDataArea.Height = iArea.Bottom - iHeaderArea.Bottom;
            }
        }

        private void ExtractMetaData(float[, ,] data, /*out float[,,] municipalityData,*/ out float[, ,] valueData, out float[, ,] metaData)
        {
            valueData = new float[data.GetLength(0) - 2, data.GetLength(1), data.GetLength(2)];
            metaData = new float[2, data.GetLength(1), data.GetLength(2)];
            //municipalityData = new float[1, data.GetLength(1), data.GetLength(2)];

            //for (int rowCounter = 0; rowCounter < municipalityData.GetLength(1); rowCounter++)
            //{
            //    for (int sheetCounter = 0; sheetCounter < municipalityData.GetLength(2); sheetCounter++)
            //    {
            //        municipalityData[0, rowCounter, sheetCounter] = data[0, rowCounter, sheetCounter];
            //    }
            //}

            for (int columnCounter = 0; columnCounter < valueData.GetLength(0); columnCounter++)
            {
                for (int rowCounter = 0; rowCounter < valueData.GetLength(1); rowCounter++)
                {
                    for (int sheetCounter = 0; sheetCounter < valueData.GetLength(2); sheetCounter++)
                    {
                        valueData[columnCounter, rowCounter, sheetCounter] = data[columnCounter, rowCounter, sheetCounter];
                    }
                }
            }

            for (int rowCounter = 0; rowCounter < metaData.GetLength(1); rowCounter++)
            {
                for (int sheetCounter = 0; sheetCounter < metaData.GetLength(2); sheetCounter++)
                {
                    metaData[0, rowCounter, sheetCounter] = data[data.GetLength(0) - 2, rowCounter, sheetCounter];
                    metaData[1, rowCounter, sheetCounter] = data[data.GetLength(0) - 1, rowCounter, sheetCounter];
                }
            }

        }

        private void DrawData(float[, ,] aData, float[,,] aDataOutlier)
        {
            for (int i = 0; i < iRowCell.Count; i++ )
            {
                if (iRowCell[i].FocusRectangle)
                {
                    DrawDataFocusRectangle(aData, aDataOutlier, iRowCell[i]);
                }
                else
                {
                    DrawDataContextRectangle(aData, iRowCell[i]);
                }
            }
        }

        private void DrawDataFocusRectangle(float[, ,] aData, float[, ,] aDataOutlier, TableLensRectangle iRowCell)
        {
            //Sprite batchSprite = new Sprite(iDevice);

            //batchSprite.Begin(SpriteFlags.AlphaBlend | SpriteFlags.SortTexture);
            for (int columnCounter = 0; columnCounter < aData.GetLength(0); columnCounter++)
            {
                float dataValue = aData[columnCounter, iRowCell.SelectedRow, 0];
                //float outlierValue = aDataOutlier[columnCounter, (int)iMetaData[0, iRowCell.SelectedRow, 0], 0];
                float outlierValue = aDataOutlier[columnCounter, iRowCell.SelectedRow, 0];

                if (float.IsNaN(dataValue))
                {
                    dataValue = 0.0f;
                }
                Color focusColor = iFocusColor;
                Color bgColor = Color.White;

                if (!(iDataOutlierFilter.Range[columnCounter] == DataOutlierFilter.TDataValueRange.EPositive))
                {
                    if (outlierValue == DataOutlierFilter.KNegativeOutLier)
                    {
                        focusColor = iFocusNegativeOutLierColor;
                        bgColor = iFocusNegativeOutLierBackGroundColor;
                    }
                }

                if (!(iDataOutlierFilter.Range[columnCounter] == DataOutlierFilter.TDataValueRange.ENegative))
                {
                    if (outlierValue == DataOutlierFilter.KPositiveOutLier)
                    {
                        focusColor = iFocusPositiveOutLierColor;
                        bgColor = iFocusPositiveOutLierBackGroundColor;
                    }
                }

                RectangleF borderRect = new RectangleF();
                borderRect.X = iColumnCell[columnCounter].X;
                borderRect.Y = iRowCell.Rect.Y;
                borderRect.Width = iColumnCell[columnCounter].Width;
                borderRect.Height = iRowCell.Rect.Height - 1;
                DrawRectangle(borderRect, Color.Black, bgColor);


                DrawTableLensLine(dataValue,
                                  columnCounter,
                                  iRowCell.Rect.Y + 1,
                                  iRowCell,
                                  focusColor);
                DrawTableLensLine(dataValue,
                                  columnCounter,
                                  iRowCell.Rect.Y + 2,
                                  iRowCell,
                                  focusColor);
                DrawTableLensLine(dataValue,
                                  columnCounter,
                                  iRowCell.Rect.Y + 3,
                                  iRowCell,
                                  focusColor);

                Rectangle rect = new Rectangle();
                rect.X = (int)iColumnCell[columnCounter].X;
                rect.Y = (int)iRowCell.Rect.Y;
                rect.Width = (int)iColumnCell[columnCounter].Width;
                rect.Height = (int)iRowCell.Rect.Height;


                String text = aData[columnCounter, iRowCell.SelectedRow, 0].ToString();
                DrawTextFormat format = DrawTextFormat.Left | DrawTextFormat.VerticalCenter;
                if (columnCounter == 0)
                {
                    text = iMunicipalityNames[(int)iMetaData[0,iRowCell.SelectedRow,0]];

                    format = DrawTextFormat.Left | DrawTextFormat.VerticalCenter | DrawTextFormat.WordBreak;
                }

                if (float.IsNaN(aData[columnCounter, iRowCell.SelectedRow, 0]))
                {
                    text = "None";
                }
                iD3DFont.DrawText(null,
                                  text,
                                  rect,
                                  format,
                                  Color.Black);
            }
            //batchSprite.End();
        }

        private void DrawDataContextRectangle(float[,,] aData, TableLensRectangle aRowCell)
        {
            float numberOfPixelY = aRowCell.Rect.Height;
            float incrementPixel = (aRowCell.MaximumRowNumber - aRowCell.MinimumRowNumber) / numberOfPixelY;
            float rowNumber = aRowCell.MinimumRowNumber;

            for (int rowCounter = (int)aRowCell.Rect.Y; rowCounter < (int)aRowCell.Rect.Bottom; rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < aData.GetLength(0); columnCounter++)
                {
                    float dataValue = aData[columnCounter, (int)rowNumber, 0];
                    if (rowNumber > (aData.GetLength(1) - 1))
                    {
                        rowNumber = aData.GetLength(1) - 1;
                    }

                    if (float.IsNaN(dataValue))
                    {
                        dataValue = 0.0f;
                    }

                    DrawTableLensLine(dataValue,
                                      columnCounter,
                                      rowCounter,
                                      aRowCell,
                                      iContextColor);
                }
                rowNumber += incrementPixel;
            }
        }

        private void CreateDataAreaRectangle(float[, ,] aData, List<int> selectedRows)
        {
            iRowCell = new List<TableLensRectangle>();
            List<TableLensRectangle> focusCell = new List<TableLensRectangle>();
            int numberOfRows = aData.GetLength(1);

            TableLensRectangle tableLensRectangle;
            bool overlap = true;

            //selectedRows.Add(0);
            //selectedRows.Add(1);
            //selectedRows.Add(numberOfRows /2 - 1);
            //selectedRows.Add(numberOfRows / 2);
            //selectedRows.Add(numberOfRows - 2);
            //selectedRows.Add(numberOfRows-1);

            for (int i = 0; i < selectedRows.Count; i++)
            {
                // add rectangle for focus
                tableLensRectangle = new TableLensRectangle();

                tableLensRectangle.FocusRectangle = true;
                tableLensRectangle.SelectedRow = selectedRows[i];

                RectangleF rect = new RectangleF();
                rect.X = iDataArea.X;
                rect.Y = ((selectedRows[i] / (float)numberOfRows) * iDataArea.Height + iDataArea.Y) - iRowCellSelectedHeight / 2.0f;
                rect.Width = iDataArea.Width;
                rect.Height = iRowCellSelectedHeight;
                tableLensRectangle.Rect = rect;
                focusCell.Add(tableLensRectangle);                    
            }

            // fix cells that went overboard (outside the dataArea)
            for (int i = 0; i < focusCell.Count; i++)
            {
                RectangleF rect = focusCell[i].Rect;
                if (rect.Y < iDataArea.Y)
                {
                    rect.Y = iDataArea.Y;
                    focusCell[i].Rect = rect;
                }
            }

            // fix row so they don't overlap
            while (overlap)
            {
                overlap = false;

                for (int i = 0; i < focusCell.Count - 1; i++)
                {
                    for (int j = i + 1; j < focusCell.Count; j++)
                    {
                        RectangleF rect = focusCell[j].Rect;

                        if ((focusCell[i].Rect.Y <= rect.Y) && (focusCell[i].Rect.Bottom > rect.Y))
                        {
                            overlap = true;
                            rect.Y = focusCell[i].Rect.Bottom;
                            focusCell[j].Rect = rect;
                        }
                    }
                }
            }

            // fix cells that went overboard (outside the dataArea)
            for (int i = 0; i < focusCell.Count; i++)
            {
                RectangleF rect = focusCell[i].Rect;

                if (rect.Bottom > iDataArea.Bottom)
                {
                    rect.Y -= (rect.Bottom - iDataArea.Bottom);
                    focusCell[i].Rect = rect;
                }
            }

            overlap = true;
            // fix row so they don't overlap
            while (overlap)
            {
                overlap = false;

                for (int i = focusCell.Count - 1; i >= 0; i--)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        RectangleF rect = focusCell[j].Rect;

                        if ((focusCell[i].Rect.Y < rect.Bottom) && (focusCell[i].Rect.Bottom >= rect.Bottom))
                        {
                            overlap = true;
                            rect.Y -= (focusCell[i].Rect.Height - (focusCell[i].Rect.Bottom - focusCell[j].Rect.Bottom));
                            focusCell[j].Rect = rect;
                        }

                    }
                }
            }

            int rowNumberStart = 0;
            int rowNumberStop = 0;

            for (int i = 0; i < focusCell.Count; i++)
            {
                // the first row is selected so no context in the beginning
                // else, add rectangle for context
                if (focusCell[i].Rect.Y > iDataArea.Y)
                {
                    RectangleF contextRect = new RectangleF();
                    contextRect.X = 0.0f;
                    contextRect.Y = iDataArea.Y;
                    contextRect.Width = iDataArea.Width;
                    
                    if (i > 0)
                    {
                        contextRect.Y = focusCell[i - 1].Rect.Bottom;
                        rowNumberStart = focusCell[i-1].SelectedRow + 1;
                    }

                    if (i == focusCell.Count)
                    {
                        contextRect.Height = iDataArea.Bottom - contextRect.Y;
                        rowNumberStop = aData.GetLength(1)-1;

                    }
                    else
                    {
                        contextRect.Height = focusCell[i].Rect.Top - contextRect.Y;
                        rowNumberStop = focusCell[i].SelectedRow - 1;
                    }

                    tableLensRectangle = new TableLensRectangle();
                    tableLensRectangle.FocusRectangle = false;
                    tableLensRectangle.MinimumRowNumber = rowNumberStart;
                    tableLensRectangle.MaximumRowNumber = rowNumberStop;

                    tableLensRectangle.Rect = contextRect;

                    iRowCell.Add(tableLensRectangle);
                }

                iRowCell.Add(focusCell[i]);

            }


            if ( (iRowCell.Count > 0) && (iRowCell[iRowCell.Count - 1].Rect.Bottom < iDataArea.Bottom) )
            {
                tableLensRectangle = new TableLensRectangle();
                tableLensRectangle.FocusRectangle = false;
                tableLensRectangle.MinimumRowNumber = iRowCell[iRowCell.Count - 1].SelectedRow + 1;
                tableLensRectangle.MaximumRowNumber = aData.GetLength(1) - 1;


                RectangleF contextRect = new RectangleF();
                contextRect.X = 0.0f;
                contextRect.Y = iRowCell[iRowCell.Count-1].Rect.Bottom;
                contextRect.Width = iDataArea.Width;
                contextRect.Height = iDataArea.Bottom - contextRect.Y;

                tableLensRectangle.Rect = contextRect;

                iRowCell.Add(tableLensRectangle);

            }

            if (selectedRows.Count == 0)
            {
                tableLensRectangle = new TableLensRectangle();
                tableLensRectangle.FocusRectangle = false;
                tableLensRectangle.MinimumRowNumber = 0;
                tableLensRectangle.MaximumRowNumber = aData.GetLength(1) - 1;

                tableLensRectangle.Rect = iDataArea;

                iRowCell.Add(tableLensRectangle);

            }
        }

        private void RetrieveSelectedRows(float[, ,] aMetaData, out List<int> selectedRows)
        {
            selectedRows = new List<int>();
            for (int i=0; i<aMetaData.GetLength(1); i++)
            {
                if (aMetaData[1, i, 0] > 0.0f)
                {
                    selectedRows.Add(i);
                }
            }
        }

        private void CreateHeaderNames(float[, ,] aData, out List<string> headers)
        {
            headers = new List<string>();

            headers.Add("Municipalities");

            for (int i = 0; i < aData.GetLength(0)-1; i++)
            {
                String headerName = "Column " + i;

                if ((iHeaders != null) && (iHeaders[i] != null))
                {
                    headerName = iHeaders[i];
                }

                headers.Add(headerName);
            }
        }

        private void DrawHeaders(List<string> aHeaders)
        {

            for (int i = 0; i < iHeaderCell.Count; i++)
            {

                Rectangle rect = new Rectangle((int)iHeaderCell[i].X,
                                               (int)iHeaderCell[i].Y,
                                               (int)iHeaderCell[i].Width,
                                               (int)iHeaderCell[i].Height);

                //float maxTextWidth = iGraphics.MeasureString("Column Number One", iFont).Width;
                //float maxTextHeight = 0.0f;
                //for (int j = 0; j < aHeaders.Count; j++)
                //{
                //    float textWidth = iGraphics.MeasureString(aHeaders[j], iFont).Width;
                //    float value = iGraphics.MeasureString(aHeaders[j], iFont).Height * (float)Math.Ceiling((iDevice.Viewport.Width / maxTextWidth));
                //    if (maxTextHeight < value)
                //    {
                //        maxTextHeight = value;
                //    }
                //}

                //Rectangle textRect = new Rectangle((int)columnHeaderRectangle[i].X,
                //                                   (int)columnHeaderRectangle[i].Y,
                //                                   (int)maxTextWidth,
                //                                   (int)maxTextHeight);


                //Sprite batchSprite = new Sprite(iDevice);
                //batchSprite.Begin(SpriteFlags.AlphaBlend | SpriteFlags.SortTexture);
                //Matrix mTranslate = new Matrix();
                //Matrix mRotate = new Matrix();
                //Matrix mInvTranslate = new Matrix();
                //mTranslate.Translate(rect.X + rect.Width / 2.0f, rect.Y, 0);
                //mRotate.RotateZ((float)(-Math.PI / 2.0f));
                //mInvTranslate.Translate(-(rect.X + rect.Width / 2.0f), -rect.Y, 0);
                //batchSprite.Transform = mInvTranslate * mRotate * mTranslate;

                Color c = Color.Black;
                if (i == iTableLensFilter.ColumnToOrder)
                {
                    c = iSelectedColumnColor;
                }
                iD3DFont.DrawText(null,
                                  aHeaders[i],
                                  rect,
                                  DrawTextFormat.Center | DrawTextFormat.VerticalCenter | DrawTextFormat.WordBreak,
                                  c);

                //batchSprite.End();
                //batchSprite.
            }
        }

        private void DrawFrame()
        {
            for (int i = 0; i < iColumnCell.Count; i++)
            {
                DrawRectangle(iColumnCell[i], Color.Black);
            }


            for (int i = 0; i < iHeaderCell.Count; i++)
            {
                RectangleF rect = new RectangleF();
                rect.X = iHeaderCell[i].X;
                rect.Y = iHeaderCell[i].Y;
                rect.Width = iHeaderCell[i].Width;
                rect.Height = iHeaderCell[i].Height - 2;
                DrawRectangle(iHeaderCell[i], Color.Black);
            }
        }

        private void DrawRectangle(Rectangle aRect, Color aLineColor, Color aFillColor)
        {
            RectangleF rect = new RectangleF(aRect.X, aRect.Y, aRect.Width, aRect.Height);
            DrawRectangle(rect, aLineColor, aFillColor);
        }

        private void DrawRectangle(Rectangle aRect, Color aLineColor)
        {
            RectangleF rect = new RectangleF(aRect.X, aRect.Y, aRect.Width, aRect.Height);
            DrawRectangle(rect, aLineColor);
        }

        private void DrawRectangle(RectangleF aRect, Color aLineColor, Color aFillColor)
        {
            iDevice.Transform.World = Matrix.Identity;
            iDevice.Transform.View = Matrix.Identity;
            iDevice.Transform.Projection = Matrix.OrthoOffCenterLH(iDevice.Viewport.X, iDevice.Viewport.Width, iDevice.Viewport.Height, iDevice.Viewport.Y, 0, 1);

            CustomVertex.PositionOnly[] rect = new CustomVertex.PositionOnly[4];

            rect[0].Position = new Vector3(aRect.Left, aRect.Top, 1);
            rect[1].Position = new Vector3(aRect.Right, aRect.Top, 1);
            rect[2].Position = new Vector3(aRect.Left, aRect.Bottom, 1);
            rect[3].Position = new Vector3(aRect.Right, aRect.Bottom, 1);


            iDevice.VertexFormat = CustomVertex.PositionOnly.Format;
            iDevice.RenderState.Lighting = true;
            iDevice.RenderState.AlphaBlendEnable = false;

            Material m = new Material();
            m.Emissive = aFillColor;
            m.Diffuse = aFillColor;
            iDevice.Material = m;
            iDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, rect);

            //Texture texture = new Texture(iDevice, aRect.Width, aRect.Height,0, Usage.None, Format.A8R8G8B8, Pool.Default);
            //Sprite sprite = new Sprite(iDevice);
            //CustomVertex.PositionOnly[] iFlagVertices = new CustomVertex.PositionOnly[4];

            //iFlagVertices[0].X = (float)Math.Floor(aRect.Left);
            //iFlagVertices[0].Y = (float)Math.Floor(aRect.Top);

            //iFlagVertices[1].X = (float)Math.Floor(aRect.Right);
            //iFlagVertices[1].Y = (float)Math.Floor(aRect.Top);

            //iFlagVertices[2].X = (float)Math.Floor(aRect.Right);
            //iFlagVertices[2].Y = (float)Math.Floor(aRect.Bottom);

            //iFlagVertices[3].X = (float)Math.Floor(aRect.Left);
            //iFlagVertices[3].Y = (float)Math.Floor(aRect.Bottom);


            //Material material = new Material();
            //material.Emissive = aFillColor;
            //material.Diffuse = aFillColor;
            //iDevice.Material = material;
            //iDevice.DrawUserPrimitives(PrimitiveType.TriangleList, 2, iFlagVertices);

            //Vector2[] v2 = new Vector2[2];
            //float width = iLine.Width;
            //iLine.Begin();

            //float calculatedWidth = aRect.Height / 2.0f;
            //iLine.Width = calculatedWidth;

            //v2[0].X = (float)Math.Floor(aRect.Left);
            //v2[0].Y = (float)Math.Floor(aRect.Top + aRect.Height / 2.0f);
            //v2[1].X = (float)Math.Floor(aRect.Left);
            //v2[1].Y = (float)Math.Floor(aRect.Top + aRect.Height / 2.0f);
            //iLine.Draw(v2, aFillColor);
            //iLine.Width = width;

            Vector2[] v = new Vector2[5];

            iLine.Begin();

            v[0].X = (float)Math.Floor(aRect.Left);
            v[0].Y = (float)Math.Floor(aRect.Top);

            v[1].X = (float)Math.Floor(aRect.Right);
            v[1].Y = (float)Math.Floor(aRect.Top);

            v[2].X = (float)Math.Floor(aRect.Right);
            v[2].Y = (float)Math.Floor(aRect.Bottom);

            v[3].X = (float)Math.Floor(aRect.Left);
            v[3].Y = (float)Math.Floor(aRect.Bottom);

            v[4].X = v[0].X;
            v[4].Y = v[0].Y;

            iLine.Draw(v, aLineColor);
            iLine.End();
        }

        private void DrawRectangle(RectangleF aRect, Color aLineColor)
        {
            Vector2[] v = new Vector2[5];

            iLine.Begin();

            v[0].X = (float)Math.Floor(aRect.Left);
            v[0].Y = (float)Math.Floor(aRect.Top);

            v[1].X = (float)Math.Floor(aRect.Right);
            v[1].Y = (float)Math.Floor(aRect.Top);

            v[2].X = (float)Math.Floor(aRect.Right);
            v[2].Y = (float)Math.Floor(aRect.Bottom);

            v[3].X = (float)Math.Floor(aRect.Left);
            v[3].Y = (float)Math.Floor(aRect.Bottom);

            v[4].X = v[0].X;
            v[4].Y = v[0].Y;

            iLine.Draw(v, aLineColor);
            iLine.End();
        }




        // This method is called when the mouse is moving over the component.
        protected override void InternalMouseMove(System.Windows.Forms.MouseEventArgs e) 
        {
            if (iInited)
            {
            }
        }

        // This method is called when a mouse button is released over the component.
        protected override void InternalMouseUp(System.Windows.Forms.MouseEventArgs e) 
        {
            if (iInited)
            {
                int mouseUpColumnNumber = -1;
                int mouseDownColumnNumber = -2;
                int mouseUpRowNumber = -1;
                int mouseDownRowNumber = -2;
                if (HitHeader(iMouseButtonDownLocation, out mouseDownColumnNumber) && HitHeader(e.Location, out mouseUpColumnNumber))
                {
                    if (mouseUpColumnNumber == mouseDownColumnNumber)
                    {
                        this.iTableLensFilter.ColumnToOrder = mouseDownColumnNumber;
                        this.iTableLensFilter.CommitChanges();
                        this.Invalidate();
                    }
                }



                if (HitData(iMouseButtonDownLocation, out mouseDownRowNumber))
                {
                    PointF iMouseButtonUpLocation = new PointF(e.Location.X, e.Location.Y);


                    HitData(iMouseButtonUpLocation, out mouseUpRowNumber);

                    if (iMouseButtonUpLocation.Y < iDataArea.Top)
                    {
                        mouseUpRowNumber = 0;
                    }
                    if (iMouseButtonUpLocation.Y > iDataArea.Bottom)
                    {
                        mouseUpRowNumber = Input.GetData().Data.GetLength(1);
                    }

                    if (Control.ModifierKeys != Keys.Control)
                    {
                        iTableLensFilter.SelectedRows = new List<int>();
                    }

                    int rowMin = mouseDownRowNumber;
                    int rowMax = mouseUpRowNumber;

                    if (rowMin == rowMax)
                    {
                        if (rowMax < Input.GetData().Data.GetLength(1) - 1 )
                        {
                            rowMax++;
                        }
                        else
                        {
                            rowMin--;
                        }
                    }

                    if (rowMin < rowMax)
                    {
                        for (int i = rowMin; i < rowMax; i++)
                        {
                            if (!iTableLensFilter.SelectedRows.Contains((int)iMetaData[0, i, 0]))
                            {
                                iTableLensFilter.SelectedRows.Add((int)iMetaData[0, i, 0]);
                            }
                        }
                    }
                    else
                    {
                        int temp = rowMax;
                        rowMax = rowMin;
                        rowMin = temp;
                        for (int i = rowMax-1; i >= rowMin; i--)
                        {
                            if (!iTableLensFilter.SelectedRows.Contains((int)iMetaData[0, i, 0]))
                            {
                                iTableLensFilter.SelectedRows.Add((int)iMetaData[0, i, 0]);
                            }
                        }
                    }

                    iTableLensFilter.CommitChanges();
                    this.OnPicked(iTableLensFilter.SelectedRows);
                    this.Invalidate();
                }
            }
        }

        public int GetRowIndex(int i)
        {
            if (iMetaData != null)
            {
                return (int)iMetaData[0, i, 0];
            }

            return -1;
        }

        public int GetItemIndex(int i)
        {
            if (iMetaData != null)
            {
                for (int rowNumber=0; rowNumber < iMetaData.GetLength(1);rowNumber++)
                {
                    if (i == (int)iMetaData[0, rowNumber, 0])
                    {
                        return rowNumber;
                    }
                }
            }

            return -1;
        }

        private bool HitData(PointF pointFloat, out int rowNumber)
        {
            rowNumber = -1;
            //PointF pointFloat = new PointF(point.X, point.Y);

            for (int i = 0; i < iRowCell.Count; i++)
            {
                if (iRowCell[i].Rect.Contains(pointFloat))
                {
                    if (iRowCell[i].FocusRectangle)
                    {
                        rowNumber = iRowCell[i].SelectedRow;
                    }
                    else
                    {
                        HitContextData(iRowCell[i], pointFloat, ref rowNumber);
                    }

                    return true;
                }
            }

            return false;
        }

        private void HitContextData(TableLensRectangle aRowCell, PointF aPointFloat, ref int rowNumber)
        {
            float numberOfPixelY = aRowCell.Rect.Height;
            float incrementPixel = (aRowCell.MaximumRowNumber - aRowCell.MinimumRowNumber) / numberOfPixelY;
            //float rowNumber = aRowCell.MinimumRowNumber;

            rowNumber = (int)((aPointFloat.Y - aRowCell.Rect.Y) * incrementPixel + aRowCell.MinimumRowNumber);
        }

        private bool HitHeader(Point point, out int columnNumber)
        {
            columnNumber = 0;

            PointF pointFloat = new PointF(point.X, point.Y);
            for (int i = 0; i < iHeaderCell.Count; i++)
            {
                if (iHeaderCell[i].Contains(pointFloat))
                {
                    columnNumber = i;
                    return true;
                }
            }

            return false;
        }

        // This method is called when the mouse button is pushed down over the component.
        protected override void InternalMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (iInited)
            {
                iMouseButtonDownLocation = e.Location;
            }
        }

        // This method is called when size of the components rendertarget's size has changed.
        protected override void InternalUpdateSize() 
        {
            if (iInited)
            {
            }
        }

        private void OnPicked(List<int> indexes)
        {
            if (this.Picked != null)
            {
                this.Picked(this, new IndexesPickedEventArgs(indexes));
            }
        }
 
        public void SetSelected(List<int> indexes)
        {
            this.iTableLensFilter.SelectedRows = indexes;
            iTableLensFilter.CommitChanges();
        }
    }
}
