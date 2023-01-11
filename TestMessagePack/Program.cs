using MessagePack;
using System.Formats.Asn1;
using static TestMessagePack.ClassB;

namespace TestMessagePack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassA classA1 = new ClassA()
            {
                PropertyA = 1,
                PropertyB = 2,
                PropertyC = 3.5m,
                PropertyD = 4.7m,
                PropertyE = "EEEE",
                PropertyF = Enum1.Value1,
                PropertyG = Enum1.None,
                PropertyH = DateTime.Now,
                PropertyI = DateTime.Now.AddDays(1),
                PropertyJ = new TimeSpan(9, 0, 0),
                PropertyK = new TimeSpan(13, 30, 0),
                PropertyL = new PropertyClass1()
                {
                    Code = "1111",
                    Name = "2222"
                }
            };

            var to1 = MessagePackSerializer.Serialize(classA1);
            Console.WriteLine(to1);
            var from1 = MessagePackSerializer.Deserialize<ClassA>(to1);
            Console.WriteLine(from1);
            var from2 = MessagePackSerializer.Deserialize<IInterface1>(to1);
            Console.WriteLine(from2);

            ClassB classB1 = new ClassB()
            {
                PropertyA = 1,
                PropertyB = 2,
                PropertyC = 3.5m,
                PropertyD = 4.7m,
                PropertyE = "EEEE",
                PropertyF = Enum1.Value1,
                PropertyG = Enum1.None,
                PropertyH = DateTime.Now,
                PropertyI = DateTime.Now.AddDays(1),
                PropertyJ = new TimeSpan(9, 0, 0),
                PropertyK = new TimeSpan(13, 30, 0),
                PropertyL = new PropertyClass1()
                {
                    Code = "1111",
                    Name = "2222"
                }
            };

            var to2 = MessagePackSerializer.Serialize(classB1);
            Console.WriteLine(to2);
            var from3 = MessagePackSerializer.Deserialize<ClassB>(to2);
            Console.WriteLine(from3);
            var from4 = MessagePackSerializer.Deserialize<IInterface1>(to1);
            Console.WriteLine(from4);

            Console.ReadLine();
            
        }
    }

    [MessagePack.Union(0, typeof(ClassA))]
    [MessagePack.Union(1, typeof(ClassB))]
    public interface IInterface1
    {
        PropertyClass1 PropertyL { get; set; }
    }

    public enum Enum1
    {
        None = 0,
        Value1 = 1,
        Value2 = 2,
    }

    [MessagePackObject(keyAsPropertyName:true)]
    public class ClassA : IInterface1
    { 
        public int PropertyA { get; set; }
        public int? PropertyB { get; set; }
        public decimal PropertyC { get; set; }
        public decimal? PropertyD { get; set; }
        public string PropertyE { get; set; }
        public Enum1 PropertyF { get; set; }
        public Enum1? PropertyG { get; set; }
        public DateTime PropertyH { get; set; }
        public DateTime? PropertyI { get; set; }
        public TimeSpan PropertyJ { get; set; }
        public TimeSpan? PropertyK { get; set; }

        public virtual PropertyClass1 PropertyL { get; set; }

    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class ClassB : ClassA
    {
        private PropertyClass1 _PropertyL;
        public override PropertyClass1 PropertyL
        {
            get => _PropertyL;
            set
            {
                _PropertyL = value;
                _PropertyL.Name += "3333";
            }
        }

        public decimal Price { get; set; } = 100m;
    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class PropertyClass1
    {
        public string Code {  get; set; }
        public string Name { get; set; }
    }
}