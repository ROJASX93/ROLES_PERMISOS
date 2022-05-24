using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROLES_PERMISOS_ESCRITORIO
{
    public partial class MDI_Master : Form
    {
        private int idUsuario; 
        public MDI_Master(int idUsuario_esperado = 0)
        {
            InitializeComponent();
            idUsuario = idUsuario_esperado;
        }

        private void MDI_Master_Load(object sender, EventArgs e)
        {
            MessageBox.Show(idUsuario.ToString());
        }
    }
}
