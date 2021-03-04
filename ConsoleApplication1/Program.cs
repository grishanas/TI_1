using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string inputStr;
            Console.WriteLine("1-железнодорожная изгородь");
            Console.WriteLine("2-столбцовый метод");
            Console.WriteLine("3-поворачивание решоток");
            Console.WriteLine("4-Шифр Цезоря");
            string tmp=Console.ReadLine();
            switch (tmp)
            {
                case "1":
                    Console.WriteLine("Введите строку для шифрования");
                    inputStr=Console.ReadLine();
                    Console.WriteLine("Введите глубину для шифрования");
                    string DeepStr = Console.ReadLine();
                    int deep = Convert.ToInt32(DeepStr);
                    string outStr=(RailWayCode(inputStr, deep));
                    Console.WriteLine(outStr);
                    Console.WriteLine(RailWayDecode(outStr, deep));
                    break;
                case "2":
                    Console.WriteLine("Введите строку для шифрования");
                    inputStr=Console.ReadLine();
                    Console.WriteLine("Введите ключ для шифрования");
                    string KeyStr = Console.ReadLine();
                    string code = ColumnCode(inputStr, KeyStr);
                    Console.WriteLine(code);
                    Console.WriteLine(ColumnDeCode(code, KeyStr));

                    break;
                case "3":
                    Console.WriteLine("Введите строку для шифрования");
                    inputStr=Console.ReadLine();
                    string Lattice = LatticeCode(inputStr);
                    Console.WriteLine(Lattice);
                    Console.WriteLine(LatticeDecode(tmp));
                    break;
                case "4":
                    Console.WriteLine("Введите строку для шифрования");
                    inputStr=Console.ReadLine();
                    Console.WriteLine("Введите глубину для шифрования");
                    string keystr = Console.ReadLine();
                    int key = Convert.ToInt32(keystr);
                    string cesor = cesorShifr(inputStr, key);
                    Console.WriteLine(cesorDeShifr(cesor, key));


                    break;

            }
            Console.ReadKey();
        }

        static string RailWayCode(string InputStr,int Deep)
        {
            char[] inp,outa=new char[InputStr.Length];
            inp = InputStr.ToCharArray();
            int i = 0, j = 0, k = 0;

            while ((i + j) < InputStr.Length)
            {
                outa[k] = inp[i + j];
                k++;
                j += Deep * 2 - 2 * (1 + i);
            }
            i++;
            while(i<Deep-1)
            {
                j = 0;
                while((i+j)<InputStr.Length)
                {
                    outa[k] = inp[i + j];
                    k++;
                    j += Deep * 2 - 2*(1 + i);
                    if ((i+j)<InputStr.Length)
                    {
                        outa[k] = inp[i + j];
                        k++;
                        j += i * 2;
                    }
                }
                i++;
            }
            j = 0;
            while ((i + j) < InputStr.Length)
            {

                    outa[k] = inp[i + j];
                    k++;
                    j += i * 2;
            }
            return new String(outa);
           
        }

        static string RailWayDecode(string InputStr,int Deep)
        {
            char[] inp;
            StringBuilder outStr=new StringBuilder();
            inp = InputStr.ToCharArray();
            StringBuilder[] ArrStr = new StringBuilder[Deep];
            for (int i = 0; i < Deep; i++)
            {
                ArrStr[i] = new StringBuilder();
            }
            int period = ((Deep - 1) * 2);
            int[] Length= new int[Deep];
            for(int i=0;i<InputStr.Length;i++)
            {
                Length[Deep - 1 - Math.Abs(Deep - 1 - (i % period))] += 1;

            }

            int k = 0;
           for(int i=0;i<Deep;i++)
           {

               int j = 0;
               for(;j<Length[i];j++)
               {
                   ArrStr[i].Append(inp[j+k]);
               }
               k += j;
           }


            int[] index=new int[Deep];
            for(int i=0;i<Deep;i++)
                index[i]=0;

            for (int i = 0; i < InputStr.Length; i++)
            {
                int temp = Deep - 1 - Math.Abs(Deep - 1 - (i % period));
                outStr.Append(ArrStr[temp][index[temp]] );
                index[temp] += 1;
            }
            return outStr.ToString();

        }

        static string ColumnCode(string InputStr, string key)
        {
            char[] inp=InputStr.ToCharArray();
            int[] prior = new int[key.Length];
            key.ToLower();
            int prioriti = 0;
            for (char i = 'а'; i <= 'я'; i++)
                {
                    for(int index=0;index<key.Length;index++)
                    {
                        if (key[index] == i)
                        {
                            prior[index] = prioriti;
                            prioriti++;
                        }
                    }
                }
            StringBuilder res = new StringBuilder();
            bool b=true;
            while (b)
            {
                b = false;
                int index = -1;
                int MaxPrior = int.MaxValue;
                for (int i = 0; i < key.Length; i++)
                {
                    if (prior[i] < MaxPrior)
                    {
                        MaxPrior = prior[i];
                        index = i;
                        b = true;
                    }
                }
                int k = 0;
                if (index != -1)
                {
                    while (index + (k * key.Length) < InputStr.Length)
                    {
                        res.Append(inp[index + (k * key.Length)]);
                        k++;
                    }
                    prior[index] = int.MaxValue;
                }
            }

            return res.ToString();
        }
        static string ColumnDeCode(string InputStr,string key)
        {
            char[] inpStr = InputStr.ToCharArray();
            key.ToLower();
            int[] Length = new int[key.Length];

            for (int i = 0; i < key.Length;i++ )
            {
                Length[i]=InputStr.Length / key.Length; 
            }
            int temp = Length[0] * key.Length;
            for (int i = 0; i + temp < InputStr.Length;i++ )
            {
                Length[i] += 1;
            }

            int[] prior = new int[key.Length];
            int prioriti = 0;
            for (char i = 'а'; i <= 'я'; i++)
            {
                for (int index = 0; index < key.Length; index++)
                {
                    if (key[index] == i)
                    {
                        prior[index] = prioriti;
                        prioriti++;
                    }
                }
            }

            char[] res = new char[InputStr.Length];
            bool b = true;
            int k = 0;
            while (b)
            {
                b = false;
                int index = -1;
                int MaxPrior = int.MaxValue;
                for (int i = 0; i < key.Length; i++)
                {
                    if (prior[i] < MaxPrior)
                    {
                        MaxPrior = prior[i];
                        index = i;
                        b = true;
                    }
                }
                int j=0;
                if(index!=-1)
                {
                    while (j < Length[index])
                    {

                        res[index + (j * key.Length)] = inpStr[k + j];
                        j++;

                    }
                        k += j;
                        prior[index] = int.MaxValue;
                    }
            }
            return res.ToString();

        }

        
        static string LatticeCode(string InputStr)
        {
            int i=1;
            char[] inpStr = InputStr.ToCharArray();
            for(;i*i<InputStr.Length;i++)
            { }
            int SideLength = i;

            char[] Lattice = new char[SideLength * SideLength];
            int counter = 0;
            i = 0;
            int temp = 0;
            int k=0;
            int j = 0;
            Boolean b = true;
            while(counter<InputStr.Length)
            {
                if (k > SideLength)
                {
                    temp += 1;
                    k = 0;
                    i = temp;   
                }
                
                j = 0;
                while ((j+1 < (SideLength - k)) && (counter < InputStr.Length))
                {
                    switch (i % 4)
                    {
                        case 0:
                            {
                                Lattice[k/2* SideLength + k/2 + j] = inpStr[counter];
                                i++;
                                counter++;
                                break;
                            }
                        case 1:
                            {
                                Lattice[(SideLength*(k/2+j+1))-k/2-1] = inpStr[counter];
                                i++;
                                counter++;
                                break;
                            }
                        case 2:
                            {
                               
                                Lattice[(SideLength - k/2) * SideLength-1 - j-k/2] = inpStr[counter];
                                i++;
                                counter++;
                                break;
                            }
                        case 3:
                            {
                                Lattice[(SideLength-j-1-k/2) * (SideLength)+k/2] = inpStr[counter];
                                i++;
                                counter++;
                                break;
                            }
                            
                    }
                    if ((SideLength % 2 == 1) && (SideLength - k == 3) && (j == 1)&&b)
                    {
                        Lattice[SideLength * SideLength / 2] = inpStr[counter];
                        counter++;
                        b = false;
                        break;
                    }
                    j++;
                }  
                 k+=2;
                }


            string res = new string(Lattice);
            return res;
           

        }

        static string LatticeDecode(string InputStr)
        {
            char[] inpStr = InputStr.ToCharArray();
            int SideLength = (int)Math.Sqrt(InputStr.Length);
            var res = new StringBuilder();

            int counter = 0;
            int i = 0;
            int temp = 0;
            int k = 0;
            int j = 0;
            Boolean b = true;

            while (counter < InputStr.Length)
            {
                if (k > SideLength)
                {
                    temp += 1;
                    k = 0;
                    i = temp;
                }

                j = 0;
                while ((j + 1 < (SideLength - k)) && (counter < InputStr.Length))
                {
                    switch (i % 4)
                    {
                        case 0:
                            {
                                res.Append(inpStr[k / 2 * SideLength + k / 2 + j]);
                                i++;
                                counter++;
                                break;
                            }
                        case 1:
                            {
                                res.Append(inpStr[(SideLength * (k / 2 + j + 1)) - k / 2 - 1]);
                                i++;
                                counter++;
                                break;
                            }
                        case 2:
                            {

                                res.Append(inpStr[(SideLength - k / 2) * SideLength - 1 - j - k / 2]);
                                i++;
                                counter++;
                                break;
                            }
                        case 3:
                            {
                                res.Append(inpStr[(SideLength - j - 1 - k / 2) * (SideLength) + k / 2]);
                                i++;
                                counter++;
                                break;
                            }

                    }
                    if ((SideLength % 2 == 1) && (SideLength - k == 3) && (j == 1) && b)
                    {
                        res.Append(inpStr[SideLength * SideLength / 2]);
                        counter++;
                        b = false;
                        break;
                    }
                    j++;
                }
                k += 2;
            }
            return res.ToString();

        }

        static string cesorShifr(string InputStr,int key)
        {
            char[] InpStr = new char[InputStr.Length];
            InpStr = InputStr.ToCharArray();
            char[] OutStr = new char[InputStr.Length];
            for(int i=0;i<InputStr.Length;i++)
            {
                int temp = InpStr[i];
                temp -= 'а';
                temp +=key;
                temp =temp % ((int)'я' -(int)'а'+1);
                temp += 'а';
                OutStr[i]=(char)(temp);
            }
            string res = new string(OutStr);
            return res;
        }
        static string cesorDeShifr(string InputStr,int key)
        {
            char[] InpStr = new char[InputStr.Length];
            InpStr = InputStr.ToCharArray();
            char[] OutStr = new char[InputStr.Length];
            for (int i = 0; i < InputStr.Length; i++)
            {
                int temp = InpStr[i];
                temp -= 'а';
                temp = temp+'я'-'а'+1-key;
                temp = temp % ((int)'я' - (int)'а'+1);
                temp += 'а';
                OutStr[i] = (char)(temp);
            }
            string res = new string(OutStr);
            return res;
        }
    }
}
