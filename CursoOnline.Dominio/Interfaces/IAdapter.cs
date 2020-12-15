using System.Collections.Generic;

namespace CursoOnline.Dominio.Interfaces
{
    public interface IAdapter<TSource, TDestiny>
    {
         TDestiny Parse(TSource source);
         IEnumerable<TDestiny> Parse(IEnumerable<TSource> source);
    }
}