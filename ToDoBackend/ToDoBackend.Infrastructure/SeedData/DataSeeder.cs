using Common.Common.Enums;
using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.SeedData;

internal static class DataSeeder
{
    public static List<User> SeedUsers()
    {
        // Создаем пользователей
        return
        [
            new User
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

            new User
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

            new User
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
        ];
    }

    public static List<ToDoItem> SeedToDoItems()
    {
        // Создаем задачи
        return
        [
            new ToDoItem
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

            new ToDoItem
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

            new ToDoItem
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

            new ToDoItem
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
        ];
    }

    public static List<ToDoItemGroup> SeedToDoItemGroups()
    {
        // Создаем группы
        return
        [
            new ToDoItemGroup
            {
                Id = 1,
                Title = "Рабочие задачи",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },

            new ToDoItemGroup
            {
                Id = 2,
                Title = "Личные задачи",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },

            new ToDoItemGroup
            {
                Id = 3,
                Title = "Совместный проект",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }
        ];
    }

    public static List<UserToToDoItemGroupMapping> SeedUserToToDoItemGroupMappings()
    {
        // Создаем связи многие-ко-многим между User и ToDoItemGroup
        return
        [
            new UserToToDoItemGroupMapping
            {
                Id = 1, EntityLeftId = 1, EntityRightId = 1, CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new UserToToDoItemGroupMapping
            {
                Id = 2, EntityLeftId = 1, EntityRightId = 3, CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new UserToToDoItemGroupMapping
            {
                Id = 3, EntityLeftId = 2, EntityRightId = 1, CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new UserToToDoItemGroupMapping
            {
                Id = 4, EntityLeftId = 2, EntityRightId = 2, CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new UserToToDoItemGroupMapping
            {
                Id = 5, EntityLeftId = 3, EntityRightId = 3, CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            },
            new UserToToDoItemGroupMapping
            {
                Id = 6, EntityLeftId = 3, EntityRightId = 1, CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            }
        ];
    }
}