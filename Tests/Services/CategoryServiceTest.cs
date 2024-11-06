using AutoMapper;
using Moq;
using NUnit.Framework;
using TodoList.DataAccess.Abstracts;
using TodoList.Models.Dtos.Categories.Requests;
using TodoList.Models.Dtos.Categories.Responses;
using TodoList.Models.Entities;
using TodoList.Service.Abstracts;
using TodoList.Service.Concretes;
using TodoList.Service.Constants;
using TodoList.Service.Rules;

namespace Tests.Services;

public class CategoryServiceTest
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _mockCategoryRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<CategoryBusinessRules> _mockCategoryBusinessRules;
        private CategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            // Initialize mocks for dependencies
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockCategoryBusinessRules = new Mock<CategoryBusinessRules>(_mockCategoryRepository.Object);
            _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object, _mockCategoryBusinessRules.Object);

        }

        [Test]
        public async Task Add_ShouldReturnSuccess_WhenCategoryIsAdded()
        {
            // Arrange
            var createCategoryRequest = new CreateCategoryRequest("Test Category");
            var category = new Category { Id = 1, Name = "Test Category" };
            var categoryResponseDto = new CategoryResponseDto(1, "Test Category");

            _mockMapper.Setup(m => m.Map<Category>(It.IsAny<CreateCategoryRequest>())).Returns(category);
            _mockMapper.Setup(m => m.Map<CategoryResponseDto>(It.IsAny<Category>())).Returns(categoryResponseDto);
            _mockCategoryRepository.Setup(repo => repo.Add(It.IsAny<Category>())).Returns(category);

            // Act
            var result = await _categoryService.Add(createCategoryRequest);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Test Category", result.Data.Name);
            Assert.AreEqual(Messages.CategoryAddedMessage, result.Message);
        }

        [Test]
        public void GetAll_ShouldReturnCategoryList_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };
            var categoryResponseDtos = new List<CategoryResponseDto>
            {
                new CategoryResponseDto(1, "Category 1"),
                new CategoryResponseDto(2, "Category 2")
            };

            _mockCategoryRepository.Setup(repo => repo.GetAll(null,true)).Returns(categories);
            _mockMapper.Setup(m => m.Map<List<CategoryResponseDto>>(It.IsAny<List<Category>>()))
                .Returns(categoryResponseDtos);

            // Act
            var result = _categoryService.GetAll();

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.Count);
        }
        [Test]
        public void GetById_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            var categoryResponseDto = new CategoryResponseDto (1, "Category 1");

            _mockCategoryBusinessRules.Setup(br => br.CategoryIsPresent(1));
            _mockCategoryRepository.Setup(repo => repo.GetById(1)).Returns(category);
            _mockMapper.Setup(m => m.Map<CategoryResponseDto>(It.IsAny<Category>())).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.GetById(1);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Category 1", result.Data.Name);
        }
        [Test]
        public void Update_ShouldReturnUpdatedCategory_WhenCategoryIsUpdated()
        {
            // Arrange
            var updateCategoryRequest = new UpdateCategoryRequest (1,"Updated Category");
            var category = new Category { Id = 1, Name = "Updated Category" };
            var updatedCategory = new Category { Id = 1, Name = "Updated Category" };
            var categoryResponseDto = new CategoryResponseDto (1, "Updated Category");

            _mockCategoryBusinessRules.Setup(br => br.CategoryIsPresent(1));
            _mockCategoryRepository.Setup(repo => repo.GetById(1)).Returns(category);
            _mockCategoryRepository.Setup(repo => repo.Update(It.IsAny<Category>())).Returns(updatedCategory);
            _mockMapper.Setup(m => m.Map<CategoryResponseDto>(It.IsAny<Category>())).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.Update(updateCategoryRequest);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Updated Category", result.Data.Name);
            Assert.AreEqual(Messages.CategoryUpdatedMessage, result.Message);
        }
        [Test]
        public void Remove_ShouldReturnDeletedCategory_WhenCategoryIsRemoved()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category 1" };
            var categoryResponseDto = new CategoryResponseDto (1, "Category 1");

            _mockCategoryBusinessRules.Setup(br => br.CategoryIsPresent(1));
            _mockCategoryRepository.Setup(repo => repo.GetById(1)).Returns(category);
            _mockCategoryRepository.Setup(repo => repo.Remove(It.IsAny<Category>())).Returns(category);
            _mockMapper.Setup(m => m.Map<CategoryResponseDto>(It.IsAny<Category>())).Returns(categoryResponseDto);

            // Act
            var result = _categoryService.Remove(1);

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Category 1", result.Data.Name);
            Assert.AreEqual(Messages.CategoryDeletedMessage, result.Message);
        }
    }
}