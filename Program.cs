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
                var files = Directory.GetFiles(folderIn, "*.txt");
                CreateIndex(fileOut, files);
                ProcessFiles(fileOut, files);
            }
        }

        private static void CreateIndex(StreamWriter fileOut, string[] files)
        {
            fileOut.WriteLine($"<div id=\"index\">");
            foreach (var pathIn in files)
            {
                using var fileIn = new StreamReader(pathIn);
                int fileCounter = 0;
                var allText = fileIn.ReadToEnd();
                var document = new MarkdownDocument();

                document.Parse(allText);
                foreach (var element in document.Blocks)
                {
                    if ((element is HeaderBlock headerBlock) && (headerBlock.HeaderLevel == 1))
                    {
                            fileCounter++;
                            fileCounter.ToString();
                            fileOut.WriteLine($"<p id=\"headerline\"><a href=\"#{fileCounter}\">header>{element.ToString()}</a></p>");
                    }
                }

            }  
            fileOut.WriteLine($"</div>");
        }
        
        private static void ProcessFiles(StreamWriter fileOut, string[] files)
        {
            foreach (var pathIn in files)
            {
                using var fileIn = new StreamReader(pathIn);
                int fileCounter = 0;
                var allText = fileIn.ReadToEnd();
                var document = new MarkdownDocument();
                document.Parse(allText);
                
                foreach (var element in document.Blocks)
                {
                    if (element is HeaderBlock headerBlock)
                    {
                        var headerLevel = headerBlock.HeaderLevel;
                        fileCounter++;
                        fileCounter.ToString();
                        fileOut.WriteLine($"<h{headerLevel} id=\"{fileCounter}\">{element.ToString()}</h{headerLevel}> ");
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
