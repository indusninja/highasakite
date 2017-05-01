using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace High_as_a_Kite
{
    public static class AudioManager
    {
        public static AudioEngine AudioEngine;
        public static WaveBank PaperWaveBank, WindWaveBank, BackgroundWaveBank;
        public static SoundBank PaperSoundBank, WindSoundBank, BackgroundSoundBank;

        public static void LoadAudio()
        {
            // TODO: these links need to be fixed again
            //AudioEngine = new AudioEngine("Content/Audio/audio.xgs");
            //PaperWaveBank = new WaveBank(AudioEngine, "Content/Audio/paper.xwb");
            //WindWaveBank = new WaveBank(AudioEngine, "Content/Audio/wind.xwb");
            //BackgroundWaveBank = new WaveBank(AudioEngine, "Content/Audio/effects.xwb");
            //PaperSoundBank = new SoundBank(AudioEngine, "Content/Audio/paper.xsb");
            //WindSoundBank = new SoundBank(AudioEngine, "Content/Audio/wind.xsb");
            //BackgroundSoundBank = new SoundBank(AudioEngine, "Content/Audio/effects.xsb");
        }
    }
}
