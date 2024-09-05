using System;
using System.Collections.Generic;

class Product
{
    // Свойства продукта
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Конструктор для инициализации нового продукта
    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    // Метод для обновления цены продукта
    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }

    // Метод для продажи продукта
    public void SellProduct(int amount)
    {
        // Проверяем, достаточно ли товара в наличии
        if (amount > Quantity)
        {
            Console.WriteLine("Недостаточно товара на складе!");
        }
        else
        {
            Quantity -= amount; // Уменьшаем количество товара
            Console.WriteLine($"Товар продан! Осталось {Quantity} штук.");
        }
    }

    // Переопределение метода ToString для вывода информации о продукте
    public override string ToString()
    {
        return $"{Name} - Цена: {Price}, Количество: {Quantity}";
    }
}

class Store
{
    // Список товаров в магазине
    private List<Product> products = new List<Product>();

    // Метод для добавления нового продукта в магазин
    public void AddProduct(Product product)
    {
        products.Add(product); // Добавляем продукт в список
        Console.WriteLine("Товар успешно добавлен!");
    }

    // Метод для отображения списка всех товаров
    public void ShowProducts()
    {
        // Проверяем, пуст ли список товаров
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст.");
            return;
        }

        Console.WriteLine("Список товаров:");
        for (int i = 0; i < products.Count; i++)
        {
            // Выводим информацию о каждом товаре
            Console.WriteLine($"{i + 1}. {products[i]}");
        }
    }

    // Метод для поиска продукта по названию
    public Product FindProductByName(string name)
    {
        return products.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // Метод для продажи продукта по названию и количеству
    public void SellProduct(string name, int quantity)
    {
        Product product = FindProductByName(name);
        if (product == null)
        {
            Console.WriteLine("Товар не найден!");
            return;
        }

        product.SellProduct(quantity); // Продажа найденного продукта
    }
}

class Program
{
    static void Main()
    {
        Store store = new Store(); // Создание нового магазина
        bool running = true;

        // Главный цикл программы
        while (running)
        {
            // Выводим меню выбора действий
            Console.WriteLine("\n1. Добавить товар");
            Console.WriteLine("2. Просмотреть список товаров");
            Console.WriteLine("3. Найти товар");
            Console.WriteLine("4. Продать товар");
            Console.WriteLine("5. Выход");
            Console.Write("Введите номер команды: ");

            // Обработка ввода пользователя
            switch (Console.ReadLine())
            {
                case "1":
                    AddProduct(store); // Добавление нового товара
                    break;
                case "2":
                    store.ShowProducts(); // Просмотр списка товаров
                    break;
                case "3":
                    FindProduct(store); // Поиск товара по названию
                    break;
                case "4":
                    SellProduct(store); // Продажа товара
                    break;
                case "5":
                    running = false; // Выход из программы
                    break;
                default:
                    Console.WriteLine("Неверная команда. Попробуйте снова.");
                    break;
            }
        }
    }
    // Метод для добавления нового товара в магазин
    static void AddProduct(Store store)
    {
        Console.Write("Введите название товара: ");
        string name = Console.ReadLine();

        decimal price;
        // Ввод и проверка корректности цены
        while (true)
        {
            Console.Write("Введите цену товара: ");
            if (decimal.TryParse(Console.ReadLine(), out price) && price > 0)
            {
                break;
            }
            Console.WriteLine("Неверный формат цены. Попробуйте снова.");
        }

        int quantity;
        // Ввод и проверка корректности количества
        while (true)
        {
            Console.Write("Введите количество товара: ");
            if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
            {
                break;
            }
            Console.WriteLine("Неверный формат количества. Попробуйте снова.");
        }

        Product product = new Product(name, price, quantity); // Создание нового продукта
        store.AddProduct(product); // Добавление продукта в магазин
    }

    // Метод для поиска товара в магазине
    static void FindProduct(Store store)
    {
        Console.Write("Введите название товара для поиска: ");
        string name = Console.ReadLine();
        Product product = store.FindProductByName(name);
        if (product != null)
        {
            Console.WriteLine($"Найден товар: {product}"); // Вывод информации о найденном товаре
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    // Метод для продажи товара в магазине
    static void SellProduct(Store store)
    {
        Console.Write("Введите название товара для продажи: ");
        string name = Console.ReadLine();

        int quantity;
        // Ввод и проверка корректности количества для продажи
        while (true)
        {
            Console.Write("Введите количество для продажи: ");
            if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
            {
                break;
            }
            Console.WriteLine("Неверный формат количества. Попробуйте снова.");
        }

        store.SellProduct(name, quantity); // Продажа товара
    }
}