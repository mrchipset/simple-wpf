namespace GetFieldsName
{
    public class Foo
    {
        public int Bar { get; set; } = 2;
        public int baz;

        private object foo;
        protected object bar;

        public static int Baz = 1;
        public Foo() { }

        public Foo(int bar) { }


        public void method()
        {
            Console.WriteLine("This is a member method");
        }

       

        
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Foo instance = new Foo();
            // Get public fields
            Console.WriteLine("Get Fields Name");
            var members = instance.GetType().GetFields(); 
            foreach (var member in members)
            {
                Console.WriteLine($"Name: {member.Name}\tValue: {member.GetValue(instance)}");
            }

            // Get public fields
            Console.WriteLine("Get Property Name");
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                Console.WriteLine($"Name: {property.Name}\tValue: {property.GetValue(instance)}");
            }

            Console.WriteLine("Get methods Name");
            var methods = instance.GetType().GetMethods();
            foreach (var method in methods)
            {
                Console.WriteLine($"Name: {method.Name}\tCall:");
                if (method.Name == "method")
                {
                    method.Invoke(instance, null);
                }

            }

            Console.WriteLine("Get constructor");
            var constructors = instance.GetType().GetConstructors();
            foreach (var constructor in constructors)
            {
                Console.WriteLine($"Name: {constructor.Name}");
                var _params = constructor.GetParameters();
                foreach (var param in _params)
                {
                    Console.WriteLine($"Name: {param.Name} Type: {param.ParameterType.Name}");
                }
            }

        }
    }
}