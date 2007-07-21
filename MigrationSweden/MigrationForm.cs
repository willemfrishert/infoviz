using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Gav.Data;
using Gav.Graphics;
using GAVExtra;

using Microsoft.DirectX;

namespace MigrationSweden
{
    public partial class MigrationForm : Form
    {
        //data containers for migration data
        private List<DataContainer> iDataContainerList;

        private DataContainer iMigrationMotivations;

        private DataContainer iUnemploymentMotivations;

        private List<DataMigrationCombinerFilter> iDataMigrationCombinerFilter;

        //map containers
        private List<MapContainer> iMapContainerList;

        //show excel data in parallel coordinate plot
        private ParallelCoordinatesPlot iPCPlot;

        //show data in table lens
        private TableLens iTableLens;

        // renders viz components to screen
        private Renderer iRenderer;

        // contains map data
        private MapData iMapData;

        // list containing all split panels
        private List<SplitContainer> iSplitContainerList;
        private List<SplitterPanel> iSplitterPanelList;


        // list containing all buttons with year on it
        private List<Button> iButtonList;
        private int iSelectedButton;

        // list containing all tab panels
        //private List<TabPage> iTabPageActive;
        //private List<TabPage> iTabPagePassive;

        private MapTabControl iMapTabControlActive;
        private MapTabControl iMapTabControlPassive;

        // record of keyboard event
        private KeyEventArgs iKeyboardEvent;

        // list containing selected regions
        private List<int> iSelectedRegionIndexes;

        //used to determine whether a map is dragged or region is picked
        bool iMapDragged;
        bool iMouseMotionAndClicked;
        Point iMouseClickPreviousLocation;

        Timer iYearTimer;

        //int iMapTabOldSelectedIndex;

        private EventHandler iInteractiveColorLegendSliderChangedHandle;

        private static int KMaxSelectedRegion = 10;

        public MigrationForm()
        {
            InitializeComponent();

            //iMapTabOldSelectedIndex = 0;

            iDataContainerList = new List<DataContainer>();
            iMapContainerList = new List<MapContainer>();
            iMigrationMotivations = new DataContainer();
            iUnemploymentMotivations = new DataContainer();
            iDataMigrationCombinerFilter = new List<DataMigrationCombinerFilter>();

            iPCPlot = new ParallelCoordinatesPlot();
            iRenderer = new Renderer(this);

            iSplitterPanelList = new List<SplitterPanel>();
            iSplitterPanelList.Add(splitContainer2.Panel1);
            iSplitterPanelList.Add(splitContainer6.Panel2);
            iSplitterPanelList.Add(splitContainer4.Panel2);
            iSplitterPanelList.Add(splitContainer1.Panel2);

            iSplitContainerList = new List<SplitContainer>();
            iSplitContainerList.Add(splitContainer1);
            iSplitContainerList.Add(splitContainer2);
            iSplitContainerList.Add(splitContainer3);
            iSplitContainerList.Add(splitContainer6);
            
            iButtonList = new List<Button>();
            iButtonList.Add(button2);
            iButtonList.Add(button3);
            iButtonList.Add(button4);
            iButtonList.Add(button5);
            iButtonList.Add(button6);
            iButtonList.Add(button7);
            iButtonList.Add(button8);
            iButtonList.Add(button9);

            iButtonList[0].BackColor = Color.White;
            iButtonList[0].ForeColor = Color.Red;

            iSelectedRegionIndexes = new List<int>();
            iKeyboardEvent = new KeyEventArgs(new Keys());

            iYearTimer = new Timer();
            iYearTimer.Tick += new EventHandler(OnYearTimerTick);

            iInteractiveColorLegendSliderChangedHandle = new EventHandler(OnInteractiveColorLegendSliderChanged);
            MapContainer.iMapCounter = 0;
        }

        private void OnMigrationFormLoad(object sender, EventArgs e)
        {
            int ColumnNumber = 5; // excess of migration
            int SheetNumber = 0; //1999

            SetupMapTabControl();

            SetupDataContainers();
            SetupMaps(ColumnNumber, SheetNumber);
            SetupTableLens();
            SetupPCPlot();
            SetupRenderer();

            SetupKeyEventHandlers();

            SetupYearButtonHandlers();
        }

        private void SetupYearButtonHandlers()
        {
            for (int i = 0; i < iButtonList.Count; i++)
            {
                iButtonList[i].Click +=new EventHandler(OnYearButtonClick);
            }
        }

        private void SetupTableLens()
        {
            iTableLens = new TableLens(iDataContainerList[0].MunicipalitiesNames);
            //iTableLens.Input = iDataContainerList[0].DataCubeYearFilter;
            iTableLens.Input = iDataMigrationCombinerFilter[0];
            iTableLens.Headers = iDataMigrationCombinerFilter[iMapTabControlActive.SelectedIndex].ColumnHeaders;

            iTableLens.ContextColor = Color.FromArgb(162, 209, 229);
            //iTableLens.FocusColor = Color.FromArgb(217, 35, 35);
            iTableLens.FocusColor = Color.FromArgb(162, 209, 229);
            iTableLens.FocusColorNegativeOutlier = Color.FromArgb(217, 35, 35);
            iTableLens.FocusColorPositiveOutlier = Color.FromArgb(217, 35, 35);
            iTableLens.FocusColorNegativeOutlier = Color.Blue;
            iTableLens.FocusColorPositiveOutlier = Color.Blue;
            iTableLens.FocusNegativeOutLierBackGroundColor = Color.Yellow;
            iTableLens.FocusPositiveOutLierBackGroundColor = Color.Yellow;
            //iTableLens.FocusNegativeOutLierBackGroundColor = Color.FromArgb(251, 168, 94);
            //iTableLens.FocusPositiveOutLierBackGroundColor = Color.FromArgb(251, 168, 94);
            iTableLens.Picked += new EventHandler<IndexesPickedEventArgs>(OnTableLensPicked);
        }

        void OnTableLensPicked(object sender, IndexesPickedEventArgs e)
        {
            iSelectedRegionIndexes = e.PickedIndexes;
            LimitSelectedIndexes();
            SelectItemsInControls();
        }

        private void SetupMapTabControl()
        {
            List<string> tabNames = new List<string>();
            tabNames.Add("Age 0-18");
            tabNames.Add("Age 19-29");
            tabNames.Add("Age 30-64");
            tabNames.Add("Age 65+");
            iMapTabControlActive = new MapTabControl(tabNames);
            iMapTabControlPassive = new MapTabControl(tabNames);

            iMapTabControlActive.AgeTabPage[1].Selectable = false;
            iMapTabControlPassive.AgeTabPage[0].Selectable = false;
            iMapTabControlPassive.SelectedIndex = 1;

            iSplitterPanelList[0].Controls.Add(iMapTabControlPassive);
            panelMapActive.Controls.Add(iMapTabControlActive);
            //iSplitterPanelList[1].Controls.Add(iMapTabControlActive);

            iMapTabControlActive.Selected += new TabControlEventHandler(iMapTabControlActive_Selected);
            iMapTabControlPassive.Selected += new TabControlEventHandler(iMapTabControlPassive_Selected);
        }

        void iMapTabControlActive_Selected(object sender, TabControlEventArgs e)
        {
            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].MapFlagLayer.ShowFlags = false;
            }

            ChangeChoroplethTabPage(iMapTabControlActive, iMapTabControlPassive, e.TabPage);
            OnActiveMapChanged();
        }

        void iMapTabControlPassive_Selected(object sender, TabControlEventArgs e)
        {
            ChangeChoroplethTabPage(iMapTabControlPassive, iMapTabControlActive, e.TabPage);
        }

        void ChangeChoroplethTabPage(MapTabControl aSelectedTabControl, MapTabControl aNotSelectedTabControl, TabPage aSelectedTabPage)
        {
            // set all tabs selectable again
            for (int i = 0; i < aNotSelectedTabControl.AgeTabPage.Count; i++)
            {
                aNotSelectedTabControl.AgeTabPage[i].Selectable = true;
            }

            int tabPageNumber = aSelectedTabControl.TabPages.IndexOf(aSelectedTabPage);
            // deselect tab that is selected in the "selected tab control"
            for (int i = 0; i < aNotSelectedTabControl.TabPages.Count; i++)
            {
                if (tabPageNumber == i)
                {
                    aNotSelectedTabControl.AgeTabPage[i].Selectable = false;
                }
            }

            // update "not selected tab control"
            aNotSelectedTabControl.Invalidate();
            
            // add or change render target for the map
            iMapContainerList[tabPageNumber].Control = aSelectedTabPage;
            if (iRenderer.ContainsVizComponent(iMapContainerList[tabPageNumber].ChoroplethMap))
            {
                iRenderer.ChangeRenderTarget(iMapContainerList[tabPageNumber].ChoroplethMap, aSelectedTabPage);
            }
            else
            {
                iRenderer.Add(iMapContainerList[tabPageNumber].ChoroplethMap, aSelectedTabPage);
            }
        }

        private void SetupKeyEventHandlers()
        {
            KeyUp += new KeyEventHandler(OnKeyUp);
            KeyDown += new KeyEventHandler(OnKeyDown);

            for (int i = 0; i < iSplitContainerList.Count; i++)
            {
                iSplitContainerList[i].KeyUp += new KeyEventHandler(OnKeyUp);
                iSplitContainerList[i].KeyDown += new KeyEventHandler(OnKeyDown);
            }

            iMapTabControlActive.KeyUp +=new KeyEventHandler(OnKeyUp);
            iMapTabControlActive.KeyDown += new KeyEventHandler(OnKeyDown);
            iMapTabControlPassive.KeyUp += new KeyEventHandler(OnKeyUp);
            iMapTabControlPassive.KeyDown += new KeyEventHandler(OnKeyDown);
            buttonPlay.KeyUp += new KeyEventHandler(OnKeyUp);
            buttonPlay.KeyDown += new KeyEventHandler(OnKeyDown);
            button2.KeyUp += new KeyEventHandler(OnKeyUp);
            button2.KeyDown += new KeyEventHandler(OnKeyDown);
            button3.KeyUp += new KeyEventHandler(OnKeyUp);
            button3.KeyDown += new KeyEventHandler(OnKeyDown);
            button4.KeyUp += new KeyEventHandler(OnKeyUp);
            button4.KeyDown += new KeyEventHandler(OnKeyDown);
            button5.KeyUp += new KeyEventHandler(OnKeyUp);
            button5.KeyDown += new KeyEventHandler(OnKeyDown);
            button6.KeyUp += new KeyEventHandler(OnKeyUp);
            button6.KeyDown += new KeyEventHandler(OnKeyDown);
            button7.KeyUp += new KeyEventHandler(OnKeyUp);
            button7.KeyDown += new KeyEventHandler(OnKeyDown);
            button8.KeyUp += new KeyEventHandler(OnKeyUp);
            button8.KeyDown += new KeyEventHandler(OnKeyDown);
            button9.KeyUp += new KeyEventHandler(OnKeyUp);
            button9.KeyDown += new KeyEventHandler(OnKeyDown);
            buttonSwapMaps.KeyUp += new KeyEventHandler(OnKeyUp);
            buttonSwapMaps.KeyDown += new KeyEventHandler(OnKeyDown);
        }

        private void SetupRenderer()
        {
            iRenderer.Add(iMapContainerList[0].ChoroplethMap, iMapTabControlActive.TabPages[0]);
            iRenderer.Add(iMapContainerList[1].ChoroplethMap, iMapTabControlPassive.TabPages[1]);
            iRenderer.Add(iPCPlot, iSplitterPanelList[2]);
            iRenderer.Add(iTableLens, panelTableLens);
        }

        private void SetupPCPlot()
        {
            iPCPlot.Enabled = true;
            iPCPlot.SelectedHeaderTextColor = Color.Red;
            iPCPlot.Input = iDataContainerList[iMapTabControlActive.SelectedIndex].DataCubeYearFilter;
            iPCPlot.Headers = iDataContainerList[iMapTabControlActive.SelectedIndex].ColumnHeaders;
            iPCPlot.ColorMap = iMapContainerList[iMapTabControlActive.SelectedIndex].ColorMap;
            iPCPlot.SetSelectedHeader(iPCPlot.ColorMap.Index);
            //iPCPlot.FontHeaders = new Font("Times New Roman", 8.0f);
         
            List<float> max;
            List<float> min;
            iDataContainerList[iMapTabControlActive.SelectedIndex].GetAllColumnMaxMin(out max, out min);

            //iDataContainerList[iMapTabControlActive.SelectedIndex].DataCube.GetAllColumnMaxMin(out max, out min);
            for (int i = 0; i < max.Count; i++)
            {
                iPCPlot.SetMax(i, max[i]);
                iPCPlot.SetMin(i, min[i]);
            }

            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].MapPolygonLayer.IndexVisibilityHandler = iPCPlot.IndexVisibilityHandler;
            }
            iPCPlot.HeaderClicked += new EventHandler(OnPCPHeaderClicked);
            iPCPlot.Picked += new EventHandler<IndexesPickedEventArgs>(OnPCPlotPicked);
            iPCPlot.FilterChanged += new EventHandler(OnPCPFilterChanged);

            iPCPlot.OutfilteredLineColor = Color.LightGray;
            iPCPlot.ShowOutFilteredLines = true;
        }

        private void SetupMaps(int aColumn, int aSheet)
        {
            ShapeFileReader shapeReader = new ShapeFileReader();
            iMapData = shapeReader.Read("./Map/Sweden_municipality.shp",
                                        "./Map/Sweden_municipality.dbf",
                                        "./Map/Sweden_municipality.shx");

            MapContainer mapContainer;
            mapContainer = new MapContainer(iMapData, iDataContainerList[0], aColumn, aSheet, iMapTabControlActive.TabPages[0]);
            mapContainer.ChoroplethMap.VizComponentMouseMove += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseMove);
            mapContainer.ChoroplethMap.VizComponentMouseUp += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseUp);
            iMapContainerList.Add(mapContainer);

            mapContainer = new MapContainer(iMapData, iDataContainerList[1], aColumn, aSheet, iMapTabControlPassive.TabPages[1]);
            mapContainer.ChoroplethMap.VizComponentMouseMove += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseMove);
            mapContainer.ChoroplethMap.VizComponentMouseUp += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseUp);
            iMapContainerList.Add(mapContainer);

            mapContainer = new MapContainer(iMapData, iDataContainerList[2], aColumn, aSheet);
            mapContainer.ChoroplethMap.VizComponentMouseMove += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseMove);
            mapContainer.ChoroplethMap.VizComponentMouseUp += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseUp);
            iMapContainerList.Add(mapContainer);

            mapContainer = new MapContainer(iMapData, iDataContainerList[3], aColumn, aSheet);
            mapContainer.ChoroplethMap.VizComponentMouseMove += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseMove);
            mapContainer.ChoroplethMap.VizComponentMouseUp += new EventHandler<VizComponentMouseEventArgs>(OnChoropethMapMouseUp);
            iMapContainerList.Add(mapContainer);

            iMapContainerList[iMapTabControlActive.SelectedIndex].InteractiveColorLegend.ValueSliderValuesChanged += iInteractiveColorLegendSliderChangedHandle;
            iMapContainerList[iMapTabControlActive.SelectedIndex].InteractiveColorLegend.ColorEdgeValuesChanged += iInteractiveColorLegendSliderChangedHandle;
            iMapContainerList[iMapTabControlActive.SelectedIndex].MapFlagLayer.ShowFlags = false;

            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].MapFlagLayer.DataOutLierActiveMapFilter = iMapContainerList[iMapTabControlActive.SelectedIndex].DataContainer.DataOutlierFilter;
            }

        }

        private void SetupDataContainers()
        {
            List<string> sheetNames = new List<string>();
            sheetNames.Add("1999");
            sheetNames.Add("2000");
            sheetNames.Add("2001");
            sheetNames.Add("2002");
            sheetNames.Add("2003");
            sheetNames.Add("2004");
            sheetNames.Add("2005");
            sheetNames.Add("2006");

            DataContainerMigration migDataContainer;
            List<DataContainer> dataTableLensContainer;
            DataMigrationCombinerFilter dataCombinerFilter;

            migDataContainer = new DataContainerMigration();
            migDataContainer.LoadData("./Data/Migration 0-18 in % + excess.xls", sheetNames);
            iDataContainerList.Add(migDataContainer);

            migDataContainer = new DataContainerMigration();
            migDataContainer.LoadData("./Data/Migration 19-29 in % + excess.xls", sheetNames);
            iDataContainerList.Add(migDataContainer);

            migDataContainer = new DataContainerMigration();
            migDataContainer.LoadData("./Data/Migration 30-64 in % + excess.xls", sheetNames);
            iDataContainerList.Add(migDataContainer);

            migDataContainer = new DataContainerMigration();
            migDataContainer.LoadData("./Data/Migration 65+  in % + excess.xls", sheetNames);
            iDataContainerList.Add(migDataContainer);

            iMigrationMotivations.LoadData("./Data/Migration motivations.xls", sheetNames);
            //iUnemploymentMotivations.LoadData("./Data/Unemployment by region, sex, period 1999-2006.xls", sheetNames);

            for (int i = 0; i < iDataContainerList.Count; i++)
            {
                dataTableLensContainer = new List<DataContainer>();
                dataTableLensContainer.Add(iDataContainerList[i]);
                dataTableLensContainer.Add(iMigrationMotivations);
                //dataTableLensContainer.Add(iUnemploymentMotivations);

                dataCombinerFilter = new DataMigrationCombinerFilter("1999");
                dataCombinerFilter.DataContainerList = dataTableLensContainer;
                dataCombinerFilter.CommitChanges();

                iDataMigrationCombinerFilter.Add(dataCombinerFilter);
            }
        }

        void OnPCPHeaderClicked(object sender, EventArgs e)
        {
            int column = ((ParallelCoordinatesPlot)sender).ClickedHeader;

            iPCPlot.SetSelectedHeader(column);

            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].SelectedColumn = column;
            }

            iPCPlot.ColorMap = iMapContainerList[iMapTabControlActive.SelectedIndex].ColorMap;
            iPCPlot.Invalidate();
        }

        void OnPCPFilterChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].ChoroplethMap.Invalidate();
            }
        }

        void OnPCPlotPicked(object sender, IndexesPickedEventArgs e)
        {
            HandleSelectedItem(e.PickedIndexes);
            LimitSelectedIndexes();
            SelectItemsInControls();
        }

        private void HandleSelectedItem(List<int> aItem)
        {
            if (aItem.Count > 0)
            {
                for (int i = 0; i < aItem.Count; i++)
                {
                    HandleSelectedItem(aItem[i]);
                }
            }
            else
            {
                HandleSelectedItem(-1);
            }
        }

        // if aItem is -1, then item list is deselected
        private void HandleSelectedItem(int aItem)
        {
            if (aItem != -1)
            {
                // clear the selected list if Control has not been pressed
                if (iKeyboardEvent.Control)
                {            
                    // check if the item has already been selected before
                    if (iSelectedRegionIndexes.Contains(aItem))
                    {
                        // remove it if so
                        iSelectedRegionIndexes.Remove(aItem);
                    }
                    else // add it if it's been selected just now
                    {
                        iSelectedRegionIndexes.Add(aItem);
                    }
                }
                else
                {
                    iSelectedRegionIndexes = new List<int>();
                    iSelectedRegionIndexes.Add(aItem);
                }
            }
            else
            {
                // clear the selected list if Control has not been pressed
                if (!iKeyboardEvent.Control)
                {
                    iSelectedRegionIndexes = new List<int>();
                }
            }
        }

        void SelectItemsInControls()
        {
            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].SetSelectedRegions(iSelectedRegionIndexes);
            }

            iPCPlot.SetSelectedLines(iSelectedRegionIndexes, false, false);
            iPCPlot.Invalidate();

            iTableLens.SetSelected(iSelectedRegionIndexes);
            iTableLens.Invalidate();
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            iKeyboardEvent = e;
            e.Handled = true;
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            iKeyboardEvent = e;

            if (e.KeyCode == Keys.Up)
            {
                if (iSelectedRegionIndexes.Count == 1)
                {
                    int tableLensIndex = iTableLens.GetItemIndex(iSelectedRegionIndexes[0]);
                    if (tableLensIndex != -1)
                    {
                        if (tableLensIndex > 0)
                        {
                            tableLensIndex--;
                            int nextRow = iTableLens.GetRowIndex(tableLensIndex);

                            if (nextRow != -1)
                            {
                                iSelectedRegionIndexes[0] = nextRow;
                                SelectItemsInControls();
                            }
                        }
                    }
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                if (iSelectedRegionIndexes.Count == 1)
                {
                    int tableLensIndex = iTableLens.GetItemIndex(iSelectedRegionIndexes[0]);
                    if (tableLensIndex != -1)
                    {
                        if (tableLensIndex < (iDataContainerList[iMapTabControlActive.SelectedIndex].DataCubeYearFilter.GetData().Data.GetLength(1) - 1))
                        {
                            tableLensIndex++;
                            int nextRow = iTableLens.GetRowIndex(tableLensIndex);

                            if (nextRow != -1)
                            {
                                iSelectedRegionIndexes[0] = nextRow;
                                SelectItemsInControls();
                            }
                        }
                    }
                }
            }

            e.Handled = true;
        }

        void OnChoropethMapMouseMove(object sender, VizComponentMouseEventArgs e)
        {
            ChoroplethMap map = (ChoroplethMap)(sender);

            //distinguish whether we're clicking or dragging with the left mouse button
            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                // if the left mouse button has been pressed,
                if (e.MouseEventArgs.Button == MouseButtons.Left)
                {
                    // then either record the current position of the mouse at the time the mouse is pressed down
                    if (!iMouseMotionAndClicked)
                    {
                        iMouseClickPreviousLocation = e.MouseEventArgs.Location;
                        iMouseMotionAndClicked = true;
                    }
                    else // or, in case the button is still behind held down, check out how far the mouse has been moved
                    {
                        int deltaX = iMouseClickPreviousLocation.X - e.MouseEventArgs.X;
                        int deltaY = iMouseClickPreviousLocation.Y - e.MouseEventArgs.Y;

                        if (deltaX < 0) deltaX = -deltaX;
                        if (deltaY < 0) deltaY = -deltaY;

                        // if we move more than 1 pixel, it means we're dragging and mouseup event should not called
                        if ((deltaX > 1) || (deltaY > 1))
                        {
                            iMapDragged = true;
                        }
                    }
                }
                else // clean the information
                {
                    iMapDragged = false;
                    iMouseMotionAndClicked = false;
                }
                iMapContainerList[i].ChoroplethMap.Position = map.Position;
                iMapContainerList[i].ChoroplethMap.Zoom = map.Zoom;
                iMapContainerList[i].MapFlagLayer.Zoom = map.Zoom;
                iMapContainerList[i].ChoroplethMap.Invalidate();
            }
        }

        void OnChoropethMapMouseUp(object sender, VizComponentMouseEventArgs e)
        {
            // in case we pressed the left mouse button and we're not dragging the map
            if ((e.MouseEventArgs.Button == MouseButtons.Left) && (!iMapDragged))
            {

                ChoroplethMap map = (ChoroplethMap)(sender);
                Vector2 v = map.ConvertScreenCoordinatesToMapCoordinates(e.MouseEventArgs.Location);

                int regionId = iMapData.GetRegionId(v);
                HandleSelectedItem(regionId);
                LimitSelectedIndexes();
                SelectItemsInControls();
            }
        }

        private void OnButtonSwapMapsClick(object sender, EventArgs e)
        {
            // make the opposite tabs selectable
            iMapTabControlActive.AgeTabPage[iMapTabControlPassive.SelectedIndex].Selectable = true;
            iMapTabControlActive.AgeTabPage[iMapTabControlActive.SelectedIndex].Selectable = false;

            iMapTabControlPassive.AgeTabPage[iMapTabControlActive.SelectedIndex].Selectable = true;
            iMapTabControlPassive.AgeTabPage[iMapTabControlPassive.SelectedIndex].Selectable = false;


            // select the tabs that are selectable
            int temp = iMapTabControlActive.SelectedIndex;
            iMapTabControlActive.SelectedIndex = iMapTabControlPassive.SelectedIndex;
            iMapTabControlPassive.SelectedIndex = temp;

            iMapContainerList[iMapTabControlActive.SelectedIndex].Control = iMapTabControlActive.SelectedTab;
            iRenderer.ChangeRenderTarget(iMapContainerList[iMapTabControlActive.SelectedIndex].ChoroplethMap, iMapTabControlActive.SelectedTab);

            iMapContainerList[iMapTabControlPassive.SelectedIndex].Control = iMapTabControlPassive.SelectedTab;
            iRenderer.ChangeRenderTarget(iMapContainerList[iMapTabControlPassive.SelectedIndex].ChoroplethMap, iMapTabControlPassive.SelectedTab);

            OnActiveMapChanged();
        }

        private void OnActiveMapChanged()
        {
            //MapContainer.ActiveMapNumber = iMapTabControlActive.SelectedIndex;

            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].InteractiveColorLegend.ValueSliderValuesChanged -= iInteractiveColorLegendSliderChangedHandle;
                iMapContainerList[i].MapFlagLayer.DataOutLierActiveMapFilter = iMapContainerList[iMapTabControlActive.SelectedIndex].DataContainer.DataOutlierFilter;
                iMapContainerList[i].MapFlagLayer.ShowFlags = true;
                //iMapContainerList[i].Invalidate();
            }

            iMapContainerList[iMapTabControlActive.SelectedIndex].MapFlagLayer.ShowFlags = false;
            iMapContainerList[iMapTabControlActive.SelectedIndex].InteractiveColorLegend.ColorEdgeValuesChanged += iInteractiveColorLegendSliderChangedHandle;

            // update other plots
            iPCPlot.Input = iDataContainerList[iMapTabControlActive.SelectedIndex].DataCubeYearFilter;
            iPCPlot.Headers = iDataContainerList[iMapTabControlActive.SelectedIndex].ColumnHeaders;
            iPCPlot.ColorMap = iMapContainerList[iMapTabControlActive.SelectedIndex].ColorMap;

            iTableLens.Headers = iDataMigrationCombinerFilter[iMapTabControlActive.SelectedIndex].ColumnHeaders;
            iTableLens.Input = iDataMigrationCombinerFilter[iMapTabControlActive.SelectedIndex];
            List<float> max;
            List<float> min;
            iDataContainerList[iMapTabControlActive.SelectedIndex].GetAllColumnMaxMin(out max, out min);

            for (int i = 0; i < iPCPlot.Input.GetData().Data.GetLength(0); i++)
            {
                iPCPlot.RemoveMax(i);
                iPCPlot.RemoveMin(i);
            }

            for (int i = 0; i < max.Count; i++)
            {
                iPCPlot.SetMax(i, max[i]);
                iPCPlot.SetMin(i, min[i]);
            }

            iTableLens.Invalidate();
            iPCPlot.Invalidate();
            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].Invalidate();
            }
            iDataContainerList[iMapTabControlActive.SelectedIndex].DataCubeYearFilter.CommitChanges();
        }

        void OnInteractiveColorLegendSliderChanged(object sender, EventArgs e)
        {
            if (iPCPlot != null)
            {
                iPCPlot.Invalidate();
            }
        }

        private void OnYearButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            buttonPlay.Text = "play";
            iYearTimer.Stop();

            YearUpdateData(button);
            YearUpdateMaps();
            YearUpdateTableLens();
            YearUpdatePCPlot();
            YearUpdateButtons(button);
        }

        private void YearUpdateButtons(Button button)
        {
            for (int i = 0; i < iButtonList.Count; i++)
            {
                iButtonList[i].BackColor = SystemColors.Control;
                iButtonList[i].ForeColor = SystemColors.ControlText;
            }

            button.BackColor = Color.White;
            button.ForeColor = Color.Red;
            iSelectedButton = int.Parse(button.Tag.ToString());
        }

        private void YearUpdatePCPlot()
        {
            iPCPlot.Invalidate();
        }

        private void YearUpdateTableLens()
        {
            iTableLens.Invalidate();
        }

        private void YearUpdateMaps()
        {
            for (int i = 0; i < iMapContainerList.Count; i++)
            {
                iMapContainerList[i].Invalidate();
            }
        }
        private void YearUpdateActiveMap()
        {
            iMapContainerList[iMapTabControlActive.SelectedIndex].Invalidate();
        }

        private void YearUpdateData(Button button)
        {
            for (int i = 0; i < iDataContainerList.Count; i++)
            {
                iDataContainerList[i].DataCubeYearFilter.Year = button.Text;
                iDataContainerList[i].DataCubeYearFilter.CommitChanges();
            }

            for (int i = 0; i < iDataMigrationCombinerFilter.Count; i++)
            {
                iDataMigrationCombinerFilter[i].SelectedYear = button.Text;
                iDataMigrationCombinerFilter[i].CommitChanges();
            }
        }

        private void OnButtonPlayClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "play")
            {
                button.Text = "stop";
                iYearTimer.Interval = 1000;
                iYearTimer.Start();

            }
            else
            {
                button.Text = "play";
                iYearTimer.Stop();
                OnYearButtonClick(iButtonList[iSelectedButton], e);
            }
        }

        void OnYearTimerTick(object sender, EventArgs e)
        {
            int buttonToPress = (iSelectedButton+1) % (iButtonList.Count);
            //OnYearButtonClick(iButtonList[buttonToPress], e);

            YearUpdateData(iButtonList[buttonToPress]);
            //YearUpdateMap();
            YearUpdateActiveMap();
            //YearUpdateTableLens();
            YearUpdatePCPlot();
            YearUpdateButtons(iButtonList[buttonToPress]);
        }

        void LimitSelectedIndexes()
        {
            if (iSelectedRegionIndexes.Count > KMaxSelectedRegion)
            {
                iSelectedRegionIndexes.RemoveRange(KMaxSelectedRegion, iSelectedRegionIndexes.Count - KMaxSelectedRegion);
            }
        }
    }
}