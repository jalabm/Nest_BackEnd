using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest_6._03.Data;
using Nest_6._03.ViewModels;
using Newtonsoft.Json;

namespace P237_Nest.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public CartViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<BasketVm>? basketVm = GetBasket();
        List<BasketItemsVm> basketItemsVm = new List<BasketItemsVm>();
        foreach (var basketData in basketVm)
        {
            var product = await _context.products.Include(x => x.ProductImgs).FirstOrDefaultAsync(x => x.Id == basketData.Id);
            if(product is not null)                
            basketItemsVm.Add(new BasketItemsVm()
            {
                Count = basketData.Count,
                Id = basketData.Id,
                Image = product.ProductImgs.FirstOrDefault(x => x.IsMain)?.Url ?? "",
                Price = product.SellPrice,
                Name = product.Name,
            });
        }

        return View(basketItemsVm);
    }
    private List<BasketVm> GetBasket()
    {
        List<BasketVm> basketVms;
        if (Request.Cookies["basket"] != null)
        {
            basketVms = JsonConvert.DeserializeObject<List<BasketVm>>(Request.Cookies["basket"]);
        }
        else basketVms = new List<BasketVm>();
        return basketVms;
    }
}