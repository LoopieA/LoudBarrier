using System;
using System.Linq;
using System.Windows.Forms;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics;

namespace LoudBarrier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
       
            NAudio.CoreAudioApi.MMDeviceEnumerator enumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.Active); 
            NAudio.CoreAudioApi.MMDeviceEnumerator devEnum = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            NAudio.CoreAudioApi.MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia);
            comboBox1.Items.AddRange(devices.ToArray());
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool triggervolume = false;
            if(comboBox1.SelectedItem != null)
            {

               var device = (NAudio.CoreAudioApi.MMDevice)comboBox1.SelectedItem;

               var getVolume = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
               progressBar1.Value = (int)(Math.Round(device.AudioMeterInformation.MasterPeakValue * 100));
 
               if((getVolume > 50))       
                   device.AudioEndpointVolume.MasterVolumeLevelScalar = 0.5f;    
               
                Debug.WriteLine(triggervolume);
                label1.Text = getVolume.ToString();
            }
        }
    }
}
