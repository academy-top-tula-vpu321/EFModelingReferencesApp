using EFModelingReferencesApp;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

using (CompaniesContext context = new())
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Country russia = new() { Title = "Russia" };
    context.Countries.Add(russia);
        


    Company yandex = new() { Title = "Yandex", Country = russia };
    Company ozon = new() { Title = "Ozon", Country = russia };

    context.Companies.AddRange(yandex, ozon);


    Employee tom = new()
    {
        Name = "Tom",
        BirthDate = new(2002, 9, 4),
        Company = yandex,
    };

    Employee bob = new()
    {
        Name = "Bob",
        BirthDate = new(1990, 8, 11),
        Company = ozon
    };

    Employee leo = new()
    {
        Name = "Leo",
        BirthDate = new(2001, 4, 12),
        Company = ozon,
    };

    Employee jim = new()
    {
        Name = "Jim",
        BirthDate = new(1998, 11, 25),
        Company = yandex,
    };

    context.Employees.AddRange(tom, bob, leo, jim);


    context.SaveChanges();
}

using (CompaniesContext context = new())
{
    //var companies = context.Companies;
    //var employees = context.Employees;

    //foreach (var employee in employees)
    //    Console.WriteLine($"{employee.Name} {employee?.Company?.Title}");
    //Console.WriteLine();

    //foreach (var company in companies)
    //{
    //    Console.WriteLine(company.Title);
    //    foreach (var e in company.Employees)
    //        Console.WriteLine($"\t{e.Name} {e.BirthDate.ToLongDateString()}");
    //    Console.WriteLine();
    //}


    // EAGER LOADING жадная загрузка

    //var employeesEager = context.Employees
    //                           .Include(e => e.Company)
    //                           .ToList();
    //foreach (var employee in employeesEager)
    //    Console.WriteLine($"{employee.Name} {employee?.Company?.Title}");
    //Console.WriteLine();

    //var comaniesEager = context.Companies
    //                            .Include(c => c.Employees) 
    //                            .ToList();

    //foreach(var c in comaniesEager)
    //{
    //    Console.WriteLine($"Comapny: {c.Title}");
    //    foreach(var e in c.Employees)
    //        Console.WriteLine($"\t {e.Name}");
    //    Console.WriteLine();
    //}


}