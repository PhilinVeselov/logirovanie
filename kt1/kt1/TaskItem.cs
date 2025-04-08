namespace kt1;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        return $"{Id}: {Title} - {(IsCompleted ? "Выполнено" : "Ожидает")}";
    }
}
