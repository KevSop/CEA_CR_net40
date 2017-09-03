using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Serializable]
    public class DigitalContentEntity
    {
        /// <summary>
        /// ��Դ���-�����ļ���2��Ŀ¼����˾���ܡ�У԰���ܣ�
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// ��Դ���ͣ�ͼƬ��1�� ��Ƶ��2��
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// ��Դ����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��Դ·��
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// ����ͼ
        /// </summary>
        public string SmallImage { get; set; }
       
    }
}
