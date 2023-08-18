using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkiaSharpControl
{
	public class SText
	{
		//事件
		public event EventHandler onMouseClick;

		//属性
		public Object Tag { get; set; }
		public string Name { get; set; }
		public string Text { get => text; set => text = value; }

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
		private string text;
		//内部变量
		private Point oldPoint;
		private bool isHover;
		private bool isMouseDown;
		private bool allowClick = true;//防止拖动和点击事件同时出现
		//构造
		public SText(SKPoint skPoint1, string text, SKPaint sKPaint, Action<Cursor> ChangeCursor, SKGLControl sKGLControl)
		{
			this.sKPaint = sKPaint;
			this.skPoint1 = skPoint1;
			this.text = text;
			this.ChangeCursor = ChangeCursor;

			sKGLControl.MouseMove += HandleMouseCoordinates;
			sKGLControl.MouseDown += HandleMouseDown;
			sKGLControl.MouseUp += HandleMouseUp;
			sKGLControl.MouseClick += HandlMouseClick;
		}
		//绘制
		public void Draw(SKCanvas canvas)
		{
			canvas.DrawText(this.text, this.skPoint1, this.sKPaint);
		}

		//sKGLControl MouseMove事件处理方案
		public void HandleMouseCoordinates(object sender, MouseEventArgs e)
		{
			float width = this.CalculateStringWidth(this.text, this.sKPaint.TextSize);
			float height = this.sKPaint.TextSize;
			if (sKPaint.TextAlign==SKTextAlign.Center)
			{
				isHover= SkiaTool.IsPointInsideRectangle(new SKPoint(this.skPoint1.X-width/2,(float)(this.skPoint1.Y-height*0.85)), width, height, e.Location);
			}
			else if (sKPaint.TextAlign==SKTextAlign.Right)
			{
				isHover = SkiaTool.IsPointInsideRectangle(new SKPoint(this.skPoint1.X - width, (float)(this.skPoint1.Y - height * 0.85)), width, height, e.Location);
			}
			else
			{
				isHover = SkiaTool.IsPointInsideRectangle(new SKPoint(this.skPoint1.X, (float)(this.skPoint1.Y - height * 0.85)), width, height, e.Location);
			}
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
		private float CalculateStringWidth(string text, float size)
		{
			float totalWidth = 0.0f;

			foreach (char c in text)
			{
				if (IsFullWidthCharacter(c))
				{
					totalWidth += size;
				}
				else
				{
					totalWidth += size/2;
				}
			}

			return totalWidth;
		}

		private bool IsFullWidthCharacter(char c)
		{
			// 假设全角字符的 Unicode 编码范围是 0xFF00 到 0xFFEF
			return c >= 0xFF00 && c <= 0xFFEF;
		}
	}
}
