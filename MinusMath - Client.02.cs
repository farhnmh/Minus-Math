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
            TcpClient client = new TcpClient("127.0.0.1", 8080);
            Console.WriteLine(" [Game Started...]");

            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            string option;
            int stockNumber;
            int randomAnswer1;
            int randomAnswer2;
            int randomAnswer3;

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
            Console.WriteLine("\n [" + stockNumber + "]");

            while (true)
            {
                //kirim awal
                question = reader.ReadLine();
                int result = Convert.ToInt32(question);
                Console.WriteLine("\n [Update.. The Stock Number is " + result + "]");

                string answer1 = reader.ReadLine();
                randomAnswer1 = Convert.ToInt32(answer1);
                Console.WriteLine("\n A. " + randomAnswer1);

                string answer2 = reader.ReadLine();
                randomAnswer2 = Convert.ToInt32(answer2);
                Console.WriteLine(" B. " + randomAnswer2);

                string answer3 = reader.ReadLine();
                randomAnswer3 = Convert.ToInt32(answer3);
                Console.WriteLine(" C. " + randomAnswer3);

                Console.Write("\n A/B/C/enter for reshuffle : ");
                option = Console.ReadLine();
                writer.WriteLine(option);
                writer.Flush();

                //player kirimkan jawaban
                if (option == "A")
                {
                    string answerPlayer1 = Convert.ToString(randomAnswer1);
                    writer.WriteLine(answerPlayer1);
                    writer.Flush();
                }
                if (option == "B")
                {
                    string answerPlayer2 = Convert.ToString(randomAnswer2);
                    writer.WriteLine(answerPlayer2);
                    writer.Flush();
                }
                if (option == "C")
                {
                    string answerPlayer3 = Convert.ToString(randomAnswer3);
                    writer.WriteLine(answerPlayer3);
                    writer.Flush();
                }
                if (option != "A" && option != "B" && option != "C")
                {
                    question = reader.ReadLine();
                    result = Convert.ToInt32(question);
                    Console.WriteLine("\n [Update.. The Stock Number is " + result + "]");

                    answer1 = reader.ReadLine();
                    randomAnswer1 = Convert.ToInt32(answer1);
                    Console.WriteLine(" A. " + randomAnswer1);

                    answer2 = reader.ReadLine();
                    randomAnswer2 = Convert.ToInt32(answer2);
                    Console.WriteLine(" B. " + randomAnswer2);

                    answer3 = reader.ReadLine();
                    randomAnswer3 = Convert.ToInt32(answer3);
                    Console.WriteLine(" C. " + randomAnswer3);

                    Console.Write("\n A/B/C/enter for reshuffle : ");
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