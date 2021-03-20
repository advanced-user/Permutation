using System;
using System.Diagnostics;

namespace Permutation
{
     class PermutationGenerator
	{
		public int[] Set { get; private set; }
		public int Count { get; set; } = 0;

		public PermutationGenerator(int[] set)
		{
			Stopwatch time = new Stopwatch();

			time.Start();

			MergeSort(ref set);
			
			time.Stop();
			Console.WriteLine("Минут: " + time.Elapsed.Minutes);
			Console.WriteLine("Секунд: " + time.Elapsed.Seconds);
			Console.WriteLine("Миллисекунд: " + time.Elapsed.Milliseconds);
			Console.WriteLine();


			RemoveDuplicate(ref set);

			Set = set;

			Console.WriteLine("Отсортированный массив без дубликатов:");
			for (int i = 0; i < Set.Length; i++)
			{
				Console.Write(Set[i] + " ");
			}
			Console.WriteLine();
			Console.WriteLine();
		}

		/// <summary>
		/// O(n^k)
		/// </summary>
		public void GeneratePlacements(int k, int[] prefix = null)
		{
			if (k == 0)
			{
				Count++;
				Show(prefix);
				return;
			}

			if (prefix == null)
				prefix = new int[0];

			for(int i = 0; i < Set.Length; i++)
			{
				if (Find(Set[i], prefix))
				{
					continue;
				}
				Add(Set[i], ref prefix);
				GeneratePlacements(k - 1, prefix);
				RemoveLast(ref prefix);
			}
		}

		/// <summary>
		/// O(n^k)
		/// </summary>
		public void GenerateCombinations(int k, int[] prefix = null)
		{
			if (k == 0)
			{
				Count++;
				Show(prefix);
				return;
			}

			if (prefix == null)
				prefix = new int[0];

			for (int i = 0; i < Set.Length; i++)
			{
				if (Find(Set[i], prefix) || IsGreaterThan(prefix, Set[i]))
				{
					continue;
				}
				Add(Set[i], ref prefix);
				GenerateCombinations(k - 1, prefix);
				RemoveLast(ref prefix);
			}
		}

		/// <returns>
		/// true, если последний элемент массива prefix[] больше чем параметр item,
		/// иначе - false
		/// </returns>
		public bool IsGreaterThan(int[] prefix, int item)
		{
			if (prefix.Length == 0)
				return false;
			if (prefix[prefix.Length - 1] > item)
				return true;
			return false;
		}

		/// <summary>
		/// Выводит все элементы массива prefix[] на экран.
		/// </summary>
		private void Show(int[] prefix)
		{
			Console.Write("[ ");
			for (int i = 0; i < prefix.Length; i++)
			{
				if (i == prefix.Length - 1)
					Console.Write(prefix[i]);
				else
					Console.Write(prefix[i] + ", ");
			}
			Console.WriteLine(" ]");
		}

		/// <returns>
		/// Возвращает логическое значение true, если массив prefix[] содержит элемент number.
		/// </returns>
		private bool Find(int number, int[] prefix)
		{
			foreach (var item in prefix)
			{
				if (item == number)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Добавляет элемент item в конец массива.
		/// </summary>
		private void Add(int item, ref int[] prefix)
		{
			int length = prefix.Length;
			int[] result = new int[length + 1];

			for (int i = 0; i < length; i++)
			{
				result[i] = prefix[i];
			}
			result[length] = item;

			prefix = result;
		}

		/// <summary>
		/// Удаляет последний элемент из массива. 
		/// </summary>
		private void RemoveLast(ref int[] prefix)
		{
			var length = prefix.Length;
			int[] result = new int[length - 1];

			for (int i = 0; i < length - 1; i++)
			{
				result[i] = prefix[i];
			}

			prefix = result;
		}

		/// <summary>
		/// Сортировка слиянием.
		/// Скорость О(N*logN)
		/// </summary>
		private void MergeSort(ref int[] a)
		{
			if (a.Length <= 1)
				return;

			int middle = a.Length / 2;
			int[] left = new int[middle];
			int[] right = new int[a.Length - left.Length];

			int count = 0;
			while(count < left.Length)
			{
				left[count] = a[count];
				count++;
			}
			for (int i = 0; i < right.Length; i++)
			{
				right[i] = a[count];
				count++;
			}

			MergeSort(ref left);
			MergeSort(ref right);

			int[] c = Merge(left, right);
			for (int i = 0; i < c.Length; i++)
			{
				a[i] = c[i];
			}
		}

		/// <summary>
		/// Слияние двух массивов a[]и b[].
		/// </summary>
		/// <returns>
		/// Возвращает целочисленный массив, состоящий из двух массивов a[] и b[].
		/// </returns>
		private int[] Merge(int[] a, int[] b)
		{
			int[] c = new int[a.Length + b.Length];

			int i = 0;
			int j = 0;
			int n = 0;

			while (i < a.Length && j < b.Length)
			{
				if (a[i] <= b[j])
				{
					c[n] = a[i];
					i++;
					n++;
				}
				else
				{
					c[n] = b[j];
					j++;
					n++;
				}
			}
			while (i < a.Length)
			{
				c[n] = a[i];
				i++;
				n++;
			}
			while (j < b.Length)
			{
				c[n] = b[j];
				j++;
				n++;
			}
			return c;
		}

		/// <summary>
		/// Удаляет все дубликаты из массива.
		/// </summary>
		private void RemoveDuplicate(ref int[] set)
		{
			for(int i = 1; i < set.Length;)
			{
				if (set[i - 1] == set[i])
				{
					Remove(ref set, set[i - 1]);
					continue;
				}
				i++;
			}
		}

		/// <summary>
		/// Удаляет из массива set все элементы, которые равны параметру item.
		/// </summary>
		private void Remove(ref int[] set, int item)
		{
			int[] result = new int[set.Length - 1];

			int count = 0;
			bool find = false;
			for(int i = 0; i < set.Length; i++)
			{
				if (item == set[i] && !find)
				{
					find = true;
					continue;
				}
				result[count] = set[i];
				count++;
			}
			set = result;
		}
	}
}