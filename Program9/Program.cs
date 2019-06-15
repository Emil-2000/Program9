using System;
using System.Linq;


namespace Program9
{
    class Program
    {

        static void Main(string[] args)
        {


            Console.Write("Введите количество элементов в стэке N:");
            int N = CheckInput(true, true);
            MyStack stack = new MyStack(N);
            for (int i = 0; i < N; i++)
            {
                Console.Write("Введите " + i + " элемент из " + N + ":");
                int Current = CheckInput();
                stack.Add(Current);
            }

            Console.WriteLine("\nЧисло элементов в стэке " + stack.Count);
            Console.WriteLine("Первый элемент стэка:" + stack.getElement);
            Console.WriteLine("Второй элемент стэка:" + stack.getElement);
            // Номер элемента для вывода
            int Num = 3;
            Console.WriteLine("элемент стэка с заданным номером (" + Num + "):" + stack.getElementAt(Num));
            // Номер элемента для удаления
            int DelNum = 2;
            Console.WriteLine("Удаляем элемент из стэка с номером " + DelNum);
            // удаляем заданный номер в стэке
            stack.DeleteAt(DelNum);

            Console.WriteLine("\nВывод содержимого стэка");
            stack.Reset();
            for (int i = 0; i < stack.Count; i++)
            {
                Console.WriteLine("Элемент " + i + " стэка:" + stack.getElement);
            }
            Console.ReadLine();
        }


        /// <summary>
        /// Метод ответственный за контроль ввода целых значений с клавиатуры
        /// </summary>
        /// <param name="OnlyPositive">Проверять на положительные значения</param>
        /// <param name="ExceptZero">Проверять на ноль</param>
        /// <returns></returns>
        public static int CheckInput(bool OnlyPositive = false, bool ExceptZero = false)
        {
            int resultat = 0;
            bool ok = false;
            do
            {
                string InputetString = Console.ReadLine();
                try
                {
                    resultat = Convert.ToInt32(InputetString);
                    ok = true;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка ввода!!! Необходимо ввести целое число!!!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Повторите ввод");
                    ok = false;
                }
                if (ok == true)
                {
                    // проверим условия заданные в параметрах метода
                    if (OnlyPositive)
                    {
                        if (resultat < 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ошибка !!! Необходимо ввести целое положительное значение!!!");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Повторите ввод");
                            ok = false;
                        }
                    }
                    if (ExceptZero)
                    {
                        if (resultat == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ошибка !!! Значение не должно равняться нулю!!!");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Повторите ввод");
                            ok = false;
                        }
                    }
                }
            } while (ok == false);
            return resultat;
        }
    }

    /// <summary>
    /// Класс, который моделирует работу стэка по заданным параметрам
    /// </summary>
    class MyStack
    {
        /// <summary>
        /// указывает текущщую позицию в списке
        /// </summary>
        private int Counter = 0;

        /// <summary>
        /// Количество элементов в стэке
        /// </summary>
        public int Count
        {
            get
            {
                return Counter;
            }
        }

        /// <summary>
        /// Текущая позиция в очереди
        /// </summary>
        private int Position = 0;

        /// <summary>
        /// буффер
        /// </summary>
        private int[] buffer;

        /// <summary>
        /// простой конструктор
        /// </summary>
        public MyStack()
        {
            buffer = new int[5];
        }

        /// <summary>
        /// Конструктор с заданным размером
        /// </summary>
        /// <param name="Size"></param>
        public MyStack(int Size)
        {
            if (Size <= 0)
                throw new Exception("Размер стэка не может быть отрицательным или равным нулю");
            buffer = new int[Size];
        }

        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="Value"></param>
        public void Add(int Value)
        {
            if ((Counter - 1) >= buffer.Count())
            {
                Array.Resize(ref buffer, buffer.Count() * 2);
            }
            // присваиваем значение
            buffer[Counter] = Value;
            // Увеличиваем счетчик
            Counter++;
            // Сдвигаем текущую позицию
            Position = Count - 1;
        }

        /// <summary>
        /// Извлечение элемента из стакана
        /// </summary>
        public int getElement
        {
            get
            {
                if (Position < 0 || Position >= Count)
                {
                    Position = 0;
                    // выдать ошибку
                    //throw new Exception("Записи кончились");
                    return int.MinValue;
                }
                //Position--;
                return buffer[Position--];

            }
        }

        /// <summary>
        /// Возравщает элемент по заданному индексу
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public int getElementAt(int Position)
        {
            if (Position < 0 || Position >= Count)
            {
                throw new Exception("Заданный индекс находится вне пределов диапазона");
            }
            return (buffer[Count - Position - 1]);
        }

        /// <summary>
        /// Удаляет элемент в заданной позиции
        /// </summary>
        /// <param name="Position"></param>
        public void DeleteAt(int Position)
        {
            if (Position < 0 || Position >= Count)
            {
                throw new Exception("Заданный индекс находится вне пределов диапазона");
            }

            // вычисление нормальной, не перевренутой позиции элемента массива, которую следует удалить
            int RealPosition = Count - Position - 1;

            // создаем новый массив
            int[] nArray = new int[Count - 1];
            // добавляем в него элементы до заданного индекса
            for (int i = 0; i < RealPosition; i++)
            {
                nArray[i] = buffer[i];
            }
            // добавляем в него элементы после заданного индекса
            for (int i = RealPosition + 1; i < Count; i++)
            {
                nArray[i - 1] = buffer[i];
            }
            // присваиваем буферу новый массив
            buffer = nArray;
            // уменьшаем счетчик
            Counter = nArray.Count(); ;
            // сбрасываем текущий курсор
            Reset();
        }

        /// <summary>
        /// Сбрасывает позицию стакана
        /// </summary>
        public void Reset()
        {
            Position = Counter - 1;
        }


    }
}