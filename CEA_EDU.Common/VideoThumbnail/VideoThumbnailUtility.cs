using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace CEA_EDU.Common.VideoThumbnail
{
    public class VideoThumbnailUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vFileName">视频文件路径</param>
        /// <param name="thumbnailPath">缩略图文件路径</param>
        /// <param name="imgWidth">缩略图宽度</param>
        /// <param name="imgHeight">缩略图高度</param>
        /// <returns></returns>
        public static string CatchImg(string vFileName, string flv_img, int imgWidth, int imgHeight)
        {
            string defPic = string.Empty;

            string ffmpeg = HttpContext.Current.Server.MapPath("~/bin/ffmpeg.exe");

            if (!System.IO.File.Exists(ffmpeg))
            {
                return defPic;
            }
            if (!System.IO.File.Exists(vFileName))
            {
                return defPic;
            }

            if (System.IO.File.Exists(flv_img))
            {
                //缩略图实效性（2天内有效）
                FileInfo fi = new FileInfo(flv_img);
                if (fi.CreationTime > DateTime.Now.AddDays(-1))
                {
                    return flv_img;
                }
            }

            string FlvImgSize = "560x500";
            if (imgWidth > 0 && imgHeight > 0)
            {
                FlvImgSize = imgWidth + "x" + imgHeight;
            }

            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            //startInfo.Arguments = string.Format("\"{0}\" -i \"{1}\" -ss {2} -vframes 1 -r 1 -ac 1 -ab 2 -s {3}*{4} -f image2 \"{5}\"",
            //    ffmpegPath, oriVideoPath, frameIndex, thubWidth, thubHeight, thubImagePath);

            startInfo.Arguments = string.Format(" -i {0} -y -f image2 -t 10 -s {1} {2}", vFileName, FlvImgSize, flv_img);

            try
            {
                System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
                return defPic;
            }

            if (System.IO.File.Exists(flv_img))
            {
                return flv_img;
            }

            return defPic;
        }
    }
}
