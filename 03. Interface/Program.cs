/***************************************************************************
 
 * 인터페이스 
  - 멤버를 가질 수 있지만 직접 구현하지 않고 정의만을 가진다.
  - 인터페이스를 가지는 클래스에서 반드시 인터페이스의 정의를 구현해야한다.
    - 이는 "인터페이스를 포함하는 클래스 모두가 반드시 인터페이스의 
      구성요소들을 구현했다는 것을 보장한다." 라고 할 수 있다.
  - Can-a 관계 : 클래스가 해당 *행동을 할 수 있는 경우 적합하다.

 * 인터페이스 네이밍
  - 인터페이스의 이름은 I로 시작한다.
    ex) IEnterable, IEquipable 등등

 * 추상클래스와 인터페이스
  - 인터페이스는 추상클래스의 일종으로 추상클래스와 특징이 유사한 것을 볼 수 있음.

  - 공통점 : 함수에 대한 선언만 정의하고 이를 포함하는 클래스에서 구체화하여 사용
  - 차이점 : 추상클래스 - 변수, 함수의 구현 포함 가능 / 다중상속 불가
             인터페이스 - 변수, 함수의 구현 포함 불가 / 다중포함 가능

  - 추상클래스 (A is B 관계)
    - 상속 관계인 경우, 자식클래스가 부모클래스의 하위분류인 경우
    - 상속을 통해 얻을 수 있는 효과를 얻을 수 있음
    - 부모클래스의 기능을 통해 자식클래스의 기능을 확장하는 경우 사용
  - 인터페이스 (A Can B 관계)
    - 행동 포함인 경우, 클래스가 해당 행동을 할 수 있는 경우
    - 인터페이스를 사용하는 모든 클래스와 상호작용이 가능한 효과를 얻을 수 있음
    - 인터페이스에 정의된 함수들을 클래스의 목적에 맞게 기능을 구현하는 경우 사용

***************************************************************************/

namespace _03._Interface
{
    public interface IEquipable     // 장착할 수 있는 것들
    {
        void Equiped();
        void UnEquiped();
    }

    public interface IDefendable    // 방어할 수 있는 것들
    {
        void Defend();
    }

    class Player{
        private Weapon myWeapon;
        public Weapon GetWeapon(){
            return myWeapon;    
        }
        public void SetWeapon(Weapon weapon){
            myWeapon = weapon;
        }

        public void Equip(){
            Console.WriteLine("플레이어가 무기를 듭니다.");
            myWeapon.Equiped();
        }
        public void UnEquip(){
            Console.WriteLine("플레이어가 무기를 내려놓습니다.");
            myWeapon.UnEquiped();
        }
    }



    
    public abstract class Weapon : IEquipable    // Weapon 추상 클래스
    {
        protected string name;

        public void Equiped()
        {
            Console.WriteLine($"{name} 을/를 장착합니다.");
        }
        public void UnEquiped()
        {
            Console.WriteLine($"{name} 을/를 장착해제 합니다.");
        }
        public abstract void Attack();  // 추상함수 선언
    }
    
    public class Gun : Weapon
    {
        public Gun(string name)
        {
            this.name = name;
        }

        public override void Attack()
        {
            Console.WriteLine("탕탕탕");
        }
    }
    public class Sword : Weapon, IDefendable
    {
        public Sword(string name) 
        {
            this.name = name;
        }
        public override void Attack() 
        {
            Console.WriteLine("슉슉슉");
        }
        public void Defend()
        {
            Console.WriteLine("칼로 막기");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            List<Weapon> weaponList = new List<Weapon>();
            weaponList.Add(new Gun("총"));
            weaponList.Add(new Sword("칼"));
            
            foreach(Weapon weapon in weaponList)
            {
                player.SetWeapon(weapon);

                player.Equip();
                player.GetWeapon().Attack();
                #region 테스트용 추상클래스 다운캐스팅
                // 다운캐스팅이 가능하다면
                // 추후 SpecialSkill로 바꿔보기.
                if (player.GetWeapon() is Sword)
                {
                    Sword sword = player.GetWeapon() as Sword;
                    sword.Defend();
                }
                #endregion
                player.UnEquip();
                Console.WriteLine();
            }
        }
    }
}