using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Gav.Data;
using Gav.Graphics;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MigrationSweden
{
    public class MapFlagLayer : MapGlyphLayer {

        private CustomVertex.PositionOnly[] iFlagVertices;
        private CustomVertex.PositionOnly[] iFlagFillVertices;

        private bool _inited;
        private List<AxisMap> _axisMaps;

        private IDataCubeProvider<float> _input;

        //private ColorMap colorMap;

        private Material _material;

        private int iSelectedColumn;

        private int iSelectedSheet;

        private DataOutlierFilter iDataOutLierFilter;

        private DataOutlierFilter iDataOutLierActiveMapFilter;

        private bool iShowFlags;

        public int iZoom; 

        public IDataCubeProvider<float> Input
        {
            get 
            { 
                return _input; 
            }
            set 
            { 
                _input = value; 
            }
        }

        public int Zoom
        {
            get
            {
                return iZoom;
            }
            set
            {
                iZoom = value;
            }
        }



        public bool ShowFlags
        {
            get
            {
                return iShowFlags;
            }
            set
            {
                iShowFlags = value;
            }
        }

        public DataOutlierFilter DataOutLierFilter
        {
            get
            {
                return iDataOutLierFilter;
            }
            set
            {
                iDataOutLierFilter = value;
            }
        }

        public DataOutlierFilter DataOutLierActiveMapFilter
        {
            get
            {
                return iDataOutLierActiveMapFilter;
            }
            set
            {
                iDataOutLierActiveMapFilter = value;
            }
        }

        public int SelectedColumn
        {
            get
            {
                return iSelectedColumn;
            }
            set
            {
                iSelectedColumn = value;
            }
        }

        public int SelectedSheet
        {
            get
            {
                return iSelectedSheet;
            }
            set
            {
                iSelectedSheet = value;
            }
        }

        public MapFlagLayer(int aSelectedColumn, int aSelectedSheet)
        {
            iSelectedColumn = aSelectedColumn;
            iSelectedSheet = aSelectedSheet;

            iShowFlags = true;
        }

        private void CreateFlagVerts()
        {
            iFlagVertices = new CustomVertex.PositionOnly[4];

            iFlagVertices[0].Position = new Vector3( 0, 0, 0);
            iFlagVertices[1].Position = new Vector3( 0,  10, -8);
            iFlagVertices[2].Position = new Vector3( 7,  8, -6);
            iFlagVertices[3].Position = new Vector3( 0,  6, -4);

            iFlagFillVertices = new CustomVertex.PositionOnly[3];

            iFlagFillVertices[0].Position = new Vector3(0, 10, -8);
            iFlagFillVertices[1].Position = new Vector3(7, 8, -6);
            iFlagFillVertices[2].Position = new Vector3(0, 6, -4);
        }

        // This method is called everytime the map is rendered. 
        protected override void InternalRender() 
        {

            // If the input is null we cannot render.
            if (_input == null) 
            {
                return;
            }

            // If the glyph is not inited, call InternalInit. 
            if (!_inited) 
            {
                InternalInit(_device);
                if (!_inited) {
                    return;
                }
            }

            _device.RenderState.CullMode = Cull.None;

            //// Tells the device ("graphics card") to use the created material.
            //_device.Material = _material;

            // Get the mapped values from the first axis map. This axis map is connected to the first column in the data cube.
            // The index corresponds to the column in the data cube.
            float[] mappedValues = _axisMaps[0].MappedValues;

            // Tells the device that the next primitives to draw are of type CustomVertex.PositionOnly.
            _device.VertexFormat = CustomVertex.PositionOnly.Format;

            float zoom = (float)Math.Abs(iZoom / 125.0f);
            Console.WriteLine(zoom.ToString());
            if (iShowFlags)
            {
                // Loops through the regions in the map.
                for (int i = 0; i < _input.GetData().GetAxisLength(Axis.Y); i++)
                {
                    float outlierValueActiveMap = iDataOutLierActiveMapFilter.GetData().Data[iSelectedColumn, i, iSelectedSheet];
                    float outlierValueThisMap = iDataOutLierFilter.GetData().Data[iSelectedColumn, i, iSelectedSheet];

                    if (outlierValueActiveMap == outlierValueThisMap)
                    {
                        if ((outlierValueThisMap == DataOutlierFilter.KPositiveOutLier) || (outlierValueThisMap == DataOutlierFilter.KNegativeOutLier))
                        {
                            // Resets the world transform.  
                            _device.Transform.World = _layerWorldMatrix;
                            _device.Transform.World *= Matrix.Scaling(zoom, zoom, zoom);
                            // If a glyph positioner (a class that moves the glyphs to the correct position) is set, use it. 
                            if (ActiveGlyphPositioner != null)
                            {
                                //Gets the position for the glyph with index i.
                                Vector2 pos = ActiveGlyphPositioner.GetPosition(i);
                                // Translates the world transform. 
                                _device.Transform.World *= Matrix.Translation(
                                    pos.X,
                                    pos.Y,
                                    0
                                    );
                            }
                            _material.Emissive = Color.GreenYellow;
                            _material.Diffuse = Color.GreenYellow;
                            _device.Material = _material;

                            _device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, iFlagFillVertices);

                            _material.Emissive = Color.Black;
                            _material.Diffuse = Color.Black;
                            _device.Material = _material;

                            _device.DrawUserPrimitives(PrimitiveType.LineStrip, 4, iFlagVertices);


                            //if (IndexVisibilityHandler.GetVisibility(i))
                            //{
                            //    _device.DrawUserPrimitives(PrimitiveType.TriangleFan, 4, iFlagVertices);
                            //    _device.DrawUserPrimitives(PrimitiveType.LineStrip, 4, _boxVertices);
                            //}
                        }
                    }
                }
            }
        }

        // This method is called once when the glyph is inited. 
        protected override void InternalInit(Device device) 
        {

            if (_input == null) {
                return;
            }

            if (iDataOutLierFilter == null)
            {
                return;
            }

            if (iDataOutLierActiveMapFilter == null)
            {
                return;
            }

            CreateAxisMaps();
            CreateFlagVerts();

            // Create material to enable glyph coloring. 
            _material = new Material();

            _inited = true;
        }

        // Creates one axismap per column in the data set. 
        private void CreateAxisMaps() {
            _axisMaps = new List<AxisMap>();

            for (int i = 0; i < _input.GetData().GetAxisLength(Axis.X); i++) {
                AxisMap axisMap = new AxisMap();
                axisMap.Input = _input;
                axisMap.Index = i;
                axisMap.DoMapping();
                _axisMaps.Add(axisMap);
            }
        }

        protected override void InternalInvalidate() 
        { 
        }
    }
}