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

        public ValueTask<TEntidade> BuscarPorId(int id)
        {
            return _dataset.FindAsync(id);
        }

        public Task<List<TEntidade>> BuscarTodos()
        {
            return _dataset.ToListAsync();
        }

        public async Task<bool> Excluir(TEntidade entidade)
        {
            _dataset.Remove(entidade);
            var registrosAfetados = await _context.SaveChangesAsync();
            return registrosAfetados > 0;
        }

        public async Task<bool> Atualizar(TEntidade entidade)
        {
            var registrosAfetados = await _context.SaveChangesAsync();
            return registrosAfetados > 0;
        }

        public async Task<bool> Salvar(TEntidade entidade)
        {
            await _dataset.AddAsync(entidade);
            var registrosAfetados = await _context.SaveChangesAsync();
            return registrosAfetados > 0;
        }
    }
}