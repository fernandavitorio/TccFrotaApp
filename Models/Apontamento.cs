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
        // identificador unico do objeto. incrementa automáticamente a cada inserção no banco de dados
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //Data que o objeto foi persistido no banco de dados pela utima vez
        public DateTime DtAtualizacao { get; set; }
        //Informação adicional do apontamento, como a kilometragem inicial, horimetro e etc.
        public int AditionalInformation { get; set; }
        // setor onde será realizada a coleta
        public SETOR Setor { get; set; }
        // Tipo de apontamento a ser realizado, ex: inicial, km inicial e etc...
        public TIPO_APONTAMENTO Tipo { get; set; }
        //Id do banco do veiculo utilizado na coleta
        public int VeiculoId { get; set; }
        //ORM preenche automáticamento com o objeto que representa o veiculo associado
        public Veiculo Veiculo { get; set; }
        //Id do banco do colaborador do tipo motorista que realizou a atividade
        public int MotoristaId { get; set; }
        //ORM preenche automáticamento com o objeto que representa o Motorista associado
        public Colaborador Motorista { get; set; }
        //Id do banco do colaborador do tipo coletor que realizou a atividade
        public int Coletor1Id { get; set; }
        //ORM preenche automáticamento com o objeto que representa o Coletor1 associado
        public Colaborador Coletor1 { get; set; }
        //Id do banco do colaborador do tipo coletor que realizou a atividade, não é obrigatório
        public int? Coletor2Id { get; set; }
        //ORM preenche automáticamento com o objeto que representa o Coletor2 associado
        public Colaborador Coletor2 { get; set; }
        //Id do banco do colaborador do tipo coletor que realizou a atividade, não é obrigatório
        public int? Coletor3Id { get; set; }
        //ORM preenche automáticamento com o objeto que representa o Coletor3 associado
        public Colaborador Coletor3 { get; set; }
        //Id do banco do apontamento pai dessa atividade, obrigatório para todos os apontamentos realizados durante a coleta
        public int? ApontamentoInicialId { get; set; }
        //ORM preenche automáticamento com o objeto que representa o ApontamentoInicial associado
        public Apontamento ApontamentoInicial { get; set; }
        //Se for um apontamento inicial ele vai possuir vários filhos, a ORM preenche essa lista com eles
        public List<Apontamento> Apontamentos { get; set; }
    }
}