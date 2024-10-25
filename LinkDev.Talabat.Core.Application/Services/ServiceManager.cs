﻿using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Employees;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Employees;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;
        public IProductService ProductService => _productService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, Func<IBasketService> basketServiceFactory, Func<IAuthService> authServiceFactoru)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _productService = new Lazy<IProductService>(() => new ProductServices(_unitOfWork, _mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
            _authService = new Lazy<IAuthService>(authServiceFactoru);
        }
    }
}
