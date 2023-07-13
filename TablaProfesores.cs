using Profesores;
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

namespace Menú
{
    public partial class TablaProfesores : Form
    {
        private SqlConnection conn = new SqlConnection("server=LSOFT-PC;database=Académico; integrated security=true");
        public TablaProfesores()
        {
            InitializeComponent();
        }

        private void TablaProfesores_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Profesores", conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Profesores");
                dataGridView1.DataSource = ds.Tables["Profesores"]; ;
            }
            catch 
            {
                MessageBox.Show("No se ha encontrado la información");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Profesor frmProfesores = new Profesor();
            frmProfesores.Show(this);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Profesor frmProfesores = new Profesor();
            frmProfesores.Show(this);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Profesor frmProfesores = new Profesor(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            frmProfesores.Show(this);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Profesores", conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Profesores");
            dataGridView1.DataSource = ds.Tables["Profesores"]; ;
        }
    }
}
