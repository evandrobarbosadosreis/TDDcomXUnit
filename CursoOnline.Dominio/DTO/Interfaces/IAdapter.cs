using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio.DTO.Interfaces
{
    public interface IAdapter<TSource, TDestiny>
    {
         TDestiny Parse(TSource source);
         IEnumerable<TDestiny> Parse(IEnumerable<TSource> source);
    }
}