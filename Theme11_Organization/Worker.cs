using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{
    class Worker:Employee
    {
        public Worker(uint ID, string FirstName, string LastName, byte Age, uint Salary, string Department, byte ProjectsCount)
            :base (ID, FirstName, LastName, Age, Salary, Department, ProjectsCount)
        {
        }

        public Worker() : base() // this(1, "", "", 1, 1, "", 0)
        {
        }
    }
}
