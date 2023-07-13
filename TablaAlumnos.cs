using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alumnos;
using Profesores;

namespace Menú
{
    public partial class TablaAlumnos : Form
    {
        private SqlConnection conn = new SqlConnection("server=LSOFT-PC;database=Académico; integrated security=true");

        public TablaAlumnos()
        {
            InitializeComponent();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {         
            Alumno frmAlumnos = new Alumno();
            frmAlumnos.Show(this);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TablaAlumnos_Load(object sender, EventArgs e)
        {
            // SE AGREGAN LOS DATOS DE SQL EN LA TABLA 
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Estudiantes", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Estudiantes");
                dataGridView1.DataSource = ds.Tables["Estudiantes"]; ;
            }
            catch
            {
                MessageBox.Show("No Record Found");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            Alumno frmAlumnos = new Alumno(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            frmAlumnos.Show(this);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Alumno frmAlumno = new Alumno();
            frmAlumno.Show(this);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            //conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Estudiantes", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Estudiantes");
            dataGridView1.DataSource = ds.Tables["Estudiantes"]; ;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Hola buenas tardes");
        }
    }
}
