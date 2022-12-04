using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;
using Prism.Mvvm;
using VaughnApp.Models;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using Prism;
using Prism.Commands;

namespace VaughnApp.ViewModels
{
    public class ArduinoViewModel : BindableBase
    {
        private SerialPort _serialPort = new SerialPort();
        private BitmapSource _cameraImage;
        private Port _selectedPort;
        public ObservableCollection<Port> Ports { get; set; } = new ObservableCollection<Port>();
        private DelegateCommand<string> _sendCommand;
        private TimeSpan _playTime;

        public TimeSpan PlayTime
        {
            get => _playTime;
            set => SetProperty(ref _playTime, value);
        }

        public DelegateCommand<string> SendCommand => _sendCommand = _sendCommand ?? new DelegateCommand<string>(Play);

        private void Play(string p)
        {
            _serialPort.Write(p);
        }

        public Port SelectedPort
        {
            get => _selectedPort;
            set => SetProperty(ref _selectedPort, value);
        }

        public ArduinoViewModel()
        {
            InitCamera();
            InitSerialPort();
        }

        private void InitCamera()
        {
            // enumerate video devices
            var videoDevices = new FilterInfoCollection(
                FilterCategory.VideoInputDevice);
            // create video source
            if (videoDevices.Count <= 0)
                return;

            var videoSource = new VideoCaptureDevice(
                videoDevices[0].MonikerString);
            // set NewFrame event handler
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            // start the video source
            videoSource.Start();


            // signal to stop
            // videoSource.SignalToStop();
            // ...
        }



        private void video_NewFrame(object sender,
            NewFrameEventArgs eventArgs)
        {
            // get new frame

            var bitmap = eventArgs.Frame;
            Application.Current.Dispatcher.Invoke(() => CameraImage = bitmap.ToBitmapSource());


        }

        public BitmapSource CameraImage
        {
            get => _cameraImage;
            set => SetProperty(ref _cameraImage, value);
        }

        private void InitSerialPort()
        {
            PropertyChanged += ArduinoViewModel_PropertyChanged;
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portNames = SerialPort.GetPortNames();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString()).ToArray();

                var portList = portNames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n))).ToList();

                foreach (var portName in portNames)
                {
                    var friendlyName = ports.FirstOrDefault(p => p.Contains(portName));
                    Ports.Add(new Port
                    {
                        FriendlyName = friendlyName,
                        PortName = portName
                    });
                }

                SelectedPort = Ports.FirstOrDefault();

            }
        }

        private void ArduinoViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedPort))
            {

                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                _serialPort.PortName = SelectedPort.PortName;
                _serialPort.Open();
                PlayTime = TimeSpan.FromTicks(0);
            }
        }


        public void CleanUp()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
    }
}
