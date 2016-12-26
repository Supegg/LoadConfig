using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MyDll
{
    public class Class1
    {
        public Class1()
        { 
            
        }

        public Configuration Foo()
        {
            return getConfig();
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
