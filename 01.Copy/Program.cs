/*********************************************************************************
  
 <Struct와 Class>
 * Struct : 값 형식 (스택)
 * Class : 참조 형식 (힙)
 * C++과 다른점
    * C#에서는 struct의 상속과 명시적 기본생성자를 지원하지 않는다.
    * C++에서는 기본적으로 모든 멤버가 public인데 C#에서는 따로 명시해주어야한다.


 <메모리 저장방식>
 * 값 형식
    : 변수가 값을 담는 데이터 형식 (*스택 메모리에 할당.) 
 * 참조 형식
    : 변수의 값이 아닌 값이 있는 곳의 *주소를 담는 데이터 형식 (*힙 메모리에 할당.)

 <깊은 복사와 얕은 복사>
 * 얕은 복사 (Shallow Copy)
    : 객체를 복사할 때 참조만을 복사하는 것.
    : 참조를 복사한 것이기에 복사된 값을 변경할 경우 원본의 값도 변경됨. 
        ex) A클래스 객체를 생성한 뒤 B클래스에 복사하는 경우 (클래스는 참조형식)
            B클래스의 값을 변경하면 원본인 A클래스의 값도 변경됨.
            
 * 깊은 복사 (Deep Copy)
    : 객체의 값을 복사하고 별도의 힙 메모리에 할당하는 것.
    : 복사본만의 별도의 힙 메모리에 할당되어 수정해도 원본 값에 영향을 주지 않는다.
    : C#에서는 기능 지원이 없어 DeepCopy 함수를 직접 만들어 사용하자.

 *********************************************************************************/

namespace _01._Copy
{
    /// <summary>
    /// 얕은 복사 예시 클래스
    /// </summary>
    class ShallowCopyClass
    {
        public int number1;
        public int number2;
    }
    /// <summary>
    /// 깊은 복사 예시 클래스
    /// </summary>
    /// 
    // 오버로드 오버라이드
    // 오버로딩
    // 
    public class over
    {

        // 함수에서 덮어쓰는 행위 오버로딩
        int func1(int a, int b) { return a + b; }
        double func1(double a, double b) { return a + b; }
    
        // 부모 클래스의 함수를 덮어쓰는 행위 오버라이딩
    }

    // 추상 : abstract 자식에서 무조건 형체를 만들어주어야함. -> 정의를 포함하면 안됨. 선언만 가능.
    // 가상 : virtual 자식에서 덮어쓰는 느낌. -> 정의를 포함해도 되지만, 자식에서 재정의 할 경우 덮어쓰여짐.


    class DeepCopyClass
    {
        public int number1;
        public int number2;
        
        public DeepCopyClass DeepCopy()
        {
            // 새 인스턴스를 생성해 참조를 별도의 힙 영역에 할당한다.
            DeepCopyClass tempClass = new DeepCopyClass();
            tempClass.number1 = this.number1;
            tempClass.number2 = this.number2;
            return tempClass;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "깊은 복사와 얕은 복사";    // Console창의 제목을 설정

            // 얕은 복사 예제
            ShallowCopyClass scpy1 = new ShallowCopyClass();
            scpy1.number1 = 10;
            scpy1.number2 = 20;

            ShallowCopyClass scpy2 = scpy1; // 얕은 복사
            Console.WriteLine("-- 얕은 복사 예제 --\n");
            Console.WriteLine("<복사본 변경 전>");
            Console.WriteLine($"원본1 : {scpy1.number1}");
            Console.WriteLine($"원본2 : {scpy1.number2}");
            Console.WriteLine($"복사본1 : {scpy2.number1}");
            Console.WriteLine($"복사본2 : {scpy2.number2}");
            scpy2.number1 += 10;
            Console.WriteLine("\n\n<복사본 1 +10>");
            Console.WriteLine($"원본1 : {scpy1.number1}");
            Console.WriteLine($"원본2 : {scpy1.number2}");
            Console.WriteLine($"복사본1 : {scpy2.number1}");
            Console.WriteLine($"복사본2 : {scpy2.number2}");
            Console.ResetColor();

            // 깊은 복사 예제
            DeepCopyClass dcpy1 = new DeepCopyClass();
            dcpy1.number1 = 10;
            dcpy1.number2 = 20;

            DeepCopyClass dcpy2 = dcpy1.DeepCopy(); // 깊은 복사
            Console.WriteLine("\n\n-- 깊은 복사 예제 --\n");
            Console.WriteLine("<복사본 변경 전>");
            Console.WriteLine($"원본1 : {dcpy1.number1}");
            Console.WriteLine($"원본2 : {dcpy1.number2}");
            Console.WriteLine($"복사본1 : {dcpy2.number1}");
            Console.WriteLine($"복사본2 : {dcpy2.number2}");
            dcpy2.number1 += 10;
            Console.WriteLine("\n\n<복사본 1 +10>");
            Console.WriteLine($"원본1 : {dcpy1.number1}");
            Console.WriteLine($"원본2 : {dcpy1.number2}");
            Console.WriteLine($"복사본1 : {dcpy2.number1}");
            Console.WriteLine($"복사본2 : {dcpy2.number2}");
        }
    }
}