using SqlApp.Services;

var program = new ProgramService();

while (true)
{
    
    Console.Clear();
    Console.WriteLine("What do you want to do?");
    Console.WriteLine("1. Create new contact");
    Console.WriteLine("2. Show all contacts");
    Console.WriteLine("3. Show a specific contact");
    Console.WriteLine("4. Update an existing contact");
    Console.WriteLine("5. Remove a contact");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await ProgramService.CreateContactAsync(); //typ klar
            break;


        case "2":
            Console.Clear();
            await ProgramService.ShowAllContactsAsync(); // typ klar

            break;


        case "3":
            Console.Clear();
            await ProgramService.ShowSpecificContactAsync(); // typ klar
            break;


        case "4":
            Console.Clear();
            await ProgramService.UpdateContactAsync(); // typ klar
            break;


        case "5":
            Console.Clear();
            await ProgramService.RemoveContactAsync(); //typ klar
            break;
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    
}
