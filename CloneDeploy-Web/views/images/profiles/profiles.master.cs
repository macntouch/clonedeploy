﻿using System;
using CloneDeploy_Entities;
using CloneDeploy_Web.BasePages;

public partial class views_masters_Profile : MasterBaseMaster
{
    private Images imageBasePage { get; set; }
    public ImageProfileEntity ImageProfile { get; set; }
    public ImageEntity Image { get; set; }

    public void Page_Load(object sender, EventArgs e)
    {
        imageBasePage = (Page as Images);
        ImageProfile = imageBasePage.ImageProfile;
        Image = imageBasePage.Image;

        if (ImageProfile == null)
        {
            osx_profile.Visible = false;
            linux_profile.Visible = false;
            winpe_profile.Visible = false;
            return;
        }
        if (Image == null) Response.Redirect("~/", true);
        divProfiles.Visible = false;
        if (Image.Environment == "macOS")
        {
            osx_profile.Visible = true;
            linux_profile.Visible = false;
            winpe_profile.Visible = false;
        }
        else if(Image.Environment == "linux" || Image.Environment == "")
        {
            osx_profile.Visible = false;
            linux_profile.Visible = true;
            winpe_profile.Visible = false;
        }
        else if (Image.Environment == "winpe")
        {
            osx_profile.Visible = false;
            linux_profile.Visible = false;
            winpe_profile.Visible = true;
        }
    } 
}