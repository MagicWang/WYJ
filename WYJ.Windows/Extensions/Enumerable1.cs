using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class Enumerable1
    {
        /// <summary>
        /// 返回转换函数值最大的源元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TSource SelectMax<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, double> selector)
        {

            var maxValue = double.MinValue;
            TSource result = default(TSource);
            if (source == null || source.Count() <= 0)
                return result;
            source.ToList().ForEach(l =>
            {
                if (selector(l) >= maxValue)
                {
                    maxValue = selector(l);
                    result = l;
                }
            });
            return result;
        }
        /// <summary>
        /// 返回转换函数值最小的源元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TSource SelectMin<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource, double> selector)
        {

            var minValue = double.MaxValue;
            TSource result = default(TSource);
            if (source == null || source.Count() <= 0)
                return result;
            source.ToList().ForEach(l =>
            {
                if (selector(l) <= minValue)
                {
                    minValue = selector(l);
                    result = l;
                }
            });
            return result;
        }
    }
}
