namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome7532();
            Welcome0871();
            Console.ReadKey();
        }

        private static void Welcome7532()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
        static partial void Welcome0871();
    }
}