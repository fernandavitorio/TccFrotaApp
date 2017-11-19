using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TccFrotaApp.Models
{
    public class Colaborador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public TIPO_COLABORADOR Funcao { get; set; }

        public string LoginId { get; set; }
        public Login Login { get; set; }

        [InverseProperty("Motorista")]
        public List<Apontamento> ApontamentosMotorista { get; set; }

        [InverseProperty("Coletor1")]
        public List<Apontamento> ApontamentosColetor1 { get; set; }

        [InverseProperty("Coletor2")]
        public List<Apontamento> ApontamentosColetor2 { get; set; }

        [InverseProperty("Coletor3")]
        public List<Apontamento> ApontamentosColetor3 { get; set; }
    }
}