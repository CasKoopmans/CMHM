using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HMonServer
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			this.InitializeComponent ();
		}

		public static void open()
		{
			Application.Run (new MainWindow ());
		}
	}
}
