using Drinks.Core.Interfaces;
using Drinks.Core.Models;
using MediatR;
using OnlineShop.DTOs.Actions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drinks.Core.Services
{
    public class DrinksService : IDrinksService
    {
        private readonly IMediator _mediator;

        public DrinksService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<DrinkModel>> GetDrinksAsync()
        {
            List<DrinkModel> items = new List<DrinkModel>
            {
                new DrinkModel
                {
                    Name = "CocaCola",
                    Price = 1
                },
                new DrinkModel
                {
                    Name = "Fanta",
                    Price = 2
                }
            };

            return items;
        }

        public string BuyDrink(string drinkName)
        {
            string text = $"You have bought a {drinkName}";

            _mediator.Publish(new SendEmailCommand(text));

            return text;
        }
    }
}
