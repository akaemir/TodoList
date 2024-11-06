using AutoMapper;
using Moq;
using NUnit.Framework;
using TodoList.DataAccess.Abstracts;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Models.Dtos.ToDos.Responses;
using TodoList.Models.Entities;
using TodoList.Service.Concretes;
using TodoList.Service.Rules;

namespace Tests.Services;

public class TodoServiceTest
{
    private TodoService _todoService;
    private Mock<IMapper> _mockMapper;
    private Mock<IToDoRepository> _toDoRepositoryMock;
    private Mock<TodoBusinessRules> _rulesMock;

    [SetUp]
    public void SetUp()
    {
        _toDoRepositoryMock = new Mock<IToDoRepository>();
        _mockMapper = new Mock<IMapper>();
        _rulesMock = new Mock<TodoBusinessRules>(_toDoRepositoryMock.Object);
        _todoService = new TodoService(_toDoRepositoryMock.Object, _mockMapper.Object, _rulesMock.Object);
    }

    [Test]
    public void GetAll_ReturnsSuccess()
    {
        // Arange
        List<Todo> todos = new List<Todo>();
        List<TodoResponseDto> responses = new();
        _toDoRepositoryMock.Setup(x => x.GetAll(null, true)).Returns(todos);
        _mockMapper.Setup(x => x.Map<List<TodoResponseDto>>(todos)).Returns(responses);

        // Act 

        var result = _todoService.GetAll();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(responses, result.Data);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(string.Empty, result.Message);
    }

    [Test]
    public void Add_ShouldAddTodoAndReturnSuccessResponse()
    {
        // Arrange
        var createRequest = new CreateTodoRequest("Test Title", "Test Description", DateTime.Now,
            DateTime.Now.AddDays(7), Priority.Normal, 1);
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Title = createRequest.Title,
            Description = createRequest.Description,
            UserId = "1259cd1b-8426-4111-a93b-f14df09b31fc",
            StartDate = createRequest.StartDate,
            EndDate = createRequest.EndDate,
            Priority = Priority.Normal,
            CategoryId = createRequest.CategoryId
        };
        var todoResponseDto = new TodoResponseDto
            { Id = todo.Id, Title = todo.Title, Description = todo.Description };

        _mockMapper.Setup(m => m.Map<Todo>(createRequest)).Returns(todo);
        _mockMapper.Setup(m => m.Map<TodoResponseDto>(todo)).Returns(todoResponseDto);
        _toDoRepositoryMock.Setup(r => r.Add(It.IsAny<Todo>())).Returns(todo);

        // Act
        var result = _todoService.Add(createRequest, "1259cd1b-8426-4111-a93b-f14df09b31fc").Result;

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Yapılacak iş Eklendi!", result.Message);
        Assert.AreEqual(todoResponseDto, result.Data);
    }

    [Test]
    public void GetById_ShouldReturnTodo_WhenTodoExists()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new Todo { Id = todoId, Title = "Test Todo", Description = "Test Description" };
        var todoResponseDto = new TodoResponseDto
            { Id = todo.Id, Title = todo.Title, Description = todo.Description };

        _toDoRepositoryMock.Setup(r => r.GetById(todoId)).Returns(todo);
        _mockMapper.Setup(m => m.Map<TodoResponseDto>(todo)).Returns(todoResponseDto);
        _rulesMock.Setup(r => r.TodoIsPresent(todoId));

        // Act
        var result = _todoService.GetById(todoId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(todoResponseDto, result.Data);
    }

    [Test]
    public void Update_ShouldUpdateTodoAndReturnSuccessResponse()
    {
        // Arrange
        var updateRequest = new UpdateTodoRequest(Guid.NewGuid(), "Updated Title", "Updated Description",
            DateTime.Now.AddDays(7));
        var todo = new Todo { Id = updateRequest.Id, Title = "Old Title", Description = "Old Description" };

        _toDoRepositoryMock.Setup(r => r.GetById(updateRequest.Id)).Returns(todo);
        _rulesMock.Setup(r => r.TodoIsPresent(updateRequest.Id));
        _toDoRepositoryMock.Setup(r => r.Update(It.IsAny<Todo>()));
        _mockMapper.Setup(m => m.Map<TodoResponseDto>(todo)).Returns(new TodoResponseDto
            { Id = todo.Id, Title = updateRequest.Title, Description = updateRequest.Description });

        // Act
        var result = _todoService.Update(updateRequest);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Yapılacak iş Güncellendi!", result.Message);
    }

    [Test]
    public void Delete_ShouldDeleteTodoAndReturnSuccessResponse()
    {
        // Arrange
        var todoId = Guid.NewGuid();
        var todo = new Todo { Id = todoId, Title = "Test Todo" };

        _toDoRepositoryMock.Setup(r => r.GetById(todoId)).Returns(todo);
        _toDoRepositoryMock.Setup(r => r.Remove(todo)).Returns(todo);
        _rulesMock.Setup(r => r.TodoIsPresent(todoId));

        // Act
        var result = _todoService.Delete(todoId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual($"Görev Başlığı : {todo.Title}", result.Data);
        Assert.AreEqual("Yapılacak iş Silindi!", result.Message);
    }

    [Test]
    public void GetAll_ShouldReturnAllTodos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = Guid.NewGuid(), Title = "Test 1", Description = "Description 1" },
            new Todo { Id = Guid.NewGuid(), Title = "Test 2", Description = "Description 2" }
        };

        var responseDtos = new List<TodoResponseDto>
        {
            new TodoResponseDto { Id = todos[0].Id, Title = todos[0].Title, Description = todos[0].Description },
            new TodoResponseDto { Id = todos[1].Id, Title = todos[1].Title, Description = todos[1].Description }
        };

        _toDoRepositoryMock.Setup(r => r.GetAll(null, true)).Returns(todos);
        _mockMapper.Setup(m => m.Map<List<TodoResponseDto>>(todos)).Returns(responseDtos);

        // Act
        var result = _todoService.GetAll();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(responseDtos, result.Data);
    }
}