using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteEstagio.Data;
using TesteEstagio.Models;
using TesteEstagio.ViewModels.Supplier;

namespace TesteEstagio.Controllers;

public class SupplierController : Controller
{
    private readonly DataContext _context;

    public SupplierController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var suppliers = await _context.Suppliers.ToListAsync();

        return View(suppliers);
    }

    public string Alert(string title, string icon)
    {
        var alert =
            "Swal.mixin({toast: true,position: 'top-end',showConfirmButton: false,timer: 2700,timerProgressBar: true}).fire({icon: '" +
            icon + "',title: '" + title + "' })";
        return alert;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(EditorSupplierViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["errorMessage"] = "Não foi possivel cadastrar o fornecedor!";
            return View();
        }

        try
        {
            var supplier = new Supplier()
            {
                Id = 0,
                Name = model.Name,
                Specialty = model.Specialty,
                CNPJ = Regex.Replace(model.CNPJ, "[^0-9]", "")
            };

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
            TempData["successMessage"] = Alert("Fornecedor cadastrado com sucesso!", "success");

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null) return NotFound();
            var supplierViewModel = new EditorSupplierViewModel()
            {
                Name = supplier.Name,
                Specialty = supplier.Specialty,
                CNPJ = supplier.CNPJ
            };
            return View(supplierViewModel);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditorSupplierViewModel model, int id)
    {
        if (!ModelState.IsValid)
        {
            TempData["errorMessage"] = "Não foi possivel atualizar o fornecedor!";
            return View();
        }

        try
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null) return NotFound();

            supplier.Name = model.Name;
            supplier.Specialty = model.Specialty;
            supplier.CNPJ = Regex.Replace(model.CNPJ, "[^0-9]", "");

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            TempData["successMessage"] = Alert("Fornecedor atualizado com sucesso!", "success");

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.Write(ex.InnerException);
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null) return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}