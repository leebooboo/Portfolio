using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Portfolio.Services.Interfaces;
using Portfolio.Services.Services;
using Portfolio.Entities;
using Ninject.Web.Common;

namespace AspNet.BoardGameMall.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {   
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<ICheckoutService>().To<CheckoutService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IInquiryService>().To<InquiryService>();
            kernel.Bind<IReviewService>().To<ReviewService>();
            kernel.Bind<PortfolioContext>().ToSelf().InRequestScope();
        }
    }
}