using System;
using System.Linq;

namespace PropertyManager.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DBContext())
            {
                //context.Database.Initialize(true);

                Console.Write("Run a migration? Y or N: ");
                bool runMigration = Console.ReadLine() == "Y" ? true : false;

                while (runMigration)
                {
                    //var migratorConfig = new ConfigurationV1();
                    //var dbMigrator = new DbMigrator(migratorConfig);

                    //Console.Write("Please provide a migration name: ");
                    //string migrationName = Console.ReadLine();
                    //Console.WriteLine("Running migration " + migrationName);
                    //dbMigrator.Update(migrationName);

                    //Console.WriteLine("Finished.");
                    //Console.Write("Run another migration? Y or N: ");
                    //runMigration = Console.ReadLine() == "Y" ? true : false;

                }



            }
        }
    }
}
