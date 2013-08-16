using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DictionaryExtractor
{
    class Program
    {
        private static void Main(string[] args)
        {
            string sourceFile = ConfigurationManager.AppSettings["SourceFile"];
            string targetFile = ConfigurationManager.AppSettings["TargetFile"];
            string extractRegularExpression = ConfigurationManager.AppSettings["ExtractRegularExpression"];

            List<string> words = GetWords(sourceFile, extractRegularExpression);
            WriteWords(targetFile, words);

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static List<string> GetWords(string sourceFile, string extractRegularExpression)
        {
            var words = new List<string>();

            using (TextReader reader = new StreamReader(sourceFile))
            {
                string content = reader.ReadToEnd();
                MatchCollection matchCollection = Regex.Matches(content, extractRegularExpression,
                    RegexOptions.Multiline);
                words.AddRange(from Match match in matchCollection select match.ToString());
            }

            return words;
        }

        private static void WriteWords(string targetFile, List<string> words)
        {
            using (TextWriter writer = new StreamWriter(targetFile, true))
            {
                words.ForEach(writer.WriteLine);
            }
        }
    }
}
