using Electro_goods_API.Controllers;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Tests.Controller
{
    public class CategoriesControllerTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private readonly CategoriesController _categoriesController;
        public CategoriesControllerTests()
        {
            _categoryRepository = A.Fake<ICategoryRepository>();
            _mapper = A.Fake<IMapper>();

            _httpContext = new DefaultHttpContext();
            _httpContext.Request.Headers["Accept-Language"] = "ru";

            _categoriesController = new CategoriesController(_categoryRepository, _mapper)
            {
                ControllerContext = new ControllerContext() { HttpContext = _httpContext },
            };
        }

        [Fact]
        public async void CategoriesController_GetCategories_ReturnOK()
        {
            //Arrange
            var categories = A.Fake<List<Category>>();
            var categoryDtos = A.Fake<List<CategoryDTO>>();

            A.CallTo(() => _mapper.GetLanguageFromHeaders(A<IHeaderDictionary>._)).Returns("ru");
            A.CallTo(() => _mapper.MapCategoryToCategoryDTO(categories, "ru")).Returns(categoryDtos);
            A.CallTo(() => _categoryRepository.GetAllCategories()).Returns(categories);

            //Act
            var result = await _categoriesController.GetCategories();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<CategoryDTO>>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(categoryDtos);
        }

        [Fact]
        public async void CategoriesController_GetCategoryById_ReturnOk()
        {
            //Arrange
            var category = A.Fake<Category>();
            var categoryDto = A.Fake<CategoryDTO>();

            A.CallTo(() => _categoryRepository.GetCategoryById(1));
            A.CallTo(() => _mapper.GetLanguageFromHeaders(A<IHeaderDictionary>._)).Returns("ru");
            A.CallTo(() => _mapper.MapCategoryToCategoryDTO(category, "ru")).Returns(categoryDto);

            //Act
            var result = await _categoriesController.GetCategoryById(1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<CategoryDTO>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(categoryDto);
        }
    }
}
