using System;
using System.IO;
using System.Linq;

namespace CPKModifier
{
	class Program
	{
		static void Main(string[] args)
		{
			string folderPath;

			if (args.Length > 0)
			{
				folderPath = args[0];
			}
			else
			{
				Console.Write("Enter the folder path: ");
				folderPath = Console.ReadLine();
			}

			if (!Directory.Exists(folderPath))
			{
				Console.WriteLine("The provided path is not a valid folder.");
				return;
			}

			string[] fileList = Directory.GetFiles(folderPath);

			foreach (string filePath in fileList)
			{
				byte[] byteArray = File.ReadAllBytes(filePath);
				string byteString = string.Join(" ", byteArray.Select(b => b.ToString("X2")));

				byteString = byteString.Replace("00 11 42 69 70 30 31 20 4C 54 68 69 67 68 54 77 69 73 74",
												"00 12 42 69 70 30 31 20 4C 20 54 68 69 67 68 54 77 69 73 74")
									 .Replace("00 12 42 69 70 30 31 20 4C 54 68 69 67 68 54 77 69 73 74 31",
											  "00 13 42 69 70 30 31 20 4C 20 54 68 69 67 68 54 77 69 73 74 31")
									 .Replace("00 11 42 69 70 30 31 20 52 54 68 69 67 68 54 77 69 73 74",
											  "00 12 42 69 70 30 31 20 52 20 54 68 69 67 68 54 77 69 73 74")
									 .Replace("00 12 42 69 70 30 31 20 52 54 68 69 67 68 54 77 69 73 74 31",
											  "00 13 42 69 70 30 31 20 52 20 54 68 69 67 68 54 77 69 73 74 31");

				byte[] newByteArray = byteString.Split(' ').Select(s => Convert.ToByte(s, 16)).ToArray();
				File.WriteAllBytes(filePath, newByteArray);

				Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
			}

			Console.WriteLine("Modification complete.");
		}
	}
}
