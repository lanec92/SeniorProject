using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmReservation
{
    public partial class PassengerInfo : Form
    {
        private SqlCommand command;
        private DataTable dt;
        public PassengerInfo(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void PassengerInfo_Load(object sender, EventArgs e)
        {
            dgvOutput.DataSource = dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvOutput_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the row number clicked
            var index = e.RowIndex;

            // Get the passengerID of the passenger and pass to command
            int selectedID = Convert.ToInt32(dgvOutput.Rows[index].Cells[0].Value);

            //select all passenger info from all three tables that matches passengerID
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();

                command = new SqlCommand
                        ("SELECT p.PassengerID as ID, s.SeatID, p.PassengerName as Name, s.SeatRow, s.SeatColumn, " +
                        "p.PassengerOnWaitingList as OnWaitingList " +
                        "FROM(Passengers p " +
                        "INNER JOIN PassengerSeats ps ON p.PassengerID = ps.PassengerID) " +
                        "INNER JOIN Seats s ON s.SeatID = ps.SeatID " +
                        "WHERE p.PassengerID = @PassengerID " +
                        "UNION " +
                        "SELECT p.PassengerID, NULL, p.PassengerName, NULL, NULL, p.PassengerOnWaitingList " +
                        "FROM Passengers p " +
                        "WHERE p.PassengerOnWaitingList = 1 AND p.PassengerID = @PassengerID " +
                        "ORDER BY s.SeatRow, s.SeatColumn", con);
                Methods.AddParameters(command, "PassengerID", selectedID);

                //execute command and place results in datatable and pass to EditPassInfo form
                DataTable dTable = new DataTable();
                dTable.Load(command.ExecuteReader());
                EditPassInfo frm = new EditPassInfo(dTable);
                frm.ShowDialog();
                Close();
            }
        }
    }
}
