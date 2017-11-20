using System;
using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;


namespace TccFrotaApp.ViewModels
{
    [Validator(typeof(ApontamentoViewModelValidator))]
    public class ApontamentoViewModel
    {
        public int Id { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public object Information {get;set;}
        public string Setor { get; set; }
        public string Tipo { get; set; }

        public int VeiculoId { get; set; }
        public string VeiculoIdentificador { get; set; }
        public string VeiculoPlaca { get; set; }

        public int MotoristaId { get; set; }
        public string MotoristaNome { get; set; }
        
        public int Coletor1Id { get; set; }
        public string Coletor1Nome { get; set; }

        public int Coletor2Id { get; set; }
        public string Coletor2Nome { get; set; }

        public int Coletor3Id { get; set; }
        public string Coletor3Nome { get; set; }

        public int? ApontamentoInicialId { get; set; }
        public ApontamentoViewModel ApontamentoInicial { get; set; }

    }
}