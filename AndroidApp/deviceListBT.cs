using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Bluetooth;

namespace AndroidApp
{
    [Activity(Label = "deviceListBT", MainLauncher = true)]
    public class deviceListBT : Activity
    {
        //widgets
        Button btnPaired;
        ListView deviceList;
        //Bluetooth
        private BluetoothAdapter myBluetooth = null;
        private ICollection<BluetoothDevice> pairedDevices;// sets:http://stackoverflow.com/questions/183685/c-sharp-set-collection
        public static string EXTRA_ADDRESS = "device_address";

        List<string> list = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.deviceListBT);

            //Calling widgets
            btnPaired = FindViewById<Button>(Resource.Id.button1);
            deviceList = FindViewById<ListView>(Resource.Id.listView1);

            //if the device has bluetooth
            myBluetooth = BluetoothAdapter.DefaultAdapter;

            if (myBluetooth == null)
            {
                //Show a message that the device has no bluetooth adapter
                Toast.MakeText((this), "Bluetooth Device Not Available" , ToastLength.Long).Show();
                //finsih apk
                Finish();
            }
            else if(!myBluetooth.IsEnabled)
            {
                //Ask user to turn Bluetooth on
                Intent turnBTon = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(turnBTon, 1);
            }

            btnPaired.Click += delegate { pairedDevicesList(); };


            // Create your application here
        }

        private void pairedDevicesList()
        {
            pairedDevices = myBluetooth.BondedDevices;
            //List<string> list = new List<string>();

            if (pairedDevices.Count > 0)
            {
                Toast.MakeText((this), pairedDevices.Count + " Devices found", ToastLength.Long).Show();
                foreach (BluetoothDevice bt in pairedDevices)
                {
                    list.Add(bt.Name + "\n" + bt.Address);
                }
            }
            else
            {
                Toast.MakeText((this), "No Paired Bluetooth Devices Found.", ToastLength.Long).Show();
            }

            
            ArrayAdapter adapter = new ArrayAdapter(this, Resource.Layout.simple_list_item_1, list);
            deviceList.Adapter = adapter;
            deviceList.ItemClick += DeviceList_ItemClick;

        }

        private void DeviceList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText((this), e.ToString() + "", ToastLength.Long).Show();
            //ListView lView = (ListView)e.View;

            string info = list[e.Position];
            string address = info.Substring(info.Length - 17);
            Toast.MakeText((this), address, ToastLength.Long).Show();

            Intent i = new Intent(this, typeof(MainActivity));
            i.PutExtra(EXTRA_ADDRESS, address);
            StartActivity(i);

            //throw new NotImplementedException();
        }
    }
}