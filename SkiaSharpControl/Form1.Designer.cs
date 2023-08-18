
namespace SkiaSharpControl
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl();
			this.SuspendLayout();
			// 
			// skglControl1
			// 
			this.skglControl1.BackColor = System.Drawing.Color.White;
			this.skglControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skglControl1.Location = new System.Drawing.Point(0, 0);
			this.skglControl1.Name = "skglControl1";
			this.skglControl1.Size = new System.Drawing.Size(858, 552);
			this.skglControl1.TabIndex = 0;
			this.skglControl1.VSync = false;
			this.skglControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControl1_PaintSurface);
			this.skglControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.skglControl1_MouseMove);
			this.skglControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.skglControl1_MouseMove);
			this.skglControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.skglControl1_MouseMove);
			this.skglControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.skglControl1_MouseMove);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(858, 552);
			this.Controls.Add(this.skglControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private SkiaSharp.Views.Desktop.SKGLControl skglControl1;
	}
}

