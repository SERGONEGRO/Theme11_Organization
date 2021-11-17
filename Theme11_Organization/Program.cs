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
            //Organization TheBestCoders = new Organization(5);
            Organization TheBestCoders = Organization.JsonToOrganization();
            
            TheBestCoders.PrintOrganizationToConsole();

            //TheBestCoders.OrganizationToJSON();
            //Console.WriteLine("Экспорт завершен!\n");

        }

       
    }
}
