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
            /* Создаем организацию, Печатаем, экспортируем. Потом импортируем и снова печатаем для наглядности*/

            //создание организации автоматической генерацией:
            Organization TheBestCoders = new Organization(5);

            //Создание организации чтением из файла:
            //Organization TheBestCoders = Organization.JsonToOrganization();

            
            TheBestCoders.PrintOrganizationToConsole();
            

            //Экспорт в JSON
            TheBestCoders.OrganizationToJSON();
            Console.WriteLine("Экспорт завершен!\n");

            //Чтение из файла
            TheBestCoders = Organization.JsonToOrganization();

            TheBestCoders.PrintOrganizationToConsole();
           


        }

       
    }
}
