using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Entity;













using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Data
{
    public class DInvoice
    {
        public object MessageBox { get; private set; }

        public DateTime Transformar(DateTime a)
        {
            string[] fechaFormateada = a.ToString().Split(' ');
            return DateTime.Parse(fechaFormateada[0]);
        }

        
        public List<invoices> Get()
        {
            DInvoice func = new DInvoice();

            string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=db;User ID=userTecsup;Password=123456";

            var Lista_invoices = new List<invoices>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                string query = "usp_ListarInvoice";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila
                                Lista_invoices.Add(new invoices
                                {
                                    invoice_id = (int)reader["invoice_id"],
                                    customer_id = (int)reader["customer_id"],
                                    date =   func.Transformar((DateTime)reader["date"]),
                                    total = (decimal)reader["total"]
                                });

                            }
                        }
                    }
                }
                // Cerrar la conexión
                connection.Close();
            }
            return Lista_invoices;
        }


        public string Put(int customer_id, DateTime date , Decimal total)
        {
            try
            {
                string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=db;User ID=userTecsup;Password=123456";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertarInvoice", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", customer_id);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@total", total);
                
                    cmd.ExecuteNonQuery();
                    return "Se insertó correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al ingresar los datos.";
            }
        }



        public string Delete(int invoice_id)
        {
            try
            {
                string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=db;User ID=userTecsup;Password=123456";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("EliminarInvoice", connection);
                    cmd.Parameters.AddWithValue("@invoice_id", invoice_id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Se eliminó correctamente.";
                    }
                    else
                    {
                        return "No se encontró el registro para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error al eliminar el registro.";
            }
        }




    }




}
