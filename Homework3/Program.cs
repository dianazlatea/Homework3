using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace BusinessApp{
    class Program
    {
        static List<object> entities = new List<object>();

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nBusiness Application");
                Console.WriteLine("1. Add New Entity");
                Console.WriteLine("2. Display All Entities");
                Console.WriteLine("3. Update an Entity");
                Console.WriteLine("4. Delete an Entity");
                Console.WriteLine("5. Save Entities to CSV");
                Console.WriteLine("6. Load Entities from CSV");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddEntity();
                        break;
                    case "2":
                        DisplayEntities();
                        break;
                    case "3":
                        UpdateEntity();
                        break;
                    case "4":
                        DeleteEntity();
                        break;
                    case "5":
                        SaveToCSV();
                        break;
                    case "6":
                        LoadFromCSV();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
        static void AddEntity()
{
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Employee");
            Console.WriteLine("3. Product");
            Console.WriteLine("4. Order");
            Console.WriteLine("Choose entity type: ");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Phone: ");
        string phone = Console.ReadLine();
        entities.Add(new Customer(id, name, email, phone));
    }
    else if (choice == "2")
    {
        Console.Write("Enter ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Department: ");
        string department = Console.ReadLine();
        Console.Write("Enter Salary: ");
        double salary = double.Parse(Console.ReadLine());
        entities.Add(new Employee(id, name, department, salary));
    }
    else if (choice == "3")
    {
        Console.Write("Enter Product ID: ");
        int productId = int.Parse(Console.ReadLine());
        Console.Write("Enter Product Name: ");
        string productName = Console.ReadLine();
        Console.Write("Enter Price: ");
        double price = double.Parse(Console.ReadLine());
        entities.Add(new Product(productId, productName, price));
    }
    else if (choice == "4")
    {
        Console.Write("Enter Order ID: ");
        int orderId = int.Parse(Console.ReadLine());
        Console.Write("Enter Order Date: ");
        string orderDate = Console.ReadLine();

        List<Product> products = new List<Product>();
        bool addingProducts = true;

        while (addingProducts)
        {
            Console.WriteLine("Add Product to Order? (y/n)");
            string addProductChoice = Console.ReadLine().ToLower();

            if (addProductChoice == "y")
            {
                Console.Write("Enter Product ID: ");
                int productId = int.Parse(Console.ReadLine());
                Console.Write("Enter Product Name: ");
                string productName = Console.ReadLine();
                Console.Write("Enter Price: ");
                double price = double.Parse(Console.ReadLine());
                products.Add(new Product(productId, productName, price));
            }
            else
            {
                addingProducts = false;
            }
        }

        entities.Add(new Order(orderId, orderDate, products));
    }
    else
    {
        Console.WriteLine("Invalid choice.");
    }
}
           static void DisplayEntities()
        {
            foreach (var entity in entities)
            {
                Console.WriteLine(entity.ToString());
            }
        }
        static void UpdateEntity()
{
    if (entities.Count == 0)
    {
        Console.WriteLine("No entities available to update.");
        return;
    }

    DisplayEntities();
    Console.Write("Enter the index of the entity to update: ");
    if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= entities.Count)
    {
        Console.WriteLine("Invalid index.");
        return;
    }

    var entity = entities[index];

    if (entity is Customer customer)
    {
        Console.Write("Enter new Name (or press Enter to keep the current): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) customer.Name = newName;

        Console.Write("Enter new Email (or press Enter to keep the current): ");
        string newEmail = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newEmail)) customer.Email = newEmail;

        Console.Write("Enter new Phone (or press Enter to keep the current): ");
        string newPhone = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newPhone)) customer.Phone = newPhone;
    }
    else if (entity is Employee employee)
    {
        Console.Write("Enter new Name (or press Enter to keep the current): ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) employee.Name = newName;

        Console.Write("Enter new Department (or press Enter to keep the current): ");
        string newDepartment = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDepartment)) employee.Department = newDepartment;

        Console.Write("Enter new Salary (or press Enter to keep the current): ");
        string salaryInput = Console.ReadLine();
        if (double.TryParse(salaryInput, out double newSalary)) employee.Salary = newSalary;
    }
    else if (entity is Product product)
    {
        Console.Write("Enter new Product Name (or press Enter to keep the current): ");
        string newProductName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newProductName)) product.ProductName = newProductName;

        Console.Write("Enter new Price (or press Enter to keep the current): ");
        string priceInput = Console.ReadLine();
        if (double.TryParse(priceInput, out double newPrice)) product.Price = newPrice;
    }
    else if (entity is Order order)
    {
        Console.Write("Enter new Order Date (or press Enter to keep the current): ");
        string newOrderDate = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newOrderDate)) order.OrderDate = newOrderDate;

        Console.WriteLine("Modify Products in Order:");
        for (int i = 0; i < order.ProductList.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {order.ProductList[i]}");
        }

        Console.WriteLine("1. Update Product");
        Console.WriteLine("2. Add New Product");
        Console.WriteLine("3. Remove Product");
        Console.WriteLine("4. Skip Product Updates");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.Write("Enter product index to update: ");
            if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= order.ProductList.Count)
            {
                var prod = order.ProductList[productIndex - 1];

                Console.Write("Enter new Product Name (or press Enter to keep the current): ");
                string newProdName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newProdName)) prod.ProductName = newProdName;

                Console.Write("Enter new Price (or press Enter to keep the current): ");
                string prodPriceInput = Console.ReadLine();
                if (double.TryParse(prodPriceInput, out double newProdPrice)) prod.Price = newProdPrice;
            }
            else
            {
                Console.WriteLine("Invalid product index.");
            }
        }
        else if (choice == "2")
        {
            Console.Write("Enter Product ID: ");
            int prodId = int.Parse(Console.ReadLine());
            Console.Write("Enter Product Name: ");
            string prodName = Console.ReadLine();
            Console.Write("Enter Price: ");
            double prodPrice = double.Parse(Console.ReadLine());
            order.ProductList.Add(new Product(prodId, prodName, prodPrice));
        }
        else if (choice == "3")
        {
            Console.Write("Enter product index to remove: ");
            if (int.TryParse(Console.ReadLine(), out int prodIndex) && prodIndex > 0 && prodIndex <= order.ProductList.Count)
            {
                order.ProductList.RemoveAt(prodIndex - 1);
                Console.WriteLine("Product removed.");
            }
            else
            {
                Console.WriteLine("Invalid product index.");
            }
        }
    }
    else
    {
        Console.WriteLine("Unsupported entity type.");
    }

    Console.WriteLine("Entity updated successfully.");
}
static void DeleteEntity()
{
    if (entities.Count == 0)
    {
        Console.WriteLine("No entities available to delete.");
        return;
    }

    DisplayEntities();
    Console.Write("Enter the index of the entity to delete: ");
    if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= entities.Count)
    {
        Console.WriteLine("Invalid index.");
        return;
    }

    Console.WriteLine($"You selected: {entities[index]}");
    Console.Write("Are you sure you want to delete this entity? (y/n): ");
    string confirmation = Console.ReadLine().ToLower();

    if (confirmation == "y")
    {
        entities.RemoveAt(index);
        Console.WriteLine("Entity deleted successfully.");
    }
    else
    {
        Console.WriteLine("Deletion canceled.");
    }
}

static void SaveToCSV()
{
    if (entities.Count == 0)
    {
        Console.WriteLine("No entities available to save.");
        return;
    }

    try
    {
        using (StreamWriter writer = new StreamWriter("entities.csv"))
        {
            foreach (var entity in entities)
            {
                if (entity is Customer customer)
                {
                    // Header for Customer
                    writer.WriteLine("ID,Name,Email,Phone");
                    // Data for Customer
                    writer.WriteLine($"{customer.Id},{customer.Name},{customer.Email},{customer.Phone}");
                }
                else if (entity is Employee employee)
                {
                    // Header for Employee
                    writer.WriteLine("ID,Name,Department,Salary");
                    // Data for Employee
                    writer.WriteLine($"{employee.Id},{employee.Name},{employee.Department},{employee.Salary}");
                }
                else if (entity is Product product)
                {
                    // Header for Product
                    writer.WriteLine("ProductID,ProductName,Price");
                    // Data for Product
                    writer.WriteLine($"{product.ProductId},{product.ProductName},{product.Price}");
                }
                else if (entity is Order order)
                {
                    // Header for Order
                    writer.WriteLine("OrderID,OrderDate,Products");
                    // Data for Order (Products serialized as a string)
                    string products = string.Join(";", order.ProductList.Select(p => $"[ID={p.ProductId},Name={p.ProductName},Price={p.Price}]"));
                    writer.WriteLine($"{order.OrderId},{order.OrderDate},{products}");
                }
                else
                {
                    writer.WriteLine("Unknown,Unable to process this entity.");
                }
            }
        }

        Console.WriteLine("Data saved successfully to entitites.csv");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving to CSV: {ex.Message}");
    }
}

static void LoadFromCSV()
{
    if (!File.Exists("entities.csv"))
    {
        Console.WriteLine($"File not found: entities.csv");
        return;
    }

    try
    {
        entities.Clear();

        using (StreamReader reader = new StreamReader("entities.csv"))
        {
            string line;
            string currentHeader = null;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("ID,Name,Email,Phone"))
                {
                    currentHeader = "Customer";
                }
                else if (line.StartsWith("ID,Name,Department,Salary"))
                {
                    currentHeader = "Employee";
                }
                else if (line.StartsWith("ProductID,ProductName,Price"))
                {
                    currentHeader = "Product";
                }
                else if (line.StartsWith("OrderID,OrderDate,Products"))
                {
                    currentHeader = "Order";
                }
                else
                {
            
                    if (currentHeader == "Customer")
                    {
                        var parts = line.Split(',');
                        var customer = new Customer(
                            int.Parse(parts[0]), // ID
                            parts[1],            // Name
                            parts[2],            // Email
                            parts[3]             // Phone
                        );
                        entities.Add(customer);
                    }
                    else if (currentHeader == "Employee")
                    {
                        var parts = line.Split(',');
                        var employee = new Employee(
                            int.Parse(parts[0]), // ID
                            parts[1],            // Name
                            parts[2],            // Department
                            double.Parse(parts[3]) // Salary
                        );
                        entities.Add(employee);
                    }
                    else if (currentHeader == "Product")
                    {
                        var parts = line.Split(',');
                        var product = new Product(
                            int.Parse(parts[0]), // ProductID
                            parts[1],            // ProductName
                            double.Parse(parts[2]) // Price
                        );
                        entities.Add(product);
                    }
                    else if (currentHeader == "Order")
                    {
                        var parts = line.Split(new[] { ',' }, 3); // Split into ID, Date, and Products
                        var productList = new List<Product>();

                        if (!string.IsNullOrWhiteSpace(parts[2]))
                        {
                            var productStrings = parts[2].Split(';'); // Separate product strings
                            foreach (var prodStr in productStrings)
                            {
                                var productDetails = prodStr.Trim('[', ']').Split(',');
                                var productId = int.Parse(productDetails[0].Split('=')[1]);
                                var productName = productDetails[1].Split('=')[1];
                                var productPrice = double.Parse(productDetails[2].Split('=')[1]);

                                productList.Add(new Product(productId, productName, productPrice));
                            }
                        }

                        var order = new Order(
                            int.Parse(parts[0]), // OrderID
                            parts[1],            // OrderDate
                            productList          // ProductList
                        );
                        entities.Add(order);
                    }
                }
            }
        }

        Console.WriteLine("Data loaded successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading from CSV: {ex.Message}");
    }
}

}
}

