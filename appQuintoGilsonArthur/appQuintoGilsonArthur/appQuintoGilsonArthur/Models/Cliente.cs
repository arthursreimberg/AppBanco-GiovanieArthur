using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace appQuintoGiovaniArthur.Models
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public int? IdCli { get; set; }

        [Display(Name = "Nome completo")]
        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        public string NomeCli { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "O CPF deve conter exatamente 9 dígitos numéricos.")]
        public string RG { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos.")]
        public string CPF { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        [DataType(DataType.DateTime)]
        public DateTime DataNasCli { get; set; }
    }
}
