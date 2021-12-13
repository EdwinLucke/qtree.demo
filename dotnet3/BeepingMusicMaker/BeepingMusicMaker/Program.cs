using System;

namespace BeepingMusicMaker
{
	class Program
	{
		static void Main(string[] args)
		{

			//valid frequency >37 && frequency< 32767 hertz.

			//var MyPiano = new Intro();

			//https://blog.dhampir.no/content/fun-with-beep

			Console.WriteLine("Start the nice intro music");

			Console.Beep(38, 1000); // low 

			Console.Beep(55, 1000); // medium A1

			Console.Beep(110, 1000); // medium A2

			Console.Beep(220, 1000); // medium A3

			Console.Beep(440, 1000); // medium A4

			Console.Beep(880, 1000); // medium A5

			Console.Beep(1760, 1000); // medium A6

			Console.WriteLine("Intro music has ended");

		}


		private int Frequency(int keyNr)

		{

			var toPower = ((keyNr - 49) / 12);

			var powered = Math.Pow((double)keyNr, (double)toPower);

			var resultingFrequency = (int)Math.Round(powered * 440);

			Console.Write($"[{keyNr}-{powered}]{resultingFrequency} ");

			return resultingFrequency;

		}

	}
}
