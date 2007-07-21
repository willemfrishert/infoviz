using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Gav.Data;
using Gav.Graphics;

using Microsoft.DirectX;

namespace MigrationSweden
{
    class MapContainer
    {
        public static int iMapCounter;

        private int iMapNumber;

        private static int iActiveMapNumber;
        //private static int iPassiveMapNumber;


        private ChoroplethMap iChoroplethMap;
        private MapPolygonLayer iMapPolygonLayer;
        private MapPolygonBorderLayer iMapPolygonBorderLayer;
        //private MapPolygonBorderLayer iMapSelectedBorderLayer;
        private MapFlagLayer iMapFlagLayer;
        private MapSelectionBorderLayer iMapSelectedBorderLayer;
        private MapLabelLayer iMapLabelLayer;
        private InteractiveColorLegend iInteractiveColorLegend;
        private ColorMapOutLierFilter iColorMapOutLier;
        private IndexVisibilityList iIndexSelectedRegionsList;

        private MapData iMapData;

        private Control iControl;

        private DataContainer iDataContainer;
        private int iSelectedColumn;
        private int iSelectedSheet;

        // controls to be able to brush on the map
        private Timer iToolTipTimer;
        private ToolTip iToolTip;
        private Point iToolTipPosition;
        private bool iToolTipShow;
        private String iToolTipText;

        private EventHandler iControlMouseLeaveHandler;

        public ChoroplethMap ChoroplethMap
        {
            //set
            //{
            //    iChoropethMap = value;
            //}
            get
            {
                return iChoroplethMap;
            }
        }

        public MapPolygonLayer MapPolygonLayer
        {
            //set
            //{
            //    iMapPolygonLayer = value;
            //}
            get
            {
                return iMapPolygonLayer;
            }
        }

        public MapPolygonBorderLayer MapPolygonBorderLayer
        {
            //set
            //{
            //    iMapPolygonBorderLayer = value;
            //}
            get
            {
                return iMapPolygonBorderLayer;
            }
        }

        public MapPolygonBorderLayer MapSelectedBorderLayer
        {
            get
            {
                return iMapSelectedBorderLayer;
            }
        }
        public IndexVisibilityList IndexVisiblityList
        {
            get
            {
                return iIndexSelectedRegionsList;
            }
        }

        public InteractiveColorLegend InteractiveColorLegend
        {
            //set
            //{
            //    iInteractiveColorLegend = value;
            //}
            get
            {
                return iInteractiveColorLegend;
            }
        }

        public MapFlagLayer MapFlagLayer
        {
            //set
            //{
            //    iMapFlagLayer = value;
            //}
            get
            {
                return iMapFlagLayer;
            }
        }

        public ColorMap ColorMap
        {
            //set
            //{
            //    iColorMap = value;
            //    this.Invalidate();
            //}
            get
            {
                return iColorMapOutLier;
            }
        }

        public DataContainer DataContainer
        {
            //set
            //{
            //    iDataContainer = value;
            //}
            get
            {
                return iDataContainer;
            }
        }

        public Control Control
        {
            set
            {
                if (iControl != null)
                {
                    iControl.MouseLeave -= iControlMouseLeaveHandler;
                }
                iControl = value;
                iControl.MouseLeave += iControlMouseLeaveHandler;
                iMapLabelLayer.Control = iControl;
            }
            get
            {
                return Control;
            }
        }

        public int SelectedColumn
        {
            set
            {
                iSelectedColumn = value;
                this.Invalidate();
            }
            get
            {
                return iSelectedColumn;
            }
        }

        public int SelectedSheet
        {
            set
            {
                iSelectedSheet = value;
                this.Invalidate();
            }
            get
            {
                return iSelectedSheet;
            }
        }

        public static int ActiveMapNumber
        {
            set
            {
                iActiveMapNumber = value;
            }
            get
            {
                return iActiveMapNumber;
            }
        }

        //public static int PassiveMapNumber
        //{
        //    set
        //    {
        //        iPassiveMapNumber = value;
        //    }
        //    get
        //    {
        //        return iPassiveMapNumber;
        //    }
        //}

        public MapContainer(MapData aMapData, DataContainer aDataContainer, int aSelectedColumn, int aSelectedSheet)
        {
            SetupMapContainer(aMapData, aDataContainer, aSelectedColumn, aSelectedSheet);
        }

        public MapContainer(MapData aMapData, DataContainer aDataContainer, int aSelectedColumn, int aSelectedSheet, Control aControl)
        {
            iControl = aControl;
            SetupMapContainer(aMapData, aDataContainer, aSelectedColumn, aSelectedSheet);
            iControl.MouseLeave += iControlMouseLeaveHandler;
        }

        private void SetupMapContainer(MapData aMapData, DataContainer aDataContainer, int aSelectedColumn, int aSelectedSheet)
        {
            iMapData = aMapData;
            iDataContainer = aDataContainer;
            iSelectedColumn = aSelectedColumn;
            iSelectedSheet = aSelectedSheet;

            iChoroplethMap = new ChoroplethMap();
            iMapPolygonLayer = new MapPolygonLayer();
            iMapPolygonBorderLayer = new MapPolygonBorderLayer();
            //iMapSelectedBorderLayer = new MapPolygonBorderLayer();
            iMapSelectedBorderLayer = new MapSelectionBorderLayer(1.5f);
            iColorMapOutLier = new ColorMapOutLierFilter();
            iInteractiveColorLegend = new InteractiveColorLegend();

            iMapNumber = iMapCounter;
            iMapCounter++;


            SetupColorMap();
            SetupInteractiveColorLegend();

            // setup border
            iMapPolygonBorderLayer.MapData = iMapData;
            iMapPolygonBorderLayer.BorderColor = Color.DarkSlateGray;

            iMapSelectedBorderLayer.MapData = iMapData;
            iMapSelectedBorderLayer.BorderColor = Color.GreenYellow;
            iMapSelectedBorderLayer.IndexVisibilityHandler = new IndexVisibilityHandler(iDataContainer.MunicipalitiesNames.Count + 1);
            iIndexSelectedRegionsList = iMapSelectedBorderLayer.IndexVisibilityHandler.CreateVisibilityList();

            ClearSelectedRegions();


            // setup polygons
            iMapPolygonLayer.MapData = aMapData;
            iMapPolygonLayer.ColorMap = iColorMapOutLier;

            iMapLabelLayer = new MapLabelLayer(iControl);
            iMapLabelLayer.Labels.Add(iDataContainer.SheetName[iSelectedSheet]);
            iMapLabelLayer.Labels.Add(iDataContainer.ColumnHeaders[iSelectedColumn]);

            iChoroplethMap.Enabled = true;
            iChoroplethMap.VizComponentMouseMove += new EventHandler<VizComponentMouseEventArgs>(iChoroplethMap_VizComponentMouseMove);

            // Add layers on the proper order
            iChoroplethMap.AddLayer(iMapPolygonLayer);
            iChoroplethMap.AddLayer(iMapPolygonBorderLayer);
            iChoroplethMap.AddLayer(iMapSelectedBorderLayer);
            iChoroplethMap.AddLayer(iMapLabelLayer);

            iMapFlagLayer = new MapFlagLayer(iSelectedColumn, iSelectedSheet);
            iMapFlagLayer.Input = iDataContainer.DataCubeYearFilter;
            iMapFlagLayer.DataOutLierFilter = iDataContainer.DataOutlierFilter;
            iMapFlagLayer.ActiveGlyphPositioner = new CenterGlyphPositioner();
            iMapFlagLayer.ActiveGlyphPositioner.MapData = iMapData;
            iMapFlagLayer.Zoom = iChoroplethMap.Zoom;

            iChoroplethMap.AddLayer(iMapFlagLayer);

            //iChoroplethMap.AddSubComponent(iInteractiveColorLegend);
            iChoroplethMap.Invalidate();


            iToolTipTimer = new Timer();
            iToolTipTimer.Interval = 1000;
            iToolTipTimer.Tick += new EventHandler(OnToolTipTimerTick);
            iToolTipShow = false;
            iToolTip = new ToolTip();
            iToolTipPosition = new Point();

            iControlMouseLeaveHandler = new EventHandler(iControl_MouseLeave);

            iChoroplethMap.Position = new Vector2(50.0f, 200.0f);

        }

        void iControl_MouseLeave(object sender, EventArgs e)
        {
            iToolTipTimer.Stop();
            iToolTip.RemoveAll();
            iToolTipShow = false;
        }

        void OnToolTipTimerTick(object sender, EventArgs e)
        {
            iToolTipTimer.Stop();
            iToolTipShow = true;
            iToolTip.Show(iToolTipText, iControl, iToolTipPosition.X, iToolTipPosition.Y + 21);
        }

        void iChoroplethMap_VizComponentMouseMove(object sender, VizComponentMouseEventArgs e)
        {
            Vector2 v = iChoroplethMap.ConvertScreenCoordinatesToMapCoordinates(e.MouseEventArgs.Location);
            int regionId = iMapData.GetRegionId(v);
            if (regionId >= 0)
            {
                if (iControl != null)
                {
                    iToolTipText = iDataContainer.MunicipalitiesNames[regionId];
                    // interactive update of tooltip
                    // gather information
                    float value = iDataContainer.DataCubeYearFilter.GetData().Data[iSelectedColumn, regionId, 0];
                    if (!float.IsNaN(value))
                    {
                        iToolTipText += "\n" + iDataContainer.DataCubeYearFilter.GetData().Data[iSelectedColumn, regionId, 0];
                    }
                    //iToolTipText = iDataContainer.MunicipalitiesNames[regionId]
                    iToolTipPosition = e.MouseEventArgs.Location;

                    // Using SetToolTip causes the tooltip to show up directly. Somehow it screwes up the delay. Question is why?
                    // Workaround is to use a timer.
                    if (iToolTipShow)
                    {
                        iToolTip.SetToolTip(iControl, iToolTipText);
                    }
                    else
                    {
                        iToolTipTimer.Start();
                    }
                }
            }
            else
            {
                iToolTipTimer.Stop();
                iToolTip.RemoveAll();
                iToolTipShow = false;
            }

            //iMapFlagLayer.Zoom = iChoroplethMap.Zoom;
            //iChoroplethMap.Invalidate();
        }

        public void Invalidate()
        {
            UpdateColorMap();
            iInteractiveColorLegend.ColorMap = iColorMapOutLier;
            iMapFlagLayer.SelectedColumn = iSelectedColumn;
            iMapFlagLayer.SelectedSheet = iSelectedSheet;
            iMapFlagLayer.DataOutLierActiveMapFilter.CommitChanges();
            iMapFlagLayer.DataOutLierFilter.CommitChanges();

            iMapLabelLayer.Labels[0] = iDataContainer.DataCubeYearFilter.Year;
            iMapLabelLayer.Labels[1] = iDataContainer.ColumnHeaders[iSelectedColumn];
            iChoroplethMap.Invalidate();
        }

        private void SetupColorMap()
        {
            UpdateColorMap();
        }

        private void UpdateColorMap()
        {
            iColorMapOutLier.CalculateColorMapShowingOutliers(iDataContainer, iSelectedColumn, iSelectedSheet);
            //iColorMapOutLier.Input = iDataContainer.DataCubeYearFilter;
            //iColorMapOutLier.Index = iSelectedColumn;

            //// get the minimum, maximum and median value of a specific column in a specific sheet.
            //float min    = iDataContainer.GetColumnMinimum(SelectedColumn, SelectedSheet);
            //float max    = iDataContainer.GetColumnMaximum(SelectedColumn, SelectedSheet);
            //float median = iDataContainer.GetColumnMedian(SelectedColumn, SelectedSheet);

            //float distance = 0;
            //if (median - min < max - median)
            //{
            //    distance = median - min;
            //}
            //else
            //{
            //    distance = max - median;
            //}
            //iColorMap.Max = median + distance;
            //iColorMap.Min = median - distance;
            //iColorMap.UseGlobalMaxMin = true;
            //iColorMap.CalculateEdges();

        }

        private void SetupInteractiveColorLegend()
        {
            iInteractiveColorLegend.Enabled = true;
            iInteractiveColorLegend.ShowValueSliders = true;
            iInteractiveColorLegend.ShowValueSliderValue = true;
            iInteractiveColorLegend.UseRelativePosition = true;
            iInteractiveColorLegend.UseRelativeSize = true;
            iInteractiveColorLegend.SetPosition(0.01f, 0.015f);
            iInteractiveColorLegend.SetLegendSize(0.05f, 0.5f);
            iInteractiveColorLegend.SliderColor = Color.Black;
            iInteractiveColorLegend.ShowColorEdgeSliders = true;
            iInteractiveColorLegend.ShowColorEdgeSliderValue = true;
            iInteractiveColorLegend.ShowMinMaxValues = true;
            iInteractiveColorLegend.SliderTextColor = Color.Black;
            iInteractiveColorLegend.SetEdgeSliders(InteractiveColorLegend.SliderLinePosition.RightOrBottom,
                                                  InteractiveColorLegend.TextPosition.RightOrBottom,
                                                  true);
            iInteractiveColorLegend.SetUpperThreshold(InteractiveColorLegend.SliderLinePosition.RightOrBottom,
                                                     InteractiveColorLegend.TextPosition.RightOrBottom,
                                                     true);
            iInteractiveColorLegend.SetLowerThreshold(InteractiveColorLegend.SliderLinePosition.RightOrBottom,
                                                     InteractiveColorLegend.TextPosition.RightOrBottom,
                                                     true);
            iInteractiveColorLegend.MinMaxTextColor = Color.Gray;

            iInteractiveColorLegend.ColorMap = iColorMapOutLier;
        }

        public void SetSelectedRegions(List<int> aSelectedRegionIndexes)
        {
            ClearSelectedRegions();

            for (int i = 0; i < aSelectedRegionIndexes.Count; i++)
            {
                iIndexSelectedRegionsList.SetVisibility(aSelectedRegionIndexes[i], 0, true);
            }

            iIndexSelectedRegionsList.CommitChanges();

            iChoroplethMap.Invalidate();
        }

        private void ClearSelectedRegions()
        {
            for (int i = 0; i < (iDataContainer.MunicipalitiesNames.Count + 1); i++)
            {
                iIndexSelectedRegionsList.SetVisibility(i, 0, false);
            }
        }


    }
}
