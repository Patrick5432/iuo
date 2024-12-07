class Program
{
    public static void Main(string[] args)
    {
        List<double> numbers = new List<double>();
        for (int i = 0; i < 4; i++)
        {
            while (true)
            {
                if (i == 3) { Console.WriteLine("Для второго задания:"); }
                Console.WriteLine($"Введите число {i + 1}:");
                string input = Console.ReadLine();
                if (double.TryParse(input, out double oneNumber))
                {
                    numbers.Add(oneNumber);
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                }
            }
        }

        bool check1 = SquareAlingmenr(numbers[0], numbers[1], numbers[2]);
        bool check2 = CubeAlingment(numbers[0], numbers[1], numbers[2], numbers[3]);

        if (!check1 || !check2)
        {
            Console.WriteLine("Желаете повторить?\nДа:1\nНет:2");
            int checkQuestion;
            while (!int.TryParse(Console.ReadLine(), out checkQuestion) || (checkQuestion != 1 && checkQuestion != 2))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 или 2.");
            }

            switch (checkQuestion)
            {
                case 1:
                    Main(args);
                    break;
                case 2:
                    Console.WriteLine("Выход из программы.");
                    break;
            }
        }

        TestSquareAlignment();
        TestCubeAlignment();
        Console.WriteLine("Все тесты пройдены.");
    }

    public static void TestSquareAlignment()
    {
        Console.WriteLine("Тестирование SquareAlignment...");

        Assert(SquareAlingmenr(1, -3, 2) == true, "Квадратное уравнение с D > 0");

        Assert(SquareAlingmenr(1, -2, 1) == true, "Квадратное уравнение с D == 0");

        Assert(SquareAlingmenr(1, 0, 1) == false, "Квадратное уравнение с D < 0");

        Assert(SquareAlingmenr(0, 2, 1) == false, "Коэффициент a равен 0");

        Console.WriteLine("SquareAlignment успешно пройден.");
    }

    public static void TestCubeAlignment()
    {
        Console.WriteLine("Тестирование CubeAlignment...");

        Assert(CubeAlingment(1, -6, 11, -6) == true, "Кубическое уравнение с h > 0");

        Assert(CubeAlingment(1, -4, 6, -4) == true, "Кубическое уравнение с h == 0");

        Assert(CubeAlingment(1, 0, -3, 2) == true, "Кубическое уравнение с h < 0");

        Assert(CubeAlingment(0, 2, 3, 4) == false, "Коэффициент a равен 0");

        Console.WriteLine("CubeAlignment успешно пройден.");
    }

    public static void Assert(bool condition, string testName)
    {
        if (!condition)
        {
            throw new Exception($"Тест \"{testName}\" не пройден.");
        }
    }

    public static bool SquareAlingmenr(double a, double b, double c)
    {
        if (a == 0)
        {
            Console.WriteLine("Коэффициент 'a' не может быть равен 0.");
            return false;
        }
        double discriminant = Math.Pow(b, 2) - (4 * a * c);
        if (!(discriminant < 0))
        {
            if (!(discriminant == 0))
            {
                double d1 = ((b * -1) + Math.Sqrt(discriminant)) / 2 * a;
                double d2 = ((b * -1) - Math.Sqrt(discriminant)) / 2 * a;
                Console.WriteLine($"{discriminant} {d1} {d2}");
                return true;
            }
            else
            {
                Console.WriteLine($"{discriminant} {(b * -1) / 2 * a}");
                return true;
            }
        }
        else
        {
            Console.WriteLine("Дискриминант меньше 0");
            return false;
        }
    }

    public static bool CubeAlingment(double a, double b, double c, double d)
    {
        if (a == 0)
        {
            Console.WriteLine("Коэффициент 'a' не может быть равен 0.");
            return false;
        }
        double A = b / a;
        double B = c / a;
        double C = d / a;

        double f = ((3 * B) - (A * A)) / 3;
        double g = ((2 * A * A * A) - (9 * A * B) + (27 * C)) / 27;
        double h = (g * g) / 4 + (f * f * f) / 27;

        if (h > 0)
        {
            double R = -(g / 2) + Math.Sqrt(h);
            double S = Math.Sign(R) * Math.Pow(Math.Abs(R), 1.0 / 3.0);
            double T = -(g / 2) - Math.Sqrt(h);
            double U = Math.Sign(T) * Math.Pow(Math.Abs(T), 1.0 / 3.0);

            double x1 = (S + U) - (A / 3);
            Console.WriteLine($"Один действительный корень: {x1}");
            return true;
        }
        else if (h == 0)
        {
            double R = -(g / 2);
            double S = Math.Sign(R) * Math.Pow(Math.Abs(R), 1.0 / 3.0);

            double x1 = 2 * S - (A / 3);
            double x2 = -S - (A / 3);
            Console.WriteLine($"Два равных корня: {x1}, {x2}");
            return true;
        }
        else
        {
            double i = Math.Sqrt((g * g / 4) - h);
            double j = Math.Sign(i) * Math.Pow(Math.Abs(i), 1.0 / 3.0);
            double k = Math.Acos(-(g / (2 * i)));
            double L = -j;
            double M = Math.Cos(k / 3);
            double N = Math.Sqrt(3) * Math.Sin(k / 3);
            double P = -(A / 3);

            double x1 = 2 * j * Math.Cos(k / 3) + P;
            double x2 = L * (M + N) + P;
            double x3 = L * (M - N) + P;

            Console.WriteLine($"Три действительных корня: {x1}, {x2}, {x3}");
            return true;
        }
    }
}