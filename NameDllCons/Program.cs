using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;




namespace NameDllCons
{

    class Program
    {
        private string path;

        string Ofd()
        {
            Console.WriteLine("Программа просмотра классов и методов в dll\r\nДля продолжения нажмите любую клавишу");
            Console.ReadKey(true);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "dll files (*.dll)|*.dll";
            ofd.FilterIndex = 1;            
            ofd.ShowDialog();
            //string path ;
            return path = ofd.FileName;
        }


        [STAThread]
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Ofd();
            string dir = Path.GetDirectoryName(program.path);
            string file = Path.GetFileName(program.path);

            Console.WriteLine(program.path);
            Console.WriteLine(string.Format("Входные данные: {0}", dir));
            Console.WriteLine(string.Format("Имя сборки: {0}", file));
            try
            {
                var assembly = Assembly.LoadFile(program.path);
                var typeAssem = assembly.GetType();

                MethodInfo[] methods = typeAssem.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);               
                Console.WriteLine("Результат работы функции: ");
                 
                foreach (MethodInfo methodDt in methods)
                {
                    var classMethod = methodDt.DeclaringType.Name;
                    Console.WriteLine(classMethod);
                    foreach (MethodInfo method in methods)
                    {
                        if (method.IsPublic)
                        {
                            Console.WriteLine(string.Format("- {0} (PUBLIC)", method.Name));
                        }
                        /*if (method.IsPrivate)
                        {
                            Console.WriteLine(string.Format("- {0} (PRIVATE)", method.Name));
                        }*/
                        if (method.IsFamily)
                        {
                            Console.WriteLine(string.Format("- {0} (PROTECTED)", method.Name));
                        }
                    }
                }

            }
            catch (BadImageFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey(true);
        }
    }
}
