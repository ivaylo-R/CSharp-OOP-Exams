using System;

namespace Sand
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string result = IsValidate(null);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            
        }

        public static string IsValidate(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("stop");
            }
            return name;
        }
    }
}
