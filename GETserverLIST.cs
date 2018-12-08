using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Net;
namespace ConsoleApp1
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(@"This aplication requires a list of server addresses in file C:\adresy.txt");
            String GetTimestamp(DateTime value)
            {
                return value.ToString("yyyyMMddHHmmssffff");
            } 
            string filepath = @"C:\adresy.txt";
            string statusy = @"C:\statusy.txt";
            Console.Write("GET:");
            string zapytanieget = Console.ReadLine();
            if (File.Exists(filepath))
                { 
                    GetRequest(zapytanieget);
                } else
            {
                Console.WriteLine(@"Could not locate file " + filepath);
            }


            async void GetRequest(string request)
            {
                List<string> adresy = File.ReadAllLines(filepath).ToList();
                foreach (string adres in adresy)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            using (HttpResponseMessage response = await client.GetAsync(@"http://" + adres + @"/?" + request))
                            {
                                String Timestamp = GetTimestamp(DateTime.Now);
                                int status = (int)response.StatusCode;
                                string statuss = status.ToString();
                                Console.WriteLine(Timestamp + "   server:" + adres + "   status:" + statuss);
                                File.AppendAllText(statusy, Timestamp + "   server:" + adres + "   status:" + statuss + Environment.NewLine);
                            }
                        }
                        catch (Exception)
                        {
                            String Timestamp = GetTimestamp(DateTime.Now);
                            Console.WriteLine(Timestamp + "   INCORRECT ADDRESS INPUT: " + adres);
                            File.AppendAllText(statusy, Timestamp + "   INCORRECT ADDRESS INPUT: " + adres + Environment.NewLine);
                        }
                    }
                }
                Console.WriteLine("Saved to: " + statusy);
            }
            Console.ReadLine();
        }

        
    }
}
