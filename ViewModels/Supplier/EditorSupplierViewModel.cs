using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TesteEstagio.Models;

namespace TesteEstagio.ViewModels.Supplier;

public class EditorSupplierViewModel
{
    [DisplayName("Name")]
    [Required(ErrorMessage = "Preeencha o campo")]
    public string Name { get; set; }

    [DisplayName("CNPJ")]
    [Required(ErrorMessage = "Preeencha o campo")]
    [MaxLength(18)]
    [MinLength(14)]
    public string CNPJ { get; set; }

    [DisplayName("Specialty")]
    [EnumDataType(typeof(Specialties))]
    [Required(ErrorMessage = "Selecione alguma opção")]
    public Specialties Specialty { get; set; }
}