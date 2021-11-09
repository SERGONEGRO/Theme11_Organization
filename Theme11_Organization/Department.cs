﻿using System;
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
        public List<Worker> workers;

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
            this.workers = new List<Worker>();

            for (int i = 1; i <= empCount; i++)
            {
                workers.Add(
                    new Worker(
                        (uint)(depNumber * 1000 + i),
                        $"Имя_{i}",
                        $"Фамилия_{i}",
                        (byte)r.Next(20, 100),
                        (uint)r.Next(10, 20) * 1000,
                        this.depName,
                        (byte)r.Next(1, 5)));
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
        public Department(int depNumber, string depName, string depDate, List<Worker> works, Manager depMan)
        {
            this.depId = depNumber;
            this.depName = depName;
            this.depCreationDate = DateTime.Parse(depDate);
            this.titles = new string[7] { "id", "Имя", "Фамилия", "Возраст", "Департамент", "Зарплата", "Проектов", };
            this.workers = works;
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
            foreach (var item in workers)
            {
                Console.WriteLine(item.Print());
            }
        }

        /// <summary>
        /// Распарсивает JSON строку в массив воркеров
        /// </summary>
        /// <param name="s">Строка</param>
        /// <returns>массив воркеров</returns>
        public static List<Worker> GetWorkersJSON(string s)
        {
            var wrks = JObject.Parse(s)["workers"].ToArray();

            List<Worker> workers = new List<Worker>();
            foreach (var item in wrks)
            {
                workers.Add(new Worker(Convert.ToUInt32(item["ID"]),
                                       item["FirstName"].ToString(),
                                       item["LastName"].ToString(),
                                       Convert.ToByte(item["Age"]),
                                       Convert.ToUInt32(item["Salary"]),
                                       item["Department"].ToString(),
                                       Convert.ToByte(item["ProjectCount"])));
            }
            return workers;
        }

        /// <summary>
        /// Сортировка по возрасту
        /// </summary>
        public void OrderDepartmentByAge()
        {
            List<Worker> result = this.workers;

            result.Sort((b1, b2) => string.Compare(b1.Age.ToString(), b2.Age.ToString()));
            this.workers = result;

        }

        /// <summary>
        /// сортировка по зарплате
        /// </summary>
        public void OrderDepartmentBySalary()
        {
            List<Worker> result = this.workers;

            result.Sort((b1, b2) => string.Compare(b1.Salary.ToString(), b2.Salary.ToString()));
            this.workers = result;

        }

    
        /// <summary>
        /// Департмент в Json
        /// </summary>
        /// <returns>объект JObject</returns>
        public JObject SerializeDepartmentToJson()
        {

            JArray jArray = new JArray();
            foreach (var w in this.workers)
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
        #endregion
    }
}

