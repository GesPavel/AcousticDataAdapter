using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcousticDataAdapter
{
    class MainLogic
    {
        bool convertChannel0 = false;
        bool convertChannel1 = false;
        bool convertChannel2 = false;
        bool compressTextFile = true;
        bool checkForIntegrity = true;

        public MyDate begin;
        MyDate end;

        public bool ConvertChannel0 { get => convertChannel0; set => convertChannel0 = value; }
        public bool ConvertChannel1 { get => convertChannel1; set => convertChannel1 = value; }
        public bool ConvertChannel2 { get => convertChannel2; set => convertChannel2 = value; }
        public bool CompressTextFile { get => compressTextFile; set => compressTextFile = value; }
        public bool CheckForIntegrity { get => checkForIntegrity; set => checkForIntegrity = value; }

        public MyDate Begin { get => begin; set => begin = value; }
        public MyDate End { get => end; set => end = value; }
        public MainLogic()
        {
            begin = new MyDate();
            end = new MyDate();
        }

        public class MyDate
        {
            private string year;
            private string month;
            private string day;
            private string hour;
            private string minute;
            private string second;

            public string Year { get => year; set => year = value; }
            public string Month { get => month; set => month = value; }
            public string Day { get => day; set => day = value; }
            public string Hour { get => hour; set => hour = value; }
            public string Minute { get => minute; set => minute = value; }
            public string Second { get => second; set => second = value; }

            public DateTime GetDateTime()
            {
                int year = Int32.Parse(this.year);
                int month = Int32.Parse(this.month);
                int day = Int32.Parse(this.day);
                int hour = Int32.Parse(this.hour);
                int minute = Int32.Parse(this.minute);
                int second = Int32.Parse(this.second);
                return new DateTime(year, month, day, hour, minute, second);
            }
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

            DateTime startingDate;
            DateTime endingDate;
            try
            {
                startingDate = Begin.GetDateTime();
                endingDate = End.GetDateTime();
            }
            catch (FormatException e)
            {
                throw new FormatException("Incorrect date format!");
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Date cannot be empty!");
            }
            if (endingDate < startingDate)
                throw new FormatException("Ending date cannot be earlier than starting date");

            string[] folders = GetListOfFolders(pathToSource);
            Array.Sort(folders);
            bool atLeastOneFolderIsFound = false;

            if (checkForIntegrity)
            {
                if (ParseDateTimeFromName(folders[0]) > startingDate)
                    throw new ArgumentOutOfRangeException("Theres is no entries for entered starting date!");
                string logPath = CompilePath(pathToDest, startingDate, endingDate, "log.txt");
                if (compressTextFile  && CheckIfZipExists(logPath))
                    throw new Exception("Zip with the same name already exists");
                StreamWriter logStream = new StreamWriter(logPath);
                string prevFolder = "";
                DateTime prevEndingDate = new DateTime();
                int numOfMissingFolders = 0;
                try
                {
                    for (int i = 0; i < folders.Length; i++)
                    {
                        DateTime dt = ParseDateTimeFromName(folders[i]);
                        if (startingDate <= dt && dt < endingDate)
                        {
                            if (prevFolder.Length == 0)
                            {
                                prevFolder = folders[i];
                                ChannelFiles prevFiles = GetFilesFromFolder(prevFolder);
                                prevEndingDate = ExtractFileProperties(prevFiles.propertiesFile).end;
                                atLeastOneFolderIsFound = true;
                                continue;
                            }
                            ChannelFiles files = GetFilesFromFolder(folders[i]);
                            Properties prop = ExtractFileProperties(files.propertiesFile);
                            if (prevEndingDate != prop.begin)
                            {
                                numOfMissingFolders++;
                                logStream.WriteLine(String.Format("There is a missing folder between {0} and {1}", prevEndingDate, prop.begin));
                            }
                            prevEndingDate = prop.end;

                        }
                    }
                    if (prevEndingDate < endingDate)
                    {
                        throw new ArgumentOutOfRangeException("There is not enough folders to fill until ending date");
                    }
                    if (!atLeastOneFolderIsFound)
                        throw new ArgumentOutOfRangeException("There is no WAV files recorded in the interval in the directory.");
                    if (numOfMissingFolders > 0)
                    {
                        throw new MissingDataException(String.Format("There is/are {0} missing folders. See log for details. Abandoning conversion.", numOfMissingFolders));
                    }
                }
                catch (MissingDataException e) {
                    throw new MissingDataException(e.Message);
                }
                catch (Exception e)
                {
                    if (logStream != null)
                    {
                        logStream.Flush();
                        logStream.Close();
                    }
                    if (Directory.Exists(logPath.Substring(0, logPath.LastIndexOf('\\'))))
                        Directory.Delete(logPath.Substring(0, logPath.LastIndexOf('\\')), true);
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (logStream != null)
                    {
                        logStream.Flush();
                        logStream.Close();
                    }
                }
            }

            string destinationPath = CompilePath(pathToDest, startingDate, endingDate, "result.txt");
            StreamWriter ostream = new StreamWriter(destinationPath);
            string previousFolder = "";
            
            int foldersFound = 0;
            DateTime previousEndingDate = new DateTime();
            try
            {
                foreach (string folder in folders)
                {
                    DateTime dt = ParseDateTimeFromName(folder);
                    if (startingDate <= dt)
                    {
                        if (foldersFound == 0)
                        {
                            ChannelFiles prevFiles = GetFilesFromFolder(previousFolder);
                            Properties prevProp = ExtractFileProperties(prevFiles.propertiesFile);

                            int ticksDifference = (int)((prevProp.end - startingDate).TotalMilliseconds / 1000 * prevProp.frequency);
                            short[][] prevNumbers = {   ConvertChannel0 ? ConvertWAVtoShortArray(prevFiles.channel0File) : new short[0],
                                                    ConvertChannel1 ? ConvertWAVtoShortArray(prevFiles.channel1File) : new short[0],
                                                    ConvertChannel2 ? ConvertWAVtoShortArray(prevFiles.channel2File) : new short[0]
                                                };
                            WriteNumbersToFile(prevNumbers, prevNumbers[0].Length - ticksDifference, ostream);

                            foldersFound++;
                            previousEndingDate = prevProp.end;
                        }

                        ChannelFiles files = GetFilesFromFolder(folder);
                        Properties prop = ExtractFileProperties(files.propertiesFile);
                        short[][] numbers = {   ConvertChannel0 ? ConvertWAVtoShortArray(files.channel0File) : new short[0],
                                            ConvertChannel1 ? ConvertWAVtoShortArray(files.channel1File) : new short[0],
                                            ConvertChannel2 ? ConvertWAVtoShortArray(files.channel2File) : new short[0]
                                        };
                        if (prop.end <= endingDate)
                        {
                            WriteNumbersToFile(numbers, ostream);
                            foldersFound++;
                        }

                        else if (prop.begin < endingDate)
                        {
                            int ticksDifference = (int)((endingDate - prop.begin).TotalMilliseconds / 1000 * prop.frequency);
                            WriteNumbersToFile(numbers, 0, ticksDifference, ostream);
                            foldersFound++;
                        }
                        else
                        {
                            break;
                        }
                        previousEndingDate = prop.end;
                    }
                    previousFolder = folder;

                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
            finally
            {
                if(ostream != null)
                {
                    ostream.Flush();
                    ostream.Close();
                }
            }
            if (compressTextFile)
                CompressFile(destinationPath);   
        }


        string[] GetListOfFolders(string path)
        {
            string[] folders;
            try
            {
                folders = Directory.GetDirectories(path);
            }
            catch
            {
                throw new DirectoryNotFoundException();
            }
            return folders;
        }

        DateTime ParseDateTimeFromName(string filepath)
        {
            try
            {
            string folder = filepath.Substring(filepath.LastIndexOf('\\') + 1);
            int year = 2000 + Int32.Parse(folder.Substring(0, 2));
            int month = Int32.Parse(folder.Substring(3, 2));
            int day = Int32.Parse(folder.Substring(6, 2));
            int hour = Int32.Parse(folder.Substring(9, 2));
            int minute = Int32.Parse(folder.Substring(12, 2));
            int second = Int32.Parse(folder.Substring(15, 2));
            return new DateTime(year, month, day, hour, minute, second);

            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new ArgumentOutOfRangeException("Incorrect folder is chosen!");
            }
            catch (FormatException e)
            {
                throw new FormatException("Incorrect folder format!");
            }
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


        string CompilePath(string filepath, DateTime startingDate, DateTime endingDate, string appendix)
        {
            var newFilePath = new System.Text.StringBuilder(filepath.Substring(0, filepath.LastIndexOf('\\') + 1));
            string fileName = String.Format("{0}-{1}", startingDate.ToString("s"), endingDate.ToString("s"));
            fileName = fileName.Replace(':', '.');
            newFilePath.Append(fileName);
            Directory.CreateDirectory(newFilePath.ToString());
            newFilePath.Append("\\");
            newFilePath.Append(appendix);  
            return newFilePath.ToString();
        }

        void WriteNumbersToFile(short[][] numbers, StreamWriter ostream)
        {
            int length = Math.Max(numbers[0].Length, Math.Max(numbers[1].Length, numbers[2].Length));
            WriteNumbersToFile(numbers, 0, length, ostream);
        }

        void WriteNumbersToFile(short[][] numbers, int startIndex, StreamWriter ostream)
        {
            int length = Math.Max(numbers[0].Length, Math.Max(numbers[1].Length, numbers[2].Length));
            WriteNumbersToFile(numbers, startIndex, length, ostream);
        }

        void WriteNumbersToFile(short[][] numbers, int startIndex, int endIndex, StreamWriter ostream)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                var sb = new System.Text.StringBuilder();
                if (i < numbers[0].Length)
                    sb.Append(numbers[0][i]);
                sb.Append("\t");
                if (i < numbers[1].Length)
                    sb.Append(numbers[1][i]);
                sb.Append("\t");
                if (i < numbers[2].Length)
                    sb.Append(numbers[2][i]);
                ostream.WriteLine(sb);
            }
        }

        void CompressFile(string pathToFile)
        {
            string tempFolder = pathToFile.Substring(0, pathToFile.LastIndexOf('\\'));
            string zipPath = tempFolder + ".zip";
            ZipFile.CreateFromDirectory(tempFolder, zipPath);
            Directory.Delete(tempFolder, true);
        }

        bool CheckIfZipExists(string logPath)
        {
            string pathToZip = logPath.Substring(0, logPath.LastIndexOf('\\'))+ ".zip";

            return File.Exists(pathToZip);
        }
    }
}
