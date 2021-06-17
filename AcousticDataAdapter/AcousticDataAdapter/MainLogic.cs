using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AcousticDataAdapter
{
    class MainLogic
    {
        public MainLogic()
        {
           
        }

        public void OpenFileAndSaveConversionResult(string pathToSource, string pathToDest)
        {
            short[] numbers = ConvertWAVtoShortArray(pathToSource);
            WriteNumbersToFile(numbers, pathToDest);
        }

        short[] ConvertWAVtoShortArray(string filepath)
        {

            try {
                var waveReader = new NAudio.Wave.WaveFileReader(filepath);
                int bytesNumber = (int)waveReader.Length;
                var byteBuffer = new byte[bytesNumber];
                waveReader.Read(byteBuffer, 0, byteBuffer.Length);
                int shortNumber = bytesNumber / 2 + bytesNumber % 2;
                var waveBuffer = new short[shortNumber];
                for (int i = 0, j = 0; i < bytesNumber; i += 2, j++)
                    waveBuffer[j] = BitConverter.ToInt16(byteBuffer, i);
                return waveBuffer;               
            }
            catch
            {
                throw new InvalidOperationException("Failed to read WAV file");
            }
                      
        }

        void WriteNumbersToFile(short[] numbers, string filepath)
        {
            StreamWriter ostream = new StreamWriter(filepath);
            for (int i = 0; i < numbers.Length; i++)
            {
                ostream.WriteLine(numbers[i]);
            }
        }
    }



}
