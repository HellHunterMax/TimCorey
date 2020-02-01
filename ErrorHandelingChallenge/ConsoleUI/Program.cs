using DemoLibrary;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentProcessor paymentProcessor = new PaymentProcessor();
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    var result = paymentProcessor.MakePayment($"Demo{ i }", i);

                    if (result == null)
                    {
                        Console.WriteLine($"Null value for item {i}");
                    }
                    else
                    {
                        Console.WriteLine(result.TransactionAmount);
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    if (e.InnerException != null)
                    {
                        Console.WriteLine("Skipped invalid record " + e.InnerException.Message);
                    }
                    else
                    {
                        Console.WriteLine("Skipped invalid record");
                    }
                }
                catch (FormatException e)
                {
                    if (i == 5)
                    {
                        Console.WriteLine($"Payment skipped for payment with {i} items");
                    }
                    else if (e.InnerException != null)
                    {
                        Console.WriteLine("Formatting Issue " + e.InnerException.Message);
                    }
                    else
                    {
                        Console.WriteLine("Formatting Issue");
                    }

                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                    {
                        Console.WriteLine(e.Message + e.InnerException);
                    }
                    else
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
