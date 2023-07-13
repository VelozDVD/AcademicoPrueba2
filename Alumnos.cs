using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Imaging;

namespace Alumnos
{
    public partial class Alumno : Form
    {
        private SqlConnection conn = new SqlConnection("server=LSOFT-PC;database=Académico; integrated security=true");

        public Alumno()
        {
            InitializeComponent();
        }

        public Alumno(object value)
        {
            InitializeComponent();
            txtCodigo.Text = value.ToString();
            Consultar();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Alumno_Load(object sender, EventArgs e)
        {

        }


        private bool ValidarObligatorios()
        {
            // VALIDO QUE SE INGRESEN CAMPOS OBLIGATORIOS
            if (txtNombres.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el nombre del alumno");
                txtNombres.Focus();
                return false;
            }
            else
               if (txtApellido.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar los apellidos del alumno");
                txtApellido.Focus();
                return false;
            }
            else 

               if (txtCedula.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar la cédula del alumno");
                txtCedula.Focus();
                return false;
            }
            else
               if (txtTelefono.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el teléfono del alumno");
                txtTelefono.Focus();
                return false;
            }
            else
               if (txtPadre.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el nombre del padre del alumno");
                txtPadre.Focus();
                return false;
            }
            else
               if (txtMadre.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el nombre de la madre del alumno");
                txtMadre.Focus();
                return false;
            }
            else
                return true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (ValidarObligatorios())
            {

                string strFecha = dtpFechaNacimiento.Value.ToString("MM-dd-yyyy");
               

                string cadena;

                // SI NO SE HA INGRESADO EL CODIGO, ASUMO QUE ES UN NUEVO ALUMNO
                if (txtCodigo.Text == "")
                {

                    // VALIDO QUE NO HAYA OTRO ALUMNO CON LA MISMA CEDULA
                    string strNumEstudiante = txtCodigo.Text;
                    conn.Open();
                    string cadena2 = "select * from Estudiantes where NumCedulaEs = '" + txtCedula.Text+ "'";
                    SqlCommand comando2 = new SqlCommand(cadena2, conn);
                    comando2.ExecuteNonQuery();
                    SqlDataReader registros = comando2.ExecuteReader();
                    if (registros.Read())
                    {
                        string strNombre = registros["Nombres"].ToString();
                        MessageBox.Show("Número de cédula ya existe, lo tiene " + strNombre);
                        txtCedula.Focus();
                        conn.Close();
                        return;
                    }
                    conn.Close();

                    
                    cadena = "insert into Estudiantes values('" + txtNombres.Text + "', '" + txtApellido.Text + "', '" + strFecha + "', '" + txtCedula.Text + "', '" + txtTelefono.Text + "', '" +
                        txtPadre.Text + "', '" + txtMadre.Text + "')";
                }

                // SI HA INGRESADO EL CODIGO, ES UNA MODIFICACION
                else
                {

                    // VALIDO QUE LA CEDULA NO LA TENGA UN ALUMNO DIFERENTE A ESTE
                    string strNumEstudiante = txtCodigo.Text;
                    conn.Open();
                    string cadena2 = "select * from Estudiantes where NumCedulaEs = '" + txtCedula.Text + "' and NumEstudiante != " + txtCodigo.Text + "";
                    SqlCommand comando2 = new SqlCommand(cadena2, conn);
                    comando2.ExecuteNonQuery();
                    SqlDataReader registros = comando2.ExecuteReader();
                    if (registros.Read())
                    {
                        string strNombre = registros["Nombres"].ToString();
                        MessageBox.Show("Número de cédula ya existe, lo tiene " + strNombre);
                        SoundPlayer player = new SoundPlayer();
                        player.Play();
                        txtCedula.Focus();
                        conn.Close();
                        return;
                    }
                    conn.Close();


                    // ARMO SQL PARA ACTUALIZAR EL ALUMNO
                    cadena = "update Estudiantes set Nombres = '" + txtNombres.Text + "', Apellidos = '" + txtApellido.Text + "', FechaNacimiento='" + strFecha + "', NumCedula='" + txtCedula.Text + "', Celular='" + txtTelefono.Text + "', Padre='" +
                            txtPadre.Text + "', Madre='" + txtMadre.Text + "' where NumEstudiante=" + txtCodigo.Text;

                }

                // EJECUTO EL SQL Y DESPLIEGO MENSAJE CORRESPONDIENTE
                conn.Open();
                SqlCommand comando = new SqlCommand(cadena, conn);
                int ok = comando.ExecuteNonQuery();
                conn.Close();
                if (ok == 0)
                {
                    MessageBox.Show("No existe el código de alumno " + txtCodigo.Text);
                } else
                {
                    if (txtCodigo.Text == "")
                    {
                        MessageBox.Show("Se agregó exitosamente a la base de datos!");

                    }
                    else
                        MessageBox.Show("Se actualizó exitosamente a la base de datos!");
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            // SI EL CODIGO DEL ALUMNO ESTÁ VACÍO, LE REQUIERE QUE SE INGRESE UNO
            if (txtCodigo.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Ingrese un código de alumno");
                txtCodigo.Focus();
                return;
            }

            // CONSULTO Y DESPLIEGO LOS DATOS DEL ALUMNO
            string strNumEstudiante = txtCodigo.Text;
            string cadena = "select * from Estudiantes where NumEstudiante = " + strNumEstudiante + "";
            conn.Open();
            SqlCommand comando = new SqlCommand(cadena, conn);
            comando.ExecuteNonQuery();
            SqlDataReader registros = comando.ExecuteReader();

            if (registros.Read())
            {
                txtNombres.Text = registros["Nombres"].ToString();
                txtApellido.Text = registros["Apellidos"].ToString();
                dtpFechaNacimiento.Text = registros["FechaNacimiento"].ToString();
                txtCedula.Text = registros["NumCedula"].ToString();
                txtTelefono.Text = registros["Celular"].ToString();
                txtPadre.Text = registros["Padre"].ToString();
                txtMadre.Text = registros["Madre"].ToString();
            }
            else
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("No existe el código de alumno " + txtCodigo.Text);
                txtNombres.Clear();
                txtApellido.Clear();
                //dtpFechaNacimiento.Clear();
                txtCedula.Clear();
                txtTelefono.Clear();
                txtPadre.Clear();
                txtMadre.Clear();
                txtCodigo.Clear();
                txtCodigo.Focus();
            }
            conn.Close();

        }


        private void btnBorrar_Click(object sender, EventArgs e)
        {

            // SI EL CODIGO DEL ALUMNO ESTÁ VACÍO, LE REQUIERO QUE SE INGRESE UNO
            if (txtCodigo.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el código del estudiante a borrar");
                txtCodigo.Focus();
                return;
            }

            // SE PIDE CONFIRMACIÓN PARA LA ELIMINACIÓN 
            if (MessageBox.Show("¿Este seguro de borrar este alumno?", "Dialog Value Demo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            // TRATO DE BORRAR EL REGISTRO DEL ALUMNO
            string cadena;
            cadena = "Delete from Estudiantes where NumEstudiante=" + txtCodigo.Text;
            conn.Open();
            SqlCommand comando = new SqlCommand(cadena, conn);
            int cantBorrado = comando.ExecuteNonQuery();
            conn.Close();

            // ENVIO MENSAJE CORRESPODIENTE
            if (cantBorrado > 0)
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Usted a borrado el registro '" + txtCodigo.Text + "' exitosamente");
                txtNombres.Clear();
                txtApellido.Clear();
                txtCedula.Clear();
                txtTelefono.Clear();
                txtPadre.Clear();
                txtMadre.Clear();
                txtCodigo.Clear();
                txtCodigo.Focus();
            } else
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("No se encontró el código de alumno '" + txtCodigo.Text );
                txtCodigo.Focus();
            }

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtNombres_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCedula_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // SE LIMPIAN TODOS LOS CUADROS 
            txtCodigo.Clear();
            txtNombres.Clear();
            txtApellido.Clear();
            txtCedula.Clear();  
            txtTelefono.Clear();
            txtPadre.Clear();
            txtMadre.Clear();
            txtCodigo.Focus();
        }
        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
