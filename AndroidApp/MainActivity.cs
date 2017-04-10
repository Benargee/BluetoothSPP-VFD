using System;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Bluetooth;
using Java.Util;
using Java.Lang;
using Java.IO;//IOException

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;//X

        public static Context mContext;
        public static Activity mActivity;
        public static string address = null;
        public static ProgressDialog progress;
        public static BluetoothAdapter myBluetooth = null;//not sure if public static is best use case
        public static BluetoothSocket btSocket = null;
        private EditText textMessage;
        public static bool isBtConnected = false;
        //SPP UUID
        static UUID sppUUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            address = Intent.GetStringExtra(AndroidApp.deviceListBT.EXTRA_ADDRESS);//receive the address of the bluetooth device
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonSend = FindViewById<Button>(Resource.Id.buttonSend);
            buttonSend.Click += ButtonSend_Click;
            textMessage = FindViewById<EditText>(Resource.Id.textMessage);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            Switch displaySwitch = FindViewById<Switch>(Resource.Id.switchPower);
            displaySwitch.CheckedChange += DisplaySwitch_CheckedChange;

            mContext = this;
            mActivity = this;
            Java.Lang.Object[] jObject = {mContext};
            //jObject[] = mContext;
            ConnectBT cBT = new ConnectBT();
            cBT.Execute(jObject);
            

            


        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            string str;
            str = textMessage.Text;
            sendData(str, false);
            //throw new NotImplementedException();
        }

        private void DisplaySwitch_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            string str;
            if (e.IsChecked)
            {
                str = "Chk";
            }
            else
            {
                str = "Nchk";
            }
            
            sendData(str, true);
        }

        private void sendData (string data, bool command)
        {
            string cmd;
            if (command)
            {
                cmd = "1";
            }
            else
            {
                cmd = "0";
            }

            string stx = "\x02";//Start of Text
            string nul = "\0";//Null
            data = stx + cmd + data + nul;

            byte[] buff = Encoding.ASCII.GetBytes(data);
            btSocket.OutputStream.Write(buff, 0, buff.Length);
        }

        private static void msg(string s)
        {
            Toast.MakeText((mContext), s, ToastLength.Long).Show();
        }

        public class ConnectBT : AsyncTask
        {

            private bool ConnectSuccess = true; //if it's here, it's almost connected

            protected override void OnPreExecute()
            {
                /*AndroidApp.MainActivity.*/
                /*ProgressDialog*/ progress = ProgressDialog.Show(MainActivity.mContext, "Connecting...", "Please wait!!!");
                //var progressDialog = ProgressDialog.Show(this, "Please wait...", "Checking account info...", true);
                //base.OnPreExecute();
            }
            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                //Console.WriteLine(@params[0]);
                Java.Lang.Object[] qparams = @params;

                try
                {
                    if (btSocket == null|| !isBtConnected)
                    {
                        myBluetooth = BluetoothAdapter.DefaultAdapter;//get the mobile bluetooth device
                        BluetoothDevice sppBtDevice = myBluetooth.GetRemoteDevice(address);//connects to the device's address and checks if it's available
                        btSocket = sppBtDevice.CreateInsecureRfcommSocketToServiceRecord(sppUUID);
                        BluetoothAdapter.DefaultAdapter.CancelDiscovery();
                        btSocket.Connect();

                    }
                }
                catch (IOException e)
                {
                    ConnectSuccess = false;//if the try failed, you can check the exception here
                }
                //throw new NotImplementedException();
                return qparams;
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {
                if (!ConnectSuccess)
                {
                    MainActivity.msg("Connection Failed.Is it a SPP Bluetooth ? Try again.");
                    mActivity.Finish();
                }
                else
                {
                    MainActivity.msg("Connected.");
                }
                progress.Dismiss();
                //base.OnPostExecute(result);
            }
        }

    }


}

