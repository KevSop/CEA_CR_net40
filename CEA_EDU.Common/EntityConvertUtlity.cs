using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CEA_EDU.Common
{
    /// <summary>
    /// 实体转换类。目标实体类对象在源实体类对象中检查同名属性，并对同名的属性进行赋值
    /// </summary>
   public class EntityConvertUtlity
    {
        /// <summary>
        /// 类型转换，尝试转换类型，失败后使用默认值
        /// </summary>
        /// <typeparam name="S">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="source">源输入</param>
        /// <param name="target">目标输出</param>
       public static void ConvertSEntityToTEntity<S, T>(S source, ref T target)
       {
           if (source==null||target==null)
           {
               return;
           }
               //比较属性名。目标实体在源实体中寻找名称相同的属性，找到则进行赋值
                Type sourceType=typeof(S);
                Type targetType=typeof(T);
                PropertyInfo[] sourceProperties = sourceType.GetProperties();
                PropertyInfo[] targetProperties = targetType.GetProperties();

                foreach (var titem in targetProperties)
                {
                    foreach (var sitem in sourceProperties)
                    {
                        if (titem.Name.Equals(sitem.Name))
                        {
                            titem.SetValue(target, sitem.GetValue(source, null), null);
                        }
                    }
                }
       }

       /// <summary>
       /// 列表类型转换
       /// </summary>
       /// <typeparam name="S">源类型</typeparam>
       /// <typeparam name="T">目标类型</typeparam>
       /// <param name="sourceList">源列表</param>
       /// <param name="targetList">目标列表</param>
       public static void ConvertSListToTList<S, T>(IList<S> sourceList, ref IList<T> targetList)
       {
           if (sourceList==null||targetList==null)
           {
               return;
           }
           Type sourceType = typeof(S);
           Type targetType = typeof(T);
           PropertyInfo[] sourceProperties = sourceType.GetProperties();
           PropertyInfo[] targetProperties = targetType.GetProperties();

           foreach (var item in sourceList)
           {
               object targetEntity = Activator.CreateInstance(targetType, null);

               foreach (var spitem in sourceProperties)
               {
                   foreach (var tpitem in targetProperties)
                   {
                       if (tpitem.Name.Equals(spitem.Name))
                       {
                           tpitem.SetValue(targetEntity, spitem.GetValue(item, null), null);
                       }
                   }
               }
               targetList.Add((T)targetEntity);
           }
       }
    }
}
