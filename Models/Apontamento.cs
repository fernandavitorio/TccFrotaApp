using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TccFrotaApp.Models
{
    public class Apontamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public int AditionalInformation {get;set;}
        public SETOR Setor { get; set; }
        public TIPO_APONTAMENTO Tipo { get; set; }

        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        public int MotoristaId { get; set; }
        public Colaborador Motorista { get; set; }
        
        public int Coletor1Id { get; set; }
        public Colaborador Coletor1 { get; set; }

        public int? Coletor2Id { get; set; }
        public Colaborador Coletor2 { get; set; }

        public int? Coletor3Id { get; set; }
        public Colaborador Coletor3 { get; set; }

        public int? ApontamentoInicialId { get; set; }
        public Apontamento ApontamentoInicial { get; set; }
        public List<Apontamento> Apontamentos{get;set;}
    }
}