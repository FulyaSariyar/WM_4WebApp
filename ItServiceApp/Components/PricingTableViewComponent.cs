﻿using AutoMapper;
using ItServiceApp.Data;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ItServiceApp.Components
{
    public class PricingTableViewComponent: ViewComponent
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;

        public PricingTableViewComponent(MyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var data = _dbContext.SubscriptionTypes
       .ToList()
       .Select(x => _mapper.Map<SubscriptionsViewModel>(x))
       .OrderBy(x => x.Price)
       .ToList();

            return View(data);
        }
    }
}
