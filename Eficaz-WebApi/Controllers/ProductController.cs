using Core.DTOs;
using Core.Models;
using Core.Services;
using Infrastructure.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;
        private readonly EficazDbContext _context;

        public ProductController(IProductService productService, IAuthService authService, EficazDbContext context)
        {
            _productService = productService;
            _authService = authService;
            _context = context;
        }

        [HttpGet("{id}")]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult<Product>> GetAllProductsById(string id)
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return BadRequest("Não foi possível encontrar o produto do Id pesquisado");
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            List<Product> products = await _productService.GetAllProductsAsync();
            
            if (products == null || !products.Any()) 
            {
                return NotFound();
            } 
            else 
            {
                return Ok(products);
            }
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO), "Produto não pode ser nulo.");
            };

            try
            {
                Product product = await _productService.AddProduct(productDTO);

                return CreatedAtAction(nameof(GetAllProductsById), new { id = product.Id }, product);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao salvar os dados: {ex.Message}");
            }
        }

        [HttpPost("{ProductId}/UploadImage")]
        public async Task<ActionResult<string>> UploadImage(string ProductId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No image found");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileData = new FileData
            {
                FileName = file.FileName,
                Content = memoryStream.ToArray(),
                ContentType = file.ContentType,
                Extension = Path.GetExtension(file.FileName),
            };

            try
            {
                string imageUrl = await _productService.UploadProductImage(ProductId, fileData);
                return CreatedAtAction(nameof(UploadImage), imageUrl);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); 
            }
        }

        [HttpPut("{id}")]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult<Product>> UpdateProduct(string id, ProductDTO productDTO)
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO), "Produto não pode ser nulo.");
            }

            try 
            {
                Product updateProduct = await _productService.UpdateProduct(id, productDTO);
                
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar os dados: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var userId = _authService.GetAuthenticatedUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            
            try
            {
                bool deletedProduct = await _productService.DeleteProduct(id);

                if (!deletedProduct)
                {
                    return NotFound("Produto não encontrado");
                } 
                else 
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao deletar o usuário: {ex.Message}");     
            }
        }
    }
}
