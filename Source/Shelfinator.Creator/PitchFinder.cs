using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Shelfinator.Creator
{
	class PitchFinder
	{
		static double[] ReadWavSamples(string fileName, int timeInMS, int numSamples)
		{
			using (var file = File.OpenRead(fileName))
			using (var reader = new BinaryReader(file, Encoding.ASCII, true) { })
				while (true)
				{
					var header = new string(reader.ReadChars(4));
					var len = reader.ReadInt32();
					if (header == "RIFF")
						len = 4;
					else if (header == "data")
					{
						file.Position += Math.Ceiling(timeInMS * 44.1).Round() * 4;
						var byteArray = reader.ReadBytes(numSamples * 4);
						var result = new double[numSamples];
						for (var ctr = 0; ctr < result.Length; ++ctr)
							result[ctr] = BitConverter.ToUInt16(byteArray, ctr * 4);
						return result;
					}
					file.Position += len;
				}
		}

		static int Log2(int x)
		{
			for (var exp = 0; ; ++exp)
			{
				x >>= 1;
				if (x == 0)
					return exp;
			}
		}

		static int GetReversedBits(int value, int bitCount)
		{
			var newBits = 0;
			for (var ctr = 0; ctr < bitCount; ++ctr)
			{
				newBits = (newBits << 1) | (value & 1);
				value >>= 1;
			}

			return newBits;
		}

		static void ReorderArray(double[] data, int numBits)
		{
			for (var ctr = 0; ctr < data.Length; ++ctr)
			{
				var swap = GetReversedBits(ctr, numBits);
				if (swap > ctr)
				{
					var temp = data[ctr];
					data[ctr] = data[swap];
					data[swap] = temp;
				}
			}
		}

		static double[] FFT(double[] data)
		{
			var numBits = Log2(data.Length);
			if (1 << numBits != data.Length)
				throw new Exception($"Length ({data.Length}) must be a power of 2");

			ReorderArray(data, numBits);

			var im = new double[data.Length];
			var N = 1;
			for (var level = 1; level <= numBits; ++level)
			{
				var M = N;
				N <<= 1;

				var uR = 1d;
				var uI = 0d;
				var angle = Math.PI / M;
				var wR = Math.Cos(angle);
				var wI = Math.Sin(angle);

				for (var j = 0; j < M; ++j)
				{
					for (var even = j; even < data.Length; even += N)
					{
						var odd = even + M;

						var r = data[odd];
						var i = im[odd];

						var odduR = r * uR - i * uI;
						var odduI = r * uI + i * uR;

						r = data[even];
						i = im[even];

						data[even] = r + odduR;
						im[even] = i + odduI;

						data[odd] = r - odduR;
						im[odd] = i - odduI;
					}

					var uwI = uR * wI + uI * wR;
					uR = uR * wR - uI * wI;
					uI = uwI;
				}
			}

			var result = new double[data.Length];
			for (var ctr = 0; ctr < data.Length; ++ctr)
				result[ctr] = Math.Sqrt(data[ctr] * data[ctr] + im[ctr] * im[ctr]);
			return result;
		}

		static MidiNote GetNote(double frequency) => new MidiNote(Math.Log(frequency, Math.Pow(2, 1d / 12)).Round() - 4 * 12);

		public static List<Tuple<MidiNote, double>> DetectNotes(string fileName, int timeInMS, int numSamples)
		{
			var data = ReadWavSamples(fileName, timeInMS, numSamples);
			var pitches = FFT(data);
			var frequencies = pitches.Select((value, index) => new { index, value }).Where(obj => (obj.index != 0) && (obj.index < numSamples / 2)).OrderByDescending(obj => obj.value).Select(obj => Tuple.Create((double)obj.index / numSamples * 44100, obj.value)).ToList();
			var maxValue = frequencies.Max(tuple => tuple.Item2);
			var notes = frequencies.Select(tuple => Tuple.Create(GetNote(tuple.Item1), tuple.Item2 * 100 / maxValue)).ToList();
			return notes;
		}
	}
}
