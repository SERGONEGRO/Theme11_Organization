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

        Department subDepartment;

        /// <summary>
        /// уровень вложенности
        /// </summary>
        int subLevel;


        /// <summary>
        /// номер Департамента
        /// </summary>
        uint depId;

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
        public uint DepId { get { return this.depId; } set { this.depId = value; } }

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
        /// Конструктор 
        /// </summary>
        /// <param name="depNumber">номер департамента</param>
        /// <param name="empCount">количество работников</param>
        public Department(uint depNumber, int empCount,int sl)
        {
            this.subLevel = sl;
            Thread.Sleep(1);   //для разных значений генератора
            Random r = new Random(DateTime.Now.Millisecond);
            this.depId = depNumber;
            this.depName = $"Department № {depNumber}";
            this.depCreationDate = new DateTime(2020, 03, (int)depNumber < 30 ? (int)depNumber : 30) ;
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.employees = new List<Employee>();
            Random rand = new Random();
            
            for (int i = 1; i <= empCount; i++)
            {   
                switch (rand.Next(0,2))   //добавляем в департамент рандомно работяг и интернов
                {
                    case 0:
                        AddWorker(i, depNumber, r.Next(20, 100), r.Next(1, 5));
                        break;
                    case 1:
                        AddIntern(i, depNumber, r.Next(20, 100), r.Next(1, 5));
                        break;
                }
            }
            this.depManager = new Manager((uint)depId, "Манагер" + depId,"Фамилия",(byte)r.Next(20,100),depName,0);
            ManagerSalaryCalculation();

            switch (rand.Next(0, 2))   //добавляем в департамент рандомно subDepartment
            {
                case 0:
                    //this.subLevel++;          //увеличиваем уровень вложенности
                    subDepartment = new Department(depId * 10, rand.Next(5, 8), this.subLevel+1);
                    break;
                case 1:
                    break;
            }
        }

        /// <summary>
        /// Конструктор, собирающий департамент, используется для импорта из XML и JSON
        /// </summary>
        /// <param name="depNumber">ID департамента</param>
        /// <param name="depName">Имя департамента</param>
        /// <param name="depDate">Дата создания</param>
        /// <param name="works">Массив воркеров</param>
        public Department(int sl,int depNumber, string depName, string depDate, string mngr, List<Employee> works,string sb)
        {
            this.subLevel = sl;
            this.depId = (uint)depNumber;
            this.depName = depName;
            this.depCreationDate = DateTime.Parse(depDate);
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.employees = works;
            if(sb!=null)
            this.subDepartment = AddDepartment(sb);

        }

       
        static public Department AddDepartment(string s)
        {
            var item = JObject.Parse(s);
            Department dep = new Department(Convert.ToUInt16(item["SUBLEVEL"]),
                                                 Convert.ToInt32(item["ID"]),
                                                 item["DEPNAME"].ToString(),
                                                 item["CREATIONDATE"].ToString(),
                                                 item["MANAGER"].ToString(),
                                                 GetEmployeeJSON(item["Wo"].ToString()),
                                                 item["SUBDEPARTMENT"].ToString());


            return dep;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавить Работягу
        /// </summary>
        /// <param name="iter">номер работяги</param>
        /// <param name="depnum">номер департмента</param>
        /// <param name="age">возраст</param>
        /// <param name="pc">количество проектов</param>
        public void AddWorker(int iter, uint depnum, int age, int pc)
        {
            employees.Add(
                new Worker(
                (uint)(depnum * 1000 + iter),
                $"Р_Имя_{iter}",
                $"Фамилия_{iter}",
                (byte)age,
                this.depName,
                (byte)pc));
        }

        /// <summary>
        /// Добавить Интерна
        /// </summary>
        /// <param name="iter">номер интерна</param>
        /// <param name="depnum">номер департмента</param>
        /// <param name="age">возраст</param>
        /// <param name="pc">количество проектов</param>
        public void AddIntern(int iter, uint depnum, int age, int pc)
        {
            employees.Add(
                new Intern(
                (uint)(depnum * 1000 + iter),
                $"И_Имя_{iter}",
                $"Фамилия_{iter}",
                (byte)age,
                this.depName,
                (byte)pc));
        }

        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintDepToConsole()
        {
            string addSpace = new string(' ',this.subLevel*5);  //вставляем пробелы, кол-во = уровню вложенности*5
            Console.WriteLine($"\n{addSpace}Департамент № {depId}, Дата создания: {depCreationDate.ToShortDateString()}, SubLevel: {this.subLevel}");
            Console.WriteLine($"{addSpace}Менеджер: {this.depManager.LastName} {this.depManager.FirstName}, зарплата: {this.depManager.Salary}");
            Console.WriteLine($"{addSpace}{titles[0],3} {titles[1],10} {titles[2],20} {titles[3],10} {titles[4],15}  {titles[5],15} {titles[6],10}");
            foreach (var item in employees)
            {
                Console.WriteLine($"{addSpace}{item.Print()}");
            }
            if (this.subDepartment !=null)
            {
                this.subDepartment.PrintDepToConsole();
            }
        }

        /// <summary>
        /// Вычисление зарплаты менеджера департамента
        /// </summary>
        /// <returns>Зарплата менеджера</returns>
        public void ManagerSalaryCalculation()
        {
            double managerSalary = 0;
            foreach (var e in this.employees)
            {
                managerSalary += e.Salary;
            }
            
            managerSalary = managerSalary * 0.15;
            this.depManager.Salary = (uint)(managerSalary) >= 1300 ? managerSalary : 1300;
            //return 
        
        }

        /// <summary>
        /// Подсчёт количества проектов менеджера
        /// </summary>
        /// <returns>Кол-во проектов</returns>
        public byte ManagerProjectsCountCalculation()
        {
            byte managerProjectsCount = 0;
            foreach (var e in this.employees)
            {
                managerProjectsCount = (byte)(managerProjectsCount + e.ProjectsCount);
            }
            return managerProjectsCount;
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

            JObject jDep;
            if (this.subDepartment != null)  //если есть вложенный департмент
            {
                jDep = new JObject
                {
                    ["SUBLEVEL"] = this.subLevel,
                    ["ID"] = this.DepId,
                    ["DEPNAME"] = this.DepName,
                    ["CREATIONDATE"] = this.CreationDate,
                    ["MANAGER"] = this.depManager.LastName + " " + this.depManager.FirstName,
                    ["WORKERS"] = jArray,
                    ["SUBDEPARTMENT"] = this.subDepartment.SerializeDepartmentToJson()
                };
            }
            else                            //Если нет вложенного департмента
            {
                jDep = new JObject
                {
                    ["SUBLEVEL"] = this.subLevel,
                    ["ID"] = this.DepId,
                    ["DEPNAME"] = this.DepName,
                    ["CREATIONDATE"] = this.CreationDate,
                    ["MANAGER"] = this.depManager.FirstName + " " + this.depManager.LastName,
                    ["WORKERS"] = jArray
                };
            }
            return jDep;
        }


        /// <summary>
        /// Распарсивает JSON строку в массив воркеров
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns>массив воркеров</returns>
        public static List<Employee> GetEmployeeJSON(string s)
        {
            var wrks = JObject.Parse(s)["workers"].ToArray();

            List<Employee> workers = new List<Employee>();
            foreach (var item in wrks)
            {
                if (item["TYPE"].ToString() == "Worker")
                {
                    workers.Add(new Worker(Convert.ToUInt32(item["ID"]),
                                           item["FirstName"].ToString(),
                                           item["LastName"].ToString(),
                                           Convert.ToByte(item["Age"]),
                                           item["Department"].ToString(),
                                           Convert.ToByte(item["ProjectCount"])));
                }
                else
                {
                    workers.Add(new Intern(Convert.ToUInt32(item["ID"]),
                                           item["FirstName"].ToString(),
                                           item["LastName"].ToString(),
                                           Convert.ToByte(item["Age"]),
                                           item["Department"].ToString(),
                                           Convert.ToByte(item["ProjectCount"])));
                }
            }
            return workers;
        }
        #endregion
    }
}

