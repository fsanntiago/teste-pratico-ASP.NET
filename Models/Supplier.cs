using System.ComponentModel.DataAnnotations;

namespace TesteEstagio.Models;

public enum Specialties
{
    [Display(Name = "Comércio")] Comercio,
    [Display(Name = "Serviço")] Servico,
    [Display(Name = "Indústria")] Industria
}

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public Specialties Specialty { get; set; }
}