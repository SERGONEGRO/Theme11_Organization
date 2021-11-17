using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Theme11_Organization
{
    class Organization
    {
        #region поля

        Random rand = new Random();
        int depsIndex = 5;                      //количество департаментов
        List<Department> deps = new List<Department>();    

        #endregion

        /// <summary>
        /// конструктор
        /// </summary>
        public Organization(int depsCount)
        {
            for (uint i = 0; i < depsCount; i++)
            {
                deps.Add(new Department(i + 1, rand.Next(5, 8), 0));
            }
        }
        public Organization()
        {
        }



        /// <summary>
        /// Печать в консоль
        /// </summary>
        public void PrintOrganizationToConsole()
        {
            for (int i = 0; i < deps.Count; i++)
            {
                deps[i].PrintDepToConsole();
            }
            Console.ReadKey();
        }


        /// <summary>
        /// Экспорт в json
        /// </summary>
        public void OrganizationToJSON()
        {
            JObject mainTree = new JObject();
            JArray jArray = new JArray();

            mainTree["Organization"] = "My Organization";
            mainTree["Departments"] = jArray;
            

            foreach (var dep in deps)
            {
                JObject obj = dep.SerializeDepartmentToJson();

                jArray.Add(obj);
            }

            string json = mainTree.ToString();
            File.WriteAllText("OrganizationExport.json", json);
        }


        /// <summary>
        /// Импорт из JSON
        /// </summary>
        static public Organization JsonToOrganization()
        {
            Organization org = new Organization();
            org.deps.Clear();
            org.depsIndex = 0;

            string json = System.IO.File.ReadAllText("OrganizationExport.json");

            var departments = JObject.Parse(json)["Departments"].ToArray();

            foreach (var item in departments)
            {
                org.deps.Add(Department.AddDepartment(item.ToString()));
            }
            return org;
        }
    }
}
