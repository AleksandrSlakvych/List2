using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List2
{
    class List
    {
        private object[] _items = new object[0];

        public int Count { get; private set; }

        public int Capacity => _items.Length;

        public List(int capacity)
        {
            Count = capacity;
            _items = new object[Count];
        }

        private bool ThrowIfInvalid(int index)
        {
            if ((index < 0) || (index >= Count))
            {
                return true;
            }
            return false;
        }

        public void Add(object item)
        {
            _items = new object[Count + 1];
            _items[Count] = item;
        }
        private void TryResize()
        {
            Count++;
            if (_items.Length < Count)
            {
                Array.Resize(ref _items, _items.Length == 0 ? 1 : _items.Length * 2);
            }
        }

        private void Insert(int index, object item)
        {
            TryResize();
            for (var i = Count - 1; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            _items[index] = item;
        }

        public object this[int i]
        {
            get
            {
                return _items[i];
            }
            set
            {
                _items[i] = value;
            }
        }

        public int IndexOf(object item)
        {
            int i = 0;
            while ((i < Count) && (!_items[i].Equals(item)))
            {
                i++;
            }
            if (i == Count)
            {
                return -1;
            }
            return i;
        }

        public void RemoveAt(int index)
        {
            for (var i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _items[Count - 1] = default(object);
            Count--;
        }

        public bool Contains(object item)
        {
            bool m = false;
            for (int i = 0; i < Count - 1; i++)
            {
                if (_items[i] == item)
                    m = true;
            }
            return m;
        }

        public void Clear()
        {
            _items = new object[0];
        }

        public void Remove(object item)
        {
            RemoveAt(IndexOf(item));
        }

        public void Reverse()
        {
            _items = _items.Reverse().ToArray();
        }

        public void Reverse2()
        {
            object f = null;
            for (var i = 0; i < _items.Length - 1; i++)
            {
                _items[i] = f;
                _items[i] = _items[_items.Length - i];
                _items[_items.Length - i] = f;

            }
        }

        public object[] ToArray()
        {
            object[] newArr = new object[_items.Length];
            for (int i = 0; i < _items.Length; i++)
            {
                newArr[i] = _items[i];
            }
            return newArr;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _items.Length; i++)
            {
                stringBuilder.AppendLine($"List{i} = {_items[i]}");
            }
            return stringBuilder.ToString();
        }
    }
    public class File
    {
        public string Name;
        public string Size;
        public string Extention;
        public string AdditionalInfo;
        
        public File[] ArrInput(string input)
        {
            string[] arrString = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            File[] arrFiles = new File[arrString.Length];
            int found = 0;
            for (int i = 0; i < arrString.Length; i++)
            {
                if (arrString[i].StartsWith("Text:"))
                {
                    found = arrString[i].IndexOf(":");
                    arrFiles[i] = new TextFiles(arrString[i].Substring(found + 1));
                    Print(arrFiles[i]);
                }
                else if (arrString[i].StartsWith("Image:"))
                {
                    found = arrString[i].IndexOf(":");
                    arrFiles[i] = new Images(arrString[i].Substring(found + 1));
                    Print(arrFiles[i]);
                }
                else if (arrString[i].StartsWith("Movie:"))
                {
                    found = arrString[i].IndexOf(":");
                    arrFiles[i] = new Movies(arrString[i].Substring(found + 1));
                    Print(arrFiles[i]);
                }
            }
            return arrFiles;
        }
        
        protected virtual void Parse(string text)
        {
            string word = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '(')
                {
                    Extention = word;
                    word = "";
                }
                else if (text[i] == ')')
                {
                    Size = word;
                    word = "";
                }
                else if (text[i] == '.')
                {
                    Name = word;
                    word = "";
                }
                else
                {
                    word += text[i];
                }
                AdditionalInfo = word;
            }
        }

        public virtual void Print(File file)
        {
            Console.WriteLine($"{file.Name}.{file.Extention}");
            Console.WriteLine($"           Extension: {file.Extention}");
            Console.WriteLine($"           Extension: {file.Size}");
        }
        
        public virtual File [] SortBySize(File [] files)
        {
            File temp;
            for (int i = 0; i < files.Length; i++)
            {
                for (int j = i + 1; j < files.Length; j++)
                {
                    int m = Convert.ToInt32(files[i].Size);
                    int n = Convert.ToInt32(files[j].Size);
                    if ( m > n)
                    {
                        temp = files[i];
                        files[i] = files[j];
                        files[j] = temp;
                    }
                }
            }
            return files;
        }


        
    }
    public class TextFiles : File
    {
        public string Content;

        public TextFiles(string text)
        {
            Parse(text);
            Content = AdditionalInfo;
        }

        protected override void Parse(string text)
        {
            base.Parse(text);
        }

        public override void Print(File file)
        {
            base.Print(file);
            Console.WriteLine($"           Content: {Content}");
        }
    }
    public class Movies : File
    {
        public string Resolution;
        public string Length;

        public Movies(string text)
        {
            Parse(text);
        }

        protected override void Parse(string text)
        {
            base.Parse(text);
            string word = "";
            for (int i = 0; i < AdditionalInfo.Length; i++)
            {
                
                if (Extention[i] == ';')
                {
                    Resolution = word;
                    word = "";
                }
                else
                {
                    word += text[i];
                }
                Length = word;
            }
        }

        public override void Print(File file)
        {
            base.Print(file);
            Console.WriteLine($"           Resolution: {Resolution}");
            Console.WriteLine($"           Length: {Length}");
        }
    }
    public class Images : File
    {
        public string Resolution;
        public Images(string text)
        {
            Parse(text);
            Resolution = AdditionalInfo;
        }

        protected override void Parse(string text)
        {
            base.Parse(text);
        }

        public override void Print(File file)
        {
            base.Print(file);
            Console.WriteLine($"           Resolution: {Resolution}");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {

            string text = @"        Text: file.txt(6B); Some string content
                                    Image: img.bmp(19MB); 1920õ1080
                                    Text: data.txt(12B); Another string
                                    Text: data1.txt(7B); Yet another string
                                    Movie:logan.2017.mkv(19GB); 1920õ1080; 2h12m";

            File[] files = new File[5];
            files.ArrInput(text);




        }
        
    }
}


