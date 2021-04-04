using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UE4LocalizationManager;

namespace ULMGUI
{
    public partial class Form1 : Form
    {
        LocRes Localization;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            }
            catch { }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var Data = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
            Localization = new LocRes(Data);
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Localization.Import());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                listBox1.Items[listBox1.SelectedIndex] = textBox1.Text.Replace("\\n", "\n");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var Data = Localization.Export(listBox1.Items.Cast<string>().ToArray());
            System.IO.File.WriteAllBytes(saveFileDialog1.FileName, Data);
            MessageBox.Show("Saved.");
        }
    }
}
