using System.Collections.Generic;
using kt1;
using Microsoft.Extensions.Logging;

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
            _logger.LogDebug("Вход в метод AddTask.");

            if (string.IsNullOrWhiteSpace(title))
            {
                _logger.LogWarning("Попытка добавить пустую задачу.");
                Console.WriteLine("Ошибка: задача не может быть пустой.");
                return;
            }

            var task = new TaskItem { Id = _counter++, Title = title };
            _tasks.Add(task);
            _logger.LogInformation("Задача добавлена: {Title}", title);
            Console.WriteLine("Задача успешно добавлена.");
        }


        public void ListTasks()
        {
            _logger.LogInformation("Вывод списка задач...");
            if (_tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
                return;
            }

            foreach (var task in _tasks)
            {
                Console.WriteLine(task);
            }
        }

        public void DeleteTask(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
                _logger.LogWarning("Задача с ID {Id} удалена", id);
                Console.WriteLine("Задача удалена.");
            }
            else
            {
                _logger.LogError("Задача с ID {Id} не найдена для удаления", id);
                Console.WriteLine("Задача не найдена.");
            }
        }

        public void CompleteTask(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                _logger.LogInformation("Задача с ID {Id} отмечена как выполненная", id);
                Console.WriteLine("Задача отмечена как выполненная.");
            }
            else
            {
                _logger.LogError("Задача с ID {Id} не найдена для выполнения", id);
                Console.WriteLine("Задача не найдена.");
            }
        }
    }
}
