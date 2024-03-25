using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace EasyBillingService
{
    public static class ApplicationConfiguration
    {
        private static ConfigurationFile _configurationFile;
        private const string filepath = "..\\..\\configuration.cfg";
        
        public static ConfigurationFile ConfigurationFile
        {
            get
            {
                if (_configurationFile == null)
                {
                    return InitializeConfigurationFile();
                }
                else
                {
                    return _configurationFile;
                }
            }
        }

        private static ConfigurationFile InitializeConfigurationFile()
        {
            var config = LoadConfigurationFile();
          
            _configurationFile = config;
                    
            return _configurationFile;
        }

        
        public static void SaveConfigurationFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationFile));
            
            using (TextWriter textWriter = new StreamWriter(filepath))
            {
                if (_configurationFile == null)
                {
                    var config = new ConfigurationFile();
                    serializer.Serialize(textWriter,config);
                }
                else
                {
                    serializer.Serialize(textWriter,_configurationFile);
                }
                
            }
        }

        private static ConfigurationFile LoadConfigurationFile()
        {
            var exists = File.Exists(filepath);
            
            if (exists)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationFile));
                using (var stream = new FileStream(filepath, FileMode.Open))
                {
                    var config =(ConfigurationFile) serializer.Deserialize(stream);
                    if (config != null)
                    {
                        return config;
                    }
                    
                    stream.EndRead(null);
                    stream.Close();
                }
                serializer = null;
                return null;
            }
            
            return new ConfigurationFile();
            
        }
    }

    public class ConfigurationFile
    {
        
        private string _templatePath;
        private string _billingBookPath;
        private string _billingPath;
        private bool _initBB= true;
        private bool _initT = true;
        private bool _initB = true;

        public string BillingBookPath
        {
            get => _billingBookPath;
            set
            {
                _billingBookPath = value;
                
                if (!_initBB)
                {
                    ApplicationConfiguration.SaveConfigurationFile();
                }
                _initBB = false;
            }
        }
        
        public string TemplatePath
        {
            get => _templatePath;
            set
            {
                _templatePath = value;
                if (!_initT)
                {
                    ApplicationConfiguration.SaveConfigurationFile();
                }
                _initT = false;
            }
        }
        
        public string BillingPath
        {
            get => _billingPath;
            set
            {
                _billingPath = value;
                if (!_initB)
                {
                    ApplicationConfiguration.SaveConfigurationFile();
                }
                _initB = false;
            }
        }
    }
}