using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09._Sorting
{
    internal class Training
    {
        private List<int> list;

        public Training()
        {
            list = new List<int>();
        }
        public void MakeNewList(int n)
        {
            list.Clear();
            Random rand = new Random();

            while(n-- > 0)
            {
                list.Add(rand.Next(0, 99));
            }
        }

        class SortAlgorithm
        {
            Random rand;
            public SortAlgorithm()
            {
                rand = new Random();
            }


            private void Swap(List<int> list, int left, int right)
            {
                int temp = list[left];
                list[left] = list[right];
                list[right] = temp;
            }

            public void SelectionSort(List<int> list)
            {
                int curIndex = 0;
                while(curIndex < list.Count)
                {
                    int minIndex = 0, value = int.MaxValue;

                    for (int i = curIndex; i < list.Count; i++)
                    {
                        if (list[i] < value)
                        {
                            minIndex = i;
                            value = list[i];
                        }
                    }
                    if(curIndex != minIndex)
                     Swap(list, curIndex, minIndex);
                    curIndex++;
                }
            }
            public void InsertionSort(List<int> list)
            {
                for(int i=1; i<list.Count; i++)
                {
                    for(int j=i; j > 0; j--)
                    {
                        if (list[j-1] < list[j])
                            break;

                        Swap(list, j-1, j);
                    }
                }
            }

            public void MergeSort(List<int> list, int start, int end)
            {
                if (start == end) return;

                int mid = (start+end) / 2;
                MergeSort(list, start, mid);
                MergeSort(list, mid + 1, end);
                Merge(list, start, end, mid);
            }

            private void Merge(List<int> list, int start, int end, int mid)
            {
                List<int> sortedList = new List<int>();
                int left = start;
                int right = mid + 1;

                while (left <= mid && right <= end)
                {
                    if (list[left] < list[right]) 
                        sortedList.Add(list[left++]);
                    else
                        sortedList.Add(list[right++]);
                }

                while (left <= mid) sortedList.Add(list[left++]);
                while (right <= end) sortedList.Add(list[right++]);

                int index = 0;
                for(int i=start; i<= end; i++)
                {
                    list[i] = sortedList[index++];
                }
            }

            public void BubbleSort(List<int> list)
            {
                int curIndex = 0, maxIndex = list.Count - 1;
                while(curIndex < maxIndex)
                {
                    for(int i = curIndex; i < maxIndex; i++)
                    {
                        if (list[i] > list[i + 1]) Swap(list, i, i + 1);
                    }
                    maxIndex--;
                }
            }
            public void QuickSort(List<int> list, int start, int end)
            {
                if (start >= end) return;

                int pivot = start, left = start+1, right = end;
                //do
                //{
                //    pivot = rand.Next(start, end);
                //} while (sorted[pivot] && count < sorted.Length);

                //if (count == sorted.Length)
                //    return;

                //while (sorted[left]) 
                //    left++;
                //while (sorted[right]) 
                //    right--;

                while (left <= right)
                {
                    while (list[left] <= list[pivot] && left < end)
                        left++;
                    while (list[right] >= list[pivot] && right > start)
                        right--;

                    if (left < right)     
                        Swap(list, left, right);
                    else                  
                        Swap(list, pivot, right);
                }
                

                QuickSort(list, start, right - 1);
                QuickSort(list, right + 1, end);
            }
            public void HeapSort(List<int> list)
            {

            }
        }
        class Program
        {
            public Training tr;
            public List<int> tempList;
            
            public Program()
            {
                tr = new Training();
                tr.MakeNewList(10);

                tempList = new List<int>(tr.list);
            }
            public void InitList()
            {
                tempList.Clear();
                tempList = new List<int>(tr.list);
            }

            static void Main(string[] argc)
            {
                Program pr = new Program();
                SortAlgorithm sa = new SortAlgorithm();

                Console.Write("원본 :");
                foreach(int i in pr.tr.list)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                sa.SelectionSort(pr.tempList);
                Console.Write("선택 정렬 :");
                foreach (int i in pr.tempList)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                pr.InitList();
                sa.InsertionSort(pr.tempList);
                Console.Write("삽입 정렬 :");
                foreach (int i in pr.tempList)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                Console.Write("병합 정렬 :");
                pr.InitList();
                sa.MergeSort(pr.tempList, 0, pr.tempList.Count - 1);
                foreach (int i in pr.tempList)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                pr.InitList();
                sa.BubbleSort(pr.tempList);
                Console.Write("버블 정렬 :");
                foreach (int i in pr.tempList)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                Console.Write("  퀵 정렬 :");
                pr.InitList();
                sa.QuickSort(pr.tempList, 0, pr.tempList.Count-1);
                foreach (int i in pr.tempList)
                {
                    Console.Write($"{i} ");
                }

                Console.WriteLine();
            }
        }
    }
}
