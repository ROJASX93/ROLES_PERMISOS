using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ROLES_PERMISOS_ESCRITORIO.Datos;
using ROLES_PERMISOS_ESCRITORIO.Models;

namespace ROLES_PERMISOS_ESCRITORIO
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            int idUsuario = CD_Usuario.Loguear(txtUsuario.Text, txtContraseña.Text);

            if (idUsuario != 0)
            {
                this.Hide();
                MDI_Master master = new MDI_Master(idUsuario);
                master.Show();
            }

            else
            {
                MessageBox.Show("Usuario incorrecto");
            }
        }
    }
}
