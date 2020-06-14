using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача_7
{
    class Program
    {
        // реальная функция, создающая кодирующий алфавит на основе длин слов
        public static string[] Code(int[] alf, string end)
        {
            if (alf.Length == 0) return new string[0];

            string[] ans = new string[alf.Length];
            int count = 0, count_noone = 0;

            for (int i = 0; i < alf.Length; i++)
                if (alf[i] == 1 && count < 2) ans[i] = count++ % 2 + end;
                else if (count >= 2) throw new Exception("Ошибка глубины");
                else if (alf[i] > 1) count_noone++;

            if (count == 2 && count_noone > 0) throw new Exception("Ошибка глубины");

            if (count == 1)
            {
                int[] help_int = new int[alf.Length - count];
                int find = 0;
                for (int i = 0; i < alf.Length; i++)
                    if (alf[i] == 1) find++;
                    else help_int[i - find] = alf[i] - 1;
                string[] help_str = Code(help_int, count % 2 + end);
                find = 0;
                for (int i = 0; i < ans.Length; i++)
                    if (alf[i] == 1) find++;
                    else ans[i] = help_str[i - find];
            }

            if (count == 0)
            {
                int[] alf2 = new int[alf.Length / 2];
                int[] alf1 = new int[alf.Length - alf2.Length];
                int[] pos1 = new int[alf1.Length];
                int[] pos2 = new int[alf2.Length];
                int num1 = 0;
                int num2 = 0;
                bool time = true;
                for (int i = 2; i <= alf.Max(); i++)
                    for (int j = 0; j < alf.Length; j++)
                        if (alf[j] == i)
                        {
                            if (time)
                            {
                                alf1[num1] = i - 1;
                                pos1[num1++] = j;
                            }
                            else
                            {
                                alf2[num2] = i - 1;
                                pos2[num2++] = j;
                            }
                            time = !time;
                        }
                string[] str1 = Code(alf1, "0" + end);
                string[] str2 = Code(alf2, "1" + end);
                for (int i = 0; i < pos1.Length; i++) ans[pos1[i]] = str1[i];
                for (int i = 0; i < pos2.Length; i++) ans[pos2[i]] = str2[i];
            }
            return ans;
        }
        // функция для пользования. вызывает предыдущую функцию
        public static string[] ToCode(int[] alf)
        {
            return Code(alf, null);
        }
        // сортирует полученный кодирующий алфавит в лексикографическом порядке
        public static string[] LexGraphSort(string[] strs)
        {
            int num = 1, j = num;

            while (num < strs.Length)
            {
                for (int i = j - 1; i >= 0; i--)
                {
                    int count = 0;
                    for (int k = 0; k < Math.Min(strs[i].Length, strs[j].Length); k++)
                    {
                        if (strs[i][k] < strs[j][k]) { count = 1; break; }
                        else if (strs[i][k] > strs[j][k]) { count = -1; break; }
                    }
                    string help_str = strs[i];
                    if ((count == 0 && strs[i].Length > strs[j].Length) || count == -1)
                    {
                        strs[i] = strs[j];
                        strs[j] = help_str;
                        if (i != 0) j = i;
                        else j = ++num;
                    }
                    else if ((count == 0 && strs[i].Length < strs[j].Length) || count == 1)
                    {
                        j = ++num;
                        break;
                    }
                }
            }
            return strs;
        }
        public static int EnterInt()
        {
            while (true)
            {
                try
                {
                    int a = Convert.ToInt32(Console.ReadLine());
                    if (a <= 0) throw new Exception();
                    return a;
                }
                catch { Console.WriteLine("Неверный ввод, повторите"); }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите длину кодирующего алфавита");
            int count = EnterInt();
            int[] alf = new int[count];

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Введите длину {0} буквы кодирующего алфавиту", i + 1);
                alf[i] = EnterInt();
            }

            try
            {
                string[] ans = LexGraphSort(ToCode(alf));
                Console.WriteLine("\nКодирующий алфавит в лексикографическом порядке:");
                foreach (string str in ans) Console.WriteLine(str);
            }
            catch (Exception c) { Console.WriteLine("Из данного набора длин слов невозможно получить кодирующий алфавит"); }
            Console.ReadKey();

        }
    }
}
