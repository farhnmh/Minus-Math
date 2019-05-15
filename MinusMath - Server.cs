using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Server
{
    private static void MinusMath(object argument)
    {
        TcpClient client = (TcpClient)argument;

        string playerName = "";

        try
        {
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            string option;
            int stockNumber;
            int randomAnswer1;
            int randomAnswer2;
            int randomAnswer3;
            Random randomAnswer = new Random();
            Random randomStock = new Random();

            playerName = reader.ReadLine();
            Console.WriteLine(" [There is " + playerName + "]");

            string confirm1 = reader.ReadLine();
            if (confirm1 == "n" || confirm1 == "N")
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
                    Console.WriteLine(" [There is a player out]");
                }
            }

            stockNumber = randomStock.Next(90, 100);
            Console.WriteLine(" [Sending a Question (Stock Number) : " + stockNumber + "]");
            writer.WriteLine(stockNumber);
            writer.Flush();

            while (true)
            {
                //random awal
                writer.WriteLine(stockNumber);
                writer.Flush();

                randomAnswer1 = randomAnswer.Next(1, 16);
                writer.WriteLine(randomAnswer1);
                writer.Flush();

                randomAnswer2 = randomAnswer.Next(1, 16);
                while (randomAnswer2 == randomAnswer1)
                {
                    randomAnswer2 = randomAnswer.Next(1, 16);
                }
                writer.WriteLine(randomAnswer2);
                writer.Flush();

                randomAnswer3 = randomAnswer.Next(1, 16);
                while (randomAnswer3 == randomAnswer1 || randomAnswer3 == randomAnswer2)
                {
                    randomAnswer3 = randomAnswer.Next(1, 16);
                }
                writer.WriteLine(randomAnswer3);
                writer.Flush();

                //player kirimkan jawaban
                option = reader.ReadLine();
                if (option == "A" || option == "a")
                {
                    string answer1 = reader.ReadLine();
                    randomAnswer1 = Convert.ToInt32(answer1);
                    stockNumber = stockNumber - randomAnswer1;
                }
                if (option == "B" || option == "b")
                {
                    string answer2 = reader.ReadLine();
                    randomAnswer2 = Convert.ToInt32(answer2);
                    stockNumber = stockNumber - randomAnswer2;
                }
                if (option == "C" || option == "c")
                {
                    string answer3 = reader.ReadLine();
                    randomAnswer3 = Convert.ToInt32(answer3);
                    stockNumber = stockNumber - randomAnswer3;
                }

                //ketika reshuffle
                if (option == "A" || option == "a" && option == "B" || option == "b" && option == "C" || option == "c")
                {
                    writer.WriteLine(stockNumber);
                    writer.Flush();

                    randomAnswer1 = randomAnswer.Next(1, 16);
                    writer.WriteLine(randomAnswer1);
                    writer.Flush();

                    randomAnswer2 = randomAnswer.Next(1, 16);
                    while (randomAnswer2 == randomAnswer1)
                    {
                        randomAnswer2 = randomAnswer.Next(1, 16);
                    }
                    writer.WriteLine(randomAnswer2);
                    writer.Flush();

                    randomAnswer3 = randomAnswer.Next(1, 16);
                    while (randomAnswer3 == randomAnswer1 || randomAnswer3 == randomAnswer2)
                    {
                        randomAnswer3 = randomAnswer.Next(1, 16);
                    }
                    writer.WriteLine(randomAnswer3);
                    writer.Flush();

                    option = reader.ReadLine();
                    if (option == "A" || option == "a")
                    {
                        string answer1 = reader.ReadLine();
                        randomAnswer1 = Convert.ToInt32(answer1);
                        stockNumber = stockNumber - randomAnswer1;
                    }
                    if (option == "B" || option == "b")
                    {
                        string answer2 = reader.ReadLine();
                        randomAnswer2 = Convert.ToInt32(answer2);
                        stockNumber = stockNumber - randomAnswer2;
                    }
                    if (option == "C" || option == "c")
                    {
                        string answer3 = reader.ReadLine();
                        randomAnswer3 = Convert.ToInt32(answer3);
                        stockNumber = stockNumber - randomAnswer3;
                    }
                }

                //ketika player menang
                if (stockNumber == 0)
                {
                    Console.WriteLine(" [" + playerName + " has done]");
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine(" [" + playerName + " is out]\n");
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
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            listener.Start();

            Console.WriteLine(" [MINUSMATH]\n");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine(" [There is player join]");
                Thread newThread = new Thread(MinusMath);

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