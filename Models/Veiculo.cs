using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TccFrotaApp.Models
{
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }   
        public string Identificador { get; set; }

        [InverseProperty("Veiculo")]
        public List<Apontamento> Apontamentos { get; set; }

    }
}