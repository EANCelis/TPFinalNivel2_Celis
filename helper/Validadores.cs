using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Windows.Forms;

namespace helper
{
    public class Validadores
    {
        
        public static bool soloNumeros(string cadena)
        {
            foreach (char item in cadena)
            {
                if (char.IsNumber(item))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool txtSoloNumeros(KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
                return true;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return true;
            }
            else
            {
                e.Handled = true;
                return false;
            }
        }
        public static bool txtVacio(TextBox txt)
        {
            if (txt.Text == string.Empty)
            {
                txt.Focus();
                return true;
            }
            else
                return false;
        }

    }
}
