﻿using System;
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
            public string propertiesFile;
        }

        struct Properties
        {
            public string fileName;
            public DateTime begin;
            public DateTime end;
            public int frequency;
            public int samples;
        }

        public void OpenFolderAndSaveConversionResult(string pathToSource, string pathToDest)
        {
            ChannelFiles files = GetFilesFromFolder(pathToSource);
            Properties prop = ExtractFileProperties(files.propertiesFile);
            short[][] numbers = {   ConvertChannel0 ? ConvertWAVtoShortArray(files.channel0File) : new short[0],
                                    ConvertChannel1 ? ConvertWAVtoShortArray(files.channel1File) : new short[0],
                                    ConvertChannel2 ? ConvertWAVtoShortArray(files.channel2File) : new short[0]
                                };
            WriteNumbersToFile(numbers, prop, pathToDest);
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
                else if (file.EndsWith(".prop"))
                    channels.propertiesFile = file;
            }
            return channels;
        }

        Properties ExtractFileProperties(string path)
        {
            Properties prop = new Properties();
            StreamReader sr = new StreamReader(path);

            string name = sr.ReadLine();
            prop.fileName = name.Substring(name.IndexOf('=') + 1);

            string beginDate = sr.ReadLine();
            prop.begin = ParseDate(beginDate.Substring(beginDate.IndexOf('=')+1));

            string endDate = sr.ReadLine();
            prop.end = ParseDate(endDate.Substring(endDate.IndexOf('=')+1));

            string frequency = sr.ReadLine();
            prop.frequency = Int32.Parse(frequency.Substring(frequency.IndexOf('=')+1));

            sr.ReadLine();
            string samples = sr.ReadLine();
            prop.samples = Int32.Parse(samples.Substring(samples.IndexOf('=')+1));

            sr.Close();

            return prop;
        }

        DateTime ParseDate(string str)
        {
            int year = Int32.Parse(str.Substring(0, 4));  
            int month = Int32.Parse(str.Substring(5, 2));
            int day = Int32.Parse(str.Substring(8, 2));
            int hour = Int32.Parse(str.Substring(11, 2));
            int minute = Int32.Parse(str.Substring(14, 2));
            int second = Int32.Parse(str.Substring(17, 2));
            return new DateTime(year, month, day, hour, minute, second);
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
                waveReader.Close();
                return waveBuffer;               
            }
            catch
            {
                throw new InvalidOperationException("Failed to read WAV file");
            }
                      
        }

        void WriteNumbersToFile(short[][] numbers, Properties prop, string filepath)
        {
            var newFilePath = new System.Text.StringBuilder(filepath.Substring(0, filepath.LastIndexOf('\\') + 1));
            string fileName = String.Format("{0}-{1};{2}Hz", prop.begin.ToString("s"), prop.end.ToString("s"), prop.frequency);
            fileName = fileName.Replace(':', '.');
            newFilePath.Append(fileName).Append(".txt");
            StreamWriter ostream = new StreamWriter(newFilePath.ToString());

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
            ostream.Close();
        }
    }



}
