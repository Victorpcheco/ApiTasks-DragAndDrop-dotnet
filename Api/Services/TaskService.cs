using Api.Models;
using Api.Repository;
using Api.Validators;
using FluentValidation;

namespace Api.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IValidator<TaskModel> _validator;


    public TaskService(ITaskRepository repository, IValidator<TaskModel> validator)
    {
        _repository = repository;
        _validator = validator;
    }


    public async Task<TaskModel> CreateTaskAsync(TaskModel task)
    {
        var validationResult = _validator.Validate(task);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors); 
        }
        return await _repository.AddTaskAsync(task);
    }

    public async Task<TaskModel?> UpdateTaskAsync(TaskModel task)
    {
        var validator = new TaskModelValidator();
        var validationResult = validator.Validate(task);
    
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await _repository.UpdateTaskAsync(task);
    }

    public async Task<IEnumerable<TaskModel>> GetAllTaskAsync()
    {
        return await _repository.GetAllTaskAsync();
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _repository.GetTaskByIdAsync(id);
        if (task == null)
        {
            return false;
        }
        await _repository.DeleteTaskAsync(task);
        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, string status)
    {
        var task = await _repository.GetTaskByIdAsync(id);
        if (task == null)
            return false; 
        
        task.Status = status;
        await _repository.UpdateTaskAsync(task);
        return true;
    }
    
}