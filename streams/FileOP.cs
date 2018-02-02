using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

using System.IO;
using System.IO.Compression;
//using System.IO.Compression.FileSystem;

namespace streams
{
    class FileOP
    {

        private string dir
        {
            get;set;
        }

        private string makeHash(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashedData = md5.ComputeHash(data);

            return BitConverter.ToString(hashedData).Replace("-","").ToLower();

        }

        public FileOP(string dir)
        {
            this.dir = dir;

            this.checkDirs();

        }

        private void checkDirs()
        {
            if (!Directory.Exists(this.dir)){

                Directory.CreateDirectory(this.dir);

            }
        }

        public string saveFile(string fileName,string content, string extension, out bool res, out string msg)
        {
            res = true;

            msg = null;

            string outFileName = null;
            try
            {
                string timeStamp = DateTime.Now.ToString("Y-m-d HH:mm:ss");

                string hash = this.makeHash(timeStamp);

                outFileName = string.Format(@"{3}\{0}_{1}.{2}", fileName, hash, extension,this.dir);

                StreamWriter sw = new StreamWriter(outFileName);
                sw.Write(content);
                sw.Flush();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                res = false;
                msg = ex.ToString();
            }

            return outFileName;

        }

        public string compressFile(string fileName,out bool res, out string msg)
        {
            msg = null;
            res = true;
            string zipFile = null;
            try
            {

                zipFile = fileName.Replace(".xml", ".zip");
                using (FileStream fs = new FileStream(zipFile, FileMode.Create))
                using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    arch.CreateEntryFromFile(fileName, fileName);
                }
            }
            catch (Exception ex)
            {
                res = false;
                msg = ex.ToString();
            }

            return zipFile;
        }



    }
}
