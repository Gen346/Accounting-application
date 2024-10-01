using System;
using System.Windows.Forms;

namespace Лабораторна_робота_10
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
        }
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ok_button_Click(object sender, EventArgs e)
        {
            GetNumberOfNote();
            this.Close();
        }
        public int GetNumberOfNote()
        {
            return (int)numericUpDown1.Value;
        }


    }
}
