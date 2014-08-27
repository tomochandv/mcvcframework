using MvcFramework.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFramework.Web.Controllers
{
    public class FileController : BaseController
    {
        //
        // GET: /File/

        public ActionResult Index(string id = "", string type = "")
        {
            ViewBag.id = id == "" ? id = DateTime.Now.ToString("yyyyMMddHHmmff") : id;
            ViewBag.type = type;
            return View();
        }

        public JsonResult FileSave(string hidid, string hidType)
        {
            return Json(new Loc_FileInfo().FileInfoSave(hidid, hidType, Request.Files, AUser().EMP_REF_ID));
        }

        [HttpPost]
        public JsonResult FileList(string id="")
        {
            return Json(new Loc_FileInfo().FileList(id));
        }

        public DownloadResult FileDownload(string fileUrl, string fileName)
        {
            string filePath = fileUrl;

            return new DownloadResult
            {
                FileName = fileName,
                Path = fileUrl
            };
        }

        public JsonResult Delete(int idx)
        {
            return Json(new Loc_FileInfo().FileDelete(idx));
        }

    }
}
