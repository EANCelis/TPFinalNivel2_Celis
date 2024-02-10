using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;
using System.IO;
using System.Configuration;
using helper;

namespace presentacion
{
    public partial class frmAgregar : Form
    {
        private OpenFileDialog archivo = null;
        private Producto producto = null;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(Producto producto)
        {
            InitializeComponent();
            this.producto = producto;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFrmAgregar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            try
            {
                if (producto == null)
                    producto = new Producto();
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.CodigoArticulo = txtCodigo.Text;
                producto.ImagenUrl = txtUrlImagen.Text;
                producto.Precio = decimal.Parse(txtPrecio.Text);
                producto.Categoria = (Categoria)cbxCategoria.SelectedItem;
                producto.Marca = (Marca)cbxMarca.SelectedItem;
                if (producto.Id != 0)
                {
                    negocio.modificar(producto);
                    MessageBox.Show("Modificado exitosamente");

                }
                else
                {
                    negocio.agregar(producto);
                    MessageBox.Show("Agregado exitosamente");

                }
                if (archivo != null && !(txtUrlImagen.Text.ToUpper().Contains("HTTP")))
                    guardarImagenLocal();

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void frmAgregar_Load(object sender, EventArgs e)
        {
            MarcaNegocio negocioMarca = new MarcaNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            try
            {
                cbxMarca.DataSource = negocioMarca.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
                cbxCategoria.DataSource = negocioCategoria.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                if (producto != null)
                {
                    txtNombre.Text = producto.Nombre;
                    txtCodigo.Text = producto.CodigoArticulo;
                    txtDescripcion.Text = producto.Descripcion;
                    txtPrecio.Text = producto.Precio.ToString();
                    txtUrlImagen.Text = producto.ImagenUrl;
                    cargarImagen(producto.ImagenUrl);
                    cbxCategoria.SelectedValue = producto.Categoria.Id;
                    cbxMarca.SelectedValue = producto.Marca.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pcbUrlImagen.Load(imagen);

            }
            catch (Exception)
            {

                pcbUrlImagen.Load("http://i0.wp.com/sunrisedaycamp.org/wp-content/uploads/2020/10/placeholder.png");
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo = new OpenFileDialog();
            archivo.Filter = "Imágenes JPG|*.jpg| Imágenes PNG|*.png| Imágenes JPGE|*.jpge";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtUrlImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
            }
        }
        private void guardarImagenLocal()
        {
            File.Copy(archivo.FileName, ConfigurationManager.AppSettings["carpeta-imagenes"] + Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName));
        }
        ErrorProvider errorP = new ErrorProvider();
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool validar = Validadores.txtSoloNumeros(e);
            if (!validar)
                errorP.SetError(txtPrecio, "Solo números");
            else
                errorP.Clear();
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (Validadores.txtVacio(txtCodigo))
                errorP.SetError(txtCodigo, "No puede quedar vacío");
            else
                errorP.Clear();
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (Validadores.txtVacio(txtNombre))
                errorP.SetError(txtNombre, "No puede quedar vacío");
            else
                errorP.Clear();
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (Validadores.txtVacio(txtPrecio))
                errorP.SetError(txtPrecio, "No puede quedar vacío");
            else
                errorP.Clear();
        }
    }
}
