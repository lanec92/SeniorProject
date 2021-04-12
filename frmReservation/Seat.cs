using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmReservation
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int SeatRow { get; set; }
        public string SeatColumn { get; set; }
        public bool IsSeatTaken { get; set; }
        private SqlCommand command;
        private SqlDataReader reader;

        // Check if plane is already full if so place on waiting list
        public bool IsPlaneFull()
        {
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();
                command = new SqlCommand("SELECT * FROM SEATS WHERE ISTAKEN = 0", con);
                reader = command.ExecuteReader();

                return !reader.HasRows ? true : false;
            }
        }

        // Check if seat is taken
        public bool IsSeatAlreadyTaken(string seatRow, string seatColumn)
        {
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();
                command = new SqlCommand("SELECT * FROM SEATS WHERE SEATROW = @SEATROW AND SEATCOLUMN = @SEATCOLUMN " +
                    "AND ISTAKEN = 0", con);
                //command.Parameters.Add(new SqlParameter("SEATROW", seatRow));
                Methods.AddParameters(command, "SEATROW", seatRow);
                Methods.AddParameters(command, "SEATCOLUMN", seatColumn);
                // command.Parameters.Add(new SqlParameter("SEATCOLUMN", seatColumn));
                reader = command.ExecuteReader();

                return !reader.HasRows ? true : false;
            }
        }
    }
}