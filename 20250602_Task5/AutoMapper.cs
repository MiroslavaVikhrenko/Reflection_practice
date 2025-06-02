using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _20250602_Task5
{
    public static class AutoMapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
    where TTarget : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            TTarget target = new TTarget();

            var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProp in sourceProps)
            {
                var targetProp = Array.Find(targetProps, p =>
                    p.Name == sourceProp.Name &&
                    p.PropertyType == sourceProp.PropertyType &&
                    p.CanWrite);

                if (targetProp != null)
                {
                    var value = sourceProp.GetValue(source);
                    targetProp.SetValue(target, value);
                }
            }

            return target;
        }
    }
}
