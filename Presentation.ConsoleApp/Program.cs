using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Dialogs;

var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IFileService>(new FileService());
serviceCollection.AddSingleton<IContactService, ContactService>();
serviceCollection.AddSingleton<MenuDialog>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var menuDialogs = serviceProvider.GetService<MenuDialog>();

menuDialogs?.ShowMenu();