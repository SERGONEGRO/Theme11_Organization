﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{
    class Director:Manager
    {
        public Director(uint ID, string FirstName, string LastName, byte Age, uint Salary, string Department, byte ProjectsCount)
           : base(ID, FirstName, LastName, Age, Salary, Department, ProjectsCount)
        {
        }

        public Director() : base() // this(1, "", "", 1, 1, "", 0)
        {
        }
    }
}
