namespace RAT_2._0
{
	// Token: 0x02000002 RID: 2
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002930 File Offset: 0x00000B30
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002968 File Offset: 0x00000B68
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(800, 450);
			base.Name = "Form1";
			base.ShowInTaskbar = false;
			this.Text = "Form1";
			base.WindowState = global::System.Windows.Forms.FormWindowState.Minimized;
			base.Load += new global::System.EventHandler(this.Form1_Load);
			base.ResumeLayout(false);
		}

		// Token: 0x04000007 RID: 7
		private global::System.ComponentModel.IContainer components = null;
	}
}
