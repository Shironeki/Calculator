using System;
using System.Collections.Generic;
using System.Linq;

namespace Калькулятор
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Введите первое число: ");
            string number1 = Console.ReadLine();
            Console.Write("Введите второе число: ");
            string number2 = Console.ReadLine();
            try
            {
                double m1 = double.Parse(number1);
                double m2 = double.Parse(number2);
            }
            catch (Exception e)
            {
                Console.WriteLine("Неправильный ввод данных: {0}", e.Message);
                return;
            }
            Console.Write("Введите нужную операцию (Умножение(*), деление(/), произведение(+), вычитание(-)): ");
            string sign = Console.ReadLine();
            double result = 0;
            if (sign == "*") Multiplication(number1, number2, ref result);
            else if (sign == "/") Division(number1, number2, ref result);
            else if (sign == "-") Subtraction(number1, number2, ref result);
            else if (sign == "+") Sum(number1, number2, ref result);
            else Console.WriteLine("Ошибка: Вы ввели неправильную операцию!");
        }
        //Умножение
        public static void Multiplication(string n1, string n2, ref double result)
        {
            //В List<double> будут храниться значения переменных при умножении
            List<double> list = new List<double> { };
            string shift = "       ";
            int rule = Rules(Convert.ToDouble(n1), Convert.ToDouble(n2));
            //Вывод примера
            string line1 = string.Format("{0}{1}\n{0}\b*\n{0}{2}\n----------------", shift, n1, n2);
            Console.WriteLine(line1);
            if (Convert.ToDouble(n1) < 0) n1 = Convert.ToString(Convert.ToDouble(n1) * -1);
            if (Convert.ToDouble(n2) < 0) n2 = Convert.ToString(Convert.ToDouble(n2) * -1);
            //Первое число используем целиком
            double ch1 = double.Parse(n1);
            //Второе число разделяем на цифры
            char[] ch2 = n2.ToCharArray();
            string p;
            double t = 0;
            for (int i = ch2.Length - 1; i >= 0; i--)
            {
                if (ch2[i] == ',')
                {

                    p = "0," + new string('0', list.Count - 1) + "1";
                    t = Convert.ToDouble(p);
                    continue;
                }
                //Умножаем каждую цифру на первое число
                double item = char.GetNumericValue(ch2[i]) * ch1;
                list.Add(item);
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(shift + list[i]);
                if (i != list.Count - 1)
                {
                    Console.WriteLine(shift + "+");
                }
                double temp = list[i] * Math.Pow(10, i);
                result += temp*rule;
            }
            if (t != 0) result *= t;
            Console.WriteLine("----------------\n{0}\b{1} ", shift, result);
        }
        //Деление
        public static void Division(string n1, string n2, ref double result)
        {
            //В List<double> будут храниться значения переменных при делении
            List<int> list1 = new List<int> { };
            List<int> list2 = new List<int> { };
            List<double> list3 = new List<double> { };
            double ch1 = double.Parse(n1);
            double ch2 = double.Parse(n2);
            //Длинна отступа
            int rzm;
            //Подсчет знаков после запятой
            int count1 = BitConverter.GetBytes(decimal.GetBits(Convert.ToDecimal(ch1))[3])[2];
            int count2 = BitConverter.GetBytes(decimal.GetBits(Convert.ToDecimal(ch2))[3])[2];
            int rule = Rules(ch1, ch2);
            if (ch1 < 0) ch1 *= -1;
            if (ch2 < 0) ch2 *= -1;
            if (count1 < count2)
            {
                ch1 *= Math.Pow(10, count2);
                ch2 *= Math.Pow(10, count2);
                rzm = count2;
            }
            else if (count1 > count2)
            {
                ch1 *= Math.Pow(10, count1);
                ch2 *= Math.Pow(10, count1);
                rzm = count1;
            }
            else
            {
                ch1 *= Math.Pow(10, count1);
                ch2 *= Math.Pow(10, count1);
                rzm = count1;
            }
            double r = ch1;
            if (r < ch2)
            {
                while (r < ch2)
                {
                    r *= 10;
                }
                list2.Add(Convert.ToInt32(r));
            }
            else list2.Add(Convert.ToInt32(r));
            //Вывод примерa
            string shift = "       ";
            double p = 1;
            while (ch1 > 0)
            {
                double temp;
                if (ch1 < ch2)
                {
                    while (ch1 < ch2)
                    {
                        ch1 *= 10;
                        p *= 0.1;
                    }
                    list2.Add(Convert.ToInt32(ch1));
                }
                //Полученное число при умножении
                int a = Convert.ToInt32(ch2);
                int b = 0;
                int sum = 0;
                while (ch1 >= a)
                {
                    b += a;
                    ch1 -= a;
                    sum++;
                }
                list1.Add(b);
                temp = p * sum;
                list3.Add(temp);
                if (list3.Count == 4) break;
            }
            double c = 0;
            //Подсчет
            for (int i = 0; i < list3.Count; i++)
            {
                c += list3[i];
            }
            result = c * rule;
            string line1 = string.Format("{0}{1}{0}|{2}\n{0}\b-" + "{0}"
            + new string(' ', rzm) + "----------", shift, list2[0], ch2);
            Console.WriteLine(line1);
            //Вывод второй строки
            string line2 = string.Format("{0}{1}{0}|{2}\n{0}" + new string('-', list1[0].ToString().Length), shift, list1[0], result);
            Console.WriteLine(line2);
            //Вывод оставшейся части вычисления
            for (int i = 1; i < list1.Count; i++)
            {
                string line3 = string.Format("{0}" + new string(' ', i) + "{2}\n{0}" + new string(' ', i) + "-\n{0}"
                + new string(' ', i) + "{1}\n{0}" + new string(' ', i) + new string('-', list1[i].ToString().Length)
                , shift, list1[i], list2[i]);
                Console.WriteLine(line3);
            }
            for (int i = 0; i < list1.Count; i++)
                if (i == list1.Count - 1) Console.WriteLine("{0}" + new string(' ', i) + " {1}", shift, ch1);
        }
        //Вычитание
        public static void Subtraction(string n1, string n2, ref double result)
        {
            //В List<double> будут храниться значения переменных при вычитании
            List<double> list = new List<double> { };
            int d = RulesForSum(n1, n2);
            int max = Null(ref n1, ref n2);
            double m = 1;
            for (int i = max; i > 0; i--) { m *= 0.1; }
            string shift = "       ";
            Console.WriteLine("{0}{1}\n{0}\b-\n{0}{2}\n{0}" + new string('-', n1.Length), shift, n1, n2);
            int[] ch1 = n1.ToString().Select(c => (int)char.GetNumericValue(c)).ToArray();
            int[] ch2 = n2.ToString().Select(c => (int)char.GetNumericValue(c)).ToArray();
            int temp;
            int p = n1.Length;
            for (int i = p-1; i >= 0; i--)
            {
                temp = 0;
                if (n1[i] == ',' || n2[i] == ',') continue;
                else if (ch1.Length == 1 && ch1.Length == 1) { temp = ch1[i] - ch2[i]; list.Add(temp); break; }
                if (ch1[i] < ch2[i])
                {
                    for (int j = i; j >= 0; j--)
                    {
                        if (ch1[0] == 0 && j == 0) { temp = ch1[i] - ch2[i]; list.Add(temp); break; }
                        if (j - 1 == -1) j++;
                        if (ch1[j - 1] > 0)
                        {
                            ch1[j - 1]--; ch1[j] += 10;
                            if (j - 2 == -1) j--;
                            if (ch1[i] < ch2[i]) { j = i + 1; }
                            else { i++; break; }
                        }
                        else continue;
                    }
                    continue;
                }
                else if (ch1[i] >= ch2[i])
                {
                    temp = ch1[i] - ch2[i]; list.Add(temp);
                }
            }
            string y = "";
            for (int i = list.Count - 1; i >= 0; i--)
            {
                y += list[i];
            }
            result = Convert.ToDouble(y) * d;
            if (max != 0) result *= m;
            int o = n1.Length - result.ToString().Length;
            Console.WriteLine("{0}" + new string (' ', o) + "{1}", shift, result);
        }
        //Сложение
        public static void Sum(string n1, string n2, ref double result)
        {
            //В List<double> будут храниться значения переменных при вычитании
            List<double> list = new List<double> { };
            int d = RulesForSum(n1, n2);
            int max = Null(ref n1, ref n2);
            double m = 1;
            string shift = "       ";
            Console.WriteLine("{0}{1}\n{0}\b+\n{0}{2}\n{0}" + new string('-', n1.Length), shift, n1, n2);
            for (int i = max; i > 0; i--) { m *= 0.1; }
            int[] ch1 = n1.ToString().Select(c => (int)char.GetNumericValue(c)).ToArray();
            int[] ch2 = n2.ToString().Select(c => (int)char.GetNumericValue(c)).ToArray();
            double temp;
            int p = n1.Length - 1;
            int x = 1;
            for (int i = p; i >= 0; i--)
            {
                int one = Convert.ToInt32(ch1[i]);
                int two = Convert.ToInt32(ch2[i]);
                temp = 0;
                if (n1[i] == ',' || n2[i] == ',') continue;
                if (one + two < 10) { temp = one + two; list.Add(temp); }
                else if (one + two > 10) { temp = one + two - 10; list.Add(temp); x *= 10; }
            }
            string y = "";
            for (int i = list.Count - 1; i >= 0; i--)
            {
                y += list[i];
            }
            if (x != 1) result = Convert.ToDouble(y) + x * d;
            else result = Convert.ToDouble(y) * d;
            if (max != 0) result *= m;
            Console.WriteLine("{0}{1}", shift, result);
        }
        //Правила для деления и умножения
        public static int Rules(double one, double two)
        {
            int rule = 1;
            if (one < 0 && two > 0 || one > 0 && two < 0) { rule = -1; return rule; }
            else return rule;
        }
        //Добавление "0" для вычитания и сложения
        public static int Null(ref string ch1, ref string ch2)
        {
            //Подсчет знаков после запятой
            int count1 = BitConverter.GetBytes(decimal.GetBits(Convert.ToDecimal(ch1))[3])[2];
            int count2 = BitConverter.GetBytes(decimal.GetBits(Convert.ToDecimal(ch2))[3])[2];
            int max;
            string one = ch1.Remove(ch1.Length - count1 - 1, count1 + 1);
            string two = ch2.Remove(ch2.Length - count2 - 1, count2 + 1);
            if (one.Length < two.Length) ch1 = new string('0', two.Length - one.Length) + ch1;
            else if (one.Length > two.Length) ch2 = new string('0', one.Length - two.Length) + ch2;
            if (count1 < count2) { ch1 = ch1 + new string('0', count2 - count1); max = count2; return max; }
            else if (count1 > count2) { ch2 = ch2 + new string('0', count1 - count2); max = count1; return max; }
            else { max = count1; return max; }
        }
        public static int RulesForSum(string n1, string n2)
        {
            double one = Convert.ToDouble(n1);
            double two = Convert.ToDouble(n2);
            if (one < 0 || two < 0) return -1;
            else return 1;
        }
    }
}
