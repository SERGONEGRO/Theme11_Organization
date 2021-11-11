using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{
    class Worker:Employee
    {
        /// <summary>
        /// Почасовая оплата
        /// </summary>
        new public uint Salary
        {
            get
            {
                byte workingDays = 25; // Рабочих дней в месяце
                byte workingHours = 8; // Рабочих часов в день
                int salaryPerHour = 12; //12$ в час
                return (uint)(workingDays * workingHours * salaryPerHour);
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Age"></param>
        /// <param name="Salary"></param>
        /// <param name="Department"></param>
        /// <param name="ProjectsCount"></param>
        public Worker(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectsCount)
            :base (ID, FirstName, LastName, Age, Department, ProjectsCount)
        {

        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Worker() : base() // this(1, "", "", 1, 1, "", 0)
        {
        }
    }
}
