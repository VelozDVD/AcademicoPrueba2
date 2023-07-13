using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Profesores
{
    public partial class Profesor : Form
    {
        private SqlConnection connec = new SqlConnection("server=LSOFT-PC;database=Académico; integrated security=true");

        public Profesor()
        {
            InitializeComponent();
        }
        public Profesor(object value)
        {
            InitializeComponent();
            txtCodigo.Text = value.ToString();
            Consultar();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private bool Validaciones()
        {
            // VALIDO QUE SE INGRESEN CAMPOS OBLIGATORIOS
            if (txtNombresProfe.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el nombre del profesor");
                txtNombresProfe.Focus();
                return false;

            }
            else
                if (txtApellidosProfe.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el apellido del profesor");
                txtApellidosProfe.Focus();
                return false;
            }
            else
                if (txtSueldo.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el sueldo del profesor");
                txtSueldo.Focus();
                return false;
            }
            else 
                if (txtTitulo.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el titulo del profesor");
                txtTitulo.Focus();
                return false;
            }
            else
                if (txtCedula.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar la cedula del profesor");
                txtCedula.Focus();
                return false;
            }
            else
                if (txtTelefono.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Debe ingresar el telefono del profesor");
                txtTelefono.Focus();
                return false;
            }
            else
            return true;
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            { 
    
                string strFecha = dtpFechaIngreso.Value.ToString("MM-dd-yyyy");
                string cadena;

                if (txtCodigo.Text == "")
                {
                    // VALIDO QUE NO HAYA OTRO PROFESOR CON LA MISMA CEDULA
                    string strNumProfesor = txtCodigo.Text;
                    connec.Open();
                    string cadena2 = "select * from Profesores where Cedula = '" + txtCedula.Text + "'";
                    SqlCommand comando2 = new SqlCommand(cadena2, connec);
                    comando2.ExecuteNonQuery();
                    SqlDataReader registros = comando2.ExecuteReader();
                    if (registros.Read())
                    {
                        string strNombre = registros["Nombres"].ToString();
                        MessageBox.Show("Número de cédula ya existe");
                        SoundPlayer player = new SoundPlayer();
                        player.Play();
                        txtCedula.Focus();
                        connec.Close();
                        return;
                    }
                    connec.Close();

                    // ARMO SQL PARA INSERTAR NUEVO ALUMNO
                    cadena = "insert into Profesores values('" + txtNombresProfe.Text + "', '" + txtApellidosProfe.Text +
                    "','" + txtSueldo.Text + "','" + strFecha + "','" + txtTitulo.Text + "','" + txtCedula.Text +
                    "','" + txtTelefono.Text + "')";
                }

                // SI HA INGRESADO EL CODIGO, ES UNA MODIFICACION
                else
                {

                    // VALIDO QUE LA CEDULA NO LA TENGA UN ALUMNO DIFERENTE A ESTE
                    string strNUmProfesor = txtCodigo.Text;
                    connec.Open();
                    string cadena2 = "select * from Profesores where NumCedula = '" + txtCedula.Text + "' and NumProfesor != " + txtCodigo.Text + "";
                    SqlCommand comando2 = new SqlCommand(cadena2, connec);
                    comando2.ExecuteNonQuery();
                    SqlDataReader registros = comando2.ExecuteReader();
                    if (registros.Read())
                    {
                        string strNombre = registros["Nombres"].ToString();
                        MessageBox.Show("Número de cédula ya existe");
                        SoundPlayer player = new SoundPlayer();
                        player.Play();
                        txtCedula.Focus();
                        connec.Close();
                        return;
                    }
                    connec.Close();


                    cadena = "update Profesores set Nombres = '" + txtNombresProfe.Text + "', Apellidos = '" + txtApellidosProfe.Text +
                        "', Sueldo ='" + txtSueldo.Text + "', FechaIngreso = '" + strFecha + "', Titulo ='" + txtTitulo.Text + "' ,NumCedula = '" + txtCedula.Text + "', Celular ='" + txtTelefono.Text +
                        "' where NumProfesor =" + txtCodigo.Text;
                }
                
                // EJECUTO EL SQL Y DESPLIEDO MENSAJE CORRESPONDIENTE  
                connec.Open();
                SqlCommand comand = new SqlCommand(cadena, connec);
                int Ok = comand.ExecuteNonQuery();
                connec.Close() ;
                if (Ok == 0)
                {
                    MessageBox.Show("No existe el código de profesor " + txtCodigo.Text);
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }
        private void Consultar()
        {
            // VERIFICO SI EL CUADRO DE TEXTO ESTA VACIO
            if (txtCodigo.Text == "")
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("Ingrese un código de profesor");
                txtCodigo.Focus();
                return;
            }

            // INGRESO LOS DATOS DE LA TABLA SQL EN LA APLICACIÓN 
            string strNumProfesor = txtCodigo.Text;
            string cadena = "select * from Profesores where NumProfesor = " + strNumProfesor + "";
            connec.Open();
            SqlCommand comando = new SqlCommand(cadena, connec);
            comando.ExecuteNonQuery();
            SqlDataReader registros = comando.ExecuteReader();

            if (registros.Read())
            {
                txtNombresProfe.Text = registros["Nombres"].ToString();
                txtApellidosProfe.Text = registros["Apellidos"].ToString();
                dtpFechaIngreso.Text = registros["FechaIngreso"].ToString();
                txtSueldo.Text = registros["Sueldo"].ToString();
                txtTitulo.Text = registros["Titulo"].ToString();
                txtCedula.Text = registros["NumCedula"].ToString();
                txtTelefono.Text = registros["Celular"].ToString();
            }
            else
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("No existe el código de profesor " + txtCodigo.Text);
                txtNombresProfe.Clear();
                txtApellidosProfe.Clear();
                txtSueldo.Clear();
                txtTitulo.Clear();
                txtCedula.Clear();
                txtTelefono.Clear();
                txtCodigo.Clear();
                txtCodigo.Focus();
            }

            connec.Close();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Este seguro de borrar este profesor?", "Dialog Value Demo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            if (txtCodigo.Text == "")
            {
                SoundPlayer Player = new SoundPlayer();
                Player.Play();

                MessageBox.Show("Debe ingresar un código de profesor a borrar");
                txtCodigo.Focus();
                return;
            }
            connec.Open();

            string cadena;
            cadena = "Delete from Profesores where NumProfesor=" + txtCodigo.Text;
            SqlCommand comando = new SqlCommand(cadena, connec);
            int CantBorrado = comando.ExecuteNonQuery();
            connec.Close();
            if (CantBorrado > 0)
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();

                MessageBox.Show("Usted a borrado el registro '" + txtCodigo.Text + "' exitosamente");

                txtNombresProfe.Clear();
                txtApellidosProfe.Clear();
                txtSueldo.Clear();
                txtTitulo.Clear();
                txtCedula.Clear();
                txtTelefono.Clear();
                txtCodigo.Clear();
                txtCodigo.Focus();
            } else
            {
                SoundPlayer player = new SoundPlayer();
                player.Play();
                MessageBox.Show("No se encontró el código de alumno '" + txtCodigo.Text);
                txtCodigo.Focus();
            }

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // SE LIMPIAN TODOS LOS CUADROS 
            txtNombresProfe.Clear();
            txtApellidosProfe.Clear();
            txtSueldo.Clear();
            txtTitulo.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtCodigo.Clear();
            txtCodigo.Focus();
        }

        private void Profesor_Load(object sender, EventArgs e)
        {

        }
    }
}
