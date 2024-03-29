﻿using System.IO;

public class StreamWriteDemo
{
    public static void Main()
    {
        using (var reader = new StreamReader("../../StreamWriteDemo.cs"))
        {
            using (var writer = new StreamWriter("../../reversed.txt"))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    for (var i = line.Length - 1; i >= 0; i--)
                    {
                        writer.Write(line[i]);
                    }

                    writer.WriteLine();
                    line = reader.ReadLine();
                }
            }
        }
    }
}
