using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

public class Client
{
    public static void gamePlay()
    {
        Console.Write(" [IP Server] ");
        string ip = Console.ReadLine();
        TcpClient client = new TcpClient(ip, 8080);
        Console.WriteLine(" [Game Started...]");

        StreamReader reader = new StreamReader(client.GetStream());
        StreamWriter writer = new StreamWriter(client.GetStream());

        string option;
        string playerName = "";
        int stockNumber;
        int randomAnswer1;
        int randomAnswer2;
        int randomAnswer3;
        var timer = new Stopwatch();

        Console.Write("\n [Input Your Name Bruhh] ");
        playerName = Console.ReadLine();
        writer.WriteLine(playerName);
        writer.Flush();

        string ind = reader.ReadLine();
        Console.WriteLine("\n [You're Player-" + ind + "]");

        while (true)
        {
            string start1 = " [Main Menu]\n";
            string start2 = " A. Play Game\n";
            string start3 = " B. LeaderBoard\n";
            string start4 = " C. Exit\n";
            string start5 = "\n [Choose One of Them] ";
            Console.Write(start1 + start2 + start3 + start4 + start5);

            string pilihan = Console.ReadLine();
            writer.WriteLine(pilihan);
            writer.Flush();

            if (pilihan == "a" || pilihan == "A")
            {
                break;
            }
            else if (pilihan == "b" || pilihan == "B")
            {
                int total = Convert.ToInt32(reader.ReadLine());

                Console.WriteLine("\n ------------\n\n [Leaderboard]");
                Console.WriteLine(" time || name\n");
                for (int i = 1; i <= total; i++)
                {
                    string hasil = reader.ReadLine();
                    Console.WriteLine(hasil);
                }
                Console.WriteLine("\n ------------\n");
            }
            else if (pilihan == "c" || pilihan == "C")
            {
                reader.Close();
                writer.Close();
                client.Close();
            }
        }

        Console.Write(" [Are You Ready ?] enter/anything for accept / (N) for show you 'how to play' : ");
        string confirm1 = Console.ReadLine();
        writer.WriteLine(confirm1);
        writer.Flush();

        if (confirm1 == "n" || confirm1 == "N")
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
            string menu5 = reader.ReadLine();
            Console.WriteLine(menu5);

            Console.Write("\n [Are You Ready ?] (Y) for accept / (N) for exit : ");
            string confirm2 = Console.ReadLine();
            writer.WriteLine(confirm2);
            writer.Flush();

            if (confirm2 == "N" || confirm2 == "n")
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
            timer.Start();
            //kirim awal
            question = reader.ReadLine();
            stockNumber = Convert.ToInt32(question);
            Console.WriteLine("\n [Update.. The Stock Number is " + stockNumber + "]");

            if (stockNumber == 0)
            {
                break;
            }

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
            if (option == "A" || option == "a")
            {
                string answerPlayer1 = Convert.ToString(randomAnswer1);
                writer.WriteLine(answerPlayer1);
                writer.Flush();
            }
            if (option == "B" || option == "b")
            {
                string answerPlayer2 = Convert.ToString(randomAnswer2);
                writer.WriteLine(answerPlayer2);
                writer.Flush();
            }
            if (option == "C" || option == "c")
            {
                string answerPlayer3 = Convert.ToString(randomAnswer3);
                writer.WriteLine(answerPlayer3);
                writer.Flush();
            }
        }

        //ketika player menang
        timer.Stop();
        double final = timer.Elapsed.TotalSeconds;
        string final1 = Convert.ToString(final);
        writer.WriteLine(final1);
        writer.Flush();

        Console.WriteLine(" [Congratulations .. You've Done This Game In " + final1 + "s]\n");

        while (true)
        {
            //main menu
            string over1 = " [Main Menu After Game Over]\n";
            string over2 = " A. LeaderBoard\n";
            string over3 = " B. Exit\n";
            string over4 = "\n [Choose One of Them] ";
            Console.Write(over1 + over2 + over3 + over4);

            string pilihan = Console.ReadLine();
            writer.WriteLine(pilihan);
            writer.Flush();

            int total = Convert.ToInt32(reader.ReadLine());
            if (pilihan == "a" || pilihan == "A")
            {
                Console.WriteLine("\n ------------\n\n [Leaderboard]");
                Console.WriteLine(" time || name\n");
                for (int i = 1; i <= total; i++)
                {
                    string hasil = reader.ReadLine();
                    Console.WriteLine(hasil);
                }
                Console.WriteLine("\n ------------\n");
            }
            else if (pilihan == "b" || pilihan == "B")
            {
                reader.Close();
                writer.Close();
                client.Close();
            }
        }
    }

    public static void Main()
    {
        try
        {
            gamePlay();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}