using MvcFramework.Dac;
using MvcFramework.Model;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Data;

namespace MvcFramework.Logic
{
    public class Loc_FileInfo
    {
        public List<Model_File_info> FileList(string id)
        {
            return new Utill().ConvertToList<Model_File_info>(new Dac_Com_File_Info().GetFileList(id));
        }

        public bool FileInfoSave(string id, string type, HttpFileCollectionBase files, int emp_ref_id)
        {
            bool result = false;
            if(files != null && files[0].ContentLength >0)
            {
                string folder = type.ToUpper();
                string path = CreateForder(folder);
                string ori_name = files[0].FileName;
                string name = CreateFileName(path, ori_name);
                string url = string.Format("/UploadFile/{0}/{1}", folder, name);
                files[0].SaveAs(path + name);
                result = new Dac_Com_File_Info().InsertFile(id, folder, ori_name, name, folder, url, emp_ref_id) > 0? true : false;
            }
            return result;
        }

        private string CreateForder(string folder)
        {
            string path = HttpContext.Current.Server.MapPath("/UploadFile/" + folder + "/");
            DirectoryInfo dir = new DirectoryInfo(path);
            if(!dir.Exists)
            {
                dir.Create();
            }
            return path;
        }

        private string CreateFileName(string path,string ori_name)
        {
            string fileName = string.Empty;
            string[] arrName = ori_name.Split('.');
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                fileName = string.Format("{0}.{1}", files.Length + 1, arrName[arrName.Length - 1]);
            }
            return fileName;
        }

        public bool FileDelete(int idx)
        {
            Model_File_info info = new Utill().ConvertToRow<Model_File_info>(new Dac_Com_File_Info().GetFileList(idx));
            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(info.FILE_URL));
            if(file.Exists)
            {
                file.Delete();
            }
            return new Dac_Com_File_Info().DeleteFile(idx) > 0 ? true : false;
        }
    }
}
