using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Server
{
    private static void CalculatorClient(object argument)
    {
        TcpClient client = (TcpClient)argument;

        try
        {
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            string option;
            float firstStockNumber;
            float randomAnswer1;
            float randomAnswer2;
            float randomAnswer3;
            Random randomAnswer = new Random();
            Random randomStock = new Random();

            string confirm1 = reader.ReadLine();
            if (confirm1 != "Y")
            {
                string menu1 = " 1. Server will send you a question (stock number)";
                writer.WriteLine(menu1);
                writer.Flush();

                string menu2 = " 2. After that, you will get three number that you can use one of them as your answer, and send it";
                writer.WriteLine(menu2);
                writer.Flush();

                string menu3 = " 3. With your answer, you have to reduce the stock number to zero";
                writer.WriteLine(menu3);
                writer.Flush();

                string menu4 = " 4. If you don't get the exact number, you can reshuffle the random number";
                writer.WriteLine(menu4);
                writer.Flush();

                string confirm2 = reader.ReadLine();
                if (confirm2 != "Y")
                {
                    Console.WriteLine(" There is a player out");
                }
            }

            firstStockNumber = randomStock.Next(90, 100);
            Console.WriteLine(" Sending a Question (Stock Number) : " + firstStockNumber);
            writer.WriteLine(firstStockNumber);
            writer.Flush();

            while (true)
            {
                randomAnswer1 = randomAnswer.Next(1, 11);
                writer.WriteLine(randomAnswer1);
                writer.Flush();

                randomAnswer2 = randomAnswer.Next(1, 11);
                while (randomAnswer2 == randomAnswer1)
                {
                    randomAnswer2 = randomAnswer.Next(1, 11);
                    Console.WriteLine(" Random Answer 2 is same with Random Answer 1");
                }
                writer.WriteLine(randomAnswer2);
                writer.Flush();

                randomAnswer3 = randomAnswer.Next(1, 11);
                while (randomAnswer3 == randomAnswer1 || randomAnswer3 == randomAnswer2)
                {
                    randomAnswer3 = randomAnswer.Next(1, 11);
                    Console.WriteLine(" Random Answer 3 is same with Random Answer 1 and Random Answer 2");
                }
                writer.WriteLine(randomAnswer3);
                writer.Flush();

                option = reader.ReadLine();
                if (option != "A" && option != "B" && option != "C")
                {
                    randomAnswer1 = randomAnswer.Next(1, 11);
                    writer.WriteLine(randomAnswer1);
                    writer.Flush();

                    randomAnswer2 = randomAnswer.Next(1, 11);
                    writer.WriteLine(randomAnswer2);
                    writer.Flush();

                    randomAnswer3 = randomAnswer.Next(1, 11);
                    writer.WriteLine(randomAnswer3);
                    writer.Flush();
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine(" There is a player out\n");
        }
        if (client != null)
        {
            client.Close();
        }
    }

    public static void Main()
    {
        TcpListener listener = null;
        try
        {
            listener = new TcpListener(IPAddress.Parse("10.252.39.170"), 8080);
            listener.Start();

            Console.WriteLine(" [MINUSMATH]\n");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine(" [Ada Player Masuk...]");
                Thread newThread = new Thread(CalculatorClient);

                newThread.Start(client);
            }
        }

        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        finally
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }
    }
}