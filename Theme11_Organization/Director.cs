﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{
    class Director:Manager
    {
        public Director(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectsCount,double Salary)
           : base(ID, FirstName, LastName, Age, Department, ProjectsCount, Salary)
        {
        }

        public Director() : base() // this(1, "", "", 1, 1, "", 0)
        {
        }
    }
}
