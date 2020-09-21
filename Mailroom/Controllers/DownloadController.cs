using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.ViewModels;

namespace Mailroom.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index(int idIngreso)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            GenericResponse<ArchivoIngresoResponse> response = blReportes.ObtenerArchivoIngreso(idIngreso);

            string filename = response.Result.Nombre;
            byte[] filedata = response.Result.Data;
            string contentType = MimeMapping.GetMimeMapping(response.Result.Nombre);
            
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }
    }
}