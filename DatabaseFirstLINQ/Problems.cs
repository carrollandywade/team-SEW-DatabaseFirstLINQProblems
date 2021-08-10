using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users;
            int result = users.ToList().Count;

            Console.WriteLine(result);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;
            var oneHundredFiftyDollarProducts = products.Where(product => product.Price > 150);
            foreach (var product in oneHundredFiftyDollarProducts)
            {
                Console.WriteLine($"{product.Name}:{product.Price}dollars");
            }
        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            var productLetters = products.Where(products => products.Name.Contains("s"));

            foreach (var product in productLetters)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var dateRegistered = "1/1/2016";
            var convertStringToDate = DateTime.Parse(dateRegistered);
            var registeredUsers = users.Where(user => user.RegistrationDate.Value.Year < 2016);

            foreach (var user in registeredUsers)
            {
                Console.WriteLine($"{user.Email} - {user.RegistrationDate}");
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var RegisteredUsersBetweenDates = users.Where(user => user.RegistrationDate.Value.Year > 2016).Where(user => user.RegistrationDate.Value.Year < 2018);
            foreach (var user in RegisteredUsersBetweenDates)
            {
                Console.WriteLine($"{user.Email}-{user.RegistrationDate}");
            }
        }


        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var aftonItems = _context.ShoppingCarts.Include(ai => ai.User).Include(ai => ai.Product).Where(ai => ai.User.Email == "afton@gmail.com");
            foreach (ShoppingCart item in aftonItems)
            {
                Console.WriteLine($"Prouduct Name: {item.Product.Name} Price: {item.Product.Price} Quantity: {item.Quantity}");
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var odaCost = _context.ShoppingCarts.Include(oc => oc.User).Include(oc => oc.Product).Where(oc => oc.User.Email == "oda@gmail.com")
                .Select(oc => oc.Product.Price).Sum();
            Console.WriteLine("$" + odaCost);

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employeeProducts = _context.UserRoles.Where(ep => ep.Role.RoleName == "Employee").Select(ep => ep.UserId);
            var shoppingCart = _context.ShoppingCarts.Include(sc => sc.User).Include(sc => sc.Product).Where(sc => employeeProducts.Contains(sc.UserId));

            foreach (ShoppingCart cart in shoppingCart)
            {
                Console.WriteLine($"Email: {cart.User.Email} Product Name: {cart.Product.Name} Product Price: {cart.Product.Price} Quantity: {cart.Quantity}");
            }
        }
        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Car",
                Description = "2021 Mustang Convertible",
                Price = 35000
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(pr => pr.Id == 8).Select(pr => pr.Id).SingleOrDefault();

            ShoppingCart newItem = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newItem);
            _context.SaveChanges();

        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(pr => pr.Id == 8).SingleOrDefault();
            product.Price = 2000;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(ur => ur.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            bool checkDb = false;
            var everyUser = _context.Users;

            foreach (User user in everyUser)
            {
                if (user.Email == email && user.Password == password)
                {
                    checkDb = true;
                }
            }
            if (checkDb)
            {
                Console.WriteLine("You are now signed in!");
            }
            else
            {
                Console.WriteLine("Invalid Email or Password");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var shoppingCart = _context.ShoppingCarts
              .Include(sc => sc.Product)
              .Select(sc => new { sc.UserId, sc.Product, sc.Quantity, })
              .GroupBy(sc => sc.UserId)
              .Select(sc => new {
                  UserK = sc.Key,
                  Count = sc.Count(),
                  Subtotal = sc.Sum(st => st.Quantity * st.Product.Price),
              }
              ).ToList();


            decimal AllCartsTotal = 0;
            foreach (var item in shoppingCart)
            {
                Console.WriteLine($"User ID: {item.UserK} has {item.Count} in total of ${item.Subtotal}");
                AllCartsTotal += (decimal)item.Subtotal;
            }
            Console.WriteLine($"Total cart Value is ${AllCartsTotal}");


        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            

            bool checkDb = false;
            while (checkDb == false)
            {
                Console.WriteLine("Enter your email:");
                var email = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();
                var everyUser = _context.Users;

                foreach (User user in everyUser)
                {
                    if (user.Email == email && user.Password == password)
                    {
                        checkDb = true;
                    }
                }
                if (checkDb)
                // 2. If the user succesfully signs in
                // a. Give them a menu where they perform the following actions within the console       
                {
                    Console.WriteLine("You are now signed in!");
                    Console.WriteLine("Menu: Choose number of the option you want.");
                    Console.WriteLine("1 - View products in your shopping cart.");
                    Console.WriteLine("2 - View all products for sale.");
                    Console.WriteLine("3 - Add a product to the shopping cart.");
                    Console.WriteLine("4 - Remove product from shopping cart.");
                    Console.WriteLine("5 - Logout.");
                    var userSelection = Console.ReadLine();

                    switch (userSelection)
                    {
                        case "1":
                            // View the products in their shopping cart
                            var allItems = _context.ShoppingCarts.Include(ai => ai.User).Include(ai => ai.Product).Where(ai => ai.User.Email == email);
                            foreach (ShoppingCart item in allItems)
                            {
                                Console.WriteLine($"Product Name: {item.Product.Name} Price: {item.Product.Price} Quantity: {item.Quantity}");
                            }
                            BonusThree();
                            break;
                        case "2":
                            // View all products in the Products table
                            var products = _context.Products;
                            foreach (Product product in products)
                            {
                                Console.WriteLine(product.Name);
                            }
                            BonusThree();
                            break;
                        case "3":
                            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart) 
                            // Only works for new items not previously in cart.
                            var displayProducts = _context.Products;
                            foreach (Product product in displayProducts)
                            {
                                Console.WriteLine($"{product.Id} - {product.Name}");
                            }
                            Console.WriteLine("Pick number of item to add to cart");
                            int productSelection = Convert.ToInt32(Console.ReadLine());                          

                            var userId = _context.Users.Where(u => u.Email == email).Select(u => u.Id).SingleOrDefault();
                            var productId = _context.Products.Where(pr => pr.Id == productSelection).Select(pr => pr.Id).SingleOrDefault();

                            ShoppingCart newItem = new ShoppingCart()
                            {
                                UserId = userId,
                                ProductId = productId,
                                Quantity = 1,
                            };
                            _context.ShoppingCarts.Add(newItem);
                            _context.SaveChanges();
                            BonusThree();
                            break;
                        case "4":
                            // Remove a product from their shopping cart

                            var viewCart = _context.ShoppingCarts.Include(ai => ai.User).Include(ai => ai.Product).Where(ai => ai.User.Email == email);
                            foreach (ShoppingCart item in viewCart)
                            {
                                Console.WriteLine($"Product ID: {item.Product.Id} - Product Name: {item.Product.Name} Price: {item.Product.Price} Quantity: {item.Quantity}");
                            }
                            Console.WriteLine("Choose item number to remove.");
                            var deleteSelection = Convert.ToInt32(Console.ReadLine());
                            var deleteProduct = _context.ShoppingCarts.Where(dp => dp.ProductId == deleteSelection).SingleOrDefault();
                            _context.ShoppingCarts.Remove(deleteProduct);
                            _context.SaveChanges();
                            BonusThree();
                            break;
                        case "5":
                            Console.WriteLine("Goodbye!");
                            checkDb = false;
                            break;
                    }
                        
                }
                else
                // 3. If the user does not succesfully sign in
                // a. Display "Invalid Email or Password"
                // b. Re-prompt the user for credentials
                {
                    Console.WriteLine("Invalid Email or Password");
                }
            }        
          
           

        }

    }
}



