using EFModelingReferencesApp;
using Microsoft.EntityFrameworkCore;

//using (CompaniesContext context = new())
//{
//    // DataInit(context);
//}

using (CompaniesContext context = new())
{
    // EagerLoading(context);

    // LAZY LOADING
    var companies = context.Companies.ToList();
    foreach (var c in companies)
    {
        Console.WriteLine($"Company: {c.Title} {c?.Country?.Title} - {c?.Country?.Capital?.Title}");
        foreach (var e in c!.Employees!)
            Console.WriteLine($"\tEmployee: {e.Name} ({e.BirthDate.ToShortDateString()}) - {e?.Position?.Title}");
    }

}
void DataInit(CompaniesContext context)
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    City russiaCapital = new() { Title = "Moscow" };

    Country russia = new() { Title = "Russia", Capital = russiaCapital };
    context.Countries.Add(russia);

    Position developer = new() { Title = "Developer" };
    Position tester = new() { Title = "Tester" };

    Company yandex = new() { Title = "Yandex", Country = russia };
    Company ozon = new() { Title = "Ozon", Country = russia };

    context.Companies.AddRange(yandex, ozon);


    Employee tom = new()
    {
        Name = "Tom",
        BirthDate = new(2002, 9, 4),
        Company = yandex,
        Position = developer
    };

    Employee bob = new()
    {
        Name = "Bob",
        BirthDate = new(1990, 8, 11),
        Company = ozon,
        Position = tester
    };

    Employee leo = new()
    {
        Name = "Leo",
        BirthDate = new(2001, 4, 12),
        Company = ozon,
        Position = developer
    };

    Employee jim = new()
    {
        Name = "Jim",
        BirthDate = new(1998, 11, 25),
        Company = yandex,
        Position = tester
    };

    context.Employees.AddRange(tom, bob, leo, jim);


    context.SaveChanges();
}
void EagerLoading(CompaniesContext context)
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
    //                                .ThenInclude(c => c!.Country)
    //                           .ToList();
    //foreach (var employee in employeesEager)
    //    Console.WriteLine($"{employee.Name} {employee?.Company?.Title} {employee?.Company?.Country?.Title}");
    //Console.WriteLine();

    //var comaniesEager = context.Companies
    //                            .Include(c => c.Employees)
    //                            .Include(c => c.Country)
    //                            .ToList();

    //foreach (var c in comaniesEager)
    //{
    //    Console.WriteLine($"Comapny: {c.Title} {c?.Country?.Title}");
    //    foreach (var e in c.Employees!)
    //        Console.WriteLine($"\t {e.Name}");
    //    Console.WriteLine();
    //}

    var employees = context.Employees
                            .Include(e => e.Position)
                            .Include(e => e.Company)
                                .ThenInclude(c => c!.Country)
                                    .ThenInclude(cr => cr!.Capital)
                            .ToList();

    foreach (var e in employees)
    {
        Console.WriteLine($"Name: {e.Name}");
        Console.WriteLine($"\tBirth date: {e.BirthDate}");

        Console.WriteLine($"Company: {e?.Company?.Title} ({e?.Company?.Country?.Title} - {e?.Company?.Country?.Capital?.Title})");
        Console.WriteLine($"\tPosition: {e.Position.Title}");
        Console.WriteLine();
    }

    var companies = context.Companies
                            .Include(c => c.Employees)
                                .ThenInclude(e => e.Position)
                            .Include(c => c.Country)
                                .ThenInclude(cr => cr!.Capital)
                            .ToList();
    foreach (var c in companies)
    {
        Console.WriteLine($"Company: {c.Title} ({c?.Country?.Title} - {c?.Country?.Capital?.Title})");
        foreach (var e in c!.Employees!)
            Console.WriteLine($"\tEmployee: {e.Name} ({e.BirthDate.ToShortDateString()}) - {e?.Position?.Title}");
    }
}
void ExplicitLoading(CompaniesContext context)
{
    // EXPLICIT LOADING - явная загрузка

    var companies = context.Companies;
    foreach (var c in companies)
        Console.WriteLine($"{c.Id} - {c.Title}");
    Console.Write("\nSelect company: ");
    int select = Int32.Parse(Console.ReadLine());

    var company = context.Companies.FirstOrDefault(c => c.Id == select);

    //context.Employees
    //        .Where(e => e.CompanyId == company.Id)
    //        .Load();

    context.Entry(company)
        .Collection(c => c.Employees)
        .Load();
    context.Entry(company)
            .Reference(c => c.Country)
            .Load();

    Console.WriteLine($"{company.Title}");
    foreach (var e in company.Employees)
        Console.WriteLine($"\t{e.Name}");
}