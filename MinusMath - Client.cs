using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class Client
{
    public static void Main()
    {
        try
        {
            TcpClient client = new TcpClient("10.252.39.170", 8080);
            Console.WriteLine(" [Game Started...]");

            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            string option;
            float stockNumber;
            float randomAnswer1;
            float randomAnswer2;
            float randomAnswer3;

            Console.Write(" [Are You Ready ?] (Y) for accept / (N) for show you 'how to play' : ");
            string confirm1 = Console.ReadLine();
            writer.WriteLine(confirm1);
            writer.Flush();

            if (confirm1 != "Y")
            {
                Console.WriteLine("\n [HOW TO PLAY]");
                string menu1 = reader.ReadLine();
                Console.WriteLine(menu1);
                string menu2 = reader.ReadLine();
                Console.WriteLine(menu2);
                string menu3 = reader.ReadLine();
                Console.WriteLine(menu3);
                string menu4 = reader.ReadLine();
                Console.WriteLine(menu4);

                Console.Write("\n [Are You Ready ?] (Y) for accept / (N) for exit : ");
                string confirm2 = Console.ReadLine();
                writer.WriteLine(confirm2);
                writer.Flush();

                if (confirm2 != "Y")
                {
                    reader.Close();
                    writer.Close();
                    client.Close();
                }
            }

            string question = reader.ReadLine();
            stockNumber = Convert.ToInt32(question);
            Console.WriteLine("\n The Stock Number is " + stockNumber);

            while (true)
            {
                string answer1 = reader.ReadLine();
                randomAnswer1 = Convert.ToInt32(answer1);
                Console.WriteLine(" A. " + randomAnswer1);

                string answer2 = reader.ReadLine();
                randomAnswer2 = Convert.ToInt32(answer2);
                Console.WriteLine(" B. " + randomAnswer2);

                string answer3 = reader.ReadLine();
                randomAnswer3 = Convert.ToInt32(answer3);
                Console.WriteLine(" C. " + randomAnswer3);

                Console.Write("\n [Choose The Random Number as Your Answer] A/B/C/enter for reshuffle : ");
                option = Console.ReadLine();
                writer.WriteLine(option);
                writer.Flush();

                if (option != "A" && option != "B" && option != "C")
                {
                    answer1 = reader.ReadLine();
                    randomAnswer1 = Convert.ToInt32(answer1);
                    Console.WriteLine(" A. " + randomAnswer1);

                    answer2 = reader.ReadLine();
                    randomAnswer2 = Convert.ToInt32(answer2);
                    Console.WriteLine(" B. " + randomAnswer2);

                    answer3 = reader.ReadLine();
                    randomAnswer3 = Convert.ToInt32(answer3);
                    Console.WriteLine(" C. " + randomAnswer3);

                    Console.Write("\n [Choose The Random Number as Your Answer] A/B/C/enter for reshuffle : ");
                    option = Console.ReadLine();
                }
            }

            //reader.Close();
            //writer.Close();
            //client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}