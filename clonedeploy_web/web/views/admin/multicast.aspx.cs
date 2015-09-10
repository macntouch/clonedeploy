﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Global;
using Models;

public partial class views_admin_multicast : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
     
        txtSenderArgs.Text = Settings.SenderArgs;  
        txtRecArgs.Text = Settings.ReceiverArgs;
        txtStartPort.Text = Settings.StartPort;
        txtEndPort.Text = Settings.EndPort;   
        txtRecClientArgs.Text = Settings.ClientReceiverArgs;
      
        ViewState["startPort"] = txtStartPort.Text;
        ViewState["endPort"] = txtEndPort.Text;
    }

    protected void btnUpdateSettings_OnClick(object sender, EventArgs e)
    {
        if (ValidateSettings())
        {
            List<Setting> listSettings = new List<Setting>
            {
               
             
                    new Setting {Name = "Sender Args", Value = txtSenderArgs.Text},
                   
                    new Setting {Name = "Receiver Args", Value = txtRecArgs.Text},
                    new Setting {Name = "Udpcast Start Port", Value = txtStartPort.Text},
                    new Setting {Name = "Udpcast End Port", Value = txtEndPort.Text},
                   
                    new Setting {Name = "Client Receiver Args", Value = txtRecClientArgs.Text},
                   
                  
            };

            if (new Setting().Update(listSettings))
            {
                if ((string)(ViewState["startPort"]) != txtStartPort.Text)
                {
                    var startPort = Convert.ToInt32(txtStartPort.Text);
                    startPort = startPort - 2;
                    var port = new Port { Number = startPort };
                    port.Create();
                }
            }
        }

        new Utility().Msgbox(Utility.Message);
    }

    protected bool ValidateSettings()
    {
        if (ActiveTask.ReadAll().Count > 0)
        {
            Utility.Message = "Settings Cannot Be Changed While Tasks Are Active";
            return false;
        }

       

        var startPort = Convert.ToInt32(txtStartPort.Text);
        var endPort = Convert.ToInt32(txtEndPort.Text);

        if (startPort % 2 != 0)
        {
            startPort++;
            txtStartPort.Text = startPort.ToString();
        }
        if (endPort % 2 != 0)
        {
            endPort++;
            txtEndPort.Text = endPort.ToString();
        }

        try
        {
            if ((startPort >= 2) && (endPort - startPort >= 2))
            {
                return true;
            }
            Utility.Message = "End Port Must Be At Least 2 More Than Starting Port";
            return false;
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message);
            return false;
        }
    }
}