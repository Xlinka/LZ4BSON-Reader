using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elements.Core; 
using Newtonsoft.Json;

namespace LZ4BsonExporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the input directory path:");
            string inputDirectory = Console.ReadLine();

            Console.WriteLine("Enter the output directory path:");
            string outputDirectory = Console.ReadLine();

            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine("Input directory does not exist.");
                return;
            }

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            List<string> files = Directory.GetFiles(inputDirectory, "*.lz4bson", SearchOption.AllDirectories).ToList();

            if (files.Count == 0)
            {
                Console.WriteLine("No .lz4bson files found in the input directory.");
                return;
            }

            Console.WriteLine($"{files.Count} .lz4bson files found.");

            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                string relativePath = Path.GetRelativePath(inputDirectory, file);
                string outputPathJson = Path.Combine(outputDirectory, Path.ChangeExtension(relativePath, ".json"));
                string outputPath7zBson = Path.Combine(outputDirectory, "importable", Path.ChangeExtension(relativePath, ".7zbson"));
                string outputFileDirectory = Path.GetDirectoryName(outputPathJson);
                string output7zBsonDirectory = Path.GetDirectoryName(outputPath7zBson);

                if (!Directory.Exists(outputFileDirectory))
                {
                    Directory.CreateDirectory(outputFileDirectory);
                }

                if (!Directory.Exists(output7zBsonDirectory))
                {
                    Directory.CreateDirectory(output7zBsonDirectory);
                }

                bool successJson = await ConvertFileToJson(file, outputPathJson);
                bool success7zBson = await ConvertFileTo7zBson(file, outputPath7zBson);

                double progress = ((i + 1) / (double)files.Count) * 100;
                Console.WriteLine($"{file} - {progress:F2}% {(successJson && success7zBson ? "✔" : "✖")}");
            }

            Console.WriteLine("Processing completed.");
        }

        static async Task<bool> ConvertFileToJson(string inputFile, string outputFile)
        {
            try
            {
                DataTreeDictionary dataTree = DataTreeConverter.Load(inputFile);
                await Exporter.ExportToJson(dataTree, outputFile);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"Error converting {inputFile} to JSON: Index was outside the bounds of the array.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting {inputFile} to JSON: {ex.Message}");
                return false;
            }
        }

        static async Task<bool> ConvertFileTo7zBson(string inputFile, string outputFile)
        {
            try
            {
                DataTreeDictionary dataTree = DataTreeConverter.Load(inputFile);
                await Exporter.ExportTo7zBson(dataTree, outputFile);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"Error converting{inputFile} to 7zBSON: Index was outside the bounds of the array.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting {inputFile} to 7zBSON: {ex.Message}");
                return false;
            }
        }
    }
}
