using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CaesarCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Encrypt();
            DecryptByAnalise();
            DecryptByFindMostPopilarWord();

        }

        private static void DecryptByFindMostPopilarWord()
        {
            Console.WriteLine("Input Decrypt Text:");
            var txt = Console.ReadLine().ToLower();
            var dic = new Dictionary<string, int>();
            var list = new List<string>();
            var x = new StreamReader("mostpopilarword.txt");
            while (true)
            {
                if (x.ReadLine() == null)
                {
                    break;
                }
                else
                {
                    list.Add(x.ReadLine());
                }

            }

            var txtToArray = txt.ToUpper().ToCharArray();
            for (int k = 1; k < 25; k++)
            {
                for (int i = 0; i < txt.Length; i++)
                {
                    if ((int)txtToArray[i] >= 65 && (int)txtToArray[i] <= 90)
                    {
                        int xdispacement = (int)txtToArray[i] - k;
                        txtToArray[i] = (char)xdispacement;
                        if (txtToArray[i] < 65)
                        {
                            xdispacement = (int)txtToArray[i] + 26;
                            txtToArray[i] = (char)xdispacement;
                        }
                    }
                }

                string stringFromChar = new string(txtToArray);
                //Console.WriteLine("------------------------");
                //Console.WriteLine(stringFromChar);
                txtToArray = txt.ToUpper().ToCharArray();
                dic.Add(stringFromChar, 0);


                var listDecrypt = stringFromChar.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int i = 0; i < listDecrypt.Count; i++)
                {
                    if (list.Contains(listDecrypt[i].ToLower()))
                    {
                        dic[stringFromChar]++;
                    }
                }

                //  Console.WriteLine(string.Join(" ", listDecrypt));




            }
            dic = dic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            var find = dic.First().Key;
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(find.ToString());
            Console.WriteLine("----------------------------------------------------");
        }

        private static void DecryptByAnalise()
        {
            Console.WriteLine("Input Decrypt Text:");
            var txt = Console.ReadLine().ToUpper().ToCharArray();
            var dic = new Dictionary<char, int>();
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] >= 65 && txt[i] <= 90)
                {
                    if (!dic.ContainsKey(txt[i]))
                    {
                        dic.Add(txt[i], 0);
                    }
                    dic[txt[i]]++;
                }
            }
            dic = dic.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            // Console.WriteLine(string.Join(" ",dic));
            for (int j = 1; j <= 2; j++)
            {
                var mostPopilarChar = dic.Select(x => x.Key).FirstOrDefault();
                var displacement = 0;
                if (j == 1)
                {
                    displacement = Math.Abs((int)mostPopilarChar - 69);
                }
                else if (j == 2)
                {
                    displacement = Math.Abs((int)mostPopilarChar - 84);
                }
                for (int i = 0; i < txt.Length; i++)
                {
                    if ((int)txt[i] >= 65 && (int)txt[i] <= 90)
                    {
                        int x = (int)txt[i] - displacement;
                        txt[i] = (char)x;
                        if (txt[i] < 65)
                        {
                            x = (int)txt[i] + 26;
                            txt[i] = (char)x;
                        }
                    }
                }
                Console.WriteLine("----------------------");
                Console.WriteLine($"{j} Posiable");
                Console.WriteLine("----------------------");
                Console.WriteLine(string.Join("", txt));
                dic.Remove(mostPopilarChar);
            }
            /*   In cryptography, a Caesar cipher, also known as shift cipher, Caesar's cipher, Caesar's code or Caesar shift, is one
                 of the simplest and most widely known encryption techniques. It is a type of substitution cipher in which each letter in 
                 the plaintext is 'shifted' a certain number of places down the alphabet. For example, with a shift of 1, A would be 
                 replaced by B, B would become C, and so on. The method is named after Julius Caesar, who apparently used it to communicate
                 with his generals.*/

            //test with n = 3;
        }

        private static void Encrypt()
        {
            Console.WriteLine("Input Text:");
            var txt = Console.ReadLine().ToUpper().ToCharArray();
            Console.WriteLine("Input Displacement");
            var displacement = int.Parse(Console.ReadLine());
            for (int i = 0; i < txt.Length; i++)
            {
                if ((int)txt[i] >= 65 && (int)txt[i] <= 90)
                {
                    int x = (int)txt[i] + displacement;
                    txt[i] = (char)x;
                    if (txt[i] > 90)
                    {
                        x = (int)txt[i] - 26;
                        txt[i] = (char)x;
                    }
                }

            }
            Console.WriteLine("New encryption code is:");
            Console.WriteLine(string.Join("", txt));
        }
    }
}
