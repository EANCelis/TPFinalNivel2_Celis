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
using helper;

namespace presentacion
{
    public partial class frmPrincipal : Form
    {
        private List<Producto> listaProductos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargar();
            cbxCampo.Items.Add("Precio");
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Marca");
            cbxCampo.Items.Add("Categoría");
            timer1.Enabled = true;
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                Producto seleccionado = (Producto)dgvProductos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pcbImagen.Load(imagen);

            }
            catch (Exception)
            {

                pcbImagen.Load("http://i0.wp.com/sunrisedaycamp.org/wp-content/uploads/2020/10/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.ShowDialog();
            cargar();
        }
        private void cargar()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            listaProductos = negocio.listar();
            dgvProductos.DataSource = listaProductos;
            ocultarColumnas();
            dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "N2";
            cargarImagen(listaProductos[0].ImagenUrl);
        }
        private void ocultarColumnas()
        {
            dgvProductos.Columns["ImagenUrl"].Visible = false;
            dgvProductos.Columns["Id"].Visible = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Producto seleccionado;
            try
            {
                seleccionado = (Producto)dgvProductos.CurrentRow.DataBoundItem;
                frmAgregar modificar = new frmAgregar(seleccionado);
                modificar.Text = "Modificar Producto";
                modificar.ShowDialog();
                cargar();

            }
            catch (NullReferenceException)
            {

                MessageBox.Show("Debe seleccionar un producto para modificar.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto seleccionado;
            try
            {
                seleccionado = (Producto)dgvProductos.CurrentRow.DataBoundItem;
                DialogResult confirmacion = MessageBox.Show("¿De verdad querés eliminar este producto?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.Yes)
                    negocio.eliminar(seleccionado.Id);
                cargar();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Debe seleccionar un producto para eliminar.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            Producto seleccionado;
            try
            {
                seleccionado = (Producto)dgvProductos.CurrentRow.DataBoundItem;
                txtVerDetalle.Text = "Código: " + seleccionado.CodigoArticulo + "\r\n" + "Nombre: " + seleccionado.Nombre + "\r\n" + "Marca: " + seleccionado.Marca.Descripcion + "\r\n" + "Categoría: " + seleccionado.Categoria.Descripcion + "\r\n" + "Descripción: " + seleccionado.Descripcion;
                txtVerDetalle.Visible = true;
                btnOcultar.Visible = true;

            }
            catch (NullReferenceException)
            {

                MessageBox.Show("Debe seleccionar un producto para ver el detalle.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            txtVerDetalle.Visible = false;
            btnOcultar.Visible = false;
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Producto> listaFiltrada;
            string filtro = txtFiltroRapido.Text;
            if (filtro.Length >= 3)
            {
                listaFiltrada = listaProductos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.CodigoArticulo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaProductos;
            }
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = listaFiltrada;
            ocultarColumnas();
            dgvProductos.Columns["Precio"].DefaultCellStyle.Format = "N2";
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Mayor a");
                cbxCriterio.Items.Add("Menor a");
                cbxCriterio.Items.Add("Igual a");
            }
            else
            {
                cbxCriterio.Items.Clear();
                cbxCriterio.Items.Add("Empieza con");
                cbxCriterio.Items.Add("Termina con");
                cbxCriterio.Items.Add("Contiene");
            }
        }
        
        private bool validarFiltro()
        {
            if (cbxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el campo para filtrar.");
                return false;
            }
            if (cbxCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione el criterio para filtrar.");
                return false;
            }
            if (cbxCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Tenés que cargar el filtro para el precio.");
                    return false;
                }
                if (!(Validadores.soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Por favor ingrese solo números para filtrar por precio.");
                    return false;
                }
            }
            return true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            try
            {
                if (!(validarFiltro()))
                    return;
                string campo = cbxCampo.SelectedItem.ToString();
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvProductos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception)
            {

                MessageBox.Show("Por favor, para decimales usar punto en lugar de coma.");
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblVisorFecha.Text = DateTime.Now.ToLongDateString();
            lblVisorHora.Text = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
