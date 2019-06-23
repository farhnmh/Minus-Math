using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

struct data
{
    public string name;
    public double score;
}

public static class counter
{
    public static int jumlahClient = 0;
    public static int totalPlayer = 1;
}

public class Server
{
    static data[] player = new data[500];
    public static void MinusMath(object argument)
    {
        TcpClient client = (TcpClient)argument;

        int i = counter.jumlahClient;

        try
        {
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            string option;
            int stockNumber = 101;
            int randomAnswer1;
            int randomAnswer2;
            int randomAnswer3;
            Random randomAnswer = new Random();

            player[i].name = reader.ReadLine();
            Console.WriteLine(" [There is " + player[i].name + "]");

            string ind = Convert.ToString(i);
            writer.WriteLine(ind);
            writer.Flush();

            while (true)
            {
                string pilihan = reader.ReadLine();
                if (pilihan == "a" || pilihan == "A")
                {
                    break;
                }
                else if (pilihan == "b" || pilihan == "B")
                {
                    string total = Convert.ToString(counter.jumlahClient);
                    writer.WriteLine(total);
                    writer.Flush();

                    int smallest;
                    double temp;
                    string temp2;
                    int urutan = 1;

                    //urutan
                    if (counter.jumlahClient == 2)
                    {
                        for (int a = 1; a <= counter.jumlahClient; a++)
                        {
                            Console.WriteLine(" [Terurut]");
                            smallest = a;
                            for (int b = i + 1; b <= counter.jumlahClient; b++)
                            {
                                if (player[b].score < player[smallest].score)
                                {
                                    smallest = b;
                                }
                            }
                            temp = player[smallest].score;
                            player[smallest].score = player[a].score;
                            player[a].score = temp;

                            temp2 = player[smallest].name;
                            player[smallest].name = player[a].name;
                            player[a].name = temp2;

                            string hasil = " " + urutan + ". " + player[a].score + "s || " + player[a].name;
                        }
                    }
                    else if (counter.jumlahClient != 2)
                    {
                        for (int a = 1; a < counter.jumlahClient; a++)
                        {
                            Console.WriteLine(" [Terurut]");
                            smallest = a;
                            for (int b = i + 1; b < counter.jumlahClient; b++)
                            {
                                if (player[b].score < player[smallest].score)
                                {
                                    smallest = b;
                                }
                            }
                            temp = player[smallest].score;
                            player[smallest].score = player[a].score;
                            player[a].score = temp;

                            temp2 = player[smallest].name;
                            player[smallest].name = player[a].name;
                            player[a].name = temp2;

                            string hasil = " " + urutan + ". " + player[a].score + "s || " + player[a].name;
                        }
                    }

                    //flush
                    for (int x = 1; x <= counter.jumlahClient; x++)
                    {
                        if (player[x].score != 0)
                        {
                            string hasil = " " + urutan + ". " + player[x].score + "s || " + player[x].name;
                            writer.WriteLine(hasil);
                            writer.Flush();
                        }
                        else if (player[x].score == 0)
                        {
                            string hasil = " " + urutan + ". " + player[x].name + " hasn't finished yet";
                            writer.WriteLine(hasil);
                            writer.Flush();
                        }
                        urutan++;
                    }
                }
            }

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

                string menu5 = " 5. You have to be careful, because if the number you choose is greater than your stock number, then your stock number will not be zero";
                writer.WriteLine(menu5);
                writer.Flush();

                string confirm2 = reader.ReadLine();
                if (confirm2 == "n" || confirm2 == "N")
                {
                    Console.WriteLine(" [There is a player out]");
                }
            }

            Console.WriteLine(" [" + player[i].name + " started the game]");
            writer.WriteLine(stockNumber);
            writer.Flush();

            while (true)
            {
                //random awal
                writer.WriteLine(stockNumber);
                writer.Flush();

                if (stockNumber == 0)
                {
                    break;
                }

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
                    if (randomAnswer1 <= stockNumber)
                    {
                        stockNumber = stockNumber - randomAnswer1;
                    }
                    else if (randomAnswer1 > stockNumber)
                    {
                        int temp = 0;
                        temp = randomAnswer1 - stockNumber;
                        stockNumber = temp;
                    }
                }
                if (option == "B" || option == "b")
                {
                    string answer2 = reader.ReadLine();
                    randomAnswer2 = Convert.ToInt32(answer2);
                    if (randomAnswer2 <= stockNumber)
                    {
                        stockNumber = stockNumber - randomAnswer2;
                    }
                    else if (randomAnswer2 > stockNumber)
                    {
                        int temp = 0;
                        temp = randomAnswer2 - stockNumber;
                        stockNumber = temp;
                    }
                }
                if (option == "C" || option == "c")
                {
                    string answer3 = reader.ReadLine();
                    randomAnswer3 = Convert.ToInt32(answer3);
                    if (randomAnswer3 <= stockNumber)
                    {
                        stockNumber = stockNumber - randomAnswer3;
                    }
                    else if (randomAnswer3 > stockNumber)
                    {
                        int temp = 0;
                        temp = randomAnswer3 - stockNumber;
                        stockNumber = temp;
                    }
                }
            }

            //ketika player menang
            string final = reader.ReadLine();
            player[i].score = Convert.ToDouble(final);
            Console.WriteLine("\n [" + player[i].name + " has done this game in " + player[i].score + "s]");

            while (true)
            {
                string pilihan = reader.ReadLine();
                int urutan = 1;

                string total = Convert.ToString(counter.jumlahClient);
                writer.WriteLine(total);
                writer.Flush();

                if (pilihan == "a" || pilihan == "A")
                {
                    int smallest;
                    double temp;
                    string temp2;

                    //urutan
                    if (counter.jumlahClient == 2)
                    {
                        for (int a = 1; a <= counter.jumlahClient; a++)
                        {
                            Console.WriteLine(" [Terurut]");
                            smallest = a;
                            for (int b = i + 1; b <= counter.jumlahClient; b++)
                            {
                                if (player[b].score < player[smallest].score)
                                {
                                    smallest = b;
                                }
                            }
                            temp = player[smallest].score;
                            player[smallest].score = player[a].score;
                            player[a].score = temp;

                            temp2 = player[smallest].name;
                            player[smallest].name = player[a].name;
                            player[a].name = temp2;

                            string hasil = " " + urutan + ". " + player[a].score + "s || " + player[a].name;
                        }
                    }
                    else if (counter.jumlahClient != 2)
                    {
                        for (int a = 1; a < counter.jumlahClient; a++)
                        {
                            Console.WriteLine(" [Terurut]");
                            smallest = a;
                            for (int b = i + 1; b < counter.jumlahClient; b++)
                            {
                                if (player[b].score < player[smallest].score)
                                {
                                    smallest = b;
                                }
                            }
                            temp = player[smallest].score;
                            player[smallest].score = player[a].score;
                            player[a].score = temp;

                            temp2 = player[smallest].name;
                            player[smallest].name = player[a].name;
                            player[a].name = temp2;

                            string hasil = " " + urutan + ". " + player[a].score + "s || " + player[a].name;
                        }
                    }
                    //flush
                    for (int x = 1; x <= counter.jumlahClient; x++)
                    {
                        if (player[x].score != 0)
                        {
                            string hasil = " " + urutan + ". " + player[x].score + "s || " + player[x].name;
                            writer.WriteLine(hasil);
                            writer.Flush();
                        }
                        else if (player[x].score == 0)
                        {
                            string hasil = " " + urutan + ". " + player[x].name + " hasn't finished yet";
                            writer.WriteLine(hasil);
                            writer.Flush();
                        }
                        urutan++;
                    }
                }
            }
        }
        catch (IOException)
        {
            Console.WriteLine(" [" + player[i].name + " is out]\n");
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
            listener = new TcpListener(IPAddress.Parse("192.168.43.141"), 8080);
            listener.Start();

            Console.WriteLine(" [MINUSMATH]\n");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread newThread = new Thread(MinusMath);
                newThread.Start(client);

                counter.jumlahClient++;
                Console.WriteLine(" [Total Player = " + counter.jumlahClient + "]");
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