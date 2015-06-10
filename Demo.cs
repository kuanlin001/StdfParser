using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StdfLib;

namespace StdfReaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string testFile1 = @"P:\stdf\test1.stdf.gz";
            string testFile2 = @"P:\stdf\test2.std";


            //*****************Demo1: read Mir only.  abort parsing after Mir********************
            var reader = new StdfReader(testFile1);
            reader.SetRecOfInterest(new string[] { "Mir" });

            bool foundMir = false;
            reader.onRecordProcessed = new StdfReader.recordProcessedDelegate((recName, recField, recDataType, recData) =>
            {
                if (recData == null)
                    recData = "no data";
                Console.WriteLine(recName + "|" + recField + "[" + recDataType + "] -->" + recData.ToString());
                if (recName == "Mir")
                    foundMir = true;
            });

            reader.abordProcess = new StdfReader.abortParseDelegate(() =>
            {
                if (foundMir)
                    Console.WriteLine("Mir record printed, aborting parsing...");
                return foundMir;
            });

            reader.parseFile();


            //**************Demo2: read headers only.  display unique header names*****************
            Console.WriteLine("-------------------------------------------------------------------");
            reader.ClearSettings();
            reader.InputFile = testFile2;
            var headerNames = new List<string>();
            reader.onHeaderProcessed = new StdfReader.headerProcessedDelegate((name, type, subtype, length) =>
            {
                if (!headerNames.Contains(name))
                    headerNames.Add(name);
            });

            reader.parseFile(true);
            foreach (string name in headerNames)
                Console.WriteLine(name);

            Console.WriteLine("press any key to continue");
            Console.ReadLine();
        }
    }
}
