using System.Collections.Generic;
using kt1;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace kt1
{
    public class TaskService
    {
        private readonly List<TaskItem> _tasks = new();
        private int _counter = 1;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }

        public void AddTask(string title)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogTrace("Начало операции: AddTask");

            if (string.IsNullOrWhiteSpace(title))
            {
                _logger.LogWarning("Попытка добавить пустую задачу.");
                Console.WriteLine("Ошибка: задача не может быть пустой.");
                _logger.LogTrace("Операция AddTask завершена с предупреждением. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
                return;
            }

            var task = new TaskItem { Id = _counter++, Title = title };
            _tasks.Add(task);
            _logger.LogInformation("Задача добавлена: {Title}", title);
            Console.WriteLine("Задача успешно добавлена.");

            stopwatch.Stop();
            _logger.LogTrace("Операция AddTask завершена успешно. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
        }



        public void ListTasks()
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogTrace("Начало операции: ListTasks");

            _logger.LogInformation("Вывод списка задач...");

            if (_tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                _logger.LogTrace("Операция ListTasks завершена: список пуст. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
                stopwatch.Stop();
                return;
            }

            foreach (var task in _tasks)
            {
                Console.WriteLine(task);
            }

            stopwatch.Stop();
            _logger.LogTrace("Операция ListTasks завершена успешно. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
        }


        public void DeleteTask(int id)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogTrace("Начало операции: DeleteTask");

            var task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                _logger.LogWarning("Задача с ID {Id} удалена", id);
                Console.WriteLine("Задача удалена.");
                _logger.LogTrace("Операция DeleteTask завершена успешно. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
            }
            else
            {
                _logger.LogError("Задача с ID {Id} не найдена для удаления", id);
                Console.WriteLine("Задача не найдена.");
                _logger.LogTrace("Операция DeleteTask завершена с ошибкой. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
            }

            stopwatch.Stop();
        }


        public void CompleteTask(int id)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogTrace("Начало операции: CompleteTask");

            var task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                _logger.LogInformation("Задача с ID {Id} отмечена как выполненная", id);
                Console.WriteLine("Задача отмечена как выполненная.");
                _logger.LogTrace("Операция CompleteTask завершена успешно. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
            }
            else
            {
                _logger.LogError("Задача с ID {Id} не найдена для выполнения", id);
                Console.WriteLine("Задача не найдена.");
                _logger.LogTrace("Операция CompleteTask завершена с ошибкой. Время: {Elapsed} мс", stopwatch.Elapsed.TotalMilliseconds);
            }

            stopwatch.Stop();
        }

    }
}
