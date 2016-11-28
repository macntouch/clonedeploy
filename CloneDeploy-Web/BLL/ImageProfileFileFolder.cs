﻿using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace BLL
{
    public class ImageProfileFileFolder
    {
        //moved
        public static bool AddImageProfileFileFolder(CloneDeploy_Web.Models.ImageProfileFileFolder imageProfileFileFolder)
        {
            imageProfileFileFolder.DestinationFolder = Utility.WindowsToUnixFilePath(imageProfileFileFolder.DestinationFolder);
            if (imageProfileFileFolder.DestinationFolder.Trim().EndsWith("/") && imageProfileFileFolder.DestinationFolder.Length > 1)
            {
                char[] toRemove = { '/' };
                string trimmed = imageProfileFileFolder.DestinationFolder.TrimEnd(toRemove);
                imageProfileFileFolder.DestinationFolder = trimmed;
            }
            using (var uow = new DAL.UnitOfWork())
            {
                uow.ImageProfileFileFolderRepository.Insert(imageProfileFileFolder);
                return uow.Save();
            }
        }

        //moved
        public static bool DeleteImageProfileFileFolders(int profileId)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                uow.ImageProfileFileFolderRepository.DeleteRange(x => x.ProfileId == profileId);
                return uow.Save();
            }
        }

        //moved
        public static List<CloneDeploy_Web.Models.ImageProfileFileFolder> SearchImageProfileFileFolders(int profileId)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                return uow.ImageProfileFileFolderRepository.Get(x => x.ProfileId == profileId, orderBy: q => q.OrderBy(t => t.Priority));
            }
        }
    }
}