using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ContentPortApi.Helpers;
using Tridion.ContentManager.ImportExport;

namespace ContentPortApi
{
    public partial class ExportConfig
    {
        private string _configFile;

        public ExportConfig()
        {
                
        }
        public ExportConfig(string configFile)
        {
            if (File.Exists(configFile))
            {
                _configFile = configFile;
            }
            else
            {
                string localFile = DetectLocalConfigFile(configFile);
                if (localFile == null)
                {
                    throw new ArgumentException("Could not find config file " + configFile);
                }
                else
                {
                    _configFile = localFile;
                }
            }
        }

        private string DetectLocalConfigFile(string configFilename)
        {
            String filePath = Assembly.GetExecutingAssembly().CodeBase;
            filePath = filePath.Replace("file:///", String.Empty);
            filePath = filePath.Replace("/", "\\");

            string localFile = string.Format("{0}\\{1}", Path.GetDirectoryName(filePath),
                Path.GetFileName(configFilename));
            if (File.Exists(localFile))
            {
                return localFile;
            }
            return null;
        }

        protected string GetConfigFileName()
        {
            return Path.Combine("", _configFile);
        }

        public ExportConfig Read()
        {
            // Nice trick: Paste XML special
            // http://stackoverflow.com/questions/3187444/convert-xml-string-to-object
            string filePath = GetConfigFileName();
            string xml = File.ReadAllText(filePath);

            var catalog1 = xml.ParseXML<ExportConfig>();
            return catalog1;
        }
    }

    public partial class ExportConfigGeneral
    {
        public LogLevel LogLevelAsEnum()
        {
            if ("normal".Equals(this.LogLevel, StringComparison.InvariantCulture))
            {
                return Tridion.ContentManager.ImportExport.LogLevel.Normal;
            }
            if ("debug".Equals(this.LogLevel, StringComparison.InvariantCulture))
            {
                return Tridion.ContentManager.ImportExport.LogLevel.Debug;
            }
            return Tridion.ContentManager.ImportExport.LogLevel.None;
        }

        public BluePrintMode BluePrintModeAsEnum()
        {
            if ("ExportSharedItemsFromOwningPublication".Equals(this.BluePrintMode, StringComparison.InvariantCulture))
            {
                return Tridion.ContentManager.ImportExport.BluePrintMode.ExportSharedItemsFromOwningPublication;
            }
            return Tridion.ContentManager.ImportExport.BluePrintMode.ExportSharedItemsAsShared;
        }
    }

    namespace Helpers
    {
        internal static class ParseHelpers
        {
            public static Stream ToStream(this string @this)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                writer.Write(@this);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }


            public static T ParseXML<T>(this string @this) where T : class
            {
                var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
                return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
            }

        }
    }

}
