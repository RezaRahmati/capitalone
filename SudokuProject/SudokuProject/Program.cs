using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Abstractions;

namespace SudokuProject
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var services = new ServiceCollection();
                RegisterServices(services);

                var provider = services.BuildServiceProvider();

                var verifier = provider.GetService<ISudokuVerifier>();

                validateFile(verifier, "error-duplicate-numbers.txt");
                validateFile(verifier, "input_sudoku[1].txt");

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Oops: {ex.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void validateFile(ISudokuVerifier verifier, string fileName)
        {
            var result = verifier.IsValid(fileName);

            var resultStr = result ? "a Valid" : "an Inavlid";
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"{fileName} is {resultStr} Sudoku file");
        }

        private static void RegisterServices(ServiceCollection services)
        {
            services.AddTransient<IFileSystem, FileSystem>();
            services.AddTransient<ISudokuVerifier, SudokuVerifier>();
        }
    }
}
