using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class UnitOfWork
    {
        private InventoryContext _db;

        public UnitOfWork()
        {
            this._db = new InventoryContext();
        }


        public void Save() => _db.SaveChanges();



        public IRepository<MENUMAIN> MENUMAINRepo;
        public IRepository<MENUMAIN> MENUMAINRepository
        {
            get
            {
                if(this.MENUMAINRepo == null)
                {
                    this.MENUMAINRepo = new Repository<MENUMAIN>(_db);

                }
                return MENUMAINRepo;
            }
        }

        public IRepository<MENUSUB> MENUSUBRepo;
        public IRepository<MENUSUB> MENUSUBRepository
        {
            get
            {

                if (this.MENUSUBRepo == null)
                {

                    this.MENUSUBRepo = new Repository<MENUSUB>(_db);
                }
                return MENUSUBRepo;
            }

        }



        public IRepository<MENUSUBSUB> MENUSUBSUBRepo;

        public IRepository<MENUSUBSUB> MENUSUBSUBRepository
        {
            get
            {
                if (this.MENUSUBSUBRepo == null)
                {
                    this.MENUSUBSUBRepo = new Repository<MENUSUBSUB>(_db);
                }
                return MENUSUBSUBRepo;
            }
        }




        public IRepository<ProcurementType> procurementTypeRepo;
        public IRepository<ProcurementType> ProcurementTypeRepository
        {
            get
            {
                if (this.procurementTypeRepo == null)
                {
                    this.procurementTypeRepo = new Repository<ProcurementType>(_db);

                }
                return procurementTypeRepo;
            }
        }

        public IRepository<Gender> GenderRepo;
        public IRepository<Gender> GenderRepository
        {
            get
            {

                if (this.GenderRepo == null)
                {

                    this.GenderRepo = new Repository<Gender>(_db);
                }
                return GenderRepo;
            }

        }



        public IRepository<Country> CountryRepo;

        public IRepository<Country> CountryRepository
        {
            get
            {
                if (this.CountryRepo == null)
                {
                    this.CountryRepo = new Repository<Country>(_db);
                }
                return CountryRepo;
            }
        }


        public IRepository<Principle> PrincipleRepo;

        public IRepository<Principle> PrincipleRepository
        {
            get
            {
                if (this.PrincipleRepo == null)
                {
                    this.PrincipleRepo = new Repository<Principle>(_db);
                }
                return PrincipleRepo;
            }
        }






        public IRepository<Province> ProvinceRepo;

        public IRepository<Province> ProvinceRepository
        {
            get
            {
                if (this.ProvinceRepo == null)
                {
                    this.ProvinceRepo = new Repository<Province>(_db);
                }
                return ProvinceRepo;
            }
        }




        public IRepository<State> StateRepo;

        public IRepository<State> StateRepository
        {
            get
            {
                if (this.StateRepo == null)
                {
                    this.StateRepo = new Repository<State>(_db);
                }
                return StateRepo;
            }
        }


        public IRepository<District> DistrictReo;

        public IRepository<District> DistrictRepository
        {
            get
            {
                if (this.DistrictReo == null)
                {
                    this.DistrictReo = new Repository<District>(_db);
                }
                return DistrictReo;
            }
        }


        public IRepository<Company> CompanyRepo;

        public IRepository<Company> ReligionServices
        {
            get
            {
                if (this.CompanyRepo == null)
                {
                    this.CompanyRepo = new Repository<Company>(_db);
                }
                return CompanyRepo;
            }
        }


        public IRepository<Division> DivisionRepo;

        public IRepository<Division> DivisionRepository
        {
            get
            {
                if (this.DivisionRepo == null)
                {
                    this.DivisionRepo = new Repository<Division>(_db);
                }
                return DivisionRepo;
            }
        }


        public IRepository<Nationality> NationalityRepo;

        public IRepository<Nationality> NationalityRepositoy
        {
            get
            {
                if (this.NationalityRepo == null)
                {
                    this.NationalityRepo = new Repository<Nationality>(_db);
                }
                return NationalityRepo;
            }
        }




        public IRepository<Religion> ReligionRepo;

        public IRepository<Religion> ReligionRepository
        {
            get
            {
                if (this.ReligionRepo == null)
                {
                    this.ReligionRepo = new Repository<Religion>(_db);
                }
                return ReligionRepo;
            }
        }



        public IRepository<Category> CategoryRepo;

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (this.CategoryRepo == null)
                {
                    this.CategoryRepo = new Repository<Category>(_db);
                }
                return CategoryRepo;
            }
        }



        public IRepository<SubCategory> SubCategoryRepo;

        public IRepository<SubCategory> SubCategoryRepository
        {
            get
            {
                if (this.SubCategoryRepo == null)
                {
                    this.SubCategoryRepo = new Repository<SubCategory>(_db);
                }
                return SubCategoryRepo;
            }
        }



        public IRepository<SubSubCategory> SubSubCategoryRepo;

        public IRepository<SubSubCategory> SubSubCategoryRepository
        {
            get
            {
                if (this.SubSubCategoryRepo == null)
                {
                    this.SubSubCategoryRepo = new Repository<SubSubCategory>(_db);
                }
                return SubSubCategoryRepo;
            }
        }



        public IRepository<SubSubSubCategory> SubSubSubCategoryRepo;

        public IRepository<SubSubSubCategory> SubSubSubCategoryRepository
        {
            get
            {
                if (this.SubSubSubCategoryRepo == null)
                {
                    this.SubSubSubCategoryRepo = new Repository<SubSubSubCategory>(_db);
                }
                return SubSubSubCategoryRepo;
            }
        }


        public IRepository<SubSubSubSubCategory> SubSubSubSubCategoryRepo;

        public IRepository<SubSubSubSubCategory> SubSubSubSubCategoryRepository
        {
            get
            {
                if (this.SubSubSubSubCategoryRepo == null)
                {
                    this.SubSubSubSubCategoryRepo = new Repository<SubSubSubSubCategory>(_db);
                }
                return SubSubSubSubCategoryRepo;
            }
        }







        public IRepository<Festival> FestivalRepo;

        public IRepository<Festival> FestivalRepository
        {
            get
            {
                if (this.FestivalRepo == null)
                {
                    this.FestivalRepo = new Repository<Festival>(_db);
                }
                return FestivalRepo;
            }
        }





        public IRepository<Brand> brandRepo;
        public IRepository<Brand> BrandRepository
        {
            get
            {
                if (this.brandRepo == null)
                {
                    this.brandRepo = new Repository<Brand>(_db);

                }
                return brandRepo;
            }
        }


        public IRepository<Model> modelRepo;
        public IRepository<Model> ModelRepository
        {
            get
            {
                if (this.modelRepo == null)
                {
                    this.modelRepo = new Repository<Model>(_db);

                }
                return modelRepo;
            }
        }



        public IRepository<ConditionOfItem> ConditionOfItemRepo;
        public IRepository<ConditionOfItem> ConditionOfItemRepository
        {
            get
            {
                if (this.ConditionOfItemRepo == null)
                {
                    this.ConditionOfItemRepo = new Repository<ConditionOfItem>(_db);

                }
                return ConditionOfItemRepo;
            }
        }


        public IRepository<MaritalStatus> MaritalStatusRepo;
        public IRepository<MaritalStatus> MaritalStatusRepository
        {
            get
            {
                if (this.MaritalStatusRepo == null)
                {
                    this.MaritalStatusRepo = new Repository<MaritalStatus>(_db);

                }
                return MaritalStatusRepo;
            }
        }


        public IRepository<SafetyStock> SafetyStockRepo;
        public IRepository<SafetyStock> SafetyStockRepository
        {
            get
            {
                if (this.SafetyStockRepo == null)
                {
                    this.SafetyStockRepo = new Repository<SafetyStock>(_db);

                }
                return SafetyStockRepo;
            }
        }



        public IRepository<Region> RegionRepo;
        public IRepository<Region> RegionRepository
        {
            get
            {
                if (this.RegionRepo == null)
                {
                    this.RegionRepo = new Repository<Region>(_db);

                }
                return RegionRepo;
            }
        }


        public IRepository<Celebration> CelebrationRepo;
        public IRepository<Celebration> CelebrationRepository
        {
            get
            {
                if (this.CelebrationRepo == null)
                {
                    this.CelebrationRepo = new Repository<Celebration>(_db);

                }
                return CelebrationRepo;
            }
        }




        public IRepository<Unit> unitRepo;
        public IRepository<Unit> UnitRepository
        {
            get
            {
                if (this.unitRepo == null)
                {
                    this.unitRepo = new Repository<Unit>(_db);

                }
                return unitRepo;
            }
        }

        public IRepository<Upazila> upazilaRepo;
        public IRepository<Upazila> UpazilaRepository
        {
            get
            {
                if (this.upazilaRepo == null)
                {
                    this.upazilaRepo = new Repository<Upazila>(_db);

                }
                return upazilaRepo;
            }
        }
        public IRepository<Warranty> warrantyRepo;
        public IRepository<Warranty> WarrantyRepository
        {
            get
            {
                if (this.warrantyRepo == null)
                {
                    this.warrantyRepo = new Repository<Warranty>(_db);

                }
                return warrantyRepo;
            }
        }

        public IRepository<Weather> weatherRepo;
        public IRepository<Weather> WeatherRepository
        {
            get
            {
                if (this.weatherRepo == null)
                {
                    this.weatherRepo = new Repository<Weather>(_db);

                }
                return weatherRepo;
            }
        }

        public IRepository<Hobby> hobbyRepo;
        public IRepository<Hobby> HobbyRepository
        {
            get
            {
                if (this.hobbyRepo == null)
                {
                    this.hobbyRepo = new Repository<Hobby>(_db);

                }
                return hobbyRepo;
            }
        }

        public IRepository<Season> seasonRepo;
        public IRepository<Season> SeasonRepository
        {
            get
            {
                if (this.seasonRepo == null)
                {
                    this.seasonRepo = new Repository<Season>(_db);

                }
                return seasonRepo;
            }
        }

      

        public IRepository<Store> storeRepo;
        public IRepository<Store> StoreRepository
        {
            get
            {
                if (this.storeRepo == null)
                {
                    this.storeRepo = new Repository<Store>(_db);

                }
                return storeRepo;
            }
        }

        public IRepository<SubStore> subStoreRepo;
        public IRepository<SubStore> SubStoreRepository
        {
            get
            {
                if (this.subStoreRepo == null)
                {
                    this.subStoreRepo = new Repository<SubStore>(_db);

                }
                return subStoreRepo;
            }
        }

        public IRepository<SubSubStore> subSubStoreRepo;
        public IRepository<SubSubStore> SubSubStoreRepository
        {
            get
            {
                if (this.subSubStoreRepo == null)
                {
                    this.subSubStoreRepo = new Repository<SubSubStore>(_db);

                }
                return subSubStoreRepo;
            }
        }

        public IRepository<SubSubSubStore> subSubSubStoreRepo;
        public IRepository<SubSubSubStore> SubSubSubStoreRepository
        {
            get
            {
                if (this.subSubSubStoreRepo == null)
                {
                    this.subSubSubStoreRepo = new Repository<SubSubSubStore>(_db);

                }
                return subSubSubStoreRepo;
            }
        }

        public IRepository<SubSubSubSubStore> subSubSubSubStoreRepo;
        public IRepository<SubSubSubSubStore> SubSubSubSubStoreRepository
        {
            get
            {
                if (this.subSubSubSubStoreRepo == null)
                {
                    this.subSubSubSubStoreRepo = new Repository<SubSubSubSubStore>(_db);

                }
                return subSubSubSubStoreRepo;
            }
        }

        public IRepository<SupplierCompany> supplierCompanyRepo;
        public IRepository<SupplierCompany> SupplierCompanyRepository
        {
            get
            {
                if (this.supplierCompanyRepo == null)
                {
                    this.supplierCompanyRepo = new Repository<SupplierCompany>(_db);

                }
                return supplierCompanyRepo;
            }
        }

        public IRepository<SubContractCompany> subContractCompanyRepo;
        public IRepository<SubContractCompany> SubContractCompanyRepository
        {
            get
            {
                if (this.subContractCompanyRepo == null)
                {
                    this.subContractCompanyRepo = new Repository<SubContractCompany>(_db);

                }
                return subContractCompanyRepo;
            }
        }

        public IRepository<PhoneProvider> phoneProviderrepo;
        public IRepository<PhoneProvider> PhoneProviderRepositoy
        {
            get
            {
                if (this.phoneProviderrepo == null)
                {
                    this.phoneProviderrepo = new Repository<PhoneProvider>(_db);

                }
                return phoneProviderrepo;
            }
        }

        public IRepository<EmailProvider> emailProviderRepo;
        public IRepository<EmailProvider> EmailProviderRepository
        {
            get
            {
                if (this.emailProviderRepo == null)
                {
                    this.emailProviderRepo = new Repository<EmailProvider>(_db);

                }
                return emailProviderRepo;
            }

        }

        public IRepository<EducationQualification> educationQualification;
        public IRepository<EducationQualification> EducationQualificationRepository
        {
            get
            {
                if (this.educationQualification == null)
                {
                    this.educationQualification = new Repository<EducationQualification>(_db);
                }
                return educationQualification;
            }

        }

        public IRepository<BloodGroup> bloodGroupRepo;
        public IRepository<BloodGroup> BloodGroupRepository
        {
            get
            {
                if (this.bloodGroupRepo == null)
                {
                    this.bloodGroupRepo = new Repository<BloodGroup>(_db);
                }
                return bloodGroupRepo;
            }
        }

        public IRepository<Language> languageRepo;
        public IRepository<Language> LanguageRepository
        {
            get
            {
                if (this.languageRepo == null)
                {
                    this.languageRepo = new Repository<Language>(_db);
                }

                return languageRepo;
            }
        }

        public IRepository<ProfessionInformation> professionInformationRepo;
        public IRepository<ProfessionInformation> ProfessionInformationRepository
        {
            get
            {
                if (this.professionInformationRepo == null)
                {
                    this.professionInformationRepo = new Repository<ProfessionInformation>(_db);
                }
                return professionInformationRepo;
            }
        }





        public IRepository<PoliceStation> PoliceStationRepo;
        public IRepository<PoliceStation> PoliceStationRepository
        {
            get
            {
                if (this.PoliceStationRepo == null)
                {
                    this.PoliceStationRepo = new Repository<PoliceStation>(_db);
                }
                return PoliceStationRepo;
            }
        }


        public IRepository<PostOffice> PostOfficeRepo;
        public IRepository<PostOffice> PostOfficeRepository
        {
            get
            {
                if (this.PostOfficeRepo == null)
                {
                    this.PostOfficeRepo = new Repository<PostOffice>(_db);
                }
                return PostOfficeRepo;
            }
        }


        public IRepository<Profile> ProfileRepo;
        public IRepository<Profile> ProfileRepository
        {
            get
            {
                if (this.ProfileRepo == null)
                {
                    this.ProfileRepo = new Repository<Profile>(_db);
                }
                return ProfileRepo;
            }
        }



        public IRepository<PressentAddress> PressentAddressRepo;
        public IRepository<PressentAddress> PressentAddressRepository
        {
            get
            {
                if (this.PressentAddressRepo == null)
                {
                    this.PressentAddressRepo = new Repository<PressentAddress>(_db);
                }
                return PressentAddressRepo;
            }
        }


        public IRepository<PermanentAddress> PermanentAddressRepo;
        public IRepository<PermanentAddress> PermanentAddressRepository
        {
            get
            {
                if (this.PermanentAddressRepo == null)
                {
                    this.PermanentAddressRepo = new Repository<PermanentAddress>(_db);
                }
                return PermanentAddressRepo;
            }
        }

        public IRepository<ProfileFamilyDetails> profileFamilyDetailsRepo;
        public IRepository<ProfileFamilyDetails> profileFamilyDetailsRepository
        {
            get
            {
                if (this.profileFamilyDetailsRepo == null)
                {
                    this.profileFamilyDetailsRepo = new Repository<ProfileFamilyDetails>(_db);
                }
                return profileFamilyDetailsRepo;
            }
        }





        public IRepository<Item> itemRepo;
        public IRepository<Item> ItemRepository
        {
            get
            {
                if (this.itemRepo == null)
                {
                    this.itemRepo = new Repository<Item>(_db);
                }
                return itemRepo;
            }
        }


        public IRepository<ItemElement> itemElementRepo;
        public IRepository<ItemElement> ItemElementRepository
        {
            get
            {
                if (this.itemElementRepo == null)
                {
                    this.itemElementRepo = new Repository<ItemElement>(_db);
                }
                return itemElementRepo;
            }
        }



        public IRepository<RawMaterials> RawMaterialsRepo;
        public IRepository<RawMaterials> RawMaterialsRepository
        {
            get
            {
                if (this.RawMaterialsRepo == null)
                {
                    this.RawMaterialsRepo = new Repository<RawMaterials>(_db);
                }
                return RawMaterialsRepo;
            }
        }


        public IRepository<ProcurementMaster> ProcurementMasterRepo;
        public IRepository<ProcurementMaster> ProcurementMasterRepository
        {
            get
            {
                if (this.ProcurementMasterRepo == null)
                {
                    this.ProcurementMasterRepo = new Repository<ProcurementMaster>(_db);
                }
                return ProcurementMasterRepo;
            }
        }


        public IRepository<ProcurementDetails> ProcurementDetailsRepo;
        public IRepository<ProcurementDetails> ProcurementDetailsRepository
        {
            get
            {
                if (this.ProcurementDetailsRepo == null)
                {
                    this.ProcurementDetailsRepo = new Repository<ProcurementDetails>(_db);
                }
                return ProcurementDetailsRepo;
            }
        }


        public IRepository<InventoryMaster> InventoryMasterRepo;
        public IRepository<InventoryMaster> InventoryMasterRepository
        {
            get
            {
                if (this.InventoryMasterRepo == null)
                {
                    this.InventoryMasterRepo = new Repository<InventoryMaster>(_db);
                }
                return InventoryMasterRepo;
            }
        }


        public IRepository<InventoryDetail> InventoryDetailRepo;
        public IRepository<InventoryDetail> InventoryDetailRepository
        {
            get
            {
                if (this.InventoryDetailRepo == null)
                {
                    this.InventoryDetailRepo = new Repository<InventoryDetail>(_db);
                }
                return InventoryDetailRepo;
            }
        }

        public IRepository<TransferMaster> TransferMasterRepo;
        public IRepository<TransferMaster> TransferMasterRepository
        {
            get
            {
                if (this.TransferMasterRepo == null)
                {
                    this.TransferMasterRepo = new Repository<TransferMaster>(_db);
                }
                return TransferMasterRepo;
            }
        }


        public IRepository<TransferDetails> TransferDetailsRepo;
        public IRepository<TransferDetails> TransferDetailsRepository
        {
            get
            {
                if (this.TransferDetailsRepo == null)
                {
                    this.TransferDetailsRepo = new Repository<TransferDetails>(_db);
                }
                return TransferDetailsRepo;
            }
        }

        public IRepository<TemporaryTransferInformation> TemporaryTransferInformationRepo;
        public IRepository<TemporaryTransferInformation> TemporaryTransferInformationRepository
        {
            get
            {
                if (this.TemporaryTransferInformationRepo == null)
                {
                    this.TemporaryTransferInformationRepo = new Repository<TemporaryTransferInformation>(_db);
                }
                return TemporaryTransferInformationRepo;
            }
        }



        public IRepository<Product> productRepo;
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepo == null)
                {
                    this.productRepo = new Repository<Product>(_db);
                }
                return productRepo;
            }
        }

        public IRepository<ProductDetails> productDetailsRepo;
        public IRepository<ProductDetails> ProductDetailsRepository
        {
            get
            {
                if (this.productDetailsRepo == null)
                {
                    this.productDetailsRepo = new Repository<ProductDetails>(_db);
                }
                return productDetailsRepo;
            }
        }

        public IRepository<ProductionMaster> ProductionMasterRepo;
        public IRepository<ProductionMaster> ProductionMasterRepository
        {
            get
            {
                if (this.ProductionMasterRepo == null)
                {
                    this.ProductionMasterRepo = new Repository<ProductionMaster>(_db);
                }
                return ProductionMasterRepo;
            }
        }


        public IRepository<ProductionDetails> productionDetailsRepo;
        public IRepository<ProductionDetails> ProductionDetailsRepository
        {
            get
            {
                if (this.productionDetailsRepo == null)
                {
                    this.productionDetailsRepo = new Repository<ProductionDetails>(_db);
                }
                return productionDetailsRepo;
            }
        }

        public IRepository<Method> methodRepo;
        public IRepository<Method> MethodRepository
        {
            get
            {
                if (this.methodRepo == null)
                {
                    this.methodRepo = new Repository<Method>(_db);
                }
                return methodRepo;
            }
        }

        public IRepository<TransferType> transferTypeRepo;
        public IRepository<TransferType> TransferTypeRepository
        {
            get
            {
                if (this.transferTypeRepo == null)
                {
                    this.transferTypeRepo = new Repository<TransferType>(_db);
                }
                return transferTypeRepo;
            }
        }


        public IRepository<DamagedItem> damagedItemRepo;
        public IRepository<DamagedItem> DamagedItemRepository
        {
            get
            {
                if (this.damagedItemRepo == null)
                {
                    this.damagedItemRepo = new Repository<DamagedItem>(_db);
                }
                return damagedItemRepo;
            }
        }


        public IRepository<TransferOrder> TransferOrderRepo;
        public IRepository<TransferOrder> TransferOrderRepository
        {
            get
            {
                if (this.TransferOrderRepo == null)
                {
                    this.TransferOrderRepo = new Repository<TransferOrder>(_db);
                }
                return TransferOrderRepo;
            }
        }


        public IRepository<Email> EmailRepo;
        public IRepository<Email> EmailRepository
        {
            get
            {
                if (this.EmailRepo == null)
                {
                    this.EmailRepo = new Repository<Email>(_db);
                }
                return EmailRepo;
            }
        }


        public IRepository<RightOfAcces> RightOfAccesRepo;
        public IRepository<RightOfAcces> RightOfAccesRepository
        {
            get
            {
                if (this.RightOfAccesRepo == null)
                {
                    this.RightOfAccesRepo = new Repository<RightOfAcces>(_db);
                }
                return RightOfAccesRepo;
            }
        }




        public IRepository<Layer> LayerRepo;
        public IRepository<Layer> LayerRepository
        {
            get
            {
                if (this.LayerRepo == null)
                {
                    this.LayerRepo = new Repository<Layer>(_db);
                }
                return LayerRepo;
            }
        }


        public IRepository<LayerAssign> LayerAssignRepo;
        public IRepository<LayerAssign> LayerAssignRepository
        {
            get
            {
                if (this.LayerAssignRepo == null)
                {
                    this.LayerAssignRepo = new Repository<LayerAssign>(_db);
                }
                return LayerAssignRepo;
            }
        }


        public IRepository<ApprovalLayer> ApprovalLayerRepo;
        public IRepository<ApprovalLayer> ApprovalLayerRepository
        {
            get
            {
                if (this.ApprovalLayerRepo == null)
                {
                    this.ApprovalLayerRepo = new Repository<ApprovalLayer>(_db);
                }
                return ApprovalLayerRepo;
            }
        }


        public IRepository<StoreAssign> StoreAssignRepo;
        public IRepository<StoreAssign> StoreAssignRepository
        {
            get
            {
                if (this.StoreAssignRepo == null)
                {
                    this.StoreAssignRepo = new Repository<StoreAssign>(_db);
                }
                return StoreAssignRepo;
            }
        }



        public IRepository<CustomerRegister> CustomerregisterRepo;
        public IRepository<CustomerRegister> CustomerregisterRepository
        {
            get
            {
                if (this.CustomerregisterRepo == null)
                {
                    this.CustomerregisterRepo = new Repository<CustomerRegister>(_db);
                }
                return CustomerregisterRepo;
            }
        }


        public IRepository<SalesElementStup> SalesElementStupRepo;
        public IRepository<SalesElementStup> SalesElementStupRepoRepository
        {
            get
            {
                if (this.SalesElementStupRepo == null)
                {
                    this.SalesElementStupRepo = new Repository<SalesElementStup>(_db);
                }
                return SalesElementStupRepo;
            }
        }

    }
}
