using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme11_Organization
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int depsIndex = 3;                      //количество департаментов

            List<Department> deps = new List<Department>();            //заполняем департаменты
            for (uint i = 0; i < depsIndex; i++)
            {
                deps.Add(new Department(i + 1, rand.Next(5, 8)));
            }

            for (int i = 0; i < deps.Count; i++)
            {
                deps[i].PrintDepToConsole();
            }
            Console.ReadKey();
        }
    }
}
