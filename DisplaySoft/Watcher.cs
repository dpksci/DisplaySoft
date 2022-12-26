using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using System.Windows.Forms;

namespace DisplaySoft
{
    internal class Watcher
    {
        #region Fields:
        private FileSystemWatcher FSW;
        private Dictionary<string, long> FileStreamPositions = new Dictionary<string, long>();

        private string watchFolderPath, extension, updatedFilePath;
        private int maxDataSize;/////
        private long dataIndex = 0;
        private Byte[] updatedByteData;
        private bool isUpdated = false;

        public string WatchFolderPath { get { return watchFolderPath; } set {  watchFolderPath = value; } }
        public string UpdatedFilePath { get { return updatedFilePath; } set {  updatedFilePath = value; } }
        public string Extension { get { return extension; } set { if (value != null) extension = value; } }

        public int MaxDataSize { get { return maxDataSize; } set { maxDataSize = value; } }
        public Byte[] UpdatedByteData { get { return updatedByteData; } set { updatedByteData = value; } }
        public long DataIndex { get { return dataIndex; } set { dataIndex = value; } }
        public bool IsUpdated { get { return isUpdated; } set { isUpdated = value; } }

        #endregion

        public bool IsValidFilePath(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return true;
                }
                else throw (new FileNotFoundException("File not Found. Please Enter a valid Path."));
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void ConfirmRead()
        {
            IsUpdated = false;
            //UpdatedData = new byte[] { };
            DataIndex = 0;
        }

        public Watcher(string path, string extension)
        {
            WatchFolderPath = path;
            Extension = extension;

            FSW = new FileSystemWatcher
            {
                Path = WatchFolderPath,

                // Watch both files and subdirectories.
                IncludeSubdirectories = true,

                NotifyFilter = //NotifyFilters.Attributes |
                                    NotifyFilters.CreationTime |
                                    NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName |
                                    NotifyFilters.LastAccess |
                                    NotifyFilters.LastWrite |
                                    NotifyFilters.Size,

                // Watch all files.
                Filter = "*" + Extension + "*", // Extension,
                EnableRaisingEvents = true,
            };
       

            FSW.Created += new FileSystemEventHandler(OnCreated);
            FSW.Changed += new FileSystemEventHandler(OnUpdated);
            FSW.Renamed += new RenamedEventHandler(OnRenamed);

            SetFilesPositions(WatchFolderPath);
        }
        public Watcher()
        {

            FSW = new FileSystemWatcher
            {
                Path = WatchFolderPath,

                // Watch both files and subdirectories.
                IncludeSubdirectories = true,

                NotifyFilter = //NotifyFilters.Attributes |
                                    NotifyFilters.CreationTime |
                                    NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName |
                                    NotifyFilters.LastAccess |
                                    NotifyFilters.LastWrite |
                                    NotifyFilters.Size,

                // Watch all files.
                Filter = "*.*",
                EnableRaisingEvents = true,
            };


            FSW.Created += new FileSystemEventHandler(OnCreated);
            FSW.Changed += new FileSystemEventHandler(OnUpdated);
            FSW.Renamed += new RenamedEventHandler(OnRenamed);

            SetFilesPositions(WatchFolderPath);
        }


        #region Event Handlers:
        public void OnCreated(object source, FileSystemEventArgs e)
        {
            FileInfo fInfo = new FileInfo(e.FullPath);
            //Console.WriteLine("CREATED: {0}, with path {1}", fInfo.Name, e.FullPath);

            //SetCurrentPosition(e.FullPath);
            /*if (e.FullPath.Contains(Extension))
            {
                long Pos = new FileInfo(e.FullPath).Length;
                if (!FileStreamPositions.ContainsKey(e.FullPath)) FileStreamPositions.Add(e.FullPath, Pos);
                else { FileStreamPositions[e.FullPath] = Pos; }
            }
            else
            {
                FileStreamPositions.Add(e.FullPath, 0);
            }*/
        }
        public void OnRenamed(object source, RenamedEventArgs e)
        {
            FileInfo fInfo = new FileInfo(e.FullPath);
            //Console.WriteLine("RENAMED: {0} renamed to {1} at path {2}", e.OldName, fInfo.Name, e.FullPath);

            //Checking if the newly Created File is a Text File or not.
            //SetCurrentPosition(e.FullPath);
        }
        public void OnUpdated(object source, FileSystemEventArgs e)
        {
            FileInfo fInfo = new FileInfo(e.FullPath);
            string fileName = fInfo.Name;
          
            if (fileName.Contains(Extension))
            {
                using (FileStream stream = File.OpenRead(e.FullPath))
                {
                    long strmPosition;

                    // for making newly created file position = 0;
                    if (FileStreamPositions.ContainsKey(e.FullPath))
                    {
                        strmPosition = GetLastPosition(e.FullPath);
                    }
                    else strmPosition = 0;

                    int bytesToRead = Convert.ToInt32((stream.Length - strmPosition));

                    stream.Position = strmPosition;
                    BinaryReader BR = new BinaryReader(stream);

                    byte[] byteData = BR.ReadBytes(bytesToRead);

                    Buffer.BlockCopy(byteData, 0, UpdatedByteData, Convert.ToInt32(DataIndex), bytesToRead); // byteData.Length

                    DataIndex += bytesToRead;

                    SetCurrentPosition(e.FullPath);     // FileStreamPositions[e.FullPath] = stream.Length;
                }
                IsUpdated = true;
            }

        }


        #endregion

        #region Handling File Positions:

        // Sets Stream Positions for all the pre existing Files in the Watcher Directoires. 
        // path: path of a directory 
        private void SetFilesPositions(string path)
        {
            DirectoryInfo d1 = new DirectoryInfo(path);

            // for searching into the subdirectories.
            FileInfo[] Files = d1.GetFiles(".", SearchOption.AllDirectories);

            foreach (FileInfo file in Files)
            {
                if (file.Extension.Contains(Extension))
                {
                    SetCurrentPosition(file.FullName);
                }

            }

        }

        // sets current stream position at the end of the File.Length.
        // path : path of file whose position is to be stored.
        private void SetCurrentPosition(string path)
        {
            if (path.Contains(Extension))
            {
                long Pos = new FileInfo(path).Length;
                if (!FileStreamPositions.ContainsKey(path)) FileStreamPositions.Add(path, Pos);
                else { FileStreamPositions[path] = Pos; }
            }
            else
            {
                FileStreamPositions.Add(path, new FileInfo(path).Length);
            }
        }

        // returns Last position of that Files.
        // path : path of file whose position is to be returned.
        private long GetLastPosition(string path)
        {

            long l;
            FileStreamPositions.TryGetValue(path, out l);
            return l;
        }

        #endregion

    }
}