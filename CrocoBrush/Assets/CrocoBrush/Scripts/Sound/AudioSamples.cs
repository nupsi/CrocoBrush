using UnityEngine;

namespace CrocoBrush.Sound
{
    public class AudioSamples
    {
        public AudioSamples(AudioSource source)
        {
            AudioSource = source;
            Update();
        }

        public void Update()
        {
            UpdateSamples();
            CalculateFrequencyBands();
            CreateBandBuffer();
        }

        private void UpdateSamples()
        {
            AudioSource.GetSpectrumData(Samples, 0, FFTWindow.Rectangular);
        }

        private void CalculateFrequencyBands()
        {
            /*
             *   22050 / 512 = 43 hertz per sample.
             *
             *   Hertz    Hertz
             *   20    -  60
             *   60    -  250
             *   25    -  500
             *   500   -  2.000
             *   2.000 -  4.000
             *   4.000 -  6.000
             *   6.000 -  20.000
             */

            var count = 0;

            for(int i = 0; i < 8; i++)
            {
                var avarage = 0f;
                var sampleCount = (int)Mathf.Pow(2, i) * 2;

                if(i == 7)
                {
                    sampleCount += 2;
                }

                for(int j = 0; j < sampleCount; j++)
                {
                    avarage += Samples[count] * (count + 1);
                    count++;
                }

                avarage /= count;

                FrequencyBands[i] = avarage;
            }
        }

        private void CreateBandBuffer()
        {
            for(int i = 0; i < 8; i++)
            {
                if(FrequencyBands[i] > BandBuffer[i])
                {
                    BandBuffer[i] = FrequencyBands[i];
                    BufferDecrease[i] = 0.005f;
                }

                if(FrequencyBands[i] < BandBuffer[i])
                {
                    BandBuffer[i] -= FrequencyBands[i];
                    BufferDecrease[i] *= 1.2f;
                }
            }
        }

        public AudioSource AudioSource { get; }
        public float[] Samples { get; } = new float[512];
        public float[] FrequencyBands { get; } = new float[8];
        public float[] BandBuffer { get; } = new float[8];
        public float[] BufferDecrease { get; } = new float[8];
    }
}