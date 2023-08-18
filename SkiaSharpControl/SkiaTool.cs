using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaSharpControl
{
	public class SkiaTool
	{
		/// <summary>
		/// 到线段的距离检测
		/// </summary>
		/// <param name="lineStart">线段起点</param>
		/// <param name="lineEnd">线段终点</param>
		/// <param name="point">被检测的点</param>
		/// <param name="threshold">距离</param>
		/// <returns>是否在设定的距离内</returns>
		public static bool IsMouseNear(SKPoint lineStart, SKPoint lineEnd, Point point, double threshold)
		{
			double lengthSquared = Math.Pow(lineEnd.X - lineStart.X, 2) + Math.Pow(lineEnd.Y - lineStart.Y, 2);
			if (lengthSquared == 0.0)
			{
				double distanceToStart = Math.Sqrt(Math.Pow(point.X - lineStart.X, 2) + Math.Pow(point.Y - lineStart.Y, 2));
				return distanceToStart < threshold;
			}

			double t = ((point.X - lineStart.X) * (lineEnd.X - lineStart.X) + (point.Y - lineStart.Y) * (lineEnd.Y - lineStart.Y)) / lengthSquared;
			t = Math.Max(0, Math.Min(1, t));

			double projectionX = lineStart.X + t * (lineEnd.X - lineStart.X);
			double projectionY = lineStart.Y + t * (lineEnd.Y - lineStart.Y);

			double distance = Math.Sqrt(Math.Pow(point.X - projectionX, 2) + Math.Pow(point.Y - projectionY, 2));
			return distance < threshold;
		}
		/// <summary>
		/// 检测点是否在矩形内
		/// </summary>
		/// <param name="startPoint">矩形起点</param>
		/// <param name="width">矩形宽度</param>
		/// <param name="height">矩形高度</param>
		/// <param name="point">检测点</param>
		/// <returns>在区域内返回true,否则返回false</returns>
		public static bool IsPointInsideRectangle(SKPoint startPoint, float width, float height, Point point)
		{
			float maxX = startPoint.X + width;
			float maxY = startPoint.Y + height;

			return point.X >= startPoint.X && point.X <= maxX &&
				   point.Y >= startPoint.Y && point.Y <= maxY;
		}
		/// <summary>
		/// 检测点是否在圆内
		/// </summary>
		/// <param name="center">圆心</param>
		/// <param name="radius">半径</param>
		/// <param name="point">检测点</param>
		/// <returns>在圆内返回true,否则返回false</returns>
		public static bool IsPointInsideCircle(SKPoint center, float radius, Point point)
		{
			float distance = (float)Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
			return distance <= radius;
		}
	}
}
