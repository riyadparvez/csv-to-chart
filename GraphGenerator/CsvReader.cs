using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphGenerator
{
    public class CsvReader : IEnumerable<List<string>>
    {
        private readonly string filePath;
        private List<string> lines;
        private List<List<string>> tokens;
        private List<string> headers;


        public string FilePath
        {
            get { return filePath; }
        }
        public IList<string> Header
        {
            get { return headers.AsReadOnly(); }
        }
        public IList<string> AllRows
        {
            get { return lines.AsReadOnly(); }
        }
        public IList<List<string>> DataRows
        {
            get { return tokens.AsReadOnly(); }
        }


        private CsvReader(string filePath)
        {
            this.filePath = filePath;
        }


        private void Read()
        {
            lines = File.ReadAllLines(filePath).ToList();
            headers = lines
                      .Take(1)
                      .Single()
                      .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                      .ToList();
            tokens = lines
                     .Skip(1)
                     .Select(l => l.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                   .ToList())
                     .ToList();

        }


        public IEnumerator GetEnumerator()
        {
            return tokens.GetEnumerator();
        }


        IEnumerator<List<string>> IEnumerable<List<string>>.GetEnumerator()
        {
            return tokens.GetEnumerator();
        }


        public static class Builder
        {
            public static CsvReader CreateInstance(string filePath)
            {
                if (!File.Exists(filePath))
                {
                    throw new ArgumentException();
                }
                CsvReader reader = new CsvReader(filePath);
                reader.Read();
                return reader;
            }
        }
    }
}
