using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ROLES_PERMISOS_ESCRITORIO.Datos;
using ROLES_PERMISOS_ESCRITORIO.Models;

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
            List<Models.Menu> Permisos = CD_Usuario.ObtenerPermisos(idUsuario);

            MenuStrip MiMenu = new MenuStrip();

            foreach (Models.Menu menu in Permisos)
            {
                ToolStripMenuItem menuPadre = new ToolStripMenuItem(menu.Nombre);
                menuPadre.TextImageRelation = TextImageRelation.ImageAboveText;
                string rutaImagen = Path.GetFullPath(Path.Combine(Application.StartupPath, @"../../") + menu.Icono);
                menuPadre.Image = new Bitmap(rutaImagen);
                menuPadre.ImageScaling = ToolStripItemImageScaling.None;

                foreach (SubMenu subMenu in menu.ListaSubMenu)
                {
                    ToolStripMenuItem menuHijo = new ToolStripMenuItem(subMenu.Nombre, null, Click_Menu, subMenu.NombreFormulario);
                    menuPadre.DropDownItems.Add(menuHijo);
                }

                MiMenu.Items.Add(menuPadre);
            }

            this.MainMenuStrip = MiMenu;
            Controls.Add(MiMenu);
        }

        private void Click_Menu(object sender, System.EventArgs e)
        {
            ToolStripMenuItem menuSeleccionado = (ToolStripMenuItem)sender;
            Assembly asm = Assembly.GetEntryAssembly();
            Type elemento = asm.GetType(asm.GetName().Name + "." + menuSeleccionado.Name);

            if (elemento == null)
            {
                MessageBox.Show("No se encontro el Formulario");
            }
            else
            {
                Form FormularioCreado = (Form)Activator.CreateInstance(elemento);
                int encontrado = this.MdiChildren.Where(p => p.Name.Equals(FormularioCreado.Name)).ToList().Count();

                if (encontrado.Equals(0))
                {
                    FormularioCreado.MdiParent = this;
                    FormularioCreado.Show();
                }
                else
                {
                    ((Form)(this.MdiChildren.Where(p => p.Name.Equals(FormularioCreado.Name)).FirstOrDefault())).WindowState = FormWindowState.Normal;
                    ((Form)(this.MdiChildren.Where(p => p.Name.Equals(FormularioCreado.Name)).FirstOrDefault())).Activate();
                }

                
            }


        }





    }
}
