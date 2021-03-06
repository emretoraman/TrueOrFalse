using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TrueOrFalse.Models
{
    public interface IPersistence
    {
        Statement this[int index] { get; }
        int Count { get; }
        List<Statement> List { get; }
        string FileName { get; }
        void Add(Statement statement);
        void Remove(int index);
        void New();
        void Save(string fileName);
        void Load(string fileName);
        void Change(int index, Statement statement);
        bool Exists(int index);
    }

    public class Persistence : IPersistence
    {
        private List<Statement> _list = new();

        private string _fileName;

        public Statement this[int index] => _list[index];

        public int Count => _list.Count;

        public List<Statement> List => _list;

        public string FileName => _fileName;

        public void Add(Statement statement)
        {
            _list.Add(statement);
        }

        public void Remove(int index)
        {
            _list.RemoveAt(index);
        }

        public void New()
        {
            _list = new();
            _fileName = null;
        }

        public void Save(string fileName)
        {
            _fileName = fileName;
            XmlSerializer xmlSerializer = new(typeof(List<Statement>));
            FileStream fileStream = new(_fileName, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fileStream, _list);
            fileStream.Close();
        }

        public void Load(string fileName)
        {
            _fileName = fileName;
            XmlSerializer xmlSerializer = new(typeof(List<Statement>));
            FileStream fileStream = new(_fileName, FileMode.Open, FileAccess.Read);
            _list = (List<Statement>)xmlSerializer.Deserialize(fileStream);
            fileStream.Close();
        }

        public void Change(int index, Statement statement)
        {
            _list[index] = statement;
        }

        public bool Exists(int index)
        {
            return _list.Count > index;
        }
    }
}
