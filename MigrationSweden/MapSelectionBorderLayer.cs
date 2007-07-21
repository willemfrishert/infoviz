using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Gav.Graphics;
using Gav.Data;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace MigrationSweden
{
    public class MapSelectionBorderLayer : MapPolygonBorderLayer
    {
        private Material _polygonMaterial;

        private Line iLine;

        private float iLineWidth;

        private bool iInited;

        public float LineWidth
        {
            set
            {
                iLineWidth = value;
            }
            get
            {
                return iLineWidth;
            }
        }

        public MapSelectionBorderLayer(float aLineWidth)
        {
            iLineWidth = aLineWidth;
            iInited = false;
        }

        protected override void InternalRender()
        {
            if (!iInited)
            {
                InternalInit(this._device);
                if (!iInited)
                {
                    return;
                }
            }
            if ((base.MapData != null) && (base._device != null))
            {
                base._device.RenderState.Lighting = true;
                base._device.VertexFormat = VertexFormats.Position;
                this.RenderPolygonBorders();
            }
        }

        protected override void InternalInit(Device device)
        {
            if (device == null)
            {
                return;
            }
            iLine = new Line(_device);
            iLine.Antialias = true;
            iInited = true;
        }

        protected override void InternalInvalidate()
        {

        }

        private void RenderPolygonBorders()
        {
            iLine.Width = iLineWidth;

            if (base.Alpha != 0)
            {
                Matrix m = _device.GetTransform(TransformType.World) * _device.GetTransform(TransformType.View) * _device.GetTransform(TransformType.Projection);

                base._device.Transform.World = base._layerWorldMatrix;
                this._polygonMaterial.Emissive = Color.FromArgb(base.Alpha, this.BorderColor);
                this._polygonMaterial.Ambient = Color.FromArgb(base.Alpha, this.BorderColor);
                this._polygonMaterial.Diffuse = Color.FromArgb(base.Alpha, this.BorderColor);
                this._polygonMaterial.Specular = Color.FromArgb(base.Alpha, this.BorderColor);
                base._device.Material = this._polygonMaterial;
                if (base.IndexVisibilityHandler != null)
                {
                    for (int i = 0; i < base.MapData.RegionList.Count; i++)
                    {
                        int index = base.ParseIndex(i);
                        if (base.IndexVisibilityHandler.GetVisibility(index))
                        {
                            foreach (MapPolygonData data in base.MapData.RegionList[i])
                            {
                                Vector3[] border = new Vector3[data.BorderPoints.Length];
                                for (int j = 0; j < border.Length; j++)
                                {
                                    border[j] = data.BorderPoints[j].Position;
                                }
                                //iLine.Width += 1.5f;
                                //iLine.DrawTransform(border, m, Color.White);
                                //iLine.Width = iLineWidth;
                                iLine.DrawTransform(border, m , this.BorderColor);
                                //line.Draw(border, this.BorderColor);
                                //base._device.DrawUserPrimitives(PrimitiveType.LineStrip, data.BorderPoints.Length - 1, data.BorderPoints);
                            }
                        }
                    }
                }
                else
                {
                    foreach (List<MapPolygonData> list in base.MapData.RegionList)
                    {
                        foreach (MapPolygonData data2 in list)
                        {
                            Vector3[] border = new Vector3[data2.BorderPoints.Length];
                            for (int j = 0; j < border.Length; j++)
                            {
                                border[j] = data2.BorderPoints[j].Position;
                            }
                            iLine.DrawTransform(border, m, this.BorderColor);
                            //base._device.DrawUserPrimitives(PrimitiveType.LineStrip, data2.BorderPoints.Length - 1, data2.BorderPoints);
                        }
                    }
                }
            }
        }
    }
}
