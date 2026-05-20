using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thuchanhdangnhap
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
            LoadUC(new UC_QLSV());
            ToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Bold);
        }
        private void LoadUC(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }


        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            LoadUC(new UC_QLSV());
             quảnLýLớpHọcToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Bold);
    ToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Regular);

            ToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Bold);
            quảnLýLớpHọcToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Regular);
        }
  
   

        private void quảnLýLớpHọcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            LoadUC(new UC_QLLH());
            quảnLýLớpHọcToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Bold);
            ToolStripMenuItem.Font = new Font("Arial", 9, FontStyle.Regular);
        }
    }
}
