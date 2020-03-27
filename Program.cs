using System;
using System.IO;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace MarkDownBlog
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var pathOut = "./output.html";
            var folderIn = "./";

            using (var fileOut = new StreamWriter(pathOut))
            {
                fileOut.Write("PreCSSS");

                var files = Directory.GetFiles(folderIn, "*.txt");
                ProcessFiles(fileOut, files);
            }
        }

        private static void ProcessFiles(StreamWriter fileOut, string[] files)
        {
            foreach (var pathIn in files)
            {
                using (var fileIn = new StreamReader(pathIn))
                {
                    var allText = fileIn.ReadToEnd();
                    var document = new MarkdownDocument();

                    document.Parse(allText);
                    foreach (var element in document.Blocks)
                    {
                        if (element is HeaderBlock headerBlock)
                        {
                            var headerLevel = headerBlock.HeaderLevel;
                            fileOut.WriteLine($"<h{headerLevel}>{element.ToString()}</h{headerLevel}> ");
                        }
                        else if (element is ParagraphBlock)
                        {
                            fileOut.WriteLine($"<p>{element.ToString()}</p>");
                        }
                    }
                }
            }
        }
    }
}
