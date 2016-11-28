﻿using System;
using System.Linq;
using Helpers;

namespace BLL
{
    public class Port
    {
        //moved
        public static bool AddPort(CloneDeploy_Web.Models.Port port)
        {
            using (var uow = new DAL.UnitOfWork())
            {
                uow.PortRepository.Insert(port);
                return uow.Save();
            }
        }

        //move not needed
        public static int GetNextPort()
        {
            var lastPort = new CloneDeploy_Web.Models.Port();
            var nextPort = new CloneDeploy_Web.Models.Port();
            using (var uow = new DAL.UnitOfWork())
            {
                lastPort =
                    uow.PortRepository.GetFirstOrDefault(orderBy: (q => q.OrderByDescending(p => p.Id)));

            }

            if (lastPort == null)
                nextPort.Number = Convert.ToInt32(Settings.StartPort);
            else if (nextPort.Number >= Convert.ToInt32(Settings.EndPort))
                nextPort.Number = Convert.ToInt32(Settings.StartPort);
            else
                nextPort.Number = lastPort.Number + 2;

            AddPort(nextPort);

            return nextPort.Number;
        }
    }
}