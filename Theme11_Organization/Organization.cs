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
        public Organization()
        {
            for (uint i = 0; i < depsIndex; i++)
            {
                deps.Add(new Department(i + 1, rand.Next(5, 8), 0));
            }
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
        /// Экспорт в json - доработать
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
    }
}
