using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

namespace ConsoleApp18
{
    partial class Program
    {
        // все эти переменные нужны для работы
        static int direction; // 1 - вверх, 2 - вправо, 3 - вниз, 4 - влево
        static List<int[]> snake; // каждая ячейка в змейке это массив длиной 2. Индекс 0 это Х, индекс 1 это Y
        static int speed; // задержка в мс
        static Thread threadSnake; // отдельный поток для цикла с движением змейки
        static Graphics graphics; // специальный класс для рисования
        static Random random = new Random(); // рандомайзер для яблока
        static int[] apple = new int[2]; // координаты яблока
        static int[] poisonApple = new int[2]; //координаты отравленного яблока
        static int gameScore = 0; // кол-во очков
        static bool gameRunning = true; // если выставить в false, змейка перестанет бежать
        static bool gamePause = false; // если выставить в true, змейка перестанет бежать, обратное переключение запустит змейку вновь
        static bool controlBlock = false; // блокировка управления

        static void Main(string[] args)
        {
            graphics = Graphics.FromHwnd(
                Process.GetCurrentProcess().MainWindowHandle);
            graphics.Clear(Color.Black); // очистка экрана
            InitSnake(); // начальная инициализация змейки
            GenerateApple(); // генерация яблока
            GeneratePoisonApple(); // генерация отравленного яблока
            threadSnake = new Thread(RunSnake); // создание потока для движения змейки
            threadSnake.Start(); // запуск потока
            RunConrol(); // запуск цикла с управлением
        }

        private static void RunConrol()
        {
            while (gameRunning)
            {   // в цикле читаем нажатую кнопку.
                ConsoleKeyInfo key = Console.ReadKey();
                Console.SetCursorPosition(0, 0);
                if (controlBlock)   // если controlBlock стоит в значении true, то переход к следующей итерации
                    continue;
                controlBlock = true; // временная блокировка управления, снимается в GetNextCoordinates
                int oldDirection = direction;
                CheckPressedKey(key); // меняем направление по клавишам
                if (gamePause)
                    controlBlock = false;
                if (snake.Count > 1 && oldDirection != direction && oldDirection % 2 == direction % 2) // если направление было изменено на противоположное
                {
                    GameOver(); // закончить игру
                    Restart(); // запрос с рестартом игры
                }
            }
        }

        private static void RunSnake()
        {
            while (gameRunning)
            {
                Thread.Sleep(speed); // ожидание (эмуляция скорости)
                if (gamePause) // если игра на паузе, пропускаем игровые механики
                    continue;
                int[] nextStep = GetNextCoordinates(); // расчет новых координат
                CleanTail(); // затираем хвост черным
                ReindexBody(); // перемещаем координаты ячеек внутри змейки
                ChangeHeadCoordinate(nextStep); // меняем координаты головы
                DrawHead(); // рисуем голову
                if (CheckSnakeIntersect()) // проверка на то, что змейка пересекла себя
                {
                    GameOver(); // стоп игры и вывод итогов
                    Restart(); // запрос с рестартом игры
                }
                else if (SnakeEatApple()) // проверка на то, что змейка пересекла яблоко
                {
                    IncreaseSnake(); // увеличение длины змейки
                    IncreaseGameScore(); // увеличение кол-во очков
                    IncreaseGameSpeed(); // увеличение скорости движения змейки
                    GenerateApple(); // генерация нового яблока
                }
                else if (SnakeEatPoisonApple()) // проверка на то, что змейка пересекла отравленное яблоко
                {
                    DecreaseSnake(); // уменьшение длины змейки
                    DecreaseGameScore(); // уменьшение количества очков
                    GeneratePoisonApple(); // генерация нового яблока
                    VivodOchkov(); // вывод очков в заголовок
                }
            }
        }

        private static void ReindexBody()
        {   // сдвиг ячеек внутри змейки
            // нужно для отслеживания пересечения змейки с головой
            int maxIndex = snake.Count - 1;
            if (maxIndex == 0)
                return;
            for (int i = maxIndex; i > 0; i--)
                snake[i] = snake[i - 1];
        }
        private static void ProverkaPoisonApple()
        {
            if (SnakeEatPoisonApple() && gameScore == 0)
            {
                GameOver(); // прекращение игры
                Restart(); // запрос с рестартом игры
            }
        }
    }
}
