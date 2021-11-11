using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{

    class Manager:Employee
    {
        /// <summary>
        /// Оплата = 15% от всех, но не меньше 1300
        /// </summary
        new public double Salary
        {
            get
            {
                return salary;
            }
            
            set
            {
                
            }
        }
        public Manager(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectsCount,double Salary)
            : base(ID, FirstName, LastName, Age, Department, ProjectsCount)
        {
        }

        public Manager() : base() // this(1, "", "", 1, 1, "", 0)
        {
        }
    }
}
