using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{
     // нужен ли этот класс?
    class Director:Manager
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Age"></param>
        /// <param name="Department"></param>
        /// <param name="ProjectsCount"></param>
        public Director(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectsCount)
           : base(ID, FirstName, LastName, Age, Department, ProjectsCount)
        {
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Director() : base()
        {
        }
    }
}
