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
    public partial class frmReservation : Form
    {
        // passenger and seat objects
        Passenger passenger;
        Seat seat;

        // List of all seats
        public static List<Seat> seats;

        // DB objects
        private SqlCommand command;
        private SqlDataReader reader;

        public frmReservation()
        {
            InitializeComponent();
            command = new SqlCommand();
            seats = new List<Seat>();
        }

        // When form loads display the seats and populate the dropdown with the seat rows
        private void frmReservation_Load(object sender, EventArgs e)
        {
            PopSeatRows();
            PopAirplane();
        }

        // Add a passenger
        private void btnAddPass_Click(object sender, EventArgs e)
        {
            // Passenger and seat objects
            passenger = new Passenger();
            seat = new Seat();

            // see what seat column was selected
            var checkBtn = grpSeatsColumn.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked); // use lambda expression to check the first button selected then assign to var

            //Validate input - name, seat row & column
            if (!passenger.IsValidPass(txtPassName.Text) || cmbSeatRow.SelectedIndex == -1 || checkBtn == null)
            {
                MessageBox.Show("Please enter a valid name and seat", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if plane is full. If full place passeneger on waiting list
            if (seat.IsPlaneFull())
            {
                var msg = MessageBox.Show("The plane is currently full. Would you like to be added to the waiting list?",
                    "Plane Is Full", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (msg == DialogResult.No)
                    return;

                using (var con = new SqlConnection(DBObjects.conString))
                {
                    con.Open();
                    command = new SqlCommand("INSERT INTO PASSENGERS (PassengerName, PassengerOnWaitingList)" +
                        "VALUES (@PassengerName, 1)", con);
                    Methods.AddParameters(command, "PassengerName", txtPassName);
                    command.ExecuteNonQuery(); // dont need reader since we are just inserting into db

                    command = new SqlCommand("INSERT INTO PassengerSeats (PassengerID, SeatID)" +
                        "SELECT MAX(PassengerID), 0 FROM PASSENGERS", con);
                    command.ExecuteNonQuery(); 

                    MessageBox.Show("Passenger " + txtPassName.Text + " was added to the waiting list.",
                    "Waiting List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return;

            }

            //Check if seat is taken if so exit so user can select a different seat
            if (seat.IsSeatAlreadyTaken(cmbSeatRow.SelectedItem.ToString(), checkBtn.Text))
            {
                MessageBox.Show("The seat you selected is already taken. Please select a different seat",
                    "Seat Taken", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            // If all is Ok add passenger and seat selected to db
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();

                //Insert new passenger into db
                command = new SqlCommand("INSERT INTO PASSENGERS (PassengerName, PassengerOnWaitingList)" +
                    "VALUES (@PassengerName, 0)", con);
                Methods.AddParameters(command, "PassengerName", txtPassName);
                command.ExecuteNonQuery();

                //Update seat to taken in seats db
                command = new SqlCommand("Update SEATS SET IsTaken = 1 WHERE " +
                    "SeatRow = @SeatRow AND SeatColumn = @SeatColumn", con);
                Methods.AddParameters(command, "SeatRow", cmbSeatRow);
                Methods.AddParameters(command, "SeatColumn", checkBtn);
                command.ExecuteNonQuery();

                //Insert passenger and seat ID in passengerSeats table
                command = new SqlCommand("INSERT INTO PassengerSeats (SeatID, PassengerID) " +
                    "Select Seats.SeatID, (SELECT MAX(PassengerID) FROM PASSENGERS)" +
                    " FROM SEATS WHERE Seats.SeatRow = @SeatRow AND Seats.SeatColumn = " +
                    "@SeatColumn", con);
                Methods.AddParameters(command, "SeatRow", cmbSeatRow);
                Methods.AddParameters(command, "SeatColumn", checkBtn);
                command.ExecuteNonQuery();

                MessageBox.Show("Passenger has been added.",
                    "Added Passenger", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PopAirplane();

            }
        }

        //Shows All passengers
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();

                // Get all passenger info from all 3 tables using Inner Join
                command = new SqlCommand
                    ("SELECT p.PassengerID as ID, p.PassengerName as Name, s.SeatRow, s.SeatColumn, " +
                    "p.PassengerOnWaitingList as OnWaitingList " +
                    "FROM(Passengers p " +
                    "INNER JOIN PassengerSeats ps ON p.PassengerID = ps.PassengerID) " +
                    "INNER JOIN Seats s ON s.SeatID = ps.SeatID " +
                    "UNION " +
                    "SELECT p.PassengerID, p.PassengerName, NULL, NULL, p.PassengerOnWaitingList " +
                    "FROM Passengers p " +
                    "WHERE p.PassengerOnWaitingList = 1 " +
                    "ORDER BY s.SeatRow, s.SeatColumn", con);

                // Place the result into DataTable and display result in passenger info form
                DataTable dTable = new DataTable();
                dTable.Load(command.ExecuteReader());
                PassengerInfo frm = new PassengerInfo(dTable);
                frm.ShowDialog();

                //repopulate list with updated records
                PopAirplane();
            }
        }

        //Search for a passenger
        private void btnSearchPass_Click(object sender, EventArgs e)
        {
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();
                // make sure search string was enter into the text box
                if (!txtPassName.Text.Trim().Equals(""))
                {
                    //Get all passengers that match the search string getting info from all 3 tables
                    command = new SqlCommand
                        ("SELECT p.PassengerID as ID, p.PassengerName as Name, s.SeatRow, s.SeatColumn, " +
                        "p.PassengerOnWaitingList as OnWaitingList " +
                        "FROM(Passengers p " +
                        "INNER JOIN PassengerSeats ps ON p.PassengerID = ps.PassengerID) " +
                        "INNER JOIN Seats s ON s.SeatID = ps.SeatID " +
                        "WHERE p.PassengerName LIKE @PassengerName " +
                        "UNION " +
                        "SELECT p.PassengerID, p.PassengerName, NULL, NULL, p.PassengerOnWaitingList " +
                        "FROM Passengers p " +
                        "WHERE p.PassengerOnWaitingList = 1 AND p.PassengerName LIKE @PassengerName " +
                        "ORDER BY s.SeatRow, s.SeatColumn", con);
                    command.Parameters.Add(new SqlParameter("PassengerName", "%" + txtPassName.Text + "%"));

                    //Place the result in a DataTable and display in passenger info form 
                    DataTable dTable = new DataTable();
                    dTable.Load(command.ExecuteReader());
                    PassengerInfo frm = new PassengerInfo(dTable);
                    frm.ShowDialog();

                    PopAirplane();
                }
                else
                {
                    MessageBox.Show("Please enter a valid name to search", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        

        //Display the seats
        private void PopAirplane()
        {
            //Clear previous listbox and list of Seats
            lstOutput.Items.Clear();
            seats.Clear();

            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();

                //Select * from seats db read result and create a seat object w/ ID, Row, Column and IsTaken from the reader
                command = new SqlCommand("SELECT * FROM SEATS ORDER BY SEATROW, SEATCOLUMN", con);
                reader = command.ExecuteReader(); // results of command
                while(reader.Read())
                {
                    var seat = new Seat();
                    seat.SeatID = Convert.ToInt32(reader["SEATID"]);
                    seat.SeatRow = Convert.ToInt32(reader["SEATROW"]);
                    seat.SeatColumn = reader["SEATCOLUMN"].ToString();
                    seat.IsSeatTaken = Convert.ToBoolean(reader["ISTAKEN"]);
                    seats.Add(seat); //Add the seat object to the list

                }

                //Display available seats in lstOutput
                var msg = "";
                var counter = 0;
                for(int i = 0; i < seats.Count; i++)
                {
                    counter++;
                    if (seats[i].IsSeatTaken)
                    {
                        msg += "   " + "NA" + "  "; //Displays if seat is taken as NA
                    }
                    else
                    {
                        msg += "   " + seats[i].SeatRow + seats[i].SeatColumn + "   "; // if not taken display row and column
                    }

                    if (counter % 4 == 0)
                    {
                        lstOutput.Items.Add(msg); // Adds the seats to the list box                      
                        msg = "";
                    }
                    else if (counter % 2 == 0)
                    {
                        msg += "     "; //create aisle
                    }
                }
            }
        }

        // Populate the dropdown
        private void PopSeatRows()
        {
            // get row numbers
            using (var con = new SqlConnection(DBObjects.conString))
            {
                con.Open();

                command = new SqlCommand("SELECT DISTINCT SEATROW FROM SEATS ORDER BY SeatRow", con);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cmbSeatRow.Items.Add(reader["SeatRow"]);
                }
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.Show();
        }

        private void frmReservation_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
