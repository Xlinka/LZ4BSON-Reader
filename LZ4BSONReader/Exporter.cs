using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Elements.Core;

namespace LZ4BsonExporter
{
    public static class Exporter
    {
        public static async Task ExportToJson(DataTreeDictionary root, string targetFile)
        {
            await Task.Run(() =>
            {
                using (StreamWriter file = File.CreateText(targetFile))
                {
                    JsonSerializer serializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented
                    };
                    serializer.Serialize(file, root);
                }
            });
        }

        public static async Task ExportTo7zBson(DataTreeDictionary root, string targetFile)
        {
            await Task.Run(() =>
            {
                using (FileStream fileStream = File.Create(targetFile))
                {
                    DataTreeConverter.To7zBSON(root, fileStream);
                }
            });
        }
    }
}