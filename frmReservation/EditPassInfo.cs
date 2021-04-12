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
    public partial class EditPassInfo : Form
    {
        //Create db Objects
        private DataTable dt;
        private SqlCommand command;
        SqlDataReader reader;

        public EditPassInfo(DataTable dt)
        {
            InitializeComponent();
            command = new SqlCommand();
            this.dt = dt;
        }

        //Bind the form objects to the data from the DataTable
        private void EditPassInfo_Load(object sender, EventArgs e)
        {
            //bind text boxes
            txtPassID.DataBindings.Add("Text", dt, "ID");
            txtName.DataBindings.Add("Text", dt, "Name");
            txtSeatID.DataBindings.Add("Text", dt, "SeatID");

            //bind dropdowns
            //If seats are empty since the passenger is on the waiting list make row and column None
            var r = dt.Rows[0]["SeatRow"].ToString();
            var row = r.Equals("") ? 0 : Convert.ToInt32(r);
            var c = dt.Rows[0]["SeatColumn"].ToString();
            var column = c.Equals("") ? "None" : c;
            cmbRow.SelectedIndex = row;
            cmbColumn.SelectedItem = column;

            //Bind Checkbox
            chbOnList.Checked = Convert.ToBoolean(dt.Rows[0]["OnWaitingList"]);

        }

        //edit the passenger record
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Validate input from passenger
            //check that passenger name has been entered
            if (txtName.Text.Trim().Equals(""))
            {
                MessageBox.Show("Passenger name is required.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Passenger cant have a seat and be on waiting list
            if (chbOnList.Checked && (cmbRow.SelectedIndex > 0 || cmbColumn.SelectedIndex > 0))
            {
                MessageBox.Show("Passenger cannot be on a waiting list and have a seat assigned.", "Invalid Input"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Passenger must have a seat or on waiting list
            if (!chbOnList.Checked && (cmbRow.SelectedIndex <= 0 || cmbColumn.SelectedIndex <= 0))
            {
                MessageBox.Show("Passenger must have a seat assigned or be on the waiting list.", "Invalid Input"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update passenger record
            //1. Get id of the new seat
            //2. check if seat is taken 
            //3. check is passenger is just updating name and if so ignore that the seat is taken(its the same passenger)
            //4. update all tables name seat taken or not taken etc.
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();
                command = new SqlCommand("SELECT SeatID, IsTaken FROM Seats WHERE " +
                    "SeatRow = @SeatRow AND SeatColumn = @SeatColumn", con);
                command.Parameters.Add(new SqlParameter("SeatRow", cmbRow.SelectedItem));
                command.Parameters.Add(new SqlParameter("SeatColumn", cmbColumn.SelectedItem));
                reader = command.ExecuteReader();

                var newSeatID = 0;
                bool newIsTaken = false;
                while (reader.Read())
                {
                    newSeatID = Convert.ToInt32(reader["SeatID"]);
                    newIsTaken = Convert.ToBoolean(reader["IsTaken"]);
                }

                //check if only the name is being updated. 
                //If not, exit because the user needs to pick a different seat
                int oldID = 0;
                if (txtSeatID.Text.Equals(""))
                    oldID = 0;
                else
                    oldID = Convert.ToInt32(txtSeatID.Text);

                if (!txtSeatID.Equals(""))
                {
                    if (newSeatID != oldID && newIsTaken)
                    {
                        MessageBox.Show("Seat is already taken", "Seat Taken", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                    newSeatID = 0;

                //update passenger's name
                command = new SqlCommand("UPDATE Passengers SET PassengerName = @PassengerName, " +
                    "PassengerOnWaitingList = @OnWaitingList WHERE PassengerID = @PassengerID", con);
                Methods.AddParameters(command, "PassengerName", txtName);
                Methods.AddParameters(command, "OnWaitingList", chbOnList);
                Methods.AddParameters(command, "PassengerID", txtPassID);


                command.ExecuteNonQuery();

                //make original seat available
                command = new SqlCommand("UPDATE Seats SET IsTaken = 0 WHERE " +
                    "seatID = @seatID", con);
                Methods.AddParameters(command, "seatID", txtSeatID);
                //command.Parameters.Add(new SqlParameter("seatID", txtSeatID.Text));
                command.ExecuteNonQuery();

                //make new seat taken
                command = new SqlCommand("UPDATE Seats SET IsTaken = 1 WHERE " +
                    "seatID = @seatID", con);
                Methods.AddParameters(command, "seatID", newSeatID);
                //command.Parameters.Add(new SqlParameter("seatID", newSeatID));
                command.ExecuteNonQuery();

                //update old seatID with the new one
                command = new SqlCommand("UPDATE PassengerSeats SET SeatID = @SeatID WHERE " +
                    "PassengerID = @PassengerID", con);
                Methods.AddParameters(command, "SeatID", newSeatID);
                Methods.AddParameters(command, "PassengerID", txtPassID);

                //command.Parameters.Add(new SqlParameter("SeatID", newSeatID));
                //command.Parameters.Add(new SqlParameter("PassengerID", txtPassID.Text));
                command.ExecuteNonQuery();

                MessageBox.Show("Record has been updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Ask user if they want to delete passengers
            //If not then exit
            //If yes then delete passenger from passenger and passengerseats
            //The seat still exists but we need to update the seats table and mark the seat as not taken

            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();

                var msg = MessageBox.Show("Are you sure you want to delete " + txtName.Text + ". This action cannot be undone", 
                    "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (msg == DialogResult.No)
                    return;

                command = new SqlCommand("DELETE FROM PASSENGERS WHERE PassengerID = @PassengerID", con);
                //command.Parameters.Add(new SqlParameter("PassengerID", txtPassID.Text));
                Methods.AddParameters(command, "PassengerID", txtPassID);
                command.ExecuteNonQuery();

                command = new SqlCommand("DELETE FROM PASSENGERSEATS WHERE PassengerID = @PassengerID", con);
                Methods.AddParameters(command, "PassengerID", txtPassID);

                //command.Parameters.Add(new SqlParameter("PassengerID", txtPassID.Text));
                command.ExecuteNonQuery();

                if(!txtSeatID.Text.Equals(""))
                {
                    command = new SqlCommand("UPDATE SEATS SET IsTaken = 0 WHERE SeatID = @SeatID", con);
                    //command.Parameters.Add(new SqlParameter("SeatID", txtSeatID.Text));
                    Methods.AddParameters(command, "SeatID", txtSeatID);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Passenger has been deleted.",
                    "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
