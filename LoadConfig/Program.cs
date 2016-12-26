using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using MyDll;

namespace LoadConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 foo = new Class1();
            Configuration config = foo.Foo();
            string name = config.AppSettings.Settings["name"].Value;//必须使用Settings访问
            Console.WriteLine(name);
            config.AppSettings.Settings["name"].Value = DateTime.Now.ToString();
            config.Save();
            
            Console.Read();

        }

        private Configuration  getConfig()
        {
            //获取调用当前正在执行的方法的方法的 Assembly  
            Assembly assembly = Assembly.GetCallingAssembly();
            string path = string.Format("{0}.config", assembly.Location);

            if (File.Exists(path) == false)
            {
                string msg = string.Format("{0}路径下的文件未找到 ", path);
                throw new FileNotFoundException(msg);
            }

            try
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = path;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

                return config;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }  
    }
}
