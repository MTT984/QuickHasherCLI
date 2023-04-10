using System;

namespace QuickHasherCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "-help" || args[0] == "--help")
            {
                Console.WriteLine("============================== QuickHasher Help ==============================");
                Console.WriteLine("Usage:           quickhasher [-prompt or --p] [input]");
                Console.WriteLine("Input usage:     [hashType]:[hashInput]");
                Console.WriteLine("hash types:      vlt, vlt-int, vlt-uint, bin, bin-int, bin-uint, commerce");
                Console.WriteLine("Example:         quickhasher -prompt vlt-int:text1");
                Console.WriteLine("                 quickhasher -prompt vlt-int:text1 vlt:text2");
                Console.WriteLine("                 quickhasher --p bin:text1 bin-int:text2");
                Console.WriteLine("                 quickhasher --p commerce:text1");
                Console.WriteLine("==============================================================================");
            }
            else
            {
                PromptHasher(args);
            }
        }

        private static void PromptHasher(string[] _args)
        {
            foreach (string _x in _args)
            {
                if (_x.Contains(":"))
                {
                    switch (_x.Split(':')[0])
                    {
                        case "vlt":
                            Console.Write($"{Hasher.HashConvert(_x.Split(':')[1], "vlt_mem")} ");
                            break;
                        case "vlt-int":
                            Console.Write($"{Hasher.HashConvert(_x.Split(':')[1], "vlt_mem_int")} ");
                            break;
                        case "vlt-uint":
                            Console.Write($"{Hasher.HashConvert(_x.Split(':')[1], "vlt_mem_uint")} ");
                            break;
                        case "bin":
                            Console.Write($"{Hasher.HashConvert(_x.Split(':')[1], "bin_mem")} ");
                            break;
                        case "bin-int":
                            Console.Write($"{Hasher.HashConvert(_x.Split(':')[1], "bin_mem_int")} ");
                            break;
                        case "bin-uint":
                            Console.Write($"{Hasher.HashConvert(_x.Split(':')[1], "bin_mem_uint")} ");
                            break;
                        case "commerce":
                            Console.Write($"{Hasher.CommerceHash(_x.Split(':')[1])} ");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}