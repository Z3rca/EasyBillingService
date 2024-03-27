using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace EasyBillingService
{
    // provides faster access to Files already known in the system. 
    public class DataCacheManager
    {
        public DataCacheManager()
        {
            LoadCacheFile();
        }
        private const string filepath = "..\\..\\cacheData.dat";
        private static Dictionary<double, CacheEntry> _cacheFile = new Dictionary<double, CacheEntry>();
        
        public string GetDataPath(double address)
        {
            if(_cacheFile.ContainsKey(address))
            {
                return _cacheFile[address].Path;
            }

            return "";
        }
        
        public void EnterAdress(double address,string path)
        {
            if (File.Exists(path))
            {
                var lastModifiedDateTime = File.GetLastWriteTime(path);
                CacheEntry entry = new CacheEntry(address, path, lastModifiedDateTime);
                if (!_cacheFile.ContainsKey(address))
                {
                    _cacheFile.Add(entry.Address,entry);
                }
                else
                {
                    _cacheFile[address] = entry;
                }
                
            }
            SaveCacheFile();
        }

        private void LoadCacheFile()
        {
            var exists = File.Exists(filepath);
            
            if (exists)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CacheEntry[]));
                using (var stream = new FileStream(filepath, FileMode.Open))
                {
                    var cacheFile = (CacheEntry[])serializer.Deserialize(stream);

                    foreach (var entry in cacheFile)
                    {
                        if (!_cacheFile.ContainsKey(entry.Address))
                        {
                            _cacheFile.Add(entry.Address, entry);
                        }
                        
                    }
                }
            }
            
        }

        private void SaveCacheFile()
        {
            //TODO binary data serialization
            var data = _cacheFile.Values.ToArray();
            XmlSerializer serializer = new XmlSerializer(typeof(CacheEntry[]));
            using (TextWriter textWriter = new StreamWriter(filepath))
            {
                serializer.Serialize(textWriter,data);
            }
        }

        public DateTime? GetLastModified(double address)
        {
            if(_cacheFile.ContainsKey(address))
            {
                return _cacheFile[address].LastModified;
            }

            return null;
        }
    }
    
    
    
    public class CacheEntry
    {
        public CacheEntry()
        {
            
        }
        public CacheEntry(double address, string path, DateTime dateTime)
        {
            Path = path;
            Address = address;
            LastModified = dateTime;
        }
        public double Address;
        public string Path;
        public DateTime LastModified;
    }

    public class CacheFile
    {
        [XmlElement]
        private CacheEntry[] CacheEntries;
    }
}