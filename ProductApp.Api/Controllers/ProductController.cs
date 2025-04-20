using Microsoft.AspNetCore.Mvc;
using ProductApp.Api.Extensions;
using ProductApp.Domain.Behavior.Service;
using ProductApp.Domain.Commands;
using ProductApp.Domain.Core.ResultPattern;
using ProductApp.Domain.Queries;
using System.Net;

namespace ProductApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IRegisterProduct _registerProductManager;
        private readonly IGetProduct _getProductManager;

        public ProductController(ILogger<ProductController> logger,
                                 IRegisterProduct registerProductManager,
                                 IGetProduct getProductManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _getProductManager = getProductManager ?? throw new ArgumentNullException(nameof(getProductManager));
            _registerProductManager = registerProductManager ?? throw new ArgumentNullException(nameof(registerProductManager));
        }


        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(GetProductResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> Get(string productId, CancellationToken cancellationToken = default)
        {
            var response = await _getProductManager.GetProductAsync(productId, cancellationToken);

            if (response.IsFailed)
                return this.ApiResult(HttpStatusCode.InternalServerError, response.Errors);

            return (bool)response.ResponseModel?.Id.IsEmpty()
                ? this.ApiResult(HttpStatusCode.NotFound, "No data was found!")
                : this.ApiResult(HttpStatusCode.OK, response.ResponseModel);
        }


        [HttpPost]
        [ProducesResponseType(typeof(RegisterProductResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> Post([FromBody] RegisterProductRequest registerProductRequest, CancellationToken cancellationToken = default)
        {
            var result = await _registerProductManager.RegisterProductAsync(registerProductRequest, cancellationToken);

            if (result.IsFailed)
                return this.ApiResult(HttpStatusCode.InternalServerError, result.Errors);

            if (!result.ResponseModel.Success)
            {
                return this.ApiResult(HttpStatusCode.InternalServerError, result.ResponseModel.Message);
            }

            return this.ApiResult(HttpStatusCode.OK, result.ResponseModel.Message);
        }
    }
}
