using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiaSharpControl
{
	public partial class Form1 : Form
	{
		List<SLine> listLine;
		SRectangle rectangle;
		SCycle cycle;
		SText text;
		public Form1()
		{
			InitializeComponent();
			// 创建一个订阅者对象，并将其事件处理方法绑定到鼠标移动事件上

			SKPaint paint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = SKColors.Red,
				StrokeWidth = 2,
				//设置抗锯齿
				IsAntialias = true,
			};
			//设置虚实线;
			float[] dash_param = { 5F, 3F };
			SKPathEffect dashes = SKPathEffect.CreateDash(dash_param, 0);
			paint.PathEffect = dashes;

			listLine = new List<SLine>();
			for (int i = 1; i <= 2; i++)
			{
				var line = new SLine(new SKPoint(i * 50, 10), new SKPoint(i * 50, 30), paint, this.ChangeCursor, skglControl1);
				line.Name = i + "vb";
				line.Cursor = Cursors.SizeWE;
				line.onMouseClick += Line_onMouseClick;
				listLine.Add(line);
			}

			//填充颜色
			SKPaint fill_paint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = new SKColor(255, 0, 0, 125),
				StrokeWidth = 10,
				IsAntialias = true
			};
			SKPaint text_paint = new SKPaint
			{
				FakeBoldText = true,
				TextSize = 30,
				Color = SKColors.Green,
				TextAlign = SKTextAlign.Center,
				//宋体，不然不支持中文绘制;
				Typeface = SKTypeface.FromFamilyName("SimSun"),
				IsAntialias = true
			};

			rectangle = new SRectangle(new SKPoint(200 - 180, 75), 360, 30, fill_paint, this.ChangeCursor, this.skglControl1);
			rectangle.Cursor = Cursors.Hand;
			cycle = new SCycle(new SKPoint(200, 100), 2, fill_paint, this.ChangeCursor, this.skglControl1);
			cycle.Cursor = Cursors.Hand;
			text = new SText(new SKPoint(200, 100), "5", text_paint, this.ChangeCursor, this.skglControl1);
			text.Cursor = Cursors.Hand;
		}

		private void Line_onMouseClick(object sender, EventArgs e)
		{
			SLine line = (SLine)sender;
			this.skglControl1.Refresh();
			MessageBox.Show(line.Name);
		}

		bool allowChangeCursor = true;
		public void ChangeCursor(Cursor cursor)
		{
			if (cursor != null && allowChangeCursor)
			{
				this.skglControl1.Cursor = cursor;
				allowChangeCursor = false;
			}
		}
		//大重绘事件里重绘
		private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
		{
			var canvas = e.Surface.Canvas;
			//canvas.Scale(5, 5);
			canvas.Clear(new SKColor(255, 255, 255, 255));

			for (int i = 0; i < listLine.Count; i++)
			{
				listLine[i].Draw(canvas);
			}
			rectangle.Draw(canvas);
			cycle.Draw(canvas);
			text.Draw(canvas);
			canvas.Save();
			//逆时针旋转45度绘制
			//canvas.RotateDegrees(-45, 250, 250);
			canvas.Restore();
		}
		private void skglControl1_MouseMove(object sender, MouseEventArgs e)
		{
			if (allowChangeCursor)//说明鼠标没有在任何一个对象中
			{
				this.skglControl1.Cursor = Cursors.Default;
			}
			else//说明鼠标在某一个对象中,那么在skglControl1中就不设置鼠标样式了,以免造成鼠标闪烁
			{
				allowChangeCursor = true;
			}
			skglControl1.Invalidate();
		}
	}
}
