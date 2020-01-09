﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//9679
/*if(mx[j] == x && my[k] == y && j == k)
                    {
                        do
                        {
                            x = R.Next(0, 10);
                            y = R.Next(0, 10);
                        } while (mx[j] == x && my[k] == y && j == k);
                    }*/
namespace Lines
{
    class Program
    {
        static bool GameOver(ref string [,] field , ref int points)
        {
            int i = 0;
            foreach (string ball in field)
                if (ball == Char.ConvertFromUtf32(9679))
                    i++;
            if(i == 100)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("GAME OVER");
                Console.WriteLine("Ваши очки: {0}", points);
                Console.ResetColor();
                return false;
            }
            return true;
            
        }

        //static (int[] ax, int[] ay) Zip(ref int n, ref int flag, int[] ax, int[] ay)
        //{

        //    if (flag == 1)
        //        n *= 2;

        //    Random R = new Random();
        //    int x = R.Next(0, 10);
        //    int y = R.Next(0, 10);
        //    ax[0] = x;
        //    ay[0] = y;
        //    for (int i = 1; i < n; i++)
        //    { //условие на наличие шара на поле на этом месте
        //        x = R.Next(0, 10);
        //        y = R.Next(0, 10);
        //        for (int j = 0, k = 0; j < i && k < i; j++, k++)
        //        {
        //            while (ax[j] == x && ay[k] == y && j == k)
        //            {
        //                x = R.Next(0, 10);
        //                y = R.Next(0, 10);
        //            }
        //        }
        //        ax[i] = x;
        //        ay[i] = y;
        //    }

        //    return (ax, ay);
        //}


        static (string[,], ConsoleColor[,]) Filling(ref string[,] field, ref int n, ref ConsoleColor[,] color)
        {
            //var (ax, ay) = Zip(ref n, ref flag, mx, my);
            //int[] x = ax;
            //int[] y = ay;
            //for (int i = 0; i < n; i++)
            //{
            //    field[y[i], x[i]] = Char.ConvertFromUtf32(9679);
            //    color[y[i], x[i]] = CircleColor();
            //}
            Random rand = new Random();
            int x = rand.Next(0, 10);
            int y = rand.Next(0, 10);
            for (int i = 0; i < n; i++)
            {
                while (field[y, x] == char.ConvertFromUtf32(9679))
                {
                    x = rand.Next(0, 10);
                    y = rand.Next(0, 10);
                }
                field[y, x] = char.ConvertFromUtf32(9679);
                color[y, x] = CircleColor();
            }
            return (field, color);
        }


        static (string[,], ConsoleColor[,]) Move(ref string[,] field, int x, int y, ref ConsoleColor[,] colorArray, int x1, int y1)
        {
                field[y, x] = "";
                field[y1, x1] = Char.ConvertFromUtf32(9679);
                colorArray[y1, x1] = colorArray[y, x];
                colorArray[y, x] = ConsoleColor.Black;
                return (field, colorArray);
        }
        static ConsoleColor CircleColor()
        {
            Random rand = new Random();
            ConsoleColor[] Colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.DarkGray, ConsoleColor.Cyan, ConsoleColor.Magenta };
            int index = rand.Next(Colors.Length);
            return Colors[index];
        }

        static void Cout(ref string[,] array, ref ConsoleColor[,] color)
        {
            
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if (array[i, j] == Char.ConvertFromUtf32(9679))
                    {
                        Console.ForegroundColor = color[i, j];
                        Console.Write(" {0} ", array[i, j]);
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
            
        }

        static (int, string[,], ConsoleColor[,]) Line(ref string[,] Field, ref ConsoleColor[,] colors, ref int points)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (Field[i,j] == char.ConvertFromUtf32(9679) && Field[i, j] == Field[i, j + 1] && Field[i, j] == Field[i, j + 2] && colors[i, j] == colors[i, j + 1] && colors[i, j] == colors[i, j+2])
                    {
                        points += 10;
                        Field[i, j] = "";
                        Field[i, j + 1] = "";
                        Field[i, j + 2] = "";

                        colors[i, j] = ConsoleColor.Black;
                        colors[i, j + 1] = ConsoleColor.Black;
                        colors[i, j + 2] = ConsoleColor.Black;
                    }

                    if (Field[i, j] == char.ConvertFromUtf32(9679) && Field[i, j] == Field[i + 1, j] && Field[i, j] == Field[i + 2, j] && colors[i, j] == colors[i + 1, j] && colors[i, j] == colors[i + 2, j])
                    {
                        points += 10;
                        Field[i, j] = "";
                        Field[i + 1, j] = "";
                        Field[i + 2, j] = "";

                        colors[i, j] = ConsoleColor.Black;
                        colors[i + 1, j] = ConsoleColor.Black;
                        colors[i + 2, j] = ConsoleColor.Black;
                    }
                }
            }
            return (points, Field, colors);
        }
        static void Main(string[] args)
        {
            int x, y, x1, y1, points = 0;
            
            ConsoleColor[,] colorArray = new ConsoleColor[10,10];
            string[,] Field = new string[10,10];
            
            //string balls = Char.ConvertFromUtf32(9679);
            Console.WriteLine("Очки: {0}", points);
            Console.Write("Введите кол-во стартовых шаров: ");
            int startBalls = int.Parse(Console.ReadLine());
            
            Filling(ref Field, ref startBalls, ref colorArray);

            Cout(ref Field, ref colorArray);

           
            while (GameOver(ref Field, ref points) == true)
            {
                    
                    Console.Write("Введите координаты шара (X;Y):");
                    x = int.Parse(Console.ReadLine());
                    y = int.Parse(Console.ReadLine());
                    Console.Write("Введите место (X;Y):");
                    x1 = int.Parse(Console.ReadLine());
                    y1 = int.Parse(Console.ReadLine());
                    while(Field[y,x] != char.ConvertFromUtf32(9679))
                {
                    Console.Write("Координаты шара введены неверно! Повторите ввод (X;Y): ");
                    x = int.Parse(Console.ReadLine());
                    y = int.Parse(Console.ReadLine());
                }
                while (Field[y1, x1] == char.ConvertFromUtf32(9679))
                {
                    Console.Write("Координаты места введены неверно! Повторите ввод (X;Y):");
                    x1 = int.Parse(Console.ReadLine());
                    y1 = int.Parse(Console.ReadLine());
                }
                    Console.Clear();
                    Console.WriteLine("Очки: {0}", points);
                    Move(ref Field, x, y, ref colorArray, x1, y1);
                    Line(ref Field, ref colorArray, ref points);
                    Filling(ref Field, ref startBalls, ref colorArray);
                    Cout(ref Field, ref colorArray);
            }
            Console.ReadKey();
        }
    }
}
