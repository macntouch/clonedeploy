﻿using System.Collections.Generic;

namespace BLL
{
    public static class UserRight
    {
        //moved
        public static bool AddUserRights(List<CloneDeploy_Web.Models.UserRight> listOfRights)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                foreach (var right in listOfRights)
                    uow.UserRightRepository.Insert(right);

                return uow.Save();
            }
        }

        //moved
        public static bool DeleteUserRights(int userId)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                uow.UserRightRepository.DeleteRange(x => x.UserId == userId);
                return uow.Save();
            }
        }

        //moved
        public static List<CloneDeploy_Web.Models.UserRight> Get(int userId)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                return uow.UserRightRepository.Get(x => x.UserId == userId);
            }
        }
    }
}