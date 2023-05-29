using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly IAuctionVotesService _voteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(IProductsService service, IAuctionVotesService voteService, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _voteService = voteService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync();
            return View(allMovies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allProducts.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allProducts);
        }

        //GET: Products/Details/1
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetByIdAsync(id);
            var auctionVotes = _voteService.GetAllVotes(x => x.ProductId == id);
            var result = new ProductAndVoteVM()
            {
                Product = productDetail,
                AuctionVote = auctionVotes
            };
            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> Details(ProductAndVoteVM productAndVoteVM)
        {
            var auction = productAndVoteVM.AuctionVoteRegister;
            auction.ProductId = productAndVoteVM.Product.Id;
            auction.FullName = _userManager.GetUserName(User);
            await _voteService.AddAsync(auction);
            productAndVoteVM.Product.Price = productAndVoteVM.Product.Price + auction.Price;
            await _service.UpdateProdcutPriceAsync(
                productAndVoteVM.Product.Id, productAndVoteVM.Product);

            return RedirectToAction();
        }

        //GET: Products/Create
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }


        //GET: Products/Edit/1
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageURL,StartDate,EndDate")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }
    }
}