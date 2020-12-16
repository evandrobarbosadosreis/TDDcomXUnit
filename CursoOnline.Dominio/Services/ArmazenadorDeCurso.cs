using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces;
using CursoOnline.Dominio.Utils;
using CursoOnline.Dominio.Services.Interfaces;

namespace CursoOnline.Dominio.Services
{

    public class ArmazenadorDeCurso : IArmazenadorDeCurso
    {
        private readonly ICursoRepository _repository;
        private readonly CursoAdapter _adapter;

        public ArmazenadorDeCurso(ICursoRepository repository)
        {
            _repository = repository;
            _adapter = new CursoAdapter();
        }

        public async Task<bool> Armazenar(CursoDTO cursoDTO)
        {
            var cursoCadastradoComEsteNome = await _repository.BuscarPorNome(cursoDTO.Nome);

            var nomeIndisponivel = cursoCadastradoComEsteNome != null &&
                                   cursoCadastradoComEsteNome.Id != cursoDTO.Id;

            ValidacaoBuilder
                .CriarNovo()
                .Se(nomeIndisponivel, Resources.NomeJaCadastrado)
                .Build();

            if (cursoDTO.Id > 0)
            {
                var curso = await _repository.BuscarPorId(cursoDTO.Id);
                curso.AlterarCargaHoraria(cursoDTO.CargaHoraria);
                curso.AlterarDescricao(cursoDTO.Descricao);
                curso.AlterarNome(cursoDTO.Nome);
                curso.AlterarPublicoAlvo(cursoDTO.PublicoAlvo);
                curso.AlterarValor(cursoDTO.Valor);
                await _repository.Commit();
                return true;
            }
            else
            {
                var curso = _adapter.Parse(cursoDTO);
                await _repository.Adicionar(curso);
                await _repository.Commit();
                cursoDTO.Id = curso.Id;
                return true;
            }
        }
    }
}