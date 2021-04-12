using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmReservation
{
    public class Passenger
    {
        public bool IsValidPass(string name)
        {
            if (name.Trim().Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
