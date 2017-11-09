using System.Data.Entity;
using System.Collections.Generic;
using System;
using DataLayer.DAL.Entities;

namespace DataLayer.DAL.Context
{
    internal class DataContextInit : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {

            var categories = new List<Category>
           {
               new Category
               {
                   CategoryID = 0,
                   ParentCategoryID = 0,
                   NameCayegory = "Root",
                   Products = null
               },

               new Category
               {
                   CategoryID = 1,
                   ParentCategoryID = 0,
                   NameCayegory = "Tables"
               },

               new Category
               {
                   CategoryID = 2,
                   ParentCategoryID = 0,
                   NameCayegory = "Hardware"
               },

               new Category
               {
                   CategoryID = 3,
                   ParentCategoryID = 2,
                   NameCayegory = "Motherboards"
               },

               new Category
               {
                   CategoryID = 4,
                   ParentCategoryID = 2,
                   NameCayegory = "Processors"
               },

               new Category
               {
                   CategoryID = 5,
                   ParentCategoryID = 2,
                   NameCayegory = "Memmory"
               },

               new Category
               {
                   CategoryID = 6,
                   ParentCategoryID = 2,
                   NameCayegory = "Hard drive"
               }
           };


            var products = new List<Product>
            {
                new Product
                {
                    ProductID = 1,
                    Manufacturer = "Asus",
                    Model = "Z250",
                    ExtendedModel = "JHHGGGY",
                    Category = categories.Find(f => f.CategoryID == 1),
                    Quantity = 2,
                    PriceIn = 100,
                    PriceSale = 120
                },

                new Product
                {
                    ProductID = 2,
                    Manufacturer = "Lenovo",
                    Model = "A10-70l",
                    ExtendedModel = "USIJEHT-PL",
                    Category = categories.Find(f => f.CategoryID == 1),
                    Quantity = 5,
                    PriceIn = 200,
                    PriceSale = 220
                },

                new Product
                {
                    ProductID = 3,
                    Manufacturer = "Apple",
                    Model = "IPad Pro",
                    ExtendedModel = "HHYHDFTG-FJ",
                    Description = "LTE 128GB Space Gray",
                    Category = categories.Find(f => f.CategoryID == 1),
                    Quantity = 1,
                    PriceIn = 300,
                    PriceSale = 320
                },
                new Product
                {
                    ProductID = 4,
                    Manufacturer = "Intel",
                    Model = "Core I7",
                    ExtendedModel = "q7400",
                    Description = "[BOX]",
                    Category = categories.Find(f => f.CategoryID == 4),
                    Quantity = 10,
                    PriceIn = 400,
                    PriceSale = 420
                }

            };



            var statuses = new List<Status>
            {
                new Status
                {
                    StatusID = 1,
                    NameStatus = "New"
                },
                new Status
                {
                    StatusID = 2,
                    NameStatus = "Confirmed"
                },
                new Status
                {
                    StatusID = 3,
                    NameStatus = "Processed"
                },
                new Status
                {
                    StatusID = 4,
                    NameStatus = "Ready"
                },
                new Status
                {
                    StatusID = 5,
                    NameStatus = "Delivered"
                },
                new Status
                {
                    StatusID = 6,
                    NameStatus = "Closed"
                },
                new Status
                {
                    StatusID = 7,
                    NameStatus = "Frozen"
                }

            };

           

            
            var clients = new List<Client>
            {
                new Client
                {
                    ClientID = 1,
                    NameClient = "Vitya",
                    Adress = "Shabany",
                    Mood = "Mudak",
                    PhoneNumber = "+375468795122",
                },
                new Client
                {
                    ClientID = 2,
                    NameClient = "Kolya",
                    Adress = "Nezavisimosty",
                    Mood = "Norm",
                    PhoneNumber = "+375431238546"
                },
                new Client
                {
                    ClientID = 3,
                    NameClient = "Danila",
                    Adress = "Zvezdy",
                    Mood = "Good guy",
                    PhoneNumber = "+375889994455"
                }

            };

            var orders = new List<Order>
            {
                new Order
                {
                    OrderID = 1,
                    Status = statuses.Find(f => f.StatusID == 1),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 1),
                    DeliveryDate = DateTime.Parse("08/22/2017")
                },

                new Order
                {
                    OrderID = 2,
                    Status = statuses.Find(f => f.StatusID == 2),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 2)
                },

                new Order
                {
                    OrderID = 3,
                    Status = statuses.Find(f => f.StatusID == 3),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 3)
                },

                new Order
                {
                    OrderID = 4,
                    Status = statuses.Find(f => f.StatusID == 4),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 3)
                },

                new Order
                {
                    OrderID = 5,
                    Status = statuses.Find(f => f.StatusID == 5),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 3)
                },

                new Order
                {
                    OrderID = 6,
                    Status = statuses.Find(f => f.StatusID == 6),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 3)
                },

                new Order
                {
                    OrderID = 7,
                    Status = statuses.Find(f => f.StatusID == 7),
                    CreateOrder = DateTime.Now,
                    Client = clients.Find(c => c.ClientID == 3)
                },
            };



            var supliers = new List<Supplier>
            {
                new Supplier
                {
                    FormOfBusiness = "OOO",
                    ShortNameSupplier = "BENQ",
                    NameSupplier = "NASA",
                    Country ="USA",
                    UNP = "123456789",
                    Email = "nasa@mail.com",
                    RS = "123www15e35",
                    LegalAddress = "18 lavochka str. ",
                    Phone = "+375441234567",
                    BankAddress ="address",
                    BankRequisites = "req",
                    PhysicalAddress = "sss"
                },

                new Supplier
                {
                    FormOfBusiness = "ODO",
                    ShortNameSupplier = "DSM",
                    NameSupplier = "NIX",
                    Country ="Russia",
                    UNP = "987654321",
                    Email = "nix@mail.com",
                    RS = "123www15e35",
                    LegalAddress = "18 lavochka str. ",
                    Phone = "+375441234567",
                    BankAddress ="address",
                    BankRequisites = "req",
                    PhysicalAddress = "sss"
                    
                }
            };

            var Cart = new List<CartItem>
            {
                new CartItem
                {
                    CartID = 1,
                    Order = orders.Find(o => o.OrderID == 1),
                    Product = products.Find(p => p.ProductID == 1),
                    Quantity = 1
                }
            };


            var settings = new List<Settings>
            {
                new Settings
                {
                    SettingID = 1,
                    Currency = 2.0,
                    Markup = 10,
                    CompanyName = "HARD",
                    FormOfBusiness = "OOO",
                    RS = "123456789",
                    UNP = "987654321",
                    LegalAddress = "Rafieva 25/1",
                    PhysicalAddress = "Rafieva 25/1",
                    Country = "Belarus"
                }
            };

            
            clients.ForEach(c => context.Client.Add(c));
            categories.ForEach(o => context.Category.Add(o));
            products.ForEach(p => context.Product.Add(p));
            statuses.ForEach(s => context.Status.Add(s));
            supliers.ForEach(x => context.Supplier.Add(x));
            orders.ForEach(o => context.Order.Add(o));
            Cart.ForEach(o => context.Cart.Add(o));
            settings.ForEach(s => context.Settings.Add(s));
            context.SaveChanges();
        }
    }
}