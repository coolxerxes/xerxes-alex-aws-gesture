using System;
using System.ComponentModel;
using System.Threading.Tasks;
using GSdkNet.BLE.Winapi;
using GSdkNet.Peripheral;
using GSdkNet.Board;
using GSdkNet.BLE;
using System.Collections.Generic;
using GSdkNet.Core;
using SeniorProject;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime;
using Microsoft.ML;
using System.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request.Type;
using Alexa.NET.SkillMessaging;

namespace GSdkNet.Carrier.Example
{
    class SPstream : ABstream
    {
        private IPeripheralCentral Central;
        private IBoardPeripheral Peripheral;
        private Form1 myForm;
        private StreamWriter quaterStream;
        private StreamWriter bendingStream;
        private StreamWriter accelStream;
        private float[] faSensorValue; //Sensor conductivity
        private int nThumbSensor, nIndexSensor, nMiddleSensor, nRingSensor, nPinkySensor, nPressureSensor;
        bool flag = false;
        private float[] readera = new float[66];
        private ArrayList arlist = new ArrayList();


        


        public SPstream(Form1 myForm1, String fEnd){
            myForm = myForm1;
            String quaterFileName = "Quaternion.csv";
            String bendingFileName = "Bending.csv";
            String accelFileName = "Accel.csv";
           
            try
            {
                quaterStream = new StreamWriter(quaterFileName, false);
                bendingStream = new StreamWriter(bendingFileName, false);
                accelStream = new StreamWriter(accelFileName, false);

            }
            catch (Exception exp)
            {
                myForm.SetText("\nCannot open file\n");
                //myForm.textBox3.AppendText("Cannot open file");
            }
            faSensorValue = new float[10];
            for (int i = 0; i < 10; i++)
            {
                faSensorValue[i] = 0f;
            }
            SetFingerSensor(1, 3, 5, 7, 9, 2); //Default connection of the sensors in CaptoSensor
        }
        private int SetFingerSensor(int nThumbSensor, int nIndexSensor, int nMiddleSensor, int nRingSensor, int nPinkySensor,
                               int nPressureSensor)
        {
            //ArrayPos = SensorID - 1 
            this.nThumbSensor = nThumbSensor - 1;
            this.nIndexSensor = nIndexSensor - 1;
            this.nMiddleSensor = nMiddleSensor - 1;
            this.nRingSensor = nRingSensor - 1;
            this.nPinkySensor = nPinkySensor - 1;
            this.nPressureSensor = nPressureSensor - 1;

            return 0;
        }

        public override Task StartAsync()
        {
            var adapterScanner = new AdapterScanner();
            var adapter = adapterScanner.FindAdapter();
            var configurator = new Configurator(adapter);

            Central = configurator.GetBoardCentral();
            Central.StartScan(new Dictionary<PeripheralScanFlag, object> {
                { PeripheralScanFlag.ScanType, BleScanType.Balanced }
            });
            Central.PeripheralsChanged += Central_PeripheralsChanged;
            return Task.FromResult(Type.Missing);
        }

        public override async Task StopAsync()
        {

            Central.StopScan();

            if (Peripheral != null)
            {
                await Peripheral.StopAsync();
                Peripheral.StreamReceived -= Peripheral_StreamReceived;
            }

            quaterStream.Flush();
            bendingStream.Flush();
            accelStream.Flush();

            quaterStream.Dispose();
            bendingStream.Dispose();
            accelStream.Dispose();

            try
            {
                FileInfo fi = new FileInfo("Quaternion.csv");
                StreamReader qr = fi.OpenText();
                String qrs = "";

                FileInfo fi2 = new FileInfo("Bending.csv");
                StreamReader br = fi2.OpenText();
                String brs = "";

                FileInfo fi3 = new FileInfo("Accel.csv");
                StreamReader ar = fi3.OpenText();
                String ars = "";

                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                //for (int d = 0; d < 72; d++)
                //{
                for (int i = 0; i < 6; i++)
                {
                    if ((qrs = qr.ReadLine()) != null)
                    {
                        Console.WriteLine(qrs);

                        float[] floatData = Array.ConvertAll(qrs.Split(';'), float.Parse);
                        arlist.Add(floatData[0]);
                        arlist.Add(floatData[1]);
                        arlist.Add(floatData[2]);
                    }

                    if ((brs = br.ReadLine()) != null)
                    {
                        Console.WriteLine(brs);
                        //arlist.Add(brs);
                        float[] floatData = Array.ConvertAll(brs.Split(';'), float.Parse);
                        arlist.Add(floatData[0]);
                        arlist.Add(floatData[1]);
                        arlist.Add(floatData[2]);
                        arlist.Add(floatData[3]);
                        arlist.Add(floatData[4]);
                        //arlist.Add(floatData[5]);
                    }

                    if ((ars = ar.ReadLine()) != null)
                    {
                        Console.WriteLine(ars);
                        //arlist.Add(ars);
                        float[] floatData = Array.ConvertAll(ars.Split(';'), float.Parse);
                        arlist.Add(floatData[0]);
                        arlist.Add(floatData[1]);
                        arlist.Add(floatData[2]);
                    }

                }
                //}
                foreach (var item in arlist)
                {
                    Console.WriteLine(item);
                }
                //   string text="0";
                 
                
                   // String[] text = (String[])arlist.ToArray(typeof(String));
                String text = "";
                foreach (var item in arlist)
                {
                    text = text + item + ",";
                    
                }
                text = text.Replace("E-", "");

                // Get the text to analyze
                //if (args.Length > 0)
                //{
                //   text = args[0];
                //} [[0.07 0.06 0.   0.06 0.07 0.09 0.05 0.13 0.47]] [[0.05 0.06 0.   0.06 0.1  0.09 0.04 0.12 0.48]]
                //else [[0.   0.03 0.02 0.   0.02 0.   0.   0.25 0.68]]
                //{
                //    Console.Write("Text to analyze: ");
                //    text = Console.ReadLine();
                //}


                //            if 'text' in request.args:
                //    text = request.args.get('text') (r'\d+\.\d+'
                //else:
                //    return 'No string to analyze'

                // Pass the text to the Web service
                var client = new HttpClient();
                    var url = $"http://192.168.100.22:5000/analyze?text={text}";
                    var response = await client.GetAsync(url);
                    var score = await response.Content.ReadAsStringAsync();

                    // Show the sentiment score
                    Console.WriteLine(score);

                var client2 = new AccessTokenClient(AccessTokenClient.ApiDomainBaseAddress);
                var accessToken = await client2.Send("spm4", "spm4");
                var oauthToken = accessToken.Token;
                var payload = new Dictionary<string, string> { { "testKey", "testValue" } };

                var messages = new Alexa.NET.SkillMessageClient(Alexa.NET.SkillMessageClient.EuropeEndpoint, oauthToken);
                var messageToSend = new Alexa.NET.SkillMessaging.Message(payload, 300);

                var messageId = await messages.Send(messageToSend, "spm4");
                RequestConverter.RequestConverters.Add(new MessageReceivedRequestTypeConverter());

            }
            


            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        private async void Central_PeripheralsChanged(object sender, PeripheralsEventArgs e)
        {
            foreach (var peripheral in e.Inserted)
            {
                // Enumerate peripherals and run first connected
                try
                {
                    myForm.SetText("Trying to connect peripheral" + Environment.NewLine);
                    myForm.SetText("- ID: " + peripheral.Id + Environment.NewLine);
                    myForm.SetText("- Name: " + peripheral.Name + Environment.NewLine);

                    Peripheral = peripheral as IBoardPeripheral;
                    Peripheral.PropertyChanged += Peripheral_PropertyChanged;
                    Peripheral.StreamReceived += Peripheral_StreamReceived;
                    await Peripheral.StartAsync();
                    return;
                }
                catch (Exception ex)
                {
                    myForm.SetText("Unable to start board " + ex.Message + Environment.NewLine);
                }
            }
        }

        private async void Peripheral_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PeripheralProperty.Status)
            {
                myForm.SetText("Board status: " + Peripheral.Status.ToString() + Environment.NewLine);

                if (Peripheral.Status == PeripheralStatus.Connected)
                {
                    // You can assign to 20 timeslots (StreamTimeslots.MaxTimeslots)
                    // distributed between different measurement. Please take a note,
                    // HID mouse, joystick and keyboard timeslots are dedicated from
                    // the shared amount of timeslots.
                    var streamTimeslots = new StreamTimeslots();
                    Peripheral.CalibrateGyroAsync();
                    Peripheral.TareAsync();
                    streamTimeslots.Set(6, BoardStreamType.TaredQuaternion);
                    streamTimeslots.Set(6, BoardStreamType.SensorsState);
                    streamTimeslots.Set(6, BoardStreamType.LinearAcceleration);
                    await Peripheral.StreamTimeslots.WriteAsync(streamTimeslots);
                    Peripheral.CommitChangesAsync();
                }
            }
            else
            {
                myForm.SetText("Changed peripheral property " + e.PropertyName + Environment.NewLine);
            }
        }

        private void Peripheral_StreamReceived(object sender, BoardStreamEventArgs e)
        {

            // On stream callback. Stream can contain different kind of arguments.
            // Please bear in mind, this stream is not necessary should be run on main thread.
            // In case of not necessary stream type you just skioing it handling.
            if (flag == false)
            {
                myForm.SetText("We wil start writing stream ################" + Environment.NewLine);
                flag = true;
            }
            if (e.StreamType == BoardStreamType.TaredQuaternion)
            {
                // Reading tared quaternion data
                var args = e as BoardQuaternionEventArgs;

                string serializedData;

                if (args != null)
                {
                    float quaternionX = args.Value.X;
                    float quaternionY = args.Value.Y;
                    float quaternionZ = args.Value.Z;

                    serializedData =
                        //Peripheral.Name + ";" +
                        quaternionX.ToString() + ";" +
                        quaternionY.ToString() + ";" +
                        quaternionZ.ToString() + "\n";

                    // Write to disk
                    if (quaterStream != null)
                    {
                        quaterStream.Write(serializedData);
                    }
                }
            }
            else if (e.StreamType == BoardStreamType.SensorsState)
            {
                // Reading sensors state data
                var args = e as BoardFloatSequenceEventArgs;
                string serializedData;

                if (args != null)
                {

                    for (int i = 0; i < 10; i++)
                    {
                        faSensorValue[i] = args.Value[i];
                    }

                    serializedData =
                        //Peripheral.Name + ";" +
                        faSensorValue[nThumbSensor].ToString() + ";" +
                        faSensorValue[nIndexSensor].ToString() + ";" +
                        faSensorValue[nMiddleSensor].ToString() + ";" +
                        faSensorValue[nRingSensor].ToString() + ";" +
                        faSensorValue[nPinkySensor].ToString() + ";" +
                        faSensorValue[nPressureSensor].ToString() + "\n";

                    // Write to disk
                    if (bendingStream != null)
                    {
                        bendingStream.Write(serializedData);



                    }
                }
            }
            else if (e.StreamType == BoardStreamType.LinearAcceleration)
            {
                // Reading linear acceleration data
                var args = e as BoardFloatVectorEventArgs;
                string serializedData;

                if (args != null)
                {
                    float accX = args.Value.X;
                    float accY = args.Value.Y;
                    float accZ = args.Value.Z;

                    serializedData =
                        //Peripheral.Name + ";" +
                        accX.ToString() + ";" +
                        accY.ToString() + ";" +
                        accZ.ToString() + "\n";

                    // Write to disk
                    if (accelStream != null)
                    {
                        accelStream.Write(serializedData);

                    }
                }
            }
        }


    private static string FloatsToString(float[] value)
    {
        string result = "";
        var index = 0;
        foreach (var element in value)
        {
            if (index != 0)
            {
                result += ", ";
            }
            result += element.ToString();
            index += 1;
        }
        return result;
    }
}
    
}
