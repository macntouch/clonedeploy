﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Helpers;

public partial class views_users_groupacls_imagemanagement : BasePages.Users
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RequiresAuthorization(Authorizations.Administrator);
        if (!IsPostBack) PopulateForm();
    }

    protected void SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        ChkAll(gvImages);
    }

    protected void PopulateForm()
    {
        var listOfImages = BLL.UserGroupImageManagement.Get(CloneDeployUserGroup.Id);

        gvImages.DataSource = BLL.Image.SearchImages();
        gvImages.DataBind();

        foreach (GridViewRow row in gvImages.Rows)
        {
            var chkBox = (CheckBox)row.FindControl("chkSelector");
            var dataKey = gvImages.DataKeys[row.RowIndex];
            if (dataKey == null) continue;
            foreach (var groupManagement in listOfImages)
            {
                if (groupManagement.ImageId == Convert.ToInt32(dataKey.Value))
                {
                    chkBox.Checked = true;
                }
            }
        }
    }

    protected void buttonUpdate_OnClick(object sender, EventArgs e)
    {
        var list = new List<Models.UserGroupImageManagement>();
        foreach (GridViewRow row in gvImages.Rows)
        {
            var cb = (CheckBox)row.FindControl("chkSelector");
            if (cb == null || !cb.Checked) continue;
            var dataKey = gvImages.DataKeys[row.RowIndex];
            if (dataKey == null) continue;
            var userImageManagement = new Models.UserGroupImageManagement
            {
                UserGroupId = CloneDeployUserGroup.Id,
                ImageId = Convert.ToInt32(dataKey.Value)
            };
            list.Add(userImageManagement);

        }

        BLL.UserGroupImageManagement.DeleteUserGroupImageManagements(CloneDeployUserGroup.Id);
        BLL.UserGroupImageManagement.AddUserGroupImageManagements(list);
        BLL.UserGroup.UpdateAllGroupMembersImageMgmt(CloneDeployUserGroup);
        EndUserMessage = "Updated Image Management";


    }
}