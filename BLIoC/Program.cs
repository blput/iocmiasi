using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Autofac.Configuration;
using System.IO;

namespace BLIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                #region read values
                Console.WriteLine("First argument:");
                int a = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Operator:");
                String operationOperator = Console.ReadLine();

                Console.WriteLine("Second argument:");
                int b = Convert.ToInt32(Console.ReadLine());
                #endregion

                using (var container = BootStrap.Components())
                {
                    var messenger = container.ResolveKeyed<IOperation>(operationOperator);
                    messenger.oper(a, b);
                }
                Console.WriteLine("[exit to break, enter to continue]");
                string w = Console.ReadLine();
                if(w == "exit")
                {
                    break;
                }
            }
        }
    }
    public interface IOperation
    {
        void oper(int a, int b);
    }

    public class AddOperation : IOperation
    {
        public void oper(int a, int b)
        {
            Console.WriteLine(a.ToString() + " + " + b.ToString() + " is: " + (a + b).ToString());
        }
    }

    public class SubtractOperation : IOperation
    {
        public void oper(int a, int b)
        {
            Console.WriteLine(a.ToString() + " - " + b.ToString() + " is: " + (a - b).ToString());
        }
    }

    public class MultipleOperation : IOperation
    {
        public void oper(int a, int b)
        {
            Console.WriteLine(a.ToString() + " * " + b.ToString() + " is: " + (a * b).ToString());
        }
    }

    public class DivideOperation : IOperation
    {
        public void oper(int a, int b)
        {
            Console.WriteLine(a.ToString() + " / " + b.ToString() + " is: " + (a / b).ToString());
        }
    }

    public class BootStrap
    {
        public static IContainer Components()
        {
            var config = new ConfigurationBuilder();
            string p = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            config.AddJsonFile(p + "\\autofac.json");

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            return builder.Build();
        }
    }
}
