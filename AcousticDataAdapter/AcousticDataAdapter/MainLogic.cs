using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcousticDataAdapter
{
    class MainLogic
    {
        bool convertChannel0 = false;
        bool convertChannel1 = false;
        bool convertChannel2 = false;

        public bool ConvertChannel0 { get => convertChannel0; set => convertChannel0 = value; }
        public bool ConvertChannel1 { get => convertChannel1; set => convertChannel1 = value; }
        public bool ConvertChannel2 { get => convertChannel2; set => convertChannel2 = value; }

        public MainLogic()
        {
           
        }

        struct ChannelFiles
        {
            public string channel0File;
            public string channel1File;
            public string channel2File;
        }

        public void OpenFolderAndSaveConversionResult(string pathToSource, string pathToDest)
        {
            ChannelFiles files = GetFilesFromFolder(pathToSource);
            short[][] numbers = {   ConvertChannel0 ? ConvertWAVtoShortArray(files.channel0File) : new short[0],
                                    ConvertChannel1 ? ConvertWAVtoShortArray(files.channel1File) : new short[0],
                                    ConvertChannel2 ? ConvertWAVtoShortArray(files.channel2File) : new short[0]
                                };
            WriteNumbersToFile(numbers, pathToDest);
        }


        ChannelFiles GetFilesFromFolder(string pathToSource)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(pathToSource);
            }
            catch
            {
                throw new DirectoryNotFoundException();
            }
            ChannelFiles channels = new ChannelFiles();
           
            foreach (string file in files){
                if (file.EndsWith("_0.wav"))
                    channels.channel0File = file;
                else if (file.EndsWith("_1.wav"))
                    channels.channel1File = file;
                else if (file.EndsWith("_2.wav"))
                    channels.channel2File = file;
            }
            return channels;
        }
        short[] ConvertWAVtoShortArray(string filepath)
        {
            if (filepath  == null)
                return new short[0];
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

        void WriteNumbersToFile(short[][] numbers, string filepath)
        {
            StreamWriter ostream = new StreamWriter(filepath);
            int length = Math.Max(numbers[0].Length, Math.Max(numbers[1].Length, numbers[2].Length));
            for (int i = 0; i < length; i++)
            {
                var sb = new System.Text.StringBuilder();
                if (i < numbers[0].Length)
                    sb.Append(numbers[0][i]);
                sb.Append(";");
                if (i < numbers[1].Length)
                    sb.Append(numbers[1][i]);
                sb.Append(";");
                if (i < numbers[2].Length)
                    sb.Append(numbers[2][i]);
                ostream.WriteLine(sb);
            }
        }
    }



}
