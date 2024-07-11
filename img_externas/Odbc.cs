using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_externas
{
    internal class OD_BC
    {
        private string StrinConexion = "";
        OdbcConnection db_con;
        private string UID = "hid";
        private string PWD = "hid@123";
        public OD_BC()
        {
            StrinConexion = string.Format("DSN=SISC;UID={0};PWD={1};", UID, PWD);
            db_con = new OdbcConnection(StrinConexion);

            Probar_conexion(UID, PWD);

        }

        public bool Probar_conexion(string usr, string pass)
        {
            try
            {
                using (OdbcConnection con = new OdbcConnection(string.Format("DSN=SISC;UID={0};PWD={1};", usr, pass)))
                {
                    con.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexion rechazada: ", ex.Message);
                return false;
            }
        }




        public int EjecutarParametrizada(string sql, Dictionary<string, object> parametros = null)
        {
            int rowAffected = 0;
            try
            {
                using (db_con)
                {
                    db_con.Open();
                    using (OdbcCommand cmd = db_con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (parametros != null)
                        {
                            foreach (KeyValuePair<string, object> parametro in parametros)
                            {
                                cmd.Parameters.AddWithValue(parametro.Key, parametro.Value);
                            }
                        }
                        rowAffected = cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("SE REALIZÒ LA CONSULTA CON EXITO");
            }
            catch (Exception E)
            {
                MessageBox.Show("Excepción: " + E.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (db_con.State == ConnectionState.Open)
                {
                    db_con.Close();
                }
            }
            return rowAffected;
        }
    }
}
