using Registro_Detalle.BLL;
using Registro_Detalle.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Registro_Detalle.UI.Registro
{
    /// <summary>
    /// Interaction logic for rPrestamoMoras.xaml
    /// </summary>
    public partial class rPrestamoMoras : Window
    {

        private PrestamoMoras prestamoMoras = new PrestamoMoras();
        double total;
        public rPrestamoMoras()
        {
            InitializeComponent();
            this.DataContext = prestamoMoras;
        }

        //Metodo Refrescar.
        public void Refrescar()
        {
            this.DataContext = null;
            this.DataContext = prestamoMoras;
        }

        public void Limpiar()
        {
            this.prestamoMoras = new PrestamoMoras();
            this.DataContext = prestamoMoras;
        }

        //Metodo Validar
        public bool ValidarBuscar()
        {
            if (!Regex.IsMatch(MoraIdTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Solo caracteres numericos.", "Caracter invalido.", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!PrestamoMorasBLL.Existe(prestamoMoras.MoraId))
            {
                MessageBox.Show("Este registro no existe.", "Aviso.", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }


            return true;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!ValidarBuscar())
            {
                return;
            }

            PrestamoMoras moras = PrestamoMorasBLL.Buscar(int.Parse(MoraIdTextBox.Text));
            prestamoMoras = moras;
            Refrescar();

        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var detalle = new PrestamoMorasDetalle(int.Parse(IdTextBox.Text), int.Parse(MoraIdDetalleTextBox.Text),
                int.Parse(PrestamoIdTextBox.Text), double.Parse(ValorTextBox.Text));

            prestamoMoras.MorasDetalle.Add(detalle);
            prestamoMoras.Total = double.Parse(TotalTextBox.Text);

            Refrescar();
            MoraIdDetalleTextBox.Clear();
            PrestamoIdTextBox.Clear();
            ValorTextBox.Clear();
            IdTextBox.Focus();
        }

        private void Remover_Fila_Button_Click(object sender, RoutedEventArgs e)
        {
            prestamoMoras.MorasDetalle.RemoveAt(DetalleDataGrid.SelectedIndex);
            Refrescar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (PrestamoMorasBLL.Guardar(prestamoMoras))
            {
                Limpiar();
                MessageBox.Show("Guardado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Fallo al guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarBuscar())
            {
                return;
            }

            if (PrestamoMorasBLL.Eliminar(int.Parse(MoraIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado.", "Exito.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal.", "Fallo.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





    }
}
