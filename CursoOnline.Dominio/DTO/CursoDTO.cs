using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.DTO
{
    public class CursoDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CargaHoraria { get; set; }
        public EPublicoAlvo PublicoAlvo { get; set; }
    }
}