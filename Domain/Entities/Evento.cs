using System;

namespace Domain.Entities
{
    public class Evento
    {
        public long Id { get; set; }
        public string NomeResponsavel { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long IdSala { get; set; }
        public string Sala { get; set; }
    }
}
