using NAudio.Wave;
using OpusSharp.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_Studio.Service
{
    public class NAudioService : IDisposable
    {
        private readonly OpusDecoder _decoder;
        private readonly BufferedWaveProvider _waveProvider;
        private readonly WaveOutEvent _outputDevice;

        public NAudioService()
        {
            _decoder = new OpusDecoder(48000, 1); // 单声道
            _waveProvider = new BufferedWaveProvider(new WaveFormat(48000, 16, 1));
            _outputDevice = new WaveOutEvent();
            _outputDevice.Init(_waveProvider);
            _outputDevice.Play();
        }

        public void PlayOpusData(byte[] opusFrame)
        {
            short[] pcmBuffer = new short[5760];
            int decodedSamples = _decoder.Decode(
                opusFrame, opusFrame.Length,
                pcmBuffer, pcmBuffer.Length,
                false);

            // 转换short为byte
            byte[] pcmBytes = new byte[decodedSamples * 2];
            Buffer.BlockCopy(pcmBuffer, 0, pcmBytes, 0, pcmBytes.Length);
            _waveProvider.AddSamples(pcmBytes, 0, pcmBytes.Length);
        }


        public void Dispose()
        {
            _outputDevice.Stop();
            _outputDevice.Dispose();
        }
    }
}
