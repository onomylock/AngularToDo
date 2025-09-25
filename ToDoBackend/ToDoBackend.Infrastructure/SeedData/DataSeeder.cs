using Common.Common.Enums;
using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.SeedData;

public static class DataSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        // Создаем пользователей
        var users = new List<User>
        {
            new()
            {
                Id = 1,
                Username = "john_doe",
                Password = "hashed_password_1",
                Email = "john.doe@example.com",
                FirstName = "John",
                LastName = "Doe",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 2,
                Username = "jane_smith",
                Password = "hashed_password_2",
                Email = "jane.smith@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 3,
                Username = "bob_wilson",
                Password = "hashed_password_3",
                Email = "bob.wilson@example.com",
                FirstName = "Bob",
                LastName = "Wilson",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }
        };

        // Создаем группы
        var groups = new List<ToDoItemGroup>
        {
            new()
            {
                Id = 1,
                Title = "Рабочие задачи",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 2,
                Title = "Личные задачи",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 3,
                Title = "Совместный проект",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }
        };

        // Создаем задачи
        var todoItems = new List<ToDoItem>
        {
            new()
            {
                Id = 1,
                Title = "Завершить отчет",
                Description = "Подготовить квартальный отчет по продажам",
                GroupId = 1,
                FromUserId = 1,
                ToUserId = 2,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                State = ToDoItemStateEnum.InProgress,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 2,
                Title = "Купить продукты",
                Description = "Молоко, хлеб, яйца, фрукты",
                GroupId = 2,
                FromUserId = 2,
                ToUserId = 2,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                State = ToDoItemStateEnum.InProgress,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 3,
                Title = "Разработать API",
                Description = "Создать REST API для модуля задач",
                GroupId = 3,
                FromUserId = 3,
                ToUserId = 1,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                State = ToDoItemStateEnum.Done,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new()
            {
                Id = 4,
                Title = "Провести встречу",
                Description = "Еженедельная планерка с командой",
                GroupId = 1,
                FromUserId = 2,
                ToUserId = 3,
                Deadline = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                State = ToDoItemStateEnum.InProgress,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }
        };

        // Создаем связи многие-ко-многим между User и ToDoItemGroup
        var userGroups = new[]
        {
            new { UsersId = 1, ToDoItemGroupsId = 1 },
            new { UsersId = 1, ToDoItemGroupsId = 3 },
            new { UsersId = 2, ToDoItemGroupsId = 1 },
            new { UsersId = 2, ToDoItemGroupsId = 2 },
            new { UsersId = 3, ToDoItemGroupsId = 3 },
            new { UsersId = 3, ToDoItemGroupsId = 1 }
        };
    }
}