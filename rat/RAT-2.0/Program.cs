using System;
using System.Windows.Forms;

namespace RAT_2._0
{
	// Token: 0x02000003 RID: 3
	internal static class Program
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002A18 File Offset: 0x00000C18
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
