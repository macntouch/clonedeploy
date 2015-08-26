﻿/*  
    CrucibleWDS A Windows Deployment Solution
    Copyright (C) 2011  Jon Dolny

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/.
 */

using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Global;
using Models;
using Security;
using Image = Models.Image;

namespace views.hosts
{
    public partial class Addhosts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Master.FindControl("SubNavDynamic").Visible = false;

            if (IsPostBack) return;

            if (new Authorize().IsInMembership("User"))
                Response.Redirect("~/views/dashboard/dash.aspx?access=denied");
            else
                PopulateForm();
        }

        protected void ButtonAddHost_Click(object sender, EventArgs e)
        {
      
            var host = new Computer
            {
                Name = txtHostName.Text,
                Mac = Utility.FixMac(txtHostMac.Text),
                Image = Convert.ToInt32(ddlHostImage.SelectedValue),
                ImageProfile = Convert.ToInt32(ddlImageProfile.SelectedValue),
                Description = txtHostDesc.Text,
            };

            if (host.ValidateHostData())
            {
                if (host.Create() && !createAnother.Checked)
                    Response.Redirect("~/views/computers/edit.aspx?hostid=" + host.Id);
            }

            Master.Msgbox(Utility.Message);
        }

        protected void PopulateForm()
        {
            ddlHostImage.DataSource = new Image().Search("").Select (i => new {i.Id,i.Name});
            ddlHostImage.DataValueField = "Id";
            ddlHostImage.DataTextField = "Name";
            ddlHostImage.DataBind();
            ddlHostImage.Items.Insert(0, "Select Image");

     
        }

        protected void ddlHostImage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHostImage.Text == "Select Image") return;
            ddlImageProfile.DataSource = new LinuxEnvironmentProfile().Search(Convert.ToInt32(ddlHostImage.SelectedValue)).Select(i => new { i.Id, i.Name });
            ddlImageProfile.DataValueField = "Id";
            ddlImageProfile.DataTextField = "Name";
            ddlImageProfile.DataBind();

        }
    }
}