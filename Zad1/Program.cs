using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.EmailService;
using MailServices;

namespace Zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IEmailService, EmailService>()
                .Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"))
                .BuildServiceProvider();
            
                
            var users = new List<User>
            {
                new User {Email = "kacperchromik@gmail.com", FirstName = "Dawid"},
                new User {Email = "kacperchromik@gmail.com", FirstName = "Adam"},
                new User {Email = "kacperchromik@gmail.com", FirstName = "Monika"},
                new User {Email = "kacperchromik@gmail.com", FirstName = "Nexer"},
                new User {Email = "kacperchromik@gmail.com", FirstName = "Szymon"},
            };
            
            serviceProvider.GetService<IEmailService>().SendEmailToUser(users);

            Console.WriteLine("Hello World!");
        }
    }
}