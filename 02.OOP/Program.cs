/***********************************************************************************************************************
 
 * 객체지향 프로그래밍 - OOP(Object-Oriented Programming)
    * 절차지향 : 프로그램의 순차적인 처리를 위주로 설계하는 방법 (C)
    * 객체지향 : 서로 상호작용하는 객체를 기본 단위로 구성하는 방법 (C#, C++)
               : 비교적 복잡한 구조에 대한 설계에 용이
 
 * 객체지향 4특징
    * 1.캡슐화 : 객체를 상태와 기능으로 묶는 것.
               : 객체의 내부 상태와 기능을 *숨기고, 허용한 상태와 기능만의 *엑세스 허용.
    * 2.다형성 : 부모클래스의 함수를 자식클래스에서 *재정의하여 자식 클래스의 다른 반응을 구현.
    * 3.추상화 : 관련 특성 및 엔터티의 상호 작용을 클래스로 모델링하여 시스템의 추상적 표현을 정의.
    * 4.상속   : 부모클래스의 모든 기능을 가지는 자식 클래스를 설계.
    * 
 
 * 객체설계 5원칙 (SOLID)
    * (S)단일 책임 원칙      : 객체는 오직 하나의 책임을 가져야 함.
    * (O)개방 폐쇄 원칙      : 객체는 확장에 개방적, 수정에 폐쇄적이어야 함.
    * (L)리스코프 치환 원칙   : 자식클래스는 언제나 자신의 부모클래스를 대체할 수 있어야 함.
    * (I)인터페이스 분리 원칙 : 인터페이스는 작은 단위들로 분리시켜 구성하며, 사용하지 않는 함수는 포함하지 않아야 함.
    * (D)의존성 역전 원칙     : 객체는 하위클래스(상위클래스를 구현한 객체)보다 상위클래스(추상성이 높은 상위 개념)에 의존해야함.
 
 ************************************************************************************************************************/

namespace _02._OOP
{
    #region 추상화(Abstraction)
    abstract class Item // 하나 이상의 추상함수를 포함하는 추상클래스
    {
        // 구체화 시킬 수 없는 기능을 추상적 표현으로 선언
        // 밑에 선언한 Sell, Use 등의 기능은 각각의 Item(Potion, Bomb 등)에서 각각 구체화됨.
        public abstract void Sell();    // 추상함수는 선언만 진행.
                                        // 자식에서 구체화
        public abstract void Use();
    }
    class Potion : Item
    {
        public override void Sell()
        {
            Console.WriteLine("포션을 판매하여 30골드를 획득합니다.");
        }
        public override void Use()
        {
            Console.WriteLine("포션을 사용해 체력을 회복합니다.");
        }
    }
    class Branch : Item
    {
        public override void Sell()
        {
            Console.WriteLine("나뭇가지를 판매하여 50골드를 획득합니다.");
        }
        public override void Use() { } // 추상함수를 사용하지 않는 경우라도 꼭 구현해야함.
    }
    #endregion

    #region 캡슐화(Encapsulation)
    class Bank
    {
        // 허용한 정보와 기능의 엑세스를 허용하고 보관/보호할 정보와 기능들을 캡슐/은닉화
        
        // coin이란 변수에 대한 직접적인 접근을 private으로 막는다.
        private int coin;   // coin변수는 직접 접근이 불가.

        public void EarnCoin(int value) // 외부에서 호출 -> 내부에서 동작
        {
            coin += value;
        }
        public void SpendCoin(int value)
        {
            if (value <= coin)
            {
                coin -= value;
            }
        }
    }
    #endregion

    #region 상속(Inheritance)
    class Monster
    {
        // 공통된 기능을 Monster라는 부모클래스에서 정의합니다.
        // Monster클래스를 상속하는 자식클래스에게 모든 기능을 부여합니다.
        protected string name;  // Monster클래스를 상속한 클래스에서 접근가능.
        
        public void Move()  // 공통된 기능
        {
            Console.WriteLine($"{name} 이/가 움직입니다.");
        }
        public void NormalAttack()  // 공통된 기능
        {
            Console.WriteLine($"{name} 이/가 공격합니다.");
        }
    }
    class Zombie : Monster
    {
        public Zombie()
        {
            name = "좀비";
        }
        public void Bite()  // 자식의 추가기능
        {
            Console.WriteLine($"{name} 가 깨뭅니다.");
        }
    }
    class Slime : Monster
    {
        public Slime()
        {
            name = "슬라임";
        }
        public void Split() // 자식의 추가기능
        {
            Console.WriteLine($"{name} 이 분열합니다.");
        }
    }

    #endregion

    #region 다형성(Polymorphism)
    class Skill
    {
        // 부모클래스 Skill의 멤버 Execute는 Skill을 상속하는 자식클래스에서 다양하게 재정의가 가능.
        // Skill(FireBall, Dash)의 Execute는 각각 다르게 동작.
        public virtual void Execute()   // 가상함수 정의
        {
            Console.WriteLine("스킬 재사용 대기시간을 진행시킴");
        }
    }
    class FireBall : Skill
    {
        public override void Execute()
        {
            base.Execute();     // 부모클래스(base)의 Execute 함수 실행
                                // 꼭 부모클래스의 기능을 실행할 필요는 없음.
            Console.WriteLine("화염구 발사");

        }
    }
    class Dash : Skill
    {
        public override void Execute()
        {
            Console.WriteLine("근거리 대쉬");
        }
    }

    // 새로운 클래스를 추가하거나 확장할 때 기존 코드에 영향을 최소화할 수 있음.
    class Player
    {
        Skill skill;

        public void LearnSkill(Skill skill)
        {
            this.skill = skill;
        }

        public void UseSkill()
        {
            skill.Execute();
        }
    }

    // Heal이라는 새로운 클래스를 추가한다고 가정했을 때, 기존 소스를 수정할 필요가 없음. 
    class Heal : Skill
    {
        public override void Execute()
        {
            base.Execute();
            Console.WriteLine("체력을 회복합니다.");
        }
    }

    #endregion

    class Program
    { 
        static void Main(string[] args)
        {
            #region 추상화 사용예제
            Console.WriteLine("     < 추상화 사용 예시 >\n");
            //Item item = new Item();       // error : 추상클래스는 인스턴스생성 불가
            Item potion = new Potion();
            potion.Use();   // 구체화 한 실제기능 실행
            potion.Sell();
            Item branch = new Branch();
            branch.Use();   // 정의하지 않은 함수
            branch.Sell();
            #endregion

            #region 캡슐화 사용예제
            Bank bank = new Bank();
            //bank.coin = 100000;       // error : private으로 선언된 coin에 직접 접근 불가
            bank.EarnCoin(2000);
            bank.SpendCoin(2000);
            #endregion

            #region 상속 사용예제
            Console.WriteLine("\n     <   상속 사용 예시 >\n");
            Zombie zombie1 = new Zombie();
            Slime slime1 = new Slime();

            // 자식클래스에서는 부모클래스에서 정의한 기능 모두를 사용가능
            zombie1.NormalAttack();
            slime1.NormalAttack();

            // 각각의 자식클래스에서 새로 추가한 기능을 사용가능
            zombie1.Bite();
            slime1.Split();

            // *다운캐스팅 : 부모클래스는 자식클래스 자료형으로 명시적 형변환 가능.
            //            : 명시적 = 수동적
            Monster monster1 = new Zombie();   // monster1을 자식클래스인 Zombie형으로 다운캐스팅
            Zombie zombie2 = (Zombie)monster1;
            if(monster1 is Zombie)
            {
                Console.WriteLine("형변환 성공 : monster1은 Zombie형 입니다.");    // 실행
            }
            else
            {
                Console.WriteLine("형변환 실패 : monster1은 Zombie형이 아닙니다.");   // 실행x
            }
            //Slime slime2 = (Slime)monster1;    // error : monster1은 현재 Zombie형이므로 Slime의 부모클래스가 아님.
            Slime asSlime = monster1 as Slime;  // 형변환이 가능하다면 형변환.
            if (monster1 is Slime)
            {
                Console.WriteLine("형변환 성공 : monster1은 Slime형 입니다.");    // 실행
            }
            else
            {
                Console.WriteLine("형변환 실패 : monster1은 Slime형이 아닙니다.");   // 실행x
            }
            #endregion

            #region 다형성 사용예제
            Console.WriteLine("\n     < 다형성 사용 예시 >\n");

            Skill fireBall = new FireBall();
            fireBall.Execute();

            Skill dash = new Dash();
            dash.Execute();

            Player player = new Player();
            Skill heal = new Heal();
            player.LearnSkill(heal);
            player.UseSkill();
            #endregion
        }
    }
}

namespace _03._Project
{
    // 객제지향 프로그래밍 실습예제
    #region OOP 실습 1 (객체지향 프로그래밍 다루기) 
    // a. 드라이버, 차 클래스 객체 생성
    // b. 드라이버 <-> 차
    // c. 가속, 감속
    // d. 차의 속도 변화
    //enum CarType
    //{
    //    Lamborgini = 0,
    //    Porshe,
    //    Tico
    //}

    //class Driver
    //{
    //    private Car myCar;

    //    public void RideCar(Car car)
    //    {
    //        myCar = car;
    //        Console.WriteLine($"{myCar.CarName}에 탑승합니다.");
    //        Console.WriteLine($"{myCar.CarName}의 최고 속력은 {myCar.MaxSpeed}km/h 입니다.");
    //    }
    //    public void StepAccelerator(int value)
    //    {
    //        if (myCar == null)
    //        {
    //            Console.WriteLine("우선 차량에 탑승하세요.");
    //            return;
    //        }
    //        myCar.SpeedUp(value);
    //    }
    //    public void StepBrake(int value)
    //    {
    //        if (myCar == null)
    //        {
    //            Console.WriteLine("우선 차량에 탑승하세요.");
    //            return;
    //        }
    //        myCar.SpeedDown(value);
    //    }
    //}
    //class Car
    //{
    //    private CarType myCarType;
    //    public CarType MyCarType { get { return myCarType; } }

    //    private string carName;
    //    public string CarName { get { return carName; } }

    //    private int maxSpeed;
    //    public int MaxSpeed { get { return maxSpeed; } }

    //    private int speed;
    //    public int Speed { get { return speed; } }

    //    public Car(CarType carType)
    //    {
    //        myCarType = carType;
    //        switch (myCarType)
    //        {
    //            case CarType.Lamborgini:
    //                carName = "람보르기니";
    //                maxSpeed = 600;
    //                break;
    //            case CarType.Porshe:
    //                carName = "포르쉐";
    //                maxSpeed = 500;
    //                break;
    //            case CarType.Tico:
    //                carName = "티코";
    //                maxSpeed = 100;
    //                break;
    //        }
    //    }

    //    public void SpeedUp(int value)
    //    {
    //        if (speed + value <= maxSpeed)
    //        {
    //            speed += value;
    //            Console.WriteLine($"{value} km/h 만큼 가속합니다.");
    //        }
    //        else
    //        {
    //            Console.WriteLine($"{maxSpeed - speed} km/h 만큼 가속합니다.");
    //            speed = maxSpeed;
    //        }
    //        Console.WriteLine($"{speed} / {maxSpeed} km/h");
    //    }
    //    public void SpeedDown(int value)
    //    {
    //        if (speed - value >= 0)
    //        {
    //            speed -= value;
    //            Console.WriteLine($"{value} km/h 만큼 감속합니다.");
    //        }
    //        else
    //        {
    //            Console.WriteLine($"{speed} km/h 만큼 감속합니다.");
    //            speed = 0;
    //        }
    //        Console.WriteLine($"{speed} / {maxSpeed} km/h");
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] argc)
    //    {
    //        Car lamborgini = new Car(CarType.Lamborgini);
    //        Car porshe = new Car(CarType.Porshe);
    //        Car tico = new Car(CarType.Tico);

    //        Driver driver = new Driver();
    //        Console.WriteLine("탑승할 차량을 선택하시오");
    //        Console.Write("(1:람보르기니) (2:포르쉐) (3:티코)");

    //        ConsoleKeyInfo carKey;
    //        do
    //        {
    //            carKey = Console.ReadKey();
    //        } while (carKey.Key != ConsoleKey.D1 && carKey.Key != ConsoleKey.D2 && carKey.Key != ConsoleKey.D3);

    //        Console.WriteLine("\n");

    //        switch (carKey.Key)
    //        {
    //            case ConsoleKey.D1:
    //                driver.RideCar(lamborgini);
    //                break;
    //            case ConsoleKey.D2:
    //                driver.RideCar(porshe);
    //                break;
    //            case ConsoleKey.D3:
    //                driver.RideCar(tico);
    //                break;
    //        }
    //        Console.WriteLine("");

    //        Console.Write("(1:엑셀) (2:브레이크) (P:종료)");
    //        ConsoleKeyInfo inputKey;
    //        do
    //        {
    //            inputKey = Console.ReadKey();
    //            Console.WriteLine("\n");
    //            if (inputKey.Key == ConsoleKey.D1)
    //            {
    //                driver.StepAccelerator(100);
    //            }
    //            else if (inputKey.Key == ConsoleKey.D2)
    //            {
    //                driver.StepBrake(50);
    //            }
    //            else
    //            {
    //                Console.WriteLine("잘못된 입력입니다.");
    //                Console.WriteLine("(1:엑셀) (2:브레이크) (P:종료)");
    //            }
    //            Console.WriteLine();
    //        } while (inputKey.Key != ConsoleKey.P);
    //    }
    //}
    #endregion

    #region OOP 실습 2 (캡슐화:몬스터 정의 / 상속:여러 몬스터 종류 생성) 
    // a. 몬스터에 이름, 체력, 데미지 받기
    // b. 여러 몬스터 종류 생성
    // c. 오크, 슬라임, 드래곤
    // d. 오크:분노 
    // e. 슬라임 : 분열
    // f. 드래곤 : 브레스

    //class Monster
    //{
    //    protected string name;
    //    public string Name { get { return name; } }

    //    protected int hp;
    //    protected int maxHp;

    //    public void TakeHit(int value)
    //    {
    //        if (hp - value > 0)
    //        {
    //            hp -= value;
    //            Console.ForegroundColor = ConsoleColor.Red;
    //            Console.WriteLine($"{name}이/가 {value}만큼의 데미지를 입습니다.");
    //            Console.WriteLine($"{name}의 체력 : {hp}/{maxHp}");
    //            Console.ResetColor();
    //        }
    //        else
    //        {
    //            hp = 0;
    //            Console.ForegroundColor = ConsoleColor.Red;
    //            Console.WriteLine($"{name}이/가 사망합니다.");
    //            Console.ResetColor();
    //        }
    //    }
    //}

    //class Oak : Monster
    //{
    //    public Oak()
    //    {
    //        name = "오크";
    //        maxHp = 100;
    //        hp = maxHp;
    //    }

    //    public void Anger()
    //    {
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.WriteLine($"{name}이 분노합니다.");
    //        Console.ResetColor();
    //    }
    //}
    //class Slime : Monster
    //{
    //    public Slime()
    //    {
    //        name = "슬라임";
    //        maxHp = 50;
    //        hp = maxHp;
    //    }

    //    public void Split()
    //    {
    //        Console.ForegroundColor = ConsoleColor.Cyan;
    //        Console.WriteLine($"{name}이 분열합니다.");
    //        Console.ResetColor();
    //    }
    //}

    //class Dragon : Monster
    //{
    //    public Dragon()
    //    {
    //        name = "드래곤";
    //        maxHp = 200;
    //        hp = maxHp;
    //    }

    //    public void Breath()
    //    {
    //        Console.ForegroundColor = ConsoleColor.Magenta;
    //        Console.WriteLine($"{name}이 불을 뿜습니다.");
    //        Console.ResetColor();
    //    }
    //}

    //enum ChampionType
    //{
    //    Warrior = 1,
    //    Archer,
    //    Wizard
    //}

    //class Champion
    //{
    //    private string name;       // 직업명

    //    private int atk;    // 공격력
    //    public int Atk { get { return atk; } }

    //    public Champion(ChampionType champType)
    //    {
    //        switch (champType)
    //        {
    //            case ChampionType.Warrior:
    //                atk = 10;
    //                name = "전사";
    //                break;
    //            case ChampionType.Archer:
    //                atk = 30;
    //                name = "궁수";
    //                break;
    //            case ChampionType.Wizard:
    //                atk = 50;
    //                name = "마법사";
    //                break;
    //        }
    //        Console.ForegroundColor = ConsoleColor.Blue;
    //        Console.WriteLine($"{name}을/를 선택했습니다. 공격력:{atk}");
    //        Console.ResetColor();
    //    }

    //    public void Attack(Monster trMonster)
    //    {
    //        Console.ForegroundColor = ConsoleColor.Blue;
    //        Console.WriteLine($"{name}이/가 {trMonster.Name}을/를 {atk}의 데미지로 공격합니다.");
    //        Console.ResetColor();
    //        trMonster.TakeHit(atk);
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] argc)
    //    {
    //        bool gameLoop = true;

    //        Monster[] monsters = new Monster[3];
    //        monsters[0] = new Oak();    // 다운캐스팅
    //        monsters[1] = new Slime();
    //        monsters[2] = new Dragon();

    //        while (gameLoop)
    //        {
    //            Champion myChamp;

    //            Console.WriteLine("직업을 선택하세요.");
    //            Console.Write("(1:전사) (2:궁수) (3:마법사) (P:게임종료)");


    //            ConsoleKeyInfo jobKey = Console.ReadKey();
    //            while (jobKey.Key != ConsoleKey.D1 && jobKey.Key != ConsoleKey.D2 && jobKey.Key != ConsoleKey.D2 && jobKey.Key != ConsoleKey.P)
    //            {
    //                Console.WriteLine();
    //                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요");
    //                Console.Write("(1:전사) (2:궁수) (3:마법사) (P:게임종료)");
    //                jobKey = Console.ReadKey();
    //            }
    //            Console.WriteLine("\n");

    //            switch (jobKey.Key)
    //            {
    //                case ConsoleKey.D1:
    //                    myChamp = new Champion(ChampionType.Warrior);
    //                    break;
    //                case ConsoleKey.D2:
    //                    myChamp = new Champion(ChampionType.Archer);
    //                    break;
    //                case ConsoleKey.D3:
    //                    myChamp = new Champion(ChampionType.Wizard);
    //                    break;
    //                case ConsoleKey.P:
    //                    return;
    //                default:
    //                    return;
    //            }

    //            Console.WriteLine();

    //            Console.WriteLine("공격할 대상을 선택하세요.");
    //            Console.Write("(1:오크) (2:슬라임) (3:드래곤) (P:게임종료)");

    //            Console.WriteLine();

    //            ConsoleKeyInfo targetKey = Console.ReadKey();
    //            while (targetKey.Key != ConsoleKey.D1 && targetKey.Key != ConsoleKey.D2 && targetKey.Key != ConsoleKey.D2 && targetKey.Key != ConsoleKey.P)
    //            {
    //                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요");
    //                Console.Write("(1:오크) (2:슬라임) (3:드래곤) (P:게임종료)");
    //                targetKey = Console.ReadKey();
    //            }
    //            Console.WriteLine();

    //            switch (targetKey.Key)
    //            {
    //                case ConsoleKey.D1:
    //                    myChamp.Attack(monsters[0]);
    //                    break;
    //                case ConsoleKey.D2:
    //                    myChamp.Attack(monsters[1]);
    //                    break;
    //                case ConsoleKey.D3:
    //                    myChamp.Attack(monsters[2]);
    //                    break;
    //                case ConsoleKey.P:
    //                    return;
    //            }
    //            Console.WriteLine();
    //            if (monsters[0] is Oak)
    //            {
    //                Oak tempOak = (Oak)monsters[0];
    //                tempOak.Anger();
    //            }

    //            Console.WriteLine();

    //            Slime tempSlime = monsters[1] as Slime;
    //            tempSlime.Split();

    //            Console.WriteLine();

    //            Dragon tempDragon = (Dragon)monsters[2];
    //            tempDragon.Breath();

    //            Console.WriteLine();

    //        }

    //    }
    //}


    #endregion

    #region OOP 실습 3 (제일 좋아하는 게임) 
    //// a. 객체 설계
    //// b. 캡슐화, 상속
    //enum Potential
    //{
    //    Shooting = 0,
    //    Pass,
    //    Tackle
    //}

    //class Match
    //{
    //    // 경기를 관리하는 클래스

    //    private Random randPlayerType;
    //    private Random randPlayer;

    //    private List<Striker> strikers;
    //    private List<MidFielder> midFielders;
    //    private List<Defender> defenders;

    //    private int dayCount = 1;
    //    public Match() 
    //    {
    //        randPlayerType = new Random();
    //        randPlayer = new Random();
    //    }

    //    // 경기 진행
    //    // 경기 종료
    //    // 경기 결과

    //    public void MatchSetting(Player[] players)
    //    {
    //        strikers = new List<Striker>();    
    //        midFielders = new List<MidFielder>();
    //        defenders = new List<Defender>();

    //        foreach (Player pr in players)
    //        {
    //            if(pr is Striker)
    //            {
    //                strikers.Add((Striker)pr);
    //            }
    //            else if(pr is MidFielder)
    //            {
    //                midFielders.Add((MidFielder)pr);
    //            }
    //            else if(pr is Defender)
    //            {
    //                defenders.Add((Defender)pr);
    //            }
    //        }
    //    }

    //    public void PlayMatch()
    //    {
    //        Console.WriteLine($"<매치데이 {dayCount} 일차>");
    //        Console.WriteLine("\n오늘 경기의 선발명단 입니다!");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.Write("공격수 : ");
    //        Console.ResetColor();
    //        foreach (Striker pr in strikers)
    //        {
    //            Console.Write($"{pr.Name} ");
    //        }
    //        Console.WriteLine();

    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.Write("미드필더 : ");
    //        Console.ResetColor();
    //        foreach (MidFielder pr in midFielders)
    //        {
    //            Console.Write($"{pr.Name} ");
    //        }
    //        Console.WriteLine();

    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.Write("수비수 : ");
    //        Console.ResetColor();
    //        foreach (Defender pr in defenders)
    //        {
    //            Console.Write($"{pr.Name} ");
    //        }
    //        Console.WriteLine();

    //        Thread.Sleep(1000);
    //        Console.WriteLine("경기 시작!\n");

    //        int trySkill = 5;
    //        while (trySkill > 0)
    //        {
    //            Console.ForegroundColor = ConsoleColor.Cyan;
    //            Console.Write("경기 진행중.");
    //            for (int i=0; i<5; i++)
    //            {
    //                Thread.Sleep(500);
    //                Console.Write(".");
    //            }
    //            Console.ResetColor();
    //            Console.WriteLine();

    //            int prType = 0;
    //            int prLength = 0;
    //            int prIndex = 0;

    //            while(prLength == 0)
    //            {
    //                prType = randPlayerType.Next(1,3);
    //                switch(prType)
    //                {
    //                    case 1:
    //                        prLength = strikers.Count;
    //                        break;
    //                    case 2:
    //                        prLength = midFielders.Count;
    //                        break;
    //                    case 3:
    //                        prLength = defenders.Count;
    //                        break;
    //                }
    //            }

    //            prIndex = randPlayerType.Next(0, prLength);
    //            switch (prType)
    //            {
    //                case 1:
    //                    strikers[prIndex].PowerShoot();
    //                    break;
    //                case 2:
    //                    midFielders[prIndex].KillPass();
    //                    break;
    //                case 3:
    //                    defenders[prIndex].PerfectTackle();
    //                    break;
    //            }

    //            trySkill--;
    //        }

    //        dayCount++;
    //        Thread.Sleep(1500);
    //        Console.Clear();
    //    }
    //}
    //class Player
    //{
    //    // 선수 데이터
    //    // 1. 이름
    //    // 2. 능력치   (레벨업 시 각 잠재력에 따라 증가수치가 상이)
    //    //  a. 슈팅
    //    //  b. 패스
    //    //  c. 태클
    //    // 3. 경험치
    //    // 4. 자식
    //    //  a. 포워드 : 슈팅
    //    //  b. 미드필더 : 패스
    //    //  c. 수비수 : 태클

    //    protected string name;
    //    public string Name { get { return name; } } // 읽기전용 데이터

    //    protected Potential myPotential;

    //    // 능력치
    //    protected int shootingABT;    // 슈팅
    //    public int ShootingABT { get { return ShootingABT; } }

    //    protected int passABT;     // 패스
    //    public int PassABT { get { return passABT; } }

    //    protected int tackleABT;    // 태클
    //    public int TackleABT { get { return tackleABT; } }


    //    // 레벨업
    //    public void LevelUp()
    //    {
    //        // 기본 능력치 증가
    //        shootingABT += 2;
    //        passABT += 2;
    //        tackleABT += 2;

    //        // 잠재력에 따른 능력치 증가
    //        switch (myPotential)
    //        {
    //            case Potential.Shooting:
    //                shootingABT += 3;
    //                break;
    //            case Potential.Pass:
    //                passABT += 3;
    //                break;
    //            case Potential.Tackle:
    //                tackleABT += 3;
    //                break;
    //            default:
    //                break;
    //        }

    //        Console.WriteLine();
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.Write(name);
    //        Console.ResetColor();
    //        Console.WriteLine(" 레벨업에 따른 경험치가 갱신됩니다.");
    //        Console.Write("슈팅:");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"{shootingABT}");
    //        Console.ResetColor();
    //        Console.Write("패스:");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"{passABT}");
    //        Console.ResetColor();
    //        Console.Write("태클:");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"{tackleABT}");
    //        Console.ResetColor();

    //        Thread.Sleep(3000);
    //    }

    //    // 능력치 출력
    //    public void PrintAbility()
    //    {
    //        Console.WriteLine($"<{name}의 현재 능력치 정보>");
    //        Console.Write("슈팅 : ");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"{shootingABT}");
    //        Console.ResetColor();
    //        Console.Write("패스 : ");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"{passABT}");
    //        Console.ResetColor();
    //        Console.Write("태클 : ");
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine($"{tackleABT}");
    //        Console.ResetColor();
    //        Console.WriteLine();
    //    }
    //}

    //class Striker : Player
    //{
    //    public Striker(string name)
    //    {
    //        this.name = name;
    //        myPotential = Potential.Shooting;
    //        shootingABT = 8;
    //        passABT = 5;
    //        tackleABT = 3;
    //    }
    //    public void PowerShoot()
    //    {
    //        Console.WriteLine("***************************************");
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.Write(name);
    //        Console.ResetColor();
    //        Console.WriteLine("이/가 파워슛으로 골망을 가릅니다!");
    //        Console.WriteLine("***************************************");
    //        LevelUp();
    //    }
    //}
    //class MidFielder : Player
    //{
    //    public MidFielder(string name)
    //    {
    //        this.name = name;
    //        myPotential = Potential.Pass;
    //        shootingABT = 4;
    //        passABT = 8;
    //        tackleABT = 4;
    //    }
    //    public void KillPass()
    //    {
    //        Console.WriteLine("***************************************");
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.Write(name);
    //        Console.ResetColor();
    //        Console.WriteLine("이/가 킬패스로 어시스트를 성공합니다!");
    //        Console.WriteLine("***************************************");
    //        LevelUp();
    //    }
    //}
    //class Defender : Player
    //{
    //    public Defender(string name)
    //    {
    //        this.name = name;
    //        myPotential = Potential.Tackle;
    //        shootingABT = 3;
    //        passABT = 5;
    //        tackleABT = 8;
    //    }

    //    public void PerfectTackle()
    //    {
    //        Console.WriteLine("***************************************");
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.Write(name);
    //        Console.ResetColor();
    //        Console.WriteLine("이/가 깔끔한 태클을 성공합니다!");
    //        Console.WriteLine("***************************************");
    //        LevelUp();
    //    }
    //}

    //class FCOnline
    //{
    //    const int MaxPlayers = 8;
    //    const int MaxTeamCapacity = 5;
    //    private int LoopCount = 5;
    //    private Player[] players;
    //    private bool[] includingPlayers;
    //    private Match match;


    //    public FCOnline()
    //    {
    //        players = new Player[MaxPlayers];
    //        includingPlayers = new bool[MaxPlayers] { false,false,false,false,false,false,false,false};   
    //    }

    //    private void RenderTitle()
    //    {
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.WriteLine("***************************************");
    //        Console.ResetColor();
    //        Console.WriteLine("***************************************");
    //        Console.ForegroundColor = ConsoleColor.Yellow;
    //        Console.WriteLine("*******        FC 온라인        *******");
    //        Console.ResetColor();
    //        Console.WriteLine("***************************************");
    //        Console.ForegroundColor = ConsoleColor.Green;
    //        Console.WriteLine("***************************************");
    //        Console.ResetColor();
    //        Console.WriteLine();
    //    }
    //    public void InitGame()
    //    {
    //        players[0] = new Striker("호날두");
    //        players[1] = new Striker("메시");
    //        players[2] = new Striker("음바페");
    //        players[3] = new MidFielder("이강인");
    //        players[4] = new MidFielder("심재천");
    //        players[5] = new MidFielder("김흥국");
    //        players[6] = new Defender("김민재");
    //        players[7] = new Defender("홍명보");
    //        match = new Match();

    //        Player[] team = new Player[MaxTeamCapacity];

    //        int cnt = 0;
    //        while(cnt < MaxTeamCapacity)
    //        {
    //            RenderTitle();

    //            Console.Write("명단에 포함할 선수의 번호를 입력하세요.");
    //            Console.ForegroundColor = ConsoleColor.Green;
    //            Console.WriteLine($"({cnt}/{MaxTeamCapacity})");
    //            Console.ResetColor();

    //            for(int i=0; i<players.Length; i++)
    //            {
    //                if (includingPlayers[i] == true)
    //                {
    //                    Console.Write($"   ");
    //                }
    //                else
    //                {
    //                    Console.Write($"{i + 1}:{players[i].Name} ");
    //                }
    //            }
    //            Console.WriteLine();
    //            Console.ForegroundColor = ConsoleColor.Green;
    //            for (int i=0; i<team.Length; i++)
    //            {
    //                if (team[i] == null) continue;
    //                Console.Write($"{team[i].Name} ");
    //            }
    //            Console.WriteLine();
    //            Console.ResetColor();

    //            int inputKey = int.Parse(Console.ReadLine());
    //            if(inputKey < 1 || inputKey > 8)
    //            {
    //                Console.ForegroundColor = ConsoleColor.Red;
    //                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
    //                Console.ResetColor();
    //                Thread.Sleep(1000);
    //                Console.Clear();
    //                continue; 
    //            }
    //            else
    //            {
    //                if (includingPlayers[inputKey-1] == true)
    //                {
    //                    Console.ForegroundColor = ConsoleColor.Red;
    //                    Console.WriteLine("이미 포함된 선수입니다. 다시 입력하세요.");
    //                    Console.ResetColor();
    //                    Thread.Sleep(1000);
    //                    Console.Clear();
    //                    continue;
    //                }
    //                includingPlayers[inputKey - 1] = true;
    //                team[cnt] = players[inputKey - 1];
    //                cnt++;
    //                Thread.Sleep(100);
    //                Console.Clear();
    //            }
    //        }
    //        Thread.Sleep(300);
    //        match.MatchSetting(team);
    //    }
    //    public void LoopGame()
    //    {
    //        while(LoopCount > 0)
    //        {
    //            RenderTitle();
    //            match.PlayMatch();
    //            LoopCount--;
    //        }

    //        Console.ForegroundColor= ConsoleColor.Red;
    //        Console.WriteLine("5초 뒤 게임이 종료됩니다.");
    //        Console.ResetColor();
    //        Thread.Sleep(5000);
    //    }

    //    static void Main(string[] argc)
    //    {
    //        FCOnline fcOnline = new FCOnline();
    //        fcOnline.InitGame();
    //        fcOnline.LoopGame();
    //    }
    //}
    #endregion

    #region OOP 실습 4 : 리그오브레전드
    public enum ItemSlotNumber
    {
        NUM1 = 0,
        NUM2,
        NUM3,
        NUM4,
        NUM5,
        NUM6,
    }
    public enum ItemType
    {
        Expendable = 0, // 사용가능한 소모성 아이템
        Useable,        // 사용가능한 비소모성 아이템
        UnUseable       // 사용이 불가능한 아이템
    }
    public enum SkillSlotKey
    {
        Qskill = 0,
        Wskill,
        Eskill,
        Rskill
    }

    // 유저 입력관련 인터페이스
    public interface InputHandler
    {
        public void InputKey();     // Console.ReadKey
        public void InputString();  // Console.ReadLine
    }

    #region 챔피언
    class Champion
    {
        const int MaxItem = 6;
        const int MaxSkill = 4;

        // 챔피언 정보
        protected string name;  //이름
        public string Name { get { return name; } }

        protected int hp;   // 체력
        public int HP { get { return hp; } }

        protected int speed;    // 이동속도
        public int Speed { get { return speed; } }

        private Item[] items = new Item[MaxItem]; // 아이템슬롯 배열 (0~5번 슬롯 (1~6에 대칭))
        public Item[] Items { get { return items; } }


        private Skill[] skills = new Skill[MaxSkill];   // 스킬슬롯 배열 (0:q 1:w 2:e 3:r)
        public Skill[] Skills { get { return skills; } }

        public void GetChampionInfo()    // 챔피언 정보 출력
        {
            Console.WriteLine("************************");
            Console.WriteLine($"** <{name}> 챔피언 정보 **");
            Console.WriteLine("************************\n");

            Console.WriteLine($"이동속도 : {speed}");
            Console.WriteLine($"    체력 : {hp}");
            Console.WriteLine();
        }
        public void GetItemList()    // 아이템 슬롯 출력
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) continue;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i + 1}:{items[i].Name} ");
                Console.ResetColor();
                if (i == 2)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        // 아이템 장착
        public void EquipItem(Item item, ItemSlotNumber num)
        {
            if (items[(int)num] == null)     // 빈 슬롯일 경우
            {
                item.slotNumber = (int)num;
                items[(int)num] = item; //장착
                items[(int)num].EquipedItem(this);
            }
            else  // 슬롯에 아이템이 존재할 경우
            {
                UnEquipItem(num);   // 해당 슬롯의 아이템 해제
                item.slotNumber = (int)num;
                items[(int)num] = item; //장착
            }
        }
        // 아이템 장착해제
        public void UnEquipItem(ItemSlotNumber num)
        {
            items[(int)num] = null;
        }
        // 아이템 사용
        public void UsingItem(ItemSlotNumber num)
        {
            if (items[(int)num] == null)
            {
                Console.WriteLine("아이템이 존재하지 않습니다.\n");
            }
            else
            {
                items[(int)num].UseItem(this);
            }
        }
        // 스킬 할당
        protected void SetSkill(Skill skill, SkillSlotKey key)
        {
            skills[(int)key] = skill;
        }

        // 스킬 사용
        public void CastingSkil(SkillSlotKey key)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("스킬사용:");
            Console.ResetColor();
            skills[(int)key].Execute(this);
        }
    }

    // 리신
    // 챔피언상속
    class LeeSin : Champion
    {
        public LeeSin()
        {
            name = "리신";
            hp = 100;
            speed = 325;

            SetSkill(new SonicWaveAndResonatingStrike(), SkillSlotKey.Qskill);
            SetSkill(new SafeGuard(), SkillSlotKey.Wskill);
            SetSkill(new Tempest(), SkillSlotKey.Eskill);
            SetSkill(new DragonsRage(), SkillSlotKey.Rskill);
        }
    }
    #endregion

    #region 스킬

    /// <summary>
    /// 스킬 최상위 클래스
    /// 스킬 이름, 스킬 쿨타임 멤버 변수와
    /// 실행이라는 메소드를 지님.
    /// </summary>
    class Skill
    {
        protected string skillName;
        public int SkillName { get { return SkillName; } }

        protected int skillCoolTime;    // 스킬 쿨타임
        public int SkillCoolTime { get { return skillCoolTime; } }

        public virtual void Execute(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("스킬쿨타임:");
            Console.ResetColor();
            Console.Write($"{skillName}스킬에 ");
            // 쿨타임 실행
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{skillCoolTime}초");
            Console.ResetColor();
            Console.WriteLine("의 쿨타임이 적용됩니다.");
        }
    }


    // Skill 파생 자식 클래스
    // 음파/공명의 일격
    class SonicWaveAndResonatingStrike : Skill     // 리신 q 음파/공명의 일격
    {
        enum SkillType
        {
            SONIC_WAVE = 0,
            RESONATING_STRIKE
        }

        private SkillType nextSkillType;

        private string skillName2;

        public SonicWaveAndResonatingStrike()
        {
            skillName = "음파";
            skillName2 = "공명의 일격";
            skillCoolTime = 5;
            nextSkillType = SkillType.SONIC_WAVE;
        }

        public override void Execute(Champion owner)
        {
            Console.Write($"{owner.Name}이");
            switch (nextSkillType)
            {
                case SkillType.SONIC_WAVE:
                    SonicWave();
                    break;
                case SkillType.RESONATING_STRIKE:
                    ResonatingStrike();
                    break;
                default:
                    Console.WriteLine("SonicWaveAndResonatingStrike 실행 오류");
                    break;
            }
            if (nextSkillType == SkillType.SONIC_WAVE)   // 공명의 일격이 실행되었으면
                                                         // 음파 쿨타임진행
            {
                base.Execute(owner);
            }
        }

        public void LoadingCoolTime(Champion owner)     // 쿨타임 강제실행 (첫번째 q사용이후 다른 스킬을 사용한 경우) 
        {
            nextSkillType = SkillType.SONIC_WAVE;
            base.Execute(owner);
        }


        // 첫번째 스킬 메소드 (음파)
        private void SonicWave()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(skillName);
            Console.ResetColor();

            Console.WriteLine("를 사용해 적을 시야에 노출시킵니다.");
            nextSkillType = SkillType.RESONATING_STRIKE;
        }

        // 두번째 스킬 메소드 (공명의 일격)
        // 공명의 일격 사용 후 쿨타임 진행.
        private void ResonatingStrike()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(skillName2);
            Console.ResetColor();

            Console.WriteLine("으로 음파에 맞은 적을 추격 후 공격합니다.");
            nextSkillType = SkillType.SONIC_WAVE;
        }
    }
    // 방호
    class SafeGuard : Skill // 리신 w 방호 (체력회복으로 대체)
    {
        public SafeGuard()
        {
            skillName = "방호";
            skillCoolTime = 8;
        }

        public override void Execute(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(skillName);
            Console.ResetColor();

            Console.WriteLine("를 사용해 쉴드를 얻습니다.");
            base.Execute(owner);
        }
    }
    // 폭풍
    class Tempest : Skill   // 리신 e 폭풍
    {
        public Tempest()
        {
            skillName = "폭풍";
            skillCoolTime = 3;
        }

        public override void Execute(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(skillName);
            Console.ResetColor();

            Console.WriteLine("을 사용해 주변의 적들을 무력화 시킵니다.");
            base.Execute(owner);
        }
    }
    // 용의 분노
    class DragonsRage : Skill   // 리신 r 용의 분노
    {
        public DragonsRage()
        {
            skillName = "용의 분노";
            skillCoolTime = 10;
        }

        public override void Execute(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(skillName);
            Console.ResetColor();

            Console.WriteLine("를 사용해 적을 날려버립니다.");
            base.Execute(owner);
        }
    }
    #endregion

    #region 아이템
    abstract class Item
    {
        protected string name;  // 아이템명
        public string Name { get { return name; } }

        public int slotNumber;   // 아이템이 위치한 슬롯의 번호

        protected ItemType itemType;    // 아이템 타입

        public virtual void EquipedItem(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("아이템장착:");
            Console.ResetColor();
            Console.WriteLine($"{name}을/를 장착합니다.");
        }
        public abstract void UseItem(Champion owner);
    }
    class InfinityEdge : Item   // 무한의 대검
    {
        public InfinityEdge()
        {
            name = "무한의 대검";
            itemType = ItemType.UnUseable;
        }

        public override void EquipedItem(Champion owner)
        {
            base.EquipedItem(owner);
            Console.WriteLine($"{owner.Name}의 공격력이 대폭 증가합니다.");
        }
        public override void UseItem(Champion owner)
        {

        }
    }
    class HpPotion : Item       // 체력 물약
    {
        private int remain;

        public HpPotion()
        {
            name = "체력 물약";
            itemType = ItemType.Expendable;
            remain = 3;
        }

        public override void UseItem(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("아이템사용:");
            Console.ResetColor();
            Console.WriteLine($"체력 물약을 사용해 체력을 50회복합니다. (남은 개수:{--remain})");
            if (remain <= 0) owner.UnEquipItem((ItemSlotNumber)slotNumber);

        }
    }
    class QuicksilverSash : Item        // 수은 장식띠 
    {
        public QuicksilverSash()
        {
            name = "수은 장식띠";
            itemType = ItemType.Useable;
        }

        public override void EquipedItem(Champion owner)
        {
            base.EquipedItem(owner);
            Console.WriteLine($"{owner.Name}의 마법저항력이 증가합니다.");
        }
        public override void UseItem(Champion owner)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("아이템사용:");
            Console.ResetColor();
            Console.WriteLine($"수은 장식띠를 사용해 {owner.Name}의 상태이상을 제거합니다.");
        }
    }
    #endregion

    class User : InputHandler
    {
        // 사용자 입력관련 
        ConsoleKey myKey;
        public ConsoleKey MyKey { get { return myKey; } }

        string myString;
        public string MyString { get { return myString; } }

        private Champion myChamp;   // 플레이할 챔피언
        public Champion MyChamp { get { return myChamp; } }

        public User()
        {
            myKey = ConsoleKey.Escape;
            myString = "";
            myChamp = new LeeSin();
        }

        public void InputKey()
        {
            myKey = Console.ReadKey().Key;
        }
        public void InputString()
        {
            myString = Console.ReadLine();
        }
    }

    class LeagueOfLegend
    {
        private User user;
        private bool gameLoop = true;

        public LeagueOfLegend()
        {
            user = new User();
        }

        public void RendeTitle()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("************** 리그 오브 레전드 **************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine($"      |    {user.MyChamp.Name}으로 플레이합니다.    |");
            Console.ResetColor();

            user.MyChamp.GetItemList();

            Console.Write("| Skill ");
            Console.ForegroundColor = ConsoleColor.Cyan;    // qwer s p i
            Console.Write("Q W E R");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" | Item ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("I");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" | Info ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" | Quit ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("P");
            Console.ResetColor();
            Console.WriteLine(" |");
        }
        public void RenderNormalMenual()
        {
            do
            {
                user.InputKey();
            } while (user.MyKey != ConsoleKey.Q && user.MyKey != ConsoleKey.W && user.MyKey != ConsoleKey.E && user.MyKey != ConsoleKey.R
            && user.MyKey != ConsoleKey.S && user.MyKey != ConsoleKey.P && user.MyKey != ConsoleKey.I
            && user.MyKey != ConsoleKey.D1 && user.MyKey != ConsoleKey.D2 && user.MyKey != ConsoleKey.D3 && user.MyKey != ConsoleKey.D4 && user.MyKey != ConsoleKey.D5 && user.MyKey != ConsoleKey.D6);
            Console.WriteLine();

            Console.Clear();
            RendeTitle();

            switch (user.MyKey)
            {
                // 스킬
                case ConsoleKey.Q:
                    user.MyChamp.CastingSkil(SkillSlotKey.Qskill);
                    break;
                case ConsoleKey.W:
                    user.MyChamp.CastingSkil(SkillSlotKey.Wskill);
                    break;
                case ConsoleKey.E:
                    user.MyChamp.CastingSkil(SkillSlotKey.Eskill);
                    break;
                case ConsoleKey.R:
                    user.MyChamp.CastingSkil(SkillSlotKey.Rskill);
                    break;

                // 현재 정보 출력
                case ConsoleKey.S:
                    user.MyChamp.GetChampionInfo();
                    break;

                // 아이템 장착 메뉴얼
                case ConsoleKey.I:
                    RenderItemList();
                    return;

                // 아이템 사용
                case ConsoleKey.D1:
                    user.MyChamp.UsingItem(ItemSlotNumber.NUM1);
                    break;
                case ConsoleKey.D2:
                    user.MyChamp.UsingItem(ItemSlotNumber.NUM2);
                    break;
                case ConsoleKey.D3:
                    user.MyChamp.UsingItem(ItemSlotNumber.NUM3);
                    break;
                case ConsoleKey.D4:
                    user.MyChamp.UsingItem(ItemSlotNumber.NUM4);
                    break;
                case ConsoleKey.D5:
                    user.MyChamp.UsingItem(ItemSlotNumber.NUM5);
                    break;
                case ConsoleKey.D6:
                    user.MyChamp.UsingItem(ItemSlotNumber.NUM6);
                    break;
                case ConsoleKey.P:
                    gameLoop = false;
                    return;

            }
        }
        private void RenderItemList()
        {
            Console.WriteLine();
            Console.WriteLine("장착할 아이템을 입력하세요.");
            Console.Write("| 1 ");
            Console.ForegroundColor = ConsoleColor.Yellow;    // qwer s p i
            Console.Write("무한의 대검");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" | 2 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("체력 포션");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" | 3 ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("수은 장식띠");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" |");
            Console.ResetColor();

            do
            {
                user.InputKey();
            } while (user.MyKey != ConsoleKey.D1 && user.MyKey != ConsoleKey.D2 && user.MyKey != ConsoleKey.D3);
            Console.WriteLine();
            Console.WriteLine("(장착할 아이템슬롯 : 1 ~ 6)");
            int slotNumber = -1;
            ConsoleKey numKey;
            do
            {
                numKey = Console.ReadKey().Key;
            } while (numKey != ConsoleKey.D1 && numKey != ConsoleKey.D2 && numKey != ConsoleKey.D3 && numKey != ConsoleKey.D4 && numKey != ConsoleKey.D5 && numKey != ConsoleKey.D6);
            Console.WriteLine();
            switch (numKey)
            {
                case ConsoleKey.D1:
                    slotNumber = 0;
                    break;
                case ConsoleKey.D2:
                    slotNumber = 1;
                    break;
                case ConsoleKey.D3:
                    slotNumber = 2;
                    break;
                case ConsoleKey.D4:
                    slotNumber = 3;
                    break;
                case ConsoleKey.D5:
                    slotNumber = 4;
                    break;
                case ConsoleKey.D6:
                    slotNumber = 5;
                    break;
            }

            switch (user.MyKey)
            {
                case ConsoleKey.D1:
                    user.MyChamp.EquipItem(new InfinityEdge(), (ItemSlotNumber)slotNumber);
                    break;
                case ConsoleKey.D2:
                    user.MyChamp.EquipItem(new HpPotion(), (ItemSlotNumber)slotNumber);
                    break;
                case ConsoleKey.D3:
                    user.MyChamp.EquipItem(new QuicksilverSash(), (ItemSlotNumber)slotNumber);
                    break;
            }

            Console.Clear();
            RendeTitle();
        }


        public void GameLoop()
        {
            RendeTitle();
            while (gameLoop)
            {
                RenderNormalMenual();
                if (!gameLoop)
                {
                    return;
                }
            }
        }

        static void Main(string[] argc)
        {
            LeagueOfLegend lol = new LeagueOfLegend();
            lol.GameLoop();
        }
    }
    #endregion
}