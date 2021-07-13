using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Community.Common
{
    public class UploadFilesHelper
    {
        //图片字典
        public static Dictionary<string, byte[]> ImageHeader = new Dictionary<string, byte[]>();
        //文件字典
        public static Dictionary<string, object> FilesHeader = new Dictionary<string, object>();
        //定义字段
        public IHostingEnvironment _ev;
        //定义字段
        public string _url;

        /// <summary>
        /// 构造函数（先在当前控制器构造函数中注入IHostingEnvironment 然后传到此方法）url参数:是文件要保存到的文件夹名称
        /// </summary>
        /// <param name="hosting">这个是文件上传必备的（先在当前控制器构造函数中注入IHostingEnvironment 然后传到此方法）</param>
        /// <param name="url">文件保存到文件夹名称</param>
        public UploadFilesHelper(IHostingEnvironment hosting, string url)
        {
            _url = url;
            _ev = hosting;
            //判断字典是否有数据如果没有数据进行添加
            if (ImageHeader.Count==0&&FilesHeader.Count==0)
            {
                //图片类型
                ImageHeader.Add("gif", new byte[] { 71, 73, 70, 56, 57, 97 });
                ImageHeader.Add("bmp", new byte[] { 66, 77 });
                ImageHeader.Add("jpg", new byte[] { 255, 216, 255 });
                ImageHeader.Add("png", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 });

                //文件类型
                FilesHeader.Add("pdf", new byte[] { 37, 80, 68, 70, 45, 49, 46, 53 });
                FilesHeader.Add("docx", new object[] { new byte[] { 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33 }, new Regex(@"word/_rels/document\.xml\.rels", RegexOptions.IgnoreCase) });
                FilesHeader.Add("xlsx", new object[] { new byte[] { 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33 }, new Regex(@"xl/_rels/workbook\.xml\.rels", RegexOptions.IgnoreCase) });
                FilesHeader.Add("pptx", new object[] { new byte[] { 80, 75, 3, 4, 20, 0, 6, 0, 8, 0, 0, 0, 33 }, new Regex(@"ppt/_rels/presentation\.xml\.rels", RegexOptions.IgnoreCase) });
                FilesHeader.Add("doc", new object[] { new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 }, new Regex(@"microsoft( office)? word(?![\s\S]*?microsoft)", RegexOptions.IgnoreCase) });
                FilesHeader.Add("xls", new object[] { new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 }, new Regex(@"microsoft( office)? excel(?![\s\S]*?microsoft)", RegexOptions.IgnoreCase) });
                FilesHeader.Add("ppt", new object[] { new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 }, new Regex(@"c.u.r.r.e.n.t. .u.s.e.r(?![\s\S]*?[a-z])", RegexOptions.IgnoreCase) });
                FilesHeader.Add("avi", new byte[] { 65, 86, 73, 32 });
                FilesHeader.Add("mpg", new byte[] { 0, 0, 1, 0xBA });
                FilesHeader.Add("mpeg", new byte[] { 0, 0, 1, 0xB3 });
                FilesHeader.Add("rar", new byte[] { 82, 97, 114, 33, 26, 7 });
                FilesHeader.Add("zip", new byte[] { 80, 75, 3, 4 });
            }
            

        }


        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="file">文件</param>
        public string Main(IFormFile file)
        {
            //判断文件字节长度是否为0
            if (file.Length == 0)
            {
                //返回
                return "";
            }
            //判断是否是图片类型
            if (Regex.IsMatch(file.ContentType, @"^image/"))
            {
                //确定真正的图片类型 
                string imgext = ImageType(file.OpenReadStream());
                //判断是否为空
                if (!string.IsNullOrEmpty(imgext))
                {
                    //保存图片（调用方法）
                    string fileUrl = Save(file, imgext);
                    //返回
                    return fileUrl;
                }
                else
                {
                    return "";
                }
            }

            //文件类型
            string ext = FileType(file.OpenReadStream());
            if (!string.IsNullOrEmpty(ext))
            {
                //保存文件（调用方法）
                string fileUrl = Save(file, ext);
                //返回
                return fileUrl;
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 文件类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string FileType(Stream str)
        {
            string FileExt = string.Empty;
            foreach (string ext in FilesHeader.Keys)
            {
                byte[] header = FilesHeader[ext].GetType() == (new byte[] { }).GetType() ? (byte[])FilesHeader[ext] : (byte[])(((object[])FilesHeader[ext])[0]);
                byte[] test = new byte[header.Length];
                str.Position = 0;
                str.Read(test, 0, test.Length);
                bool same = true;
                for (int i = 0; i < test.Length; i++)
                {
                    if (test[i] != header[i])
                    {
                        same = false;
                        break;
                    }
                }
                if (FilesHeader[ext].GetType() != (new byte[] { }).GetType() && same)
                {
                    object[] obj = (object[])FilesHeader[ext];
                    bool exists = false;
                    if (obj[1].GetType().ToString() == "System.Int32")
                    {
                        for (int ii = 2; ii < obj.Length; ii++)
                        {
                            if (str.Length >= (int)obj[1])
                            {
                                str.Position = str.Length - (int)obj[1];
                                byte[] more = (byte[])obj[ii];
                                byte[] testmore = new byte[more.Length];
                                str.Read(testmore, 0, testmore.Length);
                                if (Encoding.GetEncoding(936).GetString(more) == Encoding.GetEncoding(936).GetString(testmore))
                                {
                                    exists = true;
                                    break;
                                }
                            }
                        }
                    }
                    else if (obj[1].GetType().ToString() == "System.Text.RegularExpressions.Regex")
                    {
                        Regex re = (Regex)obj[1];
                        str.Position = 0;
                        byte[] buffer = new byte[(int)str.Length];
                        str.Read(buffer, 0, buffer.Length);
                        string txt = Encoding.ASCII.GetString(buffer);
                        if (re.IsMatch(txt))
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        same = false;
                    }
                }
                if (same)
                {
                    FileExt = ext;
                    break;
                }
            }
            return FileExt;
        }


        /// <summary>
        /// 图片类型
        /// </summary>
        /// <param name="str">文件流</param>
        /// <returns></returns>
        private string ImageType(Stream str)
        {
            string FileExt = string.Empty;

            foreach (string ext in ImageHeader.Keys)
            {
                byte[] header = ImageHeader[ext];
                byte[] test = new byte[header.Length];
                str.Position = 0;
                str.Read(test, 0, test.Length);
                bool same = true;
                for (int i = 0; i < test.Length; i++)
                {
                    if (test[i] != header[i])
                    {
                        same = false;
                        break;
                    }
                }
                if (same)
                {
                    FileExt = ext;
                    break;
                }
            }
            return FileExt;
        }


        /// <summary>
        /// 将图片保存到本地
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="suffix">后缀名</param>
        /// <returns></returns>
        private string Save(IFormFile file, string suffix)
        {
            //获取文件名
            string fileName = file.FileName;
            //重新设置文件名称（为了防止名称重复）
            //返回不带扩展名的指定路径字符串的文件名
            string name = Path.GetFileNameWithoutExtension(fileName);
            //时间
            string time = DateTime.Now.ToString("FFFFFFF");
            //随机数
            Random random = new Random();
            string num = random.Next(10000, 99999).ToString();
            //拼接文件名
            string myFileName = name + time + num + "." + suffix;
            //保存到本地
            using (var fs = System.IO.File.Create(_ev.WebRootPath + $"\\{_url}\\" + myFileName))
            {
                file.CopyTo(fs);  //把文件拷贝到fs这个绝对路径
                fs.Flush();          //刷新磁盘
            }
            //返回文件路径
            return $"/{_url}/" + myFileName;
        }


    }
}
