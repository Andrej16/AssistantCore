using AssistantCore.Algorithms;
using AssistantCore.DataSources;
using AssistantCore.EF;
using System;
using System.Linq;

namespace AssistantCore
{
    class Program
    {
        static void Main() 
        {
            MainMethod("ids_parsing");
        }

        ///<param name="region">Takes in the --region option from the code fence options in markdown</param>
        ///<param name="session">Takes in the --session option from the code fence options in markdown</param>
        ///<param name="package">Takes in the --package option from the code fence options in markdown</param>
        ///<param name="project">Takes in the --project option from the code fence options in markdown</param>
        ///<param name="args">Takes in any additional arguments passed in the code fence options in markdown</param>
        ///<see>To learn more see <a href="https://aka.ms/learntdn">our documentation</a></see>
        private static int MainMethod(
            string region,
            string session = null,
            string package = null,
            string project = null,
            string[] args = null)
        {
            return region switch
            {
                "divide-on-patches" => new Groupings().DivideOnPatches(Products.ProductList.Select(p => p.ProductName).ToList(), 3),
                "parse-date-from-string" => new DateTimeTests().ParseStringDate(),
                "ids_parsing" => new StringTests().ParseFromString("/1/2//3/4/"),
                "sqlite-in-memory-test" => new SqliteInMemoryBloggingTest().TestConnection(),
                //"where-indexed" => new Restrictions().IndexedWhere(),

                //"custom-comparer" => session switch
                //{
                //    "orderby-custom" => new Orderings().OrderByWithCustomComparer(),
                //    "orderby-custom-descending" => new Orderings().DescendingCustomComparer(),
                //    "orderby-custom-thenby" => new Orderings().ThenByCustom(),
                //    "orderby-custom-descending-thenby" => new Orderings().CustomThenByDescending(),
                //    _ => MissingTag(session, false),
                //},

                null => RunAll(),
                _ => MissingTag(region),
            };
        }

        private static int MissingTag(string tag, bool region = true)
        {
            Console.WriteLine($"No code snippet configured for {(region ? "region" : "session")}: {tag}");
            return 1;
        }
        private static int RunAll()
        {
            // 1- 5
            //new Restrictions().LowNumbers();
            //new Restrictions().ProductsOutOfStock();
            //new Restrictions().ExpensiveProductsInStock();
            //new Restrictions().DisplayCustomerOrders();
            //new Restrictions().IndexedWhere();

            // 6 - 19 (+ 2 for tuples)
            //new Projections().SelectSyntax();

            return 0;
        }
    }
}
