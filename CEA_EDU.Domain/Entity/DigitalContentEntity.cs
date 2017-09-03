using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Serializable]
    public class DigitalContentEntity
    {
        /// <summary>
        /// 资源类别-共享文件夹2级目录（公司介绍、校园介绍）
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 资源类型（图片：1， 视频：2）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string SmallImage { get; set; }
       
    }
}
