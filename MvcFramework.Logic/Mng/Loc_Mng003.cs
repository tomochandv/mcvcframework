using MvcFramework.Dac;
using MvcFramework.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;

namespace MvcFramework.Logic
{
    public class Loc_Mng003 : Utill
    {
        public dynamic GetPageList(int rows, int page, string sidx, string sord)
        {
            var list = new Utill().ConvertToList<Model_Page_Info>(new Dac_Com_Page_Info().GetPageList());
            int totalCount = list.Count;
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(totalCount) / rows));
            list = list.AsQueryable().OrderBy(sidx + " " + sord).ToList();
            var data = new
            {
                total = total,
                records = totalCount,
                page = page,
                rows = (from e in list
                        select new
                        {
                            PAGE_ID = e.PAGE_ID,
                            PAGE_NAME_KO = e.PAGE_NAME_KO,
                            PAGE_URL = e.PAGE_URL,
                            PAGE_IMG = e.PAGE_IMG
                        }
                       ).Skip((page - 1) * rows).Take(page * rows).ToArray()

            };
            return data;
        }

        public int InsertPageInfo(string page_name, string url, HttpFileCollectionBase files, int emp_ref_id)
        {
           string name = FileInfoSave(files);
           return new Dac_Com_Page_Info().InsertPageInfo(page_name, url, name, emp_ref_id);
        }

        public int UpdatePageInfo(int page_id, string page_name, string imgname, string url,HttpFileCollectionBase files, int emp_ref_id)
        {
            string name = imgname;
           if (files.Count > 0)
           {
               if (imgname != "") { DeleteFile(imgname); }
               name = FileInfoSave(files);
           }
           return new Dac_Com_Page_Info().UpdatePageInfo(page_id, page_name, url, name, emp_ref_id);
        }

        public int DeletePageInfo(List<int> id)
        {
            int row = 0;
            var list = new Utill().ConvertToList<Model_Page_Info>(new Dac_Com_Page_Info().GetPageList());
            foreach (var data in id)
            {
                row += new Dac_Com_Page_Info().DeletePageInfo(data);
                Model_Page_Info info = list.Where(x => x.PAGE_ID == data).ToList()[0];
                if(NullToString(info.PAGE_IMG) != "")
                {
                    DeleteFile(info.PAGE_IMG);
                }
            }


            return row;
        }

        private string path = HttpContext.Current.Server.MapPath("/UploadFile/PageInfo/");

        public string FileInfoSave(HttpFileCollectionBase files)
        {
            string name = string.Empty;
            if (files.Count >0 && files[0].ContentLength > 0)
            {
                CreateForder();
                string ori_name = files[0].FileName;
                name = CreateFileName(ori_name);
                files[0].SaveAs(path + name);
            }
            return name;
        }
        private void CreateForder()
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        private string CreateFileName(string ori_name)
        {
            string fileName = string.Empty;
            string[] arrName = ori_name.Split('.');
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                fileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMddHHmmff"), arrName[arrName.Length - 1]);
            }
            return fileName;
        }

        private void DeleteFile(string name)
        {
            FileInfo file = new FileInfo(path + name);
            if(file.Exists)
            {
                file.Delete();
            }
        }
    }
}
