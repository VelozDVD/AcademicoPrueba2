using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alumnos;
using Profesores;
using Materias;
using Cursos;
using Matriculas;
using Plantillas;
using Notas;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Menú
{
    public partial class frmMenú : Form
    {

        public frmMenú()
        {
            InitializeComponent();
        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TablaAlumnos tablaAlumnos = new TablaAlumnos();
            tablaAlumnos.Show(this);
        }

        private void profesoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TablaProfesores tablaProfesores = new TablaProfesores();
            tablaProfesores.Show(this);
        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Materia frmMateria = new Materia();
            frmMateria.Show(this);
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Curso frmCursos = new Curso();
            frmCursos.Show(this);
        }

        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nota frmNotas = new Nota();
            frmNotas.Show(this);
        }

        private void matriculasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Matricula frmMatricula = new Matricula();
            frmMatricula.Show(this);
        }

        private void plantillasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Plantilla frmPlantilla = new Plantilla();
            frmPlantilla.Show(this);
        }

        private void establecerImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Hola
        }

        private void establerColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog1.Color;
        }

        private void borrarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
    }
}
