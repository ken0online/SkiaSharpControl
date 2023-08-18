using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkiaSharpControl
{
	public class SCycle
	{
		//事件
		public event EventHandler onMouseClick;

		//属性
		public Object Tag { get; set; }
		public string Name { get; set; }
		private Cursor cursor;
		public Cursor Cursor
		{
			get
			{
				if (this.cursor == null)
				{
					return Cursors.Default;
				}
				else
				{
					return cursor;
				}
			}
			set => cursor = value;
		}

		//回调函数改变鼠标样式
		private Action<Cursor> ChangeCursor;
		//构建条件
		private SKPaint sKPaint;
		private SKPoint skPoint1;
		private float radius;
		//内部变量
		private Point oldPoint;
		private bool isHover;
		private bool isMouseDown;
		private bool allowClick = true;//防止拖动和点击事件同时出现
		//构造
		public SCycle(SKPoint skPoint1, float radius, SKPaint sKPaint, Action<Cursor> ChangeCursor, SKGLControl sKGLControl)
		{
			this.sKPaint = sKPaint;
			this.skPoint1 = skPoint1;
			this.radius = radius;
			this.ChangeCursor = ChangeCursor;

			sKGLControl.MouseMove += HandleMouseCoordinates;
			sKGLControl.MouseDown += HandleMouseDown;
			sKGLControl.MouseUp += HandleMouseUp;
			sKGLControl.MouseClick += HandlMouseClick;
		}
		//绘制
		public void Draw(SKCanvas canvas)
		{
			canvas.DrawCircle(this.skPoint1, this.radius, this.sKPaint);
		}

		//sKGLControl MouseMove事件处理方案
		public void HandleMouseCoordinates(object sender, MouseEventArgs e)
		{
			isHover = SkiaTool.IsPointInsideCircle(skPoint1, radius, e.Location);
			if (isHover)
			{
				this.ChangeCursor(this.Cursor);
			}
			if (isMouseDown)
			{
				int xIncrement = e.Location.X - oldPoint.X;
				int yIncrement = e.Location.Y - oldPoint.Y;
				skPoint1.X += xIncrement;
				skPoint1.Y += yIncrement;
			}
			oldPoint = e.Location;
		}

		//sKGLControl MouseClick事件处理方案
		public void HandlMouseClick(object sender, MouseEventArgs e)
		{
			if (onMouseClick != null && allowClick)
			{
				if (isHover)
				{
					onMouseClick(this, e);
				}
			}
		}
		//sKGLControl MouseDown事件处理方案
		public void HandleMouseDown(object sender, MouseEventArgs e)
		{
			if (isHover) isMouseDown = true;
			allowClick = true;
		}
		//sKGLControl MouseUp事件处理方案
		public void HandleMouseUp(object sender, MouseEventArgs e)
		{
			isMouseDown = false;
			allowClick = true;
		}
	}
}
