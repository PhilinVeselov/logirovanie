using kt1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                new Serilog.Formatting.Json.JsonFormatter(renderMessage: true),
                "logs/log-.json",
                rollingInterval: RollingInterval.Day
            )
            .CreateLogger();


        var serviceProvider = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            })
            .AddSingleton<TaskService>()
            .BuildServiceProvider();

        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var taskService = serviceProvider.GetRequiredService<TaskService>();

        logger.LogInformation("Приложение запущено.");

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Показать все задачи");
            Console.WriteLine("3. Удалить задачу");
            Console.WriteLine("4. Отметить задачу как выполненную");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите действие: ");
            var input = Console.ReadLine();

            try
            {
                switch (input)
                {
                    case "1":
                        Console.Write("Введите название задачи: ");
                        var title = Console.ReadLine();
                        taskService.AddTask(title);
                        break;
                    case "2":
                        taskService.ListTasks();
                        break;
                    case "3":
                        Console.Write("Введите ID задачи для удаления: ");
                        int idToDelete = int.Parse(Console.ReadLine());
                        taskService.DeleteTask(idToDelete);
                        break;
                    case "4":
                        Console.Write("Введите ID задачи для отметки как выполненной: ");
                        int idToComplete = int.Parse(Console.ReadLine());
                        taskService.CompleteTask(idToComplete);
                        break;
                    case "5":
                        logger.LogCritical("Выход из приложения.");
                        Log.CloseAndFlush();
                        return;
                    default:
                        logger.LogWarning("Выбрана неверная опция.");
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Произошло исключение.");
            }
        }
    }
}
