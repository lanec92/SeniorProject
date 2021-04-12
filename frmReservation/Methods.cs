using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmReservation
{
    class Methods
    {
        public static void AddParameters(SqlCommand cmd, string s, TextBox txt)
        {
            cmd.Parameters.Add(new SqlParameter(s, txt.Text));

        }
        public static void AddParameters(SqlCommand cmd, string s, ComboBox cmb)
        {
            cmd.Parameters.Add(new SqlParameter(s, cmb.Text));

        }

        public static void AddParameters(SqlCommand cmd, string s, string st)
        {
            cmd.Parameters.Add(new SqlParameter(s, st));

        }
        public static void AddParameters(SqlCommand cmd, string s, RadioButton rb)
        {
            cmd.Parameters.Add(new SqlParameter(s, rb.Text));

        }
        public static void AddParameters(SqlCommand cmd, string s, CheckBox cb)
        {
            cmd.Parameters.Add(new SqlParameter(s, cb.Checked));

        }
        public static void AddParameters(SqlCommand cmd, string s, int id)
        {
            cmd.Parameters.Add(new SqlParameter(s, id));

        }
    }
}
