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
        //activation of different funtions of the program, are set in GUI
        bool convertChannel0 = false;
        bool convertChannel1 = false;
        bool convertChannel2 = false;
        bool compressTextFile = true;
        bool checkForIntegrity = true;

        //The start and end points of the intervalof conversion, are set in GUI
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

        // A structure thatholds Date information in strings. Intermediate element between GUI and DateTime from std library.
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

        //A structure that containsall files read from one folder with data.
        struct ChannelFiles
        {
            public string channel0File;
            public string channel1File;
            public string channel2File;
            public string propertiesFile;
        }

        //A structure that contains viable information from .prop file.
        struct Properties
        {
            public string fileName;
            public DateTime begin;
            public DateTime end;
            public int frequency;
            public int samples;
        }
        /// <summary>
        /// Main method  that executes when the button in GUi is pressed.
        /// </summary>
        ///<param name = "pathToSource" > A string that contains path to the folder with all data folders.</param>
        ///<param name = "pathToDestination" > A string that contains path to the folder where the result folder/zip will becreated.</param>
        public void OpenFolderAndSaveConversionResult(string pathToSource, string pathToDest)
        {

            DateTime startingDate;
            DateTime endingDate;
            //  Try to parse dates from GUI input. 
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

            //Get a list of allfolders with data and sort it for more convenience
            string[] folders = GetListOfFolders(pathToSource);
            Array.Sort(folders);
            bool atLeastOneFolderIsFound = false;

            if (checkForIntegrity)
            {
                //If check for integrity was setto true in GUI then log file is created and all folders are checked for continuity. 
                //There is an additional check for existence of enough number of folders to fill the whole time interval. 
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
                            //Check every folder in the time interval
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
                                //If a time is not coherent, a log entry is written to the file.
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
                //If there are missing files justrethrow the exception
                catch (MissingDataException e) {
                    throw new MissingDataException(e.Message);
                }
                //In all other cases we also need to delete the log file since the information in it is no coherent.
                catch (Exception e)
                {
                    //
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

            //Try to convert every folderin the path and write the result into one text file
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
                            //If the folder is the firstwhich name falls into the intervalthereis a need for converting asome part of previous folder that  contains the beginning of the interval.  
                            ChannelFiles prevFiles = GetFilesFromFolder(previousFolder);
                            Properties prevProp = ExtractFileProperties(prevFiles.propertiesFile);

                            int ticksDifference = (int)((prevProp.end - startingDate).TotalMilliseconds / 1000 * prevProp.frequency); //Calculate the amount of elements thatneeds to be written

                            float[][] prevNumbers = {
                                                    ConvertChannel0 ? ConvertWAVtoFloatArray(prevFiles.channel0File) : new float[0],
                                                    ConvertChannel1 ? ConvertWAVtoFloatArray(prevFiles.channel1File) : new float[0],
                                                    ConvertChannel2 ? ConvertWAVtoFloatArray(prevFiles.channel2File) : new float[0]
                                                    };
                            WriteNumbersToFile(prevNumbers, prevNumbers[0].Length - ticksDifference, ostream);

                            foldersFound++;
                            previousEndingDate = prevProp.end;
                        }
                        //Get all info about current folder.
                        ChannelFiles files = GetFilesFromFolder(folder);
                        Properties prop = ExtractFileProperties(files.propertiesFile);
                        float[][] numbers = {
                                            ConvertChannel0 ? ConvertWAVtoFloatArray(files.channel0File) : new float[0],
                                            ConvertChannel1 ? ConvertWAVtoFloatArray(files.channel1File) : new float[0],
                                            ConvertChannel2 ? ConvertWAVtoFloatArray(files.channel2File) : new float[0]
                                            };
                        if (prop.end <= endingDate)
                        {
                            //IFthe current folder lies entirely inside of theinterval, write everything to the file.
                            WriteNumbersToFile(numbers, ostream);
                            foldersFound++;
                        }

                        else if (prop.begin < endingDate)
                        {
                            //If only a part of the filelies inside the interval calculate the amountof elemetns to be written into the text file.
                            int ticksDifference = (int)((endingDate - prop.begin).TotalMilliseconds / 1000 * prop.frequency);
                            WriteNumbersToFile(numbers, 0, ticksDifference, ostream);
                            foldersFound++;
                        }
                        else
                        {
                            //If the file is outside of the interval - finish up the loop.
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
                //If compressing was set on in the GUI invoke compression of the resulting files
                CompressFile(destinationPath);   
        }

        /// <summary>
        /// Returns a list of all subfolders in a folder.
        /// </summary>
        ///<param name = "path" > A path to the folder.</param>
        ///<returns> An array with all paths to subfolders.</returns>
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

        /// <summary>
        /// Parses date and time from the name of the folder  with data.
        /// </summary>
        ///<param name = "path" > A path to the folder.</param>
        ///<returns> The parsed date and time.</returns>
        DateTime ParseDateTimeFromName(string path)
        {
            try
            {
            string folder = path.Substring(path.LastIndexOf('\\') + 1);
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

        /// <summary>
        /// Gets all WAV files that are in line for conversion and the properties file from a folder with data and returns a struct which contains them all.
        /// </summary>
        ///<param name = "path" > A path to the folder.</param>
        ///<returns> Struct with all files.</returns>
        ChannelFiles GetFilesFromFolder(string path)
        {
            string[] files;
            try
            {
                files = Directory.GetFiles(path);
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

        /// <summary>
        /// Reads properties from properties file and returns a struct with all the info.
        /// </summary>
        ///<param name = "path" > A path to the properties file.</param>
        ///<returns> A struct with all relevant information about the conversion parameters.</returns>
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

        /// <summary>
        /// Parses date from a string in the format written in properties files.
        /// </summary>
        ///<param name = "str" > A string with a Date.</param>
        ///<returns> The parsed date and time.</returns>
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

        /// <summary>
        /// Uses NAudio library to read a WAV file and convert it to a float array.
        /// </summary>
        ///<param name = "path" > A path to the WAV file.</param>
        ///<returns> A short array with data extracted from the file.</returns>
        float[] ConvertWAVtoFloatArray(string path)
        {
            if (path  == null)
                return new float[0];
            try {
                var waveReader = new NAudio.Wave.WaveFileReader(path);
                int bytesNumber = (int)waveReader.Length;
                var byteBuffer = new byte[bytesNumber];
                waveReader.Read(byteBuffer, 0, byteBuffer.Length);
                int shortNumber = bytesNumber / 2 + bytesNumber % 2;
                var waveBuffer = new float[shortNumber];
                for (int i = 0, j = 0; i < bytesNumber; i += 2, j++)
                {
                    short value = BitConverter.ToInt16(byteBuffer, i);
                    waveBuffer[j] =  value < 0 ? -(float)(value) / Int16.MinValue
                                               : (float)(value) / Int16.MaxValue;

                }
                waveReader.Close();
                return waveBuffer;               
            }
            catch
            {
                throw new InvalidOperationException("Failed to read WAV file");
            }
                      
        }



        /// <summary>
        /// Creates a path to the file where the result of conversion or log entries will be written.
        /// </summary>
        ///<param name = "path" > A string with the  path to the folder entered by the user.</param>
        ///<param name = "startingDate" > A Date with the starting timeof conversion.</param>
        ///<param name = "endingDate" > A Date with the ending timeof conversion.</param>
        ///<param name = "appendix" > A name and an extention of the file itself.</param>
        ///<returns> A combined string with the resukting path.</returns>
        string CompilePath(string path, DateTime startingDate, DateTime endingDate, string appendix)
        {
            var newFilePath = new System.Text.StringBuilder(path.Substring(0, path.LastIndexOf('\\') + 1));
            string fileName = String.Format("{0}-{1}", startingDate.ToString("s"), endingDate.ToString("s"));
            fileName = fileName.Replace(':', '.');
            newFilePath.Append(fileName);
            Directory.CreateDirectory(newFilePath.ToString());
            newFilePath.Append("\\");
            newFilePath.Append(appendix);  
            return newFilePath.ToString();
        }

        /// <summary>
        /// Writes all numbers from a short array  intoa file stream.
        /// </summary>
        ///<param name = "numbers" > A short array.</param>
        ///<param name = "ostream" > A stream whcih writestoa file.</param>
        void WriteNumbersToFile(float[][] numbers, StreamWriter ostream)
        {
            int length = Math.Max(numbers[0].Length, Math.Max(numbers[1].Length, numbers[2].Length));
            WriteNumbersToFile(numbers, 0, length, ostream);
        }
        /// <summary>
        /// Writes all numbers from a short array  intoa file stream.
        /// </summary>
        ///<param name = "numbers" > A short array.</param>
        ///<param name = "startIndex" > A starting index from which the elements of the array will be written.</param>
        ///<param name = "ostream" > A stream whcih writestoa file.</param>
        void WriteNumbersToFile(float[][] numbers, int startIndex, StreamWriter ostream)
        {
            int length = Math.Max(numbers[0].Length, Math.Max(numbers[1].Length, numbers[2].Length));
            WriteNumbersToFile(numbers, startIndex, length, ostream);
        }
        /// <summary>
        /// Writes all numbers from a short array  intoa file stream.
        /// </summary>
        ///<param name = "numbers" > A short array.</param>
        ///<param name = "startIndex" > A starting index from which the elements of the array will be written.</param>
        ///<param name = "endIndex" > An ending index until which the elements of the array will be written.</param>
        ///<param name = "ostream" > A stream whcih writestoa file.</param>
        void WriteNumbersToFile(float[][] numbers, int startIndex, int endIndex, StreamWriter ostream)
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
        /// <summary>
        /// Compresses a folder into a ZIPfile.
        /// </summary>
        ///<param name = "path" > A path to a folder.</param>
        void CompressFile(string pathToFile)
        {
            string tempFolder = pathToFile.Substring(0, pathToFile.LastIndexOf('\\'));
            string zipPath = tempFolder + ".zip";
            ZipFile.CreateFromDirectory(tempFolder, zipPath);
            Directory.Delete(tempFolder, true);
        }

        /// <summary>
        /// Checks if a ZIP file in some path already exists.
        /// </summary>
        ///<param name = "path" > A path to a folder.</param>
        ///<returns> A boolean with the result of the check</returns>
        bool CheckIfZipExists(string path)
        {
            string pathToZip = path.Substring(0, path.LastIndexOf('\\'))+ ".zip";

            return File.Exists(pathToZip);
        }
    }
}
