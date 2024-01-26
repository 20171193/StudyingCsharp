namespace StudyingCharp
{
    internal class Program
    {
        public static int[] solution(int[] array, int[,] commands)
        {
            int[] answer = new int[commands.GetLength(0)];

            for (int i = 0; i < commands.GetLength(0); i++)
            {
                int[] temp = new int[commands[i, 1] - commands[i, 0] + 1];

                int index = 0;
                for (int j = commands[i, 0] - 1; j < commands[i, 1]; j++, index++)
                {
                    temp[index] = array[j];
                }

                foreach (int j in temp)
                {
                    Console.Write(j);
                }
                Console.WriteLine();

                Array.Sort(temp);

                foreach (int j in temp)
                {
                    Console.Write(j);
                }
                Console.WriteLine();

                //answer[i] = temp[commands[i,2]];
            }
            return answer;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            int[] arr1 = new int[7] { 1, 5, 2, 6, 3, 7, 4 };
            int[,] arr2 = new int[3, 3]
            {
                { 2,5,3},
                { 4,4,1},
                { 1,7,3}
            };
            solution(arr1, arr2);
        }
    }
}