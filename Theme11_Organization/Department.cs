using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Theme11_Organization
{
    class Department
    {
        #region Поля

        /// <summary>
        /// номер Департамента
        /// </summary>
        int depId;

        /// <summary>
        /// Название
        /// </summary>
        private string depName;

        /// <summary>
        /// Дата создания
        /// </summary>
        private DateTime depCreationDate;

        ///// <summary>
        ///// Количество сотрудников в департаменте
        ///// </summary>
        //private uint workersCount;

        /// <summary>
        /// Массив с работниками
        /// </summary>
        public List<Employee> employees;

        ///// <summary>
        ///// Массив с интернами
        ///// </summary>
        //public List<Intern> interns;

        /// <summary>
        /// Заголовки
        /// </summary>
        string[] titles;

        /// <summary>
        /// Менеджер, закрепленный за департаментом
        /// </summary>
        public Manager depManager;

        #endregion

        #region Свойства

        ///// <summary>
        ///// Индекс
        ///// </summary>
        //public int Index { get { return this.index; } set { this.index = value; } }

        /// <summary>
        /// Название
        /// </summary>
        public string DepName { get { return this.depName; } set { this.depName = value; } }

        /// <summary>
        /// id
        /// </summary>
        public int DepId { get { return this.depId; } set { this.depId = value; } }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get { return this.depCreationDate; } set { this.depCreationDate = value; } }

        ///// <summary>
        ///// Количество работников
        ///// </summary>
        //public int WorkersCount { get { return this.index; } }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Автоматической генерации
        /// </summary>
        /// <param name="depNumber">номер департамента</param>
        /// <param name="empCount">количество работников</param>
        public Department(int depNumber, int empCount)
        {
            Thread.Sleep(1);   //для разных значений генератора
            Random r = new Random(DateTime.Now.Millisecond);
            this.depId = depNumber;
            this.depName = $"Department № {depNumber}";
            this.depCreationDate = new DateTime(2020, 03, depNumber);
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.employees = new List<Employee>();
            Random rand = new Random();
            for (int i = 1; i <= empCount; i++)
            {   
                switch (rand.Next(0,2))   //добавляем в департамент рандомно работяг и интернов
                {
                    case 0:
                        employees.Add(
                             new Worker(
                            (uint)(depNumber * 1000 + i),
                             $"Р_Имя_{i}",
                             $"Фамилия_{i}",
                             (byte)r.Next(20, 100),
                            this.depName,
                            (byte)r.Next(1, 5)));
                        break;

                    case 1:
                        employees.Add(
                             new Intern(
                            (uint)(depNumber * 1000 + i),
                             $"И_Имя_{i}",
                             $"Фамилия_{i}",
                             (byte)r.Next(20, 100),
                            this.depName,
                            (byte)r.Next(1, 5)));
                        break;
                }
               
            }
            this.depManager = new Manager();

        }

        /// <summary>
        /// Конструктор, собирающий департамент, используется для импорта из XML и JSON
        /// </summary>
        /// <param name="depNumber">ID департамента</param>
        /// <param name="depName">Имя департамента</param>
        /// <param name="depDate">Дата создания</param>
        /// <param name="works">Массив воркеров</param>
        public Department(int depNumber, string depName, string depDate, List<Employee> emps, Manager depMan)
        {
            this.depId = depNumber;
            this.depName = depName;
            this.depCreationDate = DateTime.Parse(depDate);
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.employees = emps;
            this.depManager = depMan;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintDepToConsole()
        {
            Console.WriteLine($"\nДепартамент № {depId}, Дата создания: {depCreationDate.ToShortDateString()}, менеджер: {depManager}");
            Console.WriteLine($"{titles[0],3} {titles[1],10} {titles[2],20} {titles[3],10} {titles[4],15}  {titles[5],15} {titles[6],10}");
            foreach (var item in employees)
            {
                Console.WriteLine(item.Print());
            }
        }

        ///// <summary>
        ///// Распарсивает JSON строку в массив воркеров
        ///// </summary>
        ///// <param name="s">Строка</param>
        ///// <returns>массив воркеров</returns>
        //public static List<Employee> GetEmployeesJSON(string s)
        //{
        //    var emps = JObject.Parse(s)["employee"].ToArray();

        //    List<Employee> employees = new List<Employee>();
        //    foreach (var item in emps)
        //    {
        //        employees.Add(new Employee(Convert.ToUInt32(item["ID"]),
        //                               item["FirstName"].ToString(),
        //                               item["LastName"].ToString(),
        //                               Convert.ToByte(item["Age"]),
        //                               Convert.ToUInt32(item["Salary"]),
        //                               item["Department"].ToString(),
        //                               Convert.ToByte(item["ProjectCount"])));
        //    }
        //    return workers;
        //}

        /// <summary>
        /// Сортировка по возрасту
        /// </summary>
        public void OrderDepartmentByAge()
        {
            List<Employee> result = this.employees;

            result.Sort((b1, b2) => string.Compare(b1.Age.ToString(), b2.Age.ToString()));
            this.employees = result;

        }

        /// <summary>
        /// сортировка по зарплате
        /// </summary>
        public void OrderDepartmentBySalary()
        {
            List<Employee> result = this.employees;

            result.Sort((b1, b2) => string.Compare(b1.Salary.ToString(), b2.Salary.ToString()));
            this.employees = result;

        }

    
        /// <summary>
        /// Департмент в Json
        /// </summary>
        /// <returns>объект JObject</returns>
        public JObject SerializeDepartmentToJson()
        {

            JArray jArray = new JArray();
            foreach (var w in this.employees)
            {
                JObject obj = w.SerializeEmployeeToJson();

                jArray.Add(obj);
            }

            JObject jDep = new JObject
            {
                ["ID"] = this.DepId,
                ["depName"] = this.DepName,
                ["creationDate"] = this.CreationDate,
                ["manager"] = this.depManager.FirstName + this.depManager.LastName
            };
            jDep["workers"] = jArray;


            return jDep;
        }

        //public int ManagerSalaryCalculation()
        //{
        //    foreach (e Employee in wor)
        //}
        #endregion
    }
}

