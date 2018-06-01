using System;
using System.Collections.Generic;
using System.IO;

namespace Shelfinator
{
	class Pattern
	{
		public List<Sequence> Sequences { get; } = new List<Sequence>();
		public Lights Lights { get; private set; } = new Lights();

		public void Save(string fileName)
		{
			using (var output = new BinaryWriter(File.Create(fileName)))
				Save(output);
		}

		public void Save(BinaryWriter output)
		{
			output.Write(Sequences.Count);
			foreach (var sequence in Sequences)
				sequence.Save(output);
			Lights.Save(output);
		}
	}
}
