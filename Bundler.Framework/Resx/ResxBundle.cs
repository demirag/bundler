using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bundler.Framework.Utilities;
using Bundler.Framework.Files;
using System.Resources;
using System.Collections;
using System.IO;

namespace Bundler.Framework.Resx
{
    internal class ResxBundle : BundleBase,IResxBundle, IResxBundleBuilder
    {
        private static Dictionary<string, string> renderedResources = new Dictionary<string, string>();
        private List<string> resxFiles = new List<string>();
        private const string scriptTemplate = "<script type=\"text/javascript\" src=\"{0}\"></script>";


        public ResxBundle(): base(new FileWriterFactory(), new FileReaderFactory(), new DebugStatusReader())
        {

        }
        IResxBundleBuilder IResxBundleBuilder.Add(string resxPath)
        {
            resxFiles.Add(resxPath);
            return this;
        }
        IResxBundleBuilder IResxBundle.Add(string resxPath)
        {
            resxFiles.Add(resxPath);
            return this;
        }

        string IResxBundleBuilder.Render(string renderTo)
        {
            return Render(renderTo, renderTo);
        }

        public void AsNamed(string name, string renderTo)
        {
            Render(renderTo, name);
        }

        private string Render(string renderTo, string key)
        {
            if (!renderedResources.ContainsKey(key))
            {
                lock (renderedResources)
                {
                    if (!renderedResources.ContainsKey(key))
                    {
                        string outputFile = ResolveAppRelativePathToFileSystem(renderTo);
                        string minifiedResource = ProcessResourceInput(resxFiles, outputFile);
                        string hash = Hasher.Create(minifiedResource);
                        string renderedScriptTag = String.Format(scriptTemplate, ExpandAppRelativePath(renderTo) + "?r=" + hash);
                        renderedResources.Add(key, renderedScriptTag);
                    }
                }
            }
            return renderedResources[key];
        }

        private string ProcessResourceInput(List<string> resxFiles, string outputFile)
        {
            StringBuilder sb = new StringBuilder();
            
            // foreach resource file contruct the corresponding javascript object.
            foreach (string resxFilePath in resxFiles)
            {
                sb.Append(SerializeResourceToJson(resxFilePath));
            }

            string serializedResources = sb.ToString();

            WriteFiles(serializedResources, outputFile);

            return serializedResources;
        }

        private string SerializeResourceToJson(string resxFilePath)
        {
            // create a list of string that will hold the serialized Json fields of given type.
            List<string> jsonFields = new List<string>();

            // resolve relative path to fileSystem
            string fileSystemPath = ResolveAppRelativePathToFileSystem(resxFilePath);
           
            // read the content
            var resx = new ResXResourceReader(fileSystemPath);

            // loop through all values
            foreach (DictionaryEntry item in resx)
            {
                // write the json field in a string form -> field: "value"
                jsonFields.Add(item.Key + ":" + "\"" + item.Value.ToString() + "\"");
            }
                       

            string result = "var " + Path.GetFileNameWithoutExtension(fileSystemPath) + "={";

            // separate properties with a comma
            result += string.Join(",", jsonFields.ToArray());

            result += "};";

            // return the serialized json string
            return result;


        }
    }
}
