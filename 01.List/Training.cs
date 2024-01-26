using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01._List
{
    internal class Training
    {
        /***************************************************************************
         * <실습>
            * 1. 사용자에게 숫자 입력 받기
            * 2. 0부터 숫자까지 가지는 리스트 만들기
            * 3. 0요소를 제거
            * 4. 리스트가 가지는 모든 요소들을 출력해주는 반복문 작성
            * 5. 사용자의 입력을 받아서 없는 데이터면 추가, 있는 데이터면 삭제 (xor)
        ***************************************************************************/

        void MainFunction()
        {
            List<int> list = new List<int>();

            // part 1. 사용자에게 숫자 입력 받기
            int num = 0;
            int.TryParse(Console.ReadLine(), out num);

            // part 2. 0부터 숫자까지 가지는 리스트 만들기
            for (int i = 0; i <= num; i++)
                list.Add(i);

            // part 3. 0요소를 제거
            list.Remove(0);

            // part 4. 리스트가 가지는 모든 요소들을 출력해주는 반복문 작성
            foreach (int i in list)
                Console.Write($"{i} ");

            Console.WriteLine();

            // part 5. 사용자의 입력을 받아서 없는 데이터면 추가, 있는 데이터면 삭제 (xor)
            while (true)
            {
                Console.Write("없는 데이터면 추가, 있는 데이터면 삭제(종료:-1) :");
                int input = int.Parse(Console.ReadLine());
                if (input == -1) break;
                int getNum = list.IndexOf(input);
                if (getNum == -1)
                {
                    Console.WriteLine($"{input}을 추가합니다.");
                    list.Add(input);
                }
                else
                {
                    Console.WriteLine($"{input}을 삭제합니다.");
                    list.RemoveAt(getNum);
                }

                foreach (int i in list)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
            }
        }
    }

    internal class Traning_A
    {
        /***************************************************************************
        * <추가 실습>
             * A++. 인벤토리 구현 : 아이템 수집, 아이템 버리기
        ***************************************************************************/
        public class Item
        {
            private string name;
            public string Name { get { return name; } private set { name = value; } }

            public Item(string name)
            {
                Name = name;
            }
        }

        public class Player
        {
            const int Default_Inventory_Size = 6;

            private List<Item> inventory;
            public List<Item> Inventory { get { return inventory; } }

            public Player()
            {
                inventory = new List<Item>(Default_Inventory_Size);
            }

            public void GetItem(Item item)
            {
                Console.WriteLine($"{item.Name}을 인벤토리에 등록합니다.");
                inventory.Add(item);
            }
            public void ThrowItem(Item item)
            {
                int result = inventory.IndexOf(item);
                if (result == -1)
                {
                    Console.WriteLine("해당 아이템이 존재하지 않습니다.");
                }
                else
                {
                    Console.WriteLine($"{item.Name}을/를 버립니다.");
                    inventory.RemoveAt(result);
                }
            }
        }

        public class Game
        {
            private Player player;
            private List<Item> items;
            private bool gameLoop;

            public Game()
            {
                player = new Player();
                items = new List<Item>();
                items.Add(new Item("포션"));
                items.Add(new Item("검"));
                items.Add(new Item("방패"));
                items.Add(new Item("모자"));
                items.Add(new Item("신발"));
                items.Add(new Item("허브"));
                gameLoop = true;
            }
            public void RenderPlayerInventory()
            {
                Console.WriteLine("\n<인벤토리>\n");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach(Item item in player.Inventory)
                {
                    Console.Write($"{item.Name} ");
                }
                Console.ResetColor();
            }
            public void RenderItemMenu()
            {
                Console.WriteLine();
                for(int i =0; i<items.Count; i++)
                {
                    Console.Write($"{i + 1}:{items[i].Name}  ");
                }
                Console.Write("<else : 종료>");
            }
            public void RenderItemActionMenu()
            {
                Console.WriteLine();
                Console.Write("획득 : 1  |  버리기 : 2  | 종료 : 3");
            }
            public int Input()
            {
                ConsoleKeyInfo inputKey = Console.ReadKey();
                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                        return 1;
                    case ConsoleKey.D2:
                        return 2;
                    case ConsoleKey.D3:
                        return 3;
                    case ConsoleKey.D4:
                        return 4;
                    case ConsoleKey.D5:
                        return 5;
                    case ConsoleKey.D6:
                        return 6;
                    case ConsoleKey.D7:
                        return 7;
                    default:
                        return -1;
                }
            }

            public void GameLoop()
            {
                while(gameLoop)
                {
                    RenderPlayerInventory();
                    RenderItemMenu();   // 1~6, else 종료
                    int inputItemNumber = Input();
                    Console.WriteLine();

                    if (inputItemNumber < 1 && inputItemNumber > 6) return;
                    RenderItemActionMenu();     // 1~2, else 종료
                    int inputActionNumber = Input();
                    Console.WriteLine();
                    switch (inputActionNumber)
                    {
                        case 1:
                            player.GetItem(items[inputItemNumber - 1]);
                            break;
                        case 2:
                            player.ThrowItem(items[inputItemNumber - 1]);
                            break;
                        default:
                            return;
                    }
                    Console.WriteLine();

                    Thread.Sleep(1200);
                    Console.Clear();
                }
            }
        }

        public class MainProgram
        {
            void Main(string[] argc)
            {
                Game game = new Game();
                game.GameLoop();
            }
        }
    }
}
