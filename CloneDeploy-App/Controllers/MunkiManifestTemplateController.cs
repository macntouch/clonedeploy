﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using CloneDeploy_App.Controllers.Authorization;
using CloneDeploy_App.Models;

namespace CloneDeploy_App.Controllers
{
    public class MunkiManifestTemplateController: ApiController
    {
        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestTemplate> Get(string searchstring = "")
        {
            return string.IsNullOrEmpty(searchstring)
                ? BLL.MunkiManifestTemplate.SearchManifests()
                : BLL.MunkiManifestTemplate.SearchManifests(searchstring);

        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetCount()
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiManifestTemplate.TotalCount();
            return ApiDTO;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IHttpActionResult Get(int id)
        {
            var manifest = BLL.MunkiManifestTemplate.GetManifest(id);
            if (manifest == null)
                return NotFound();
            else
                return Ok(manifest);
        }

        [GlobalAuth(Permission = "GlobalCreate")]
        public ActionResult Post(Models.MunkiManifestTemplate manifest)
        {
            var actionResult = BLL.MunkiManifestTemplate.AddManifest(manifest);
            if (!actionResult.Success)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, actionResult);
                throw new HttpResponseException(response);
            }
            return actionResult;
        }

        [GlobalAuth(Permission = "GlobalUpdate")]
        public Models.ActionResult Put(int id, Models.MunkiManifestTemplate manifest)
        {
            manifest.Id = id;
            var actionResult = BLL.MunkiManifestTemplate.UpdateManifest(manifest);
            if (!actionResult.Success)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, actionResult);
                throw new HttpResponseException(response);
            }
            return actionResult;
        }

        [GlobalAuth(Permission = "GlobalDelete")]
        public Models.ActionResult Delete(int id)
        {
            var actionResult = BLL.MunkiManifestTemplate.DeleteManifest(id);
            if (!actionResult.Success)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound, actionResult);
                throw new HttpResponseException(response);
            }
            return actionResult;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestCatalog> GetManifestCatalogs(int id)
        {
            return BLL.MunkiCatalog.GetAllCatalogsForMt(id);
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetCatalogCount(int id)
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiCatalog.TotalCount(id);
            return ApiDTO;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestIncludedManifest> GetManifestIncludedManifests(int id)
        {
            return BLL.MunkiIncludedManifest.GetAllIncludedManifestsForMt(id);
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetIncludedManifestCount(int id)
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiIncludedManifest.TotalCount(id);
            return ApiDTO;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestManagedInstall> GetManifestManagedInstalls(int id)
        {
            return BLL.MunkiManagedInstall.GetAllManagedInstallsForMt(id);
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetManagedInstallCount(int id)
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiManagedInstall.TotalCount(id);
            return ApiDTO;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestManagedUnInstall> GetManifestManagedUninstalls(int id)
        {
            return BLL.MunkiManagedUninstall.GetAllManagedUnInstallsForMt(id);
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetManagedUninstallCount(int id)
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiManagedUninstall.TotalCount(id);
            return ApiDTO;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestManagedUpdate> GetManifestManagedUpdates(int id)
        {
            return BLL.MunkiManagedUpdate.GetAllManagedUpdatesForMt(id);
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetManagedUpdateCount(int id)
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiManagedUpdate.TotalCount(id);
            return ApiDTO;
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public IEnumerable<Models.MunkiManifestOptionInstall> GetManifestOptionalInstalls(int id)
        {
            return BLL.MunkiOptionalInstall.GetAllOptionalInstallsForMt(id);
        }

        [GlobalAuth(Permission = "GlobalRead")]
        public ApiDTO GetOptionalInstallCount(int id)
        {
            var ApiDTO = new ApiDTO();
            ApiDTO.Value = BLL.MunkiOptionalInstall.TotalCount(id);
            return ApiDTO;
        }
    }
}