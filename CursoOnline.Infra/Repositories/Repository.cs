using System.Collections.Generic;
using System.Threading.Tasks;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Models;
using CursoOnline.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CursoOnline.Infra.Repositories
{
    public class Repository<TEntidade> : IRepository<TEntidade> where TEntidade : Entidade
    {
        protected readonly PostgreSQLContext _context;
        protected readonly DbSet<TEntidade> _dataset;

        public Repository(PostgreSQLContext context)
        {
            _context = context;
            _dataset = context.Set<TEntidade>();
        }

        public ValueTask<TEntidade> BuscarPorId(int? id)
        {
            return _dataset.FindAsync(id);
        }

        public async Task<IEnumerable<TEntidade>> BuscarTodos()
        {
            return await _dataset.ToListAsync();
        }

        public void Excluir(TEntidade entidade)
        {
            _dataset.Remove(entidade);
        }

        public async Task Adicionar(TEntidade entidade)
        {
            await _dataset.AddAsync(entidade);
        }

        public Task<bool> RegistroExiste(int? id)
        {
            return _dataset.AnyAsync(e => e.Id == id);
        }

        public Task<int> Commit() => _context.SaveChangesAsync();
    }
}