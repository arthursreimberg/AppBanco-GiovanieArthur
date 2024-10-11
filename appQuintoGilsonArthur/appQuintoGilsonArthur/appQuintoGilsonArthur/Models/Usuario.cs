using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace appQuintoGiovaniArthur.Models
{
    public class Usuario
    {
        [Display (Name = "Código")]
        public int? IdUsu {  get; set; }

        [Display (Name = "Nome completo")]
        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        public string NomeUsu { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo cargo é obrigatório!")]
        public string Cargo { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        [DataType(DataType.DateTime)]
        public DateTime DataNasc { get; set; }
    }
}
