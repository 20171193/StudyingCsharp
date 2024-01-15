/****************************************************************
 * 이벤트 (Event)
 * 
 * 일련의 사건이 발생했다는 사실을 다른 객체에게 전달
 * 델리게이트의 일부 기능을 제한하여 이벤트의 용도로 사용
 ****************************************************************/

namespace _06._Event
{
    /// <summary>
    /// 이벤트 기본
    /// </summary>
    internal class Program
    {
        // <이벤트 선언>
        // 델리게이트 변수 앞에 event 키워드를 추가하여 이벤트로 선언
        public class Player
        {
            public Action _OnGetCoin;           // 델리게이트 추후 문제 발생. (85줄에서 다룸.)
            public event Action OnGetCoin;      // 이벤트
            public void GetCoin()
            {
                Console.WriteLine("플레이어가 코인을 얻음");

                if (OnGetCoin != null)
                    OnGetCoin();                // 일련의 사건이 발생했을 때 이벤트 발생
            }
        }


        // <이벤트 등록 & 해제>
        // 이벤트에 반응할 객체의 추가할 함수를 += 연산자를 통해 참조 추가
        // 이벤트에 반응할 객체의 제거할 함수를 -= 연산자를 통해 참조 제거
        void Main1()
        {
            Player player = new Player();
            UI ui = new UI();
            SFX sfx = new SFX();
            VFX vfx = new VFX();

            // 이벤트에 반응할 객체의 함수 추가
            player.OnGetCoin += ui.UpdateUI;
            player.OnGetCoin += sfx.CoinSound;

            // 일련의 사건이 발생했을 때 이벤트를 통한 반응
            player.GetCoin();
            // 플레이어가 코인을 얻음
            // UI에 코인수를 갱신
            // 코인을 얻는 효과음 재생

            // 이벤트 방식으로 코드 수정없이 이벤트시 반응할 객체를 추가 가능
            player.OnGetCoin += vfx.CoinEffect;

            player.GetCoin();
            // 플레이어가 코인을 얻음
            // UI에 코인수를 갱신
            // 코인을 얻는 효과음 재생
            // 코인을 얻는 반짝거리는 효과

            // 이벤트 방식으로 코드 수정없이 이벤트시 반응할 객체를 제거 가능
            player.OnGetCoin -= sfx.CoinSound;

            player.GetCoin();
            // 플레이어가 코인을 얻음
            // UI에 코인수를 갱신
            // 코인을 얻는 반짝거리는 효과
        }

        public class UI
        {
            public void UpdateUI() { Console.WriteLine("UI에 코인수를 갱신"); }
        }

        public class SFX
        {
            public void CoinSound() { Console.WriteLine("코인을 얻는 효과음 재생"); }
        }

        public class VFX
        {
            public void CoinEffect() { Console.WriteLine("코인을 얻는 반짝거리는 효과"); }
        }
    }

    // <문제점>
    // 1. 델리게이트 체인 = (대입) 가능.
    //  - 기존에 추가해놓았던 이벤트들이 모두 사라질 수 있음.
    // 2. 이벤트 발생이 외부에서 가능.
    //  - 이벤트는 public이기 때문에 원치않게 외부에서 내부적으로 은닉된 함수를 호출할 수 있음 

    // <해결>
    // event 키워드를 활용하여 델리게이트를 이벤트로 선언.
    // 이를 통해 이벤트를 외부에서 사용이 불가하게 변경.
    //  EX) public Action act; --> public event Action act;

    // 설계
    // <Call 방식>
    // 직관적이지만, 개방폐쇄원칙 위반.
    class Call
    {
        // Player가 UI를 가지고 필요시 UI 메서드를 Call.
        //  *UI, Sound등이 추가될 때마다 Player를 계속해서 수정/추가해야 함. (개방폐쇄원칙 위반)
    }
    // <Polling 방식>
    // 개방폐쇄원칙을 준수하지만, 변경사항이 없더라도 계속 확인해야하는 불필요한 연산 수행.
    class Polling
    {
        // UI가 Player를 가지고 매 프레임마다 조건을 체크하여 조건이 충족되면 메서드 실행.
        //  *Call과 다르게 Player를 수정할 필요는 없음. (개방폐쇄원칙 준수)
        //  *매 프레임 계속 확인을 하는 등 불필요한 연산을 수행해야함.

    }
    // <Event 방식>
    // 개방폐쇄원칙 준수하고 불필요한 연산을 수행하지 않음.
    class Event
    {
        // Player에 event를 두고 UI, Sound 등이 메서드를 할당.
        //  *Polling과 마찬가지로 개방폐쇄원칙 준수.
        //  *event라는 것을 따로 구현을 해야한다는 점. (추가적인 코드설계)
        //  *Call, Polling 보다 실행속도가 느려질 수 있음.
    }


    #region 이벤트 실습
    //  1.플레이어가 코인을 얻을때 발생하는 이벤트를 구현하자
    //  2.이벤트에 반응하는 UI, SFX, VFX 객체를 구현하자
    //  3.플레이어가 코인을 얻을때 발생하는 이벤트에 반응하도록 UI, SFX, VFX를 참조하자
    //  4.플레이어가 코인을 얻으면 UI, SFX, VFX가 반응하는지 확인하자

    //  A++) 방어구의 내구도 시스템을 구현해보자
    //   플레이어가 방어구를 착용하고, 플레이어 피격시마다 내구도가 1감소하도록 구현.
    //   내구도가 0이 되면 방어구가 해제되도록 구현하자.

    // < 1~4, A++ >
    public interface IEquipmentable
    {
        public void Equip(Equipment equipment);
    }
    public class Player : IEquipmentable
    {
        private int ownCoin;    // 코인
        public int OwnCoin { get { return ownCoin; } }

        private int ownHp;  // 체력
        public int OwnHp { get { return ownHp; } }

        public event Action OnGetCoin;      // 코인획득 이벤트
        public event Action<IEquipmentable> OnEquip;    // 방어구 장착 이벤트
        public event Action OnTakeDamage;   // 피격 시 이벤트

        public Armour myAmour;

        public Player()
        {
            ownCoin = 0;
            ownHp = 100;
        }

        public void GetCoin()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"코인 획득!");
            Console.ResetColor();
            ownCoin += 50;
            if (OnGetCoin != null)
                OnGetCoin();
        }

        #region A++
        // 이벤트 구현
        public void Equip(Equipment equipment)
        {
            Console.WriteLine("플레이어가 장비를 장착합니다.");
            myAmour = (Armour)equipment;
            if (OnEquip != null)
            {
                OnEquip(this);
            }
        }

        public void UnEquip(Equipment equipment)
        {
            // 방어구 해제 구현
            myAmour = null;
        }

        public void TakeDamage(int damage)
        {
            // 이벤트 발생 구현
            if (myAmour == null)
            {
                Console.WriteLine($"플레이어가 {damage}의 피해를 입었습니다.");
                if (ownHp - damage > 0)
                {
                    ownHp -= damage;
                }
                else
                {
                    // 추후 사망처리
                }
            }
            else
            {
                // Call 방식
                myAmour.OnDamage(damage);
            }
        }

        #endregion
    }
    #region A++
    public abstract class Equipment
    {
        protected IEquipmentable owner;
        protected int durability;
        public int Durability { get { return durability; } }

        public abstract void Equip(IEquipmentable owner); // 방어구 착용시 반응 구현
        public abstract void OnDamage(int value); // 피격시 행동 구현
    }

    public class Armour : Equipment
    {
        public Armour(IEquipmentable owner)
        {
            this.owner = owner;
            durability = 5;
            if (owner is Player)
            {
                Player player = (Player)owner;
                player.OnEquip -= Equip;
                player.OnEquip += Equip;
            }
        }

        public override void Equip(IEquipmentable owner)
        {
            Player player = owner as Player;
            player.myAmour = this;
            Console.WriteLine($"갑옷을 장착합니다. 내구도 : {durability}");
        }

        public override void OnDamage(int value)
        {
            if (durability - value < 1)
            {
                Console.WriteLine($"갑옷이 {value}의 피해를 입어 파괴됩니다.");
                durability = 0;
                if (owner is Player)
                {
                    Player player = (Player)owner;
                    player.OnEquip -= Equip;
                    player.UnEquip(this);
                }
            }
            else
            {
                Console.WriteLine($"갑옷이 {value}의 피해를 흡수합니다.");
                durability -= value;
            }
        }
    }

    #endregion

    public interface ICoinEventReferecable
    {
        public void GetCoinRefer();
    }

    class GameEnvironment
    {
        protected Player player;
    }

    class UI : GameEnvironment, ICoinEventReferecable
    {
        public UI(Player player)
        {
            this.player = player;
            player.OnGetCoin -= GetCoinRefer;   // 예외처리 (이미 할당되어있다면 해제 후 다시 할당)
            player.OnGetCoin += GetCoinRefer;
        }

        public void UIRendering()
        {
            Console.WriteLine("******** 플레이어 정보 ********");
            Console.Write(" $ : ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(player.OwnCoin);
            Console.ResetColor();
            Console.Write(" | HP : ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(player.OwnHp);
            Console.ResetColor();
            if (player.myAmour != null)
            {
                Console.Write(" | 갑옷 : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(player.myAmour.Durability);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public void GetCoinRefer()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("UI 출력");
            Console.ResetColor();
        }
    }
    class SFX : GameEnvironment, ICoinEventReferecable
    {
        public SFX(Player player)
        {
            this.player = player;
            player.OnGetCoin -= GetCoinRefer;
            player.OnGetCoin += GetCoinRefer;
        }
        public void GetCoinRefer()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("사운드 출력");
            Console.ResetColor();
        }
    }
    class VFX : GameEnvironment, ICoinEventReferecable
    {
        public VFX(Player player)
        {
            this.player = player;
            player.OnGetCoin -= GetCoinRefer;
            player.OnGetCoin += GetCoinRefer;
        }

        public void GetCoinRefer()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("이펙트효과 출력");
            Console.ResetColor();
        }
    }

    class Game
    {
        private Player player;
        private UI ui;
        private SFX sfx;
        private VFX vfx;

        public bool loopGame;

        public Game()
        {
            player = new Player();
            ui = new UI(player);
            sfx = new SFX(player);
            vfx = new VFX(player);
            loopGame = true;
        }

        public void Render()
        {
            ui.UIRendering();
        }
        public ConsoleKey UserInput()
        {
            Console.Write("1:코인 | 2:피격 | 3:갑옷장착 | ESC:종료 ");
            ConsoleKey inputKey = Console.ReadKey().Key;
            Console.WriteLine();
            Console.WriteLine();
            return inputKey;
        }
        public void GameLoop()
        {
            ConsoleKey inputKey;
            while (loopGame)
            {
                Render();
                inputKey = UserInput();
                switch (inputKey)
                {
                    case ConsoleKey.D1:
                        player.GetCoin();
                        break;
                    case ConsoleKey.D2:
                        player.TakeDamage(1);
                        break;
                    case ConsoleKey.D3:
                        if (player.myAmour == null)
                            player.Equip(new Armour(player));
                        else
                            Console.WriteLine("이미 존재하는 장비입니다.");
                        break;
                    case ConsoleKey.Escape:
                        loopGame = false;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
                if (inputKey != ConsoleKey.Escape)
                    Thread.Sleep(1200);
                Console.Clear();
            }
        }
    }

    class MyGame
    {
        static void Main(string[] argc)
        {
            Game game = new Game();
            game.GameLoop();
        }
    }


    // < 5 >
    //  5.두 수를 입력받고 숫자1 / 숫자2 의 결과를 출력하도록 하자.단, 숫자2가 0인 경우 예외처리를 통해 0으로 나눌 수 없다고 출력하도록 하자.
    //class TestException
    //{
    //    static void Main(string[] argc)
    //    {
    //        try
    //        {
    //            Console.Write("1번 숫자를 입력하시오 : ");
    //            int num1 = int.Parse(Console.ReadLine());
    //            Console.Write("2번 숫자를 입력하시오 : ");
    //            int num2 = int.Parse(Console.ReadLine());

    //            Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
    //        }
    //        catch(DivideByZeroException ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            Console.WriteLine("0으로 나눌 수 없습니다.");
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            Console.WriteLine("입력이 잘못되었습니다.");
    //        }
    //    }
    //}
    #endregion
}