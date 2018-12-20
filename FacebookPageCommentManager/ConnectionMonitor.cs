using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookPageCommentManager
{
    public partial class ConnectionMonitor : Form
    {
        public ConnectionMonitor(List<string> conStatus)
        {
            InitializeComponent();
            listBox1.DataSource = conStatus;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
