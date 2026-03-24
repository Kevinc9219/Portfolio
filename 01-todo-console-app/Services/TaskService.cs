using Newtonsoft.Json;
using TodoConsoleApp.Models;

namespace TodoConsoleApp.Services;

public class TaskService
{
    private readonly string _filePath = Path.Combine("Data", "tasks.json");
    private List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskService()
    {
        Load();
    }

    public IReadOnlyList<TaskItem> GetAll() => _tasks.AsReadOnly();

    public int OpenCount => _tasks.Count(t => !t.IsCompleted);
    public int CompletedCount => _tasks.Count(t => t.IsCompleted);

    public void Add(string title, string? description)
    {
        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };
        _tasks.Add(task);
        Save();
    }

    public bool Complete(int index)
    {
        var task = GetByIndex(index);
        if (task == null) return false;
        task.IsCompleted = true;
        Save();
        return true;
    }

    public bool Delete(int index)
    {
        var task = GetByIndex(index);
        if (task == null) return false;
        _tasks.Remove(task);
        Save();
        return true;
    }

    public List<TaskItem> Search(string keyword) =>
        _tasks.Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

    private TaskItem? GetByIndex(int index)
    {
        if (index < 1 || index > _tasks.Count) return null;
        return _tasks[index - 1];
    }

    private void Load()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                _tasks = new List<TaskItem>();
                return;
            }
            var json = File.ReadAllText(_filePath);
            _tasks = JsonConvert.DeserializeObject<List<TaskItem>>(json) ?? new List<TaskItem>();
            _nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fout bij laden: {ex.Message}");
            _tasks = new List<TaskItem>();
        }
    }

    private void Save()
    {
        try
        {
            Directory.CreateDirectory("Data");
            var json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fout bij opslaan: {ex.Message}");
        }
    }
}
