using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
//using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DisplaySoft
{
    internal class DataReader
    {
        #region Fields:
        private int nfft, nrgb, frameSize, maxFFT, maxRGB;
        private string watcherPath = "D:\\New folder";
        private string[] extensions = new string[2];
     
     
        public string WatcherPath { get { return watcherPath; } set { watcherPath = value; } }
        public int FrameSize { get { return frameSize; } set { frameSize = value; } }
        public int NFFT { get { return nfft; } set { nfft = value; } }
        public int NRGB { get { return nrgb; } set { nrgb = value; } }
        public int MaxFFT { get { return maxFFT; } set { maxFFT = value; } }
        public int MaxRGB { get { return maxRGB; } set { maxRGB = value; } }

        //private XMLReader XR;
        //private XElement xElements;
        #endregion

        int clock = 500;
        //**********************************

        public bool IsUpdated { get; set; }

        //**********************************

        private Watcher WH_TD, WH_PS;
        Timer Timer_WH;

        public byte[] Data_Byte_TD = new byte[(1024 * 512) * 4 * 2 + 128];
        public byte[] Data_Byte_PS = new byte[(1024 * 512) * 4 + 128];

        public int[,,] fiData_RB_TD = new int[150, 1024, 2];
        public int[,,] iData_RB_TD = new int[1024, 150, 2];
        

        public float[,] Data_RB_PS = new float[512, 1024];

        private DataHeaderStruct DH { get; set; }
        //public DataHeaderStruct DHS { get { return DH; } set { DH = value; } }


        [StructLayout(LayoutKind.Sequential)]
        public struct DataHeaderStruct
        {
            public short MagicNumber;
            public short baudlength;
            public short nrgb;
            public short nfft;
            public short ncoh;
            public short nicoh;
            public short ipp;
            public short pwd;
            public short cflag;
            public short nwin;
            public short w1start;
            public short w1len;
            public short w2start;
            public short w2len;
            public short year;
            public short month;
            public short day;
            public short hour;
            public short min;
            public short sec;
            public short nbeams;
            public short beam;
            public short scancycle;
            public short attn;
            public short w3start;
            public short w3len;
            public short simrange1;
            public short txpower;
            public short winfn;
            public short noofpulses;
            public short dtype;
            public short pulsedelay1;
            public short pulsedelay2;
            public short pulsedelay3;
            public short pulsedelay4;// = new short[4];
            public float Anglestorage1;
            public float Anglestorage2;// = new float[2];
            public short RadarType;
            public short pulsedelay10;
            public short pulsedelay11;
            public short simrange2;
            public short stc_win_start;
            public short noOfFreq;
            public float txIFFreq1;
            public float txIFFreq2;
            public float txIFFreq3;
            public float txIFFreq4; //= new float[4];
            public short operationMode;
            public short adptiveRefRange;
            public short adaptiveRefLevel;
            public char commentCode;
            public char comment; //  = new char[13];


        }

        public DataReader()
        {
            IsUpdated = false;
            Timer_WH = new Timer();
            Timer_WH.Interval = (clock);
            Timer_WH.Tick += new EventHandler(Timer_WH_Tick);
            Timer_WH.Start();

            /*XR = new XMLReader();
            XR.ReadXml();*/

            WH_TD = new Watcher(WatcherPath, ".r"); //////////
            WH_TD.UpdatedByteData = Data_Byte_TD;

            WH_PS = new Watcher(WatcherPath, ".d"); ///////////
            WH_PS.UpdatedByteData = Data_Byte_PS; //WH_PS.SetBuffer(PS_data);


            //GetContent(XR.GetContent());
            //Print(GetContent(XR.GetContent()));
        }

        public DataHeaderStruct GetDataHeaderFromByte(byte[] updatedByte)
        {
            DataHeaderStruct DH = new DataHeaderStruct();
            int l = Marshal.SizeOf(DH);
            IntPtr ptPoit = Marshal.AllocHGlobal(l);
            Marshal.Copy(updatedByte, 0, ptPoit, l);
            DH = (DataHeaderStruct)Marshal.PtrToStructure(ptPoit, typeof(DataHeaderStruct));
            Marshal.FreeHGlobal(ptPoit);
            return DH;
        }

        //public 
        
        private void Timer_WH_Tick(object sender, EventArgs e)
        {
            if (WH_TD.IsUpdated)
            {
                long dataSize = WH_TD.DataIndex;
                
                if(dataSize == 128)
                { 
                    
                    DH = GetDataHeaderFromByte(Data_Byte_TD);

                    NRGB = DH.nrgb;
                    NFFT = DH.nfft;

                    
                    WH_TD.ConfirmRead();
                }
                else
                {
                    if ((dataSize == (NRGB * NFFT * 4) * 2 ))
                    {
                        Buffer.BlockCopy(Data_Byte_TD, 0, iData_RB_TD, 0, Convert.ToInt32(dataSize));

                        Format_TD_Data(iData_RB_TD, fiData_RB_TD);

                        //Buffer.BlockCopy(Data_Byte_TD, 0, fData_RB_TD, 0, Convert.ToInt32(dataSize));

                        IsUpdated = true;
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("Error in reading NRGB * NFFT Data.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //IsUpdated = true;
                    WH_TD.ConfirmRead();
                }

               
            }

            if (WH_PS.IsUpdated)
            {
                //DataHeader DH;
                long dataSize = WH_PS.DataIndex;

                if (dataSize == 128)
                {
                    DH = GetDataHeaderFromByte(Data_Byte_PS);

                    NRGB = DH.nrgb;
                    NFFT = DH.nfft;
                    
                    MaxFFT = NFFT;
                    MaxRGB = NRGB;

                    Data_RB_PS = new float[MaxRGB, MaxFFT];

                    WH_PS.ConfirmRead();
                }
                else
                {
                    if ((dataSize == (NRGB * NFFT * 4)))
                    {
                        Buffer.BlockCopy(Data_Byte_PS, 0, Data_RB_PS, 0, Convert.ToInt32(dataSize));

                        // UPDATE DATA
                        IsUpdated = true;
                    }
                    else
                    {
                        //long d = dataSize;
                        DialogResult dr = MessageBox.Show("Error in reading NRGB * NFFT Data.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    WH_PS.ConfirmRead();
                    
                }

                //IsUpdated = true;
            }

           
        }
       
        public void Print(IEnumerable<XElement> DataReaders)
        {
            //IEnumerable<XElement> elements = GetData();
            Console.WriteLine("\nReading Radar Data");
            foreach (var item in DataReaders)
            {
                Console.WriteLine(item);
            }
        }

        private void Format_TD_Data(int[,,] iData_RB_TD, int[,,] fiData_RB_TD)
        {
            int nrgb = this.nrgb; int nfft = this.nfft;

            for(int i = 0; i < nfft; i++)
            {
                for (int j = 0; j < nrgb; j++)
                {
                    fiData_RB_TD[j, i, 0] = iData_RB_TD[i, j, 0];
                    fiData_RB_TD[j, i, 1] = iData_RB_TD[i, j, 1];
                }
            }

        }
    }
}


/*public IEnumerable<XElement> GetContent()
        {
            xElements = XR.GetContent();

            var elements = from elemList in xElements.Elements("Configurations")
                           .Elements("FileExtension")
                           select elemList;
            return elements;
        }*/
////////////////

// reads updated file using Watcher Class.
/*public void ReadUpdates(Watcher watcher)
{
    if (watcher.IsUpdated)
    {
        byte[] updatedData = watcher.UpdatedData;

        int bins = updatedData[2];
        int points = updatedData[3];

        float[,] RB_Data = new float[,] { };
        for (int i = 0; i < bins; i++)
        {
            for (int j = 0; j < points; j++)
            {
                RB_Data[i, j] = updatedData[watcher.DataIndex];
            }
        }

    }
}
*/