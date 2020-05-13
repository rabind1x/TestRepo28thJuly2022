using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Intel.FabAuto.FA.NET.CoreFBOL;
using Intel.FabAuto.FA.NET.ClientFBOL.Gen;
using System.Xml.Linq;
using Intel.FabAuto.FWServices.Instrumentation;
using Intel.FabAuto.FA.NET.Common.BaseClasses;
using Intel.FabAuto.FA.NET.Common;
using Intel.FabAuto.FA.NET.Client;
using System.Diagnostics;

namespace FAInvoke
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnInvoke_Click(object sender, EventArgs e)
        {
            try
            {
                txtReply.Text = "Invoked....";
                btnInvoke.Enabled = false;
                this.Refresh();
                GenHelper genHelper = new GenHelper();
               // string head2 = @"<Header FA:dt=""struct""><UserCredentials FA:dt=""struct""><Username dt:dt=""string"">RF3PROD\RF3_SCUser</Username></UserCredentials><TransactionInformation FA:dt=""struct""><OriginatingSite dt:dt=""string"">D1D</OriginatingSite><OriginatingEnvironment dt:dt=""string"">PROD.0</OriginatingEnvironment><TargetSite dt:dt=""string"">D1D</TargetSite><TargetEnvironment dt:dt=""string"">PROD.0</TargetEnvironment><Hostname dt:dt=""string"">R3PEDT410U</Hostname><ClientID dt:dt=""string"">NTSC</ClientID><TransactionID dt:dt=""string"">3695ad5c-0a1c-46dd-a9d8-c201bfd3a70c</TransactionID><Trace dt:dt=""boolean"">0</Trace><Timestamp dt:dt=""string"">2016-10-23T23:02:07.538-07:00</Timestamp><Timeout dt:dt=""i4"">200</Timeout></TransactionInformation><RoutingInfo FA:dt=""struct""><ClientCapabilityList FA:dt=""list""><item dt:dt=""string"">RCTGate</item><item dt:dt=""string"">AutoSIFDownload</item><item dt:dt=""string"">EPA</item><item dt:dt=""string"">AutoSIF</item></ClientCapabilityList><YASProcessInfo FA:dt=""struct""/></RoutingInfo></Header>";
              //  string opDetails = @"<Envelope FA:dt=""struct"" xmlns:dt=""urn:schemas-microsoft-com:datatypes"" xmlns:FA=""urn:Intel-FabAuto-FA300"">" + head2 + @"<Body FA:dt=""struct""> <Name dt:dt=""string"">0300</Name> </Body></Envelope>";
                XElement xReq = XElement.Parse(txtRichInput.Text);
                string methodName = txtMethod.Text;
                XElement response = genHelper.Invoke(methodName, xReq, Guid.NewGuid(), Guid.NewGuid(), "ApcFw30", Process.GetCurrentProcess().ProcessName);
                txtReply.Text = response.ToString();
            }
            catch(Exception ex)
            {
                txtReply.Text = $"Invoke failed. Message:{ex.Message}";
                if (ex.InnerException == null)
                    txtReply.Text += "; No inner Exception";
                while (ex.InnerException!=null)
                {
                    ex = ex.InnerException;
                    txtReply.Text += "; Inner Source: " + ex.Source + "; Message:" + ex.Message;                    
                }                
            }
            finally
            {
                btnInvoke.Enabled = true;
            }

        }
    }
}
