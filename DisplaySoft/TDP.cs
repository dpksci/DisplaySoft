using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplaySoft
{
    internal class TDP
    {
        private int[,] RB_IData = new int[150, 1024]; // Scaled_RB_IData = new int[512, 1024];
        private int[,] RB_QData = new int[150, 1024]; // Scaled_RB_QData = new int[512, 1024];

        private float[,] fScaled_RB_IData = new float[150, 1024];
        private float[,] fScaled_RB_QData = new float[150, 1024];

        public LinePlot LinePlot1_TD, LinePlot2_TD;

        public TDP()
        {
            LinePlot1_TD = new LinePlot();
            LinePlot1_TD.SetPlotParameters();

            LinePlot2_TD = new LinePlot();
            LinePlot2_TD.SetPlotParameters();
        }

        public void UpdateChart(int[,,] Updated_RB_Data, int nrgb, int nfft)
        {
            
            SplitData(Updated_RB_Data, nrgb, nfft);

            Scale(RB_IData, fScaled_RB_IData, nrgb, nfft);
            Scale(RB_QData, fScaled_RB_QData, nrgb, nfft);

            LinePlot1_TD.UpdateChart(fScaled_RB_IData, nrgb, nfft);
            LinePlot2_TD.UpdateChart(fScaled_RB_QData, nrgb, nfft);
        }
        /*public void UpdateChart(float[,,] Updated_RB_Data, int nrgb, int nfft)
        {
            SplitData(Updated_RB_Data, nrgb, nfft);

            Scale(RB_IData, Scaled_RB_IData, nrgb, nfft);
            Scale(RB_QData, Scaled_RB_QData, nrgb, nfft);

            LinePlot1_TD.UpdateChart(fScaled_RB_IData, nrgb, nfft);
            LinePlot2_TD.UpdateChart(fScaled_RB_QData, nrgb, nfft);
        }
*/

        private void SplitData(int[,,] IQ_Data, int nrgb, int nfft)
        {
            for(int i= 0; i < nrgb; i++)
            {
                for(int j= 0; j < nfft; j++)
                {
                    RB_IData[i, j] = IQ_Data[i, j, 0];
                    RB_QData[i, j] = IQ_Data[i, j, 1];
                }
            }
        }

        /*private void SplitData(float[,,] IQ_Data, int nrgb, int nfft)
        {
            for (int i = 0; i < nrgb; i++)
            {
                for (int j = 0; j < nfft; j++)
                {
                    fRB_IData[i, j] = IQ_Data[i, j, 0];
                    fRB_QData[i, j] = IQ_Data[i, j, 1];
                }
            }
        }
*/

        public void Scale(int[,] unscaled, float[,] scaled, int nrgb, int nfft)
        {
            for (int j = 0; j < nrgb; j++)
            {
                int maxx = int.MinValue;
                
                for (int i = 0; i < nfft; i++)
                {
                    int val = unscaled[j, i];

                    if (val > maxx) maxx = val;

                }
                //float m = maxx;
                if (maxx == 0) maxx = 1;
                for (int i = 0; i < nfft; i++)
                {
                    float val = Convert.ToSingle(unscaled[j, i]) / Convert.ToSingle(maxx);
                    scaled[j, i] = val ;
                    //Scaled_RB_Data[j, i] = Convert.ToSingle(10 * Math.Log10(RB_Data[j, i]) );
                }
            }

          

        }
        /*public void Scale(float[,] unscaled, float[,] scaled, int nrgb, int nfft)
        {
            for (int j = 0; j < nrgb; j++)
            {
                float maxx = float.MinValue;

                for (int i = 0; i < nfft; i++)
                {
                    float val = unscaled[j, i];

                    if (val > maxx) maxx = Convert.ToInt32(val);

                }
                //float m = maxx;
                if (maxx == 0) maxx = 1;
                for (int i = 0; i < nfft; i++)
                {
                    scaled[j, i] = unscaled[j, i] / maxx;
                    //Scaled_RB_Data[j, i] = Convert.ToSingle(10 * Math.Log10(RB_Data[j, i]) );
                }
            }



        }
*/
    }
}
