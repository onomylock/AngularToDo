using Common.Common.Exceptions;

namespace ToDoBackend.Application.Exceptions;

public class EntityNotFoundException(string message) : LocalizedException(message);