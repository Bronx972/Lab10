using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Data;

namespace Lab7
{
    /// <summary>
    /// Lógica de interacción para Insertar.xaml
    /// </summary>
    public partial class Insertar : Window
    {
        public Insertar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DInvoice func = new DInvoice();
            DateTime selectedDate = txtDate.SelectedDate.Value;
            Decimal total = Decimal.Parse(txtTotal.Text);
            int customer_id = int.Parse(txtCustomer_id.Text);
            func.Put(customer_id,selectedDate, total);
        }
    }
}
