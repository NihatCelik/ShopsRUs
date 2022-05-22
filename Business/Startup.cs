using Autofac;
using Business.Adapters.PosPaymentService;
using Business.Constants;
using Business.DependencyResolvers;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using Core.Utilities.UrlConfiguration;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.MongoDb.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Business
{
    public partial class BusinessStartup
    {
        protected readonly IHostEnvironment HostEnvironment;
        public IConfiguration Configuration { get; }

        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
            {
                return sp.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));
            };

            services.AddScoped<IPrincipal>(getPrincipal);
            services.AddMemoryCache();

            services.AddDependencyResolvers(Configuration, new ICoreModule[]
            {
                new CoreModule()
            });

            services.AddSingleton<ConfigurationManager>();

            services.AddTransient<IPosPaymentService, PayTRManager>();

            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddAutoMapper(typeof(ConfigurationManager));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly);

            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()?.GetName();
            };

            ConfigureImagePath();
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyPaymentInvoiceRepository, CompanyPaymentInvoiceRepository>();
            services.AddTransient<ICompanyPaymentTransferRepository, CompanyPaymentTransferRepository>();
            services.AddTransient<ICompanyPaymentRepository, CompanyPaymentRepository>();
            services.AddTransient<ICompanyPaymentBankCardRepository, CompanyPaymentBankCardRepository>();
            services.AddTransient<ISupportRequestRepository, SupportRequestRepository>();
            services.AddTransient<ISupportAnswerRepository, SupportAnswerRepository>();
            services.AddTransient<IWithholdingRepository, WithholdingRepository>();
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IETransformationUserSerialRepository, ETransformationUserSerialRepository>();
            services.AddTransient<IETransformationUnitCodeRepository, ETransformationUnitCodeRepository>();
            services.AddTransient<IETransformationIntegratorRepository, ETransformationIntegratorRepository>();
            services.AddTransient<IETransformationGibUserRepository, ETransformationGibUserRepository>();
            services.AddTransient<IEInvoiceResponseLogRepository, EInvoiceResponseLogRepository>();
            services.AddTransient<IEInvoiceLogRepository, EInvoiceLogRepository>();
            services.AddTransient<IEInvoiceExceptionRepository, EInvoiceExceptionRepository>();
            services.AddTransient<ICustomerSpecialCodeRepository, CustomerSpecialCodeRepository>();
            services.AddTransient<ICustomerGroupRepository, CustomerGroupRepository>();
            services.AddTransient<ICustomerBranchPostalCodeRepository, CustomerBranchPostalCodeRepository>();
            services.AddTransient<ICustomerBranchRepository, CustomerBranchRepository>();
            services.AddTransient<ICustomerAuthorizedUserRepository, CustomerAuthorizedUserRepository>();
            services.AddTransient<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<ITrailerRepository, TrailerRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IExpenseCardRepository, ExpenseCardRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<ICompanyETransformationInfoRepository, CompanyETransformationInfoRepository>();
            services.AddTransient<ICompanyCurrencyRepository, CompanyCurrencyRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IBankRepository, BankRepository>();
            services.AddTransient<IEntityGroupRepository, EntityGroupRepository>();
            services.AddTransient<IEntityRepository, EntityRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICommerceWithholdingRepository, CommerceWithholdingRepository>();
            services.AddTransient<ICommerceTotalRepository, CommerceTotalRepository>();
            services.AddTransient<ICommerceProductRepository, CommerceProductRepository>();
            services.AddTransient<ICommerceNoteRepository, CommerceNoteRepository>();
            services.AddTransient<ICommerceETransformationStatusRepository, CommerceETransformationStatusRepository>();
            services.AddTransient<ICommerceDocumentRepository, CommerceDocumentRepository>();
            services.AddTransient<ICommerceDespatchTransporterRepository, CommerceDespatchTransporterRepository>();
            services.AddTransient<ICommerceDespatchTrailerRepository, CommerceDespatchTrailerRepository>();
            services.AddTransient<ICommerceDespatchResponseRepository, CommerceDespatchResponseRepository>();
            services.AddTransient<ICommerceDespatchPartyRepository, CommerceDespatchPartyRepository>();
            services.AddTransient<ICommerceDespatchDriverRepository, CommerceDespatchDriverRepository>();
            services.AddTransient<ICommerceDespatchAttachmentRepository, CommerceDespatchAttachmentRepository>();
            services.AddTransient<ICommerceRepository, CommerceRepository>();
            services.AddTransient<IVariantValueRepository, VariantValueRepository>();
            services.AddTransient<IVariantRepository, VariantRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<ITrademarkRepository, TrademarkRepository>();
            services.AddTransient<IProductVariantValueRepository, ProductVariantValueRepository>();
            services.AddTransient<IProductVariantRepository, ProductVariantRepository>();
            services.AddTransient<IProductUnitRepository, ProductUnitRepository>();
            services.AddTransient<IProductSpecialCodeRepository, ProductSpecialCodeRepository>();
            services.AddTransient<IProductPriceRepository, ProductPriceRepository>();
            services.AddTransient<IProductMovementRepository, ProductMovementRepository>();
            services.AddTransient<IProductFileRepository, ProductFileRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPriceListItemRepository, PriceListItemRepository>();
            services.AddTransient<IPriceListRepository, PriceListRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserSettingRepository, UserSettingRepository>();
            services.AddTransient<IUserLogRepository, UserLogRepository>();
            services.AddTransient<IUserPasswordRequestRepository, UserPasswordRequestRepository>();
            services.AddTransient<IEmployeeAccountRepository, EmployeeAccountRepository>();
            services.AddTransient<ICustomerAccountRepository, CustomerAccountRepository>();
            services.AddTransient<IChequeAndBondRepository, ChequeAndBondRepository>();
            services.AddTransient<IExpenseAccountRepository, ExpenseAccountRepository>();
            services.AddTransient<ICustomerMovementRepository, CustomerMovementRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext, PgDbContext>();
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyPaymentInvoiceRepository, CompanyPaymentInvoiceRepository>();
            services.AddTransient<ICompanyPaymentTransferRepository, CompanyPaymentTransferRepository>();
            services.AddTransient<ICompanyPaymentRepository, CompanyPaymentRepository>();
            services.AddTransient<ICompanyPaymentBankCardRepository, CompanyPaymentBankCardRepository>();
            services.AddTransient<ISupportRequestRepository, SupportRequestRepository>();
            services.AddTransient<ISupportAnswerRepository, SupportAnswerRepository>();
            services.AddTransient<IWithholdingRepository, WithholdingRepository>();
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IETransformationUserSerialRepository, ETransformationUserSerialRepository>();
            services.AddTransient<IETransformationUnitCodeRepository, ETransformationUnitCodeRepository>();
            services.AddTransient<IETransformationIntegratorRepository, ETransformationIntegratorRepository>();
            services.AddTransient<IETransformationGibUserRepository, ETransformationGibUserRepository>();
            services.AddTransient<IEInvoiceResponseLogRepository, EInvoiceResponseLogRepository>();
            services.AddTransient<IEInvoiceLogRepository, EInvoiceLogRepository>();
            services.AddTransient<IEInvoiceExceptionRepository, EInvoiceExceptionRepository>();
            services.AddTransient<ICustomerSpecialCodeRepository, CustomerSpecialCodeRepository>();
            services.AddTransient<ICustomerGroupRepository, CustomerGroupRepository>();
            services.AddTransient<ICustomerBranchPostalCodeRepository, CustomerBranchPostalCodeRepository>();
            services.AddTransient<ICustomerBranchRepository, CustomerBranchRepository>();
            services.AddTransient<ICustomerAuthorizedUserRepository, CustomerAuthorizedUserRepository>();
            services.AddTransient<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<ITrailerRepository, TrailerRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IExpenseCardRepository, ExpenseCardRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<ICompanyETransformationInfoRepository, CompanyETransformationInfoRepository>();
            services.AddTransient<ICompanyCurrencyRepository, CompanyCurrencyRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IBankRepository, BankRepository>();
            services.AddTransient<IEntityGroupRepository, EntityGroupRepository>();
            services.AddTransient<IEntityRepository, EntityRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICommerceWithholdingRepository, CommerceWithholdingRepository>();
            services.AddTransient<ICommerceTotalRepository, CommerceTotalRepository>();
            services.AddTransient<ICommerceProductRepository, CommerceProductRepository>();
            services.AddTransient<ICommerceNoteRepository, CommerceNoteRepository>();
            services.AddTransient<ICommerceETransformationStatusRepository, CommerceETransformationStatusRepository>();
            services.AddTransient<ICommerceDocumentRepository, CommerceDocumentRepository>();
            services.AddTransient<ICommerceDespatchTransporterRepository, CommerceDespatchTransporterRepository>();
            services.AddTransient<ICommerceDespatchTrailerRepository, CommerceDespatchTrailerRepository>();
            services.AddTransient<ICommerceDespatchResponseRepository, CommerceDespatchResponseRepository>();
            services.AddTransient<ICommerceDespatchPartyRepository, CommerceDespatchPartyRepository>();
            services.AddTransient<ICommerceDespatchDriverRepository, CommerceDespatchDriverRepository>();
            services.AddTransient<ICommerceDespatchAttachmentRepository, CommerceDespatchAttachmentRepository>();
            services.AddTransient<ICommerceRepository, CommerceRepository>();
            services.AddTransient<IVariantValueRepository, VariantValueRepository>();
            services.AddTransient<IVariantRepository, VariantRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<ITrademarkRepository, TrademarkRepository>();
            services.AddTransient<IProductVariantValueRepository, ProductVariantValueRepository>();
            services.AddTransient<IProductVariantRepository, ProductVariantRepository>();
            services.AddTransient<IProductUnitRepository, ProductUnitRepository>();
            services.AddTransient<IProductSpecialCodeRepository, ProductSpecialCodeRepository>();
            services.AddTransient<IProductPriceRepository, ProductPriceRepository>();
            services.AddTransient<IProductMovementRepository, ProductMovementRepository>();
            services.AddTransient<IProductFileRepository, ProductFileRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPriceListItemRepository, PriceListItemRepository>();
            services.AddTransient<IPriceListRepository, PriceListRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserSettingRepository, UserSettingRepository>();
            services.AddTransient<IUserLogRepository, UserLogRepository>();
            services.AddTransient<IUserPasswordRequestRepository, UserPasswordRequestRepository>();
            services.AddTransient<IEmployeeAccountRepository, EmployeeAccountRepository>();
            services.AddTransient<ICustomerAccountRepository, CustomerAccountRepository>();
            services.AddTransient<IChequeAndBondRepository, ChequeAndBondRepository>();
            services.AddTransient<IExpenseAccountRepository, ExpenseAccountRepository>();
            services.AddTransient<ICustomerMovementRepository, CustomerMovementRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext, PgDbContext>();
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyPaymentInvoiceRepository, CompanyPaymentInvoiceRepository>();
            services.AddTransient<ICompanyPaymentTransferRepository, CompanyPaymentTransferRepository>();
            services.AddTransient<ICompanyPaymentRepository, CompanyPaymentRepository>();
            services.AddTransient<ICompanyPaymentBankCardRepository, CompanyPaymentBankCardRepository>();
            services.AddTransient<ISupportRequestRepository, SupportRequestRepository>();
            services.AddTransient<ISupportAnswerRepository, SupportAnswerRepository>();
            services.AddTransient<IWithholdingRepository, WithholdingRepository>();
            services.AddTransient<IPartyRepository, PartyRepository>();
            services.AddTransient<IETransformationUserSerialRepository, ETransformationUserSerialRepository>();
            services.AddTransient<IETransformationUnitCodeRepository, ETransformationUnitCodeRepository>();
            services.AddTransient<IETransformationIntegratorRepository, ETransformationIntegratorRepository>();
            services.AddTransient<IETransformationGibUserRepository, ETransformationGibUserRepository>();
            services.AddTransient<IEInvoiceResponseLogRepository, EInvoiceResponseLogRepository>();
            services.AddTransient<IEInvoiceLogRepository, EInvoiceLogRepository>();
            services.AddTransient<IEInvoiceExceptionRepository, EInvoiceExceptionRepository>();
            services.AddTransient<ICustomerSpecialCodeRepository, CustomerSpecialCodeRepository>();
            services.AddTransient<ICustomerGroupRepository, CustomerGroupRepository>();
            services.AddTransient<ICustomerBranchPostalCodeRepository, CustomerBranchPostalCodeRepository>();
            services.AddTransient<ICustomerBranchRepository, CustomerBranchRepository>();
            services.AddTransient<ICustomerAuthorizedUserRepository, CustomerAuthorizedUserRepository>();
            services.AddTransient<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient<ITrailerRepository, TrailerRepository>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddTransient<IExpenseCardRepository, ExpenseCardRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDriverRepository, DriverRepository>();
            services.AddTransient<ICompanyETransformationInfoRepository, CompanyETransformationInfoRepository>();
            services.AddTransient<ICompanyCurrencyRepository, CompanyCurrencyRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IBankRepository, BankRepository>();
            services.AddTransient<IEntityGroupRepository, EntityGroupRepository>();
            services.AddTransient<IEntityRepository, EntityRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICommerceWithholdingRepository, CommerceWithholdingRepository>();
            services.AddTransient<ICommerceTotalRepository, CommerceTotalRepository>();
            services.AddTransient<ICommerceProductRepository, CommerceProductRepository>();
            services.AddTransient<ICommerceNoteRepository, CommerceNoteRepository>();
            services.AddTransient<ICommerceETransformationStatusRepository, CommerceETransformationStatusRepository>();
            services.AddTransient<ICommerceDocumentRepository, CommerceDocumentRepository>();
            services.AddTransient<ICommerceDespatchTransporterRepository, CommerceDespatchTransporterRepository>();
            services.AddTransient<ICommerceDespatchTrailerRepository, CommerceDespatchTrailerRepository>();
            services.AddTransient<ICommerceDespatchResponseRepository, CommerceDespatchResponseRepository>();
            services.AddTransient<ICommerceDespatchPartyRepository, CommerceDespatchPartyRepository>();
            services.AddTransient<ICommerceDespatchDriverRepository, CommerceDespatchDriverRepository>();
            services.AddTransient<ICommerceDespatchAttachmentRepository, CommerceDespatchAttachmentRepository>();
            services.AddTransient<ICommerceRepository, CommerceRepository>();
            services.AddTransient<IVariantValueRepository, VariantValueRepository>();
            services.AddTransient<IVariantRepository, VariantRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<ITrademarkRepository, TrademarkRepository>();
            services.AddTransient<IProductVariantValueRepository, ProductVariantValueRepository>();
            services.AddTransient<IProductVariantRepository, ProductVariantRepository>();
            services.AddTransient<IProductUnitRepository, ProductUnitRepository>();
            services.AddTransient<IProductSpecialCodeRepository, ProductSpecialCodeRepository>();
            services.AddTransient<IProductPriceRepository, ProductPriceRepository>();
            services.AddTransient<IProductMovementRepository, ProductMovementRepository>();
            services.AddTransient<IProductFileRepository, ProductFileRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IPriceListItemRepository, PriceListItemRepository>();
            services.AddTransient<IPriceListRepository, PriceListRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserSettingRepository, UserSettingRepository>();
            services.AddTransient<IUserLogRepository, UserLogRepository>();
            services.AddTransient<IUserPasswordRequestRepository, UserPasswordRequestRepository>();
            services.AddTransient<IEmployeeAccountRepository, EmployeeAccountRepository>();
            services.AddTransient<ICustomerAccountRepository, CustomerAccountRepository>();
            services.AddTransient<IChequeAndBondRepository, ChequeAndBondRepository>();
            services.AddTransient<IExpenseAccountRepository, ExpenseAccountRepository>();
            services.AddTransient<ICustomerMovementRepository, CustomerMovementRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITranslateRepository, TranslateRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserClaimRepository, UserClaimRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IGroupClaimRepository, GroupClaimRepository>();
            services.AddTransient<IUserGroupRepository, UserGroupRepository>();
            services.AddDbContext<ProjectDbContext, PgDbContext>();
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        }

        public void ConfigureImagePath()
        {
            var urlConfiguration = Configuration.GetSection("URLConfiguration").Get<UrlConfiguration>();
            Constants.ImagePathes.ImagePathes.ImagePath = urlConfiguration.ImageUrl;
        }
    }
}
