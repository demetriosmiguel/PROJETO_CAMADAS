using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APRESENTACAO
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void menuSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuCliente_Click(object sender, EventArgs e)
        {
            FormClienteSelecionar formClienteSelecionar = new FormClienteSelecionar();
            formClienteSelecionar.MdiParent = this;
            formClienteSelecionar.Show();

        }
    }
}
