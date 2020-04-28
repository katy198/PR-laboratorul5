using NAudio.Wave;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorul5_Client
{
    public partial class Form1 : Form
    {
        //UDPSocket _socket;
        public WaveIn waveSource = null;
        private int portNumber;
        Socket socket;
        IPAddress broadcast;
        private int port;
        //private byte[] data;
        private IPEndPoint ep;
        private IPEndPoint serverEP;

        public Form1()
        {
            InitializeComponent();
            // _socket = new UDPSocket();
            ep = new IPEndPoint(IPAddress.Parse("192.168.56.1"), 5051);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ep);
            waveSource = new WaveIn();
            waveSource.BufferMilliseconds = 50;
            waveSource.WaveFormat = new WaveFormat(44100, 1);
            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(_audioSource_DataAvailable);
        }

        private void _audioSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            socket.SendTo(e.Buffer,serverEP);
            //_socket.Send(e.Buffer);
            //  Buffer(e.Buffer);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            waveSource.StopRecording();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ip = txt_IP.Text;

            try
            {
                port = int.Parse(txt_port.Text);
                broadcast = IPAddress.Parse(ip);
                serverEP = new IPEndPoint(broadcast, port);

            }
            catch
            {
                MessageBox.Show("Incorect data!");
            }
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            //ep = new IPEndPoint(broadcast, port);
            try
            {
                waveSource.StartRecording();
            }
            catch
            {
                MessageBox.Show("Voice is sharing! Talk!");
            }



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
