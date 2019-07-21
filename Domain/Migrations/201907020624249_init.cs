namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovalLayers",
                c => new
                    {
                        ApprovalId = c.Int(nullable: false, identity: true),
                        Layer1 = c.Boolean(nullable: false),
                        Layer1UserName = c.String(),
                        Layer2 = c.Boolean(nullable: false),
                        Layer2UserName = c.String(),
                        Layer3 = c.Boolean(nullable: false),
                        Layer3UserName = c.String(),
                        Layer4 = c.Boolean(nullable: false),
                        Layer4UserName = c.String(),
                        Layer5 = c.Boolean(nullable: false),
                        Layer5UserName = c.String(),
                        Layer6 = c.Boolean(nullable: false),
                        Layer6UserName = c.String(),
                        Layer7 = c.Boolean(nullable: false),
                        Layer7UserName = c.String(),
                        Layer8 = c.Boolean(nullable: false),
                        Layer8UserName = c.String(),
                        Layer9 = c.Boolean(nullable: false),
                        Layer9UserName = c.String(),
                        Layer10 = c.Boolean(nullable: false),
                        Layer10UserName = c.String(),
                        CurrentLevel = c.String(),
                        Title = c.String(),
                        Status = c.String(),
                        Layer1ApprovalDate = c.DateTime(nullable: false),
                        Layer2ApprovalDate = c.DateTime(nullable: false),
                        Layer3ApprovalDate = c.DateTime(nullable: false),
                        Layer4ApprovalDate = c.DateTime(nullable: false),
                        Layer5ApprovalDate = c.DateTime(nullable: false),
                        Layer6ApprovalDate = c.DateTime(nullable: false),
                        Layer7ApprovalDate = c.DateTime(nullable: false),
                        Layer8ApprovalDate = c.DateTime(nullable: false),
                        Layer9ApprovalDate = c.DateTime(nullable: false),
                        Layer10ApprovalDate = c.DateTime(nullable: false),
                        TransferOrderRequisitionId = c.Int(),
                        TransferRequisitionId = c.Int(),
                        ProcurementRequisitionId = c.Int(),
                        ProductionRequisitionId = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApprovalId);
            
            CreateTable(
                "dbo.BloodGroups",
                c => new
                    {
                        BloodGroupId = c.Int(nullable: false, identity: true),
                        BloodGroupName = c.String(),
                    })
                .PrimaryKey(t => t.BloodGroupId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        Category_CategoryId = c.Int(),
                        SubCategory_SubCategoryId = c.Int(),
                        SubSubCategory_SubSubCategoryId = c.Int(),
                        SubSubSubCategory_SubSubSubCategoryId = c.Int(),
                        SubSubSubSubCategory_SubSubSubSubCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategory_SubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategory_SubSubSubCategoryId)
                .ForeignKey("dbo.SubSubSubSubCategories", t => t.SubSubSubSubCategory_SubSubSubSubCategoryId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.SubCategory_SubCategoryId)
                .Index(t => t.SubSubCategory_SubSubCategoryId)
                .Index(t => t.SubSubSubCategory_SubSubSubCategoryId)
                .Index(t => t.SubSubSubSubCategory_SubSubSubSubCategoryId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(),
                        SubSubCategoryId = c.Int(),
                        SubSubSubCategoryId = c.Int(),
                        SubSubSubSubCategoryId = c.Int(),
                        ModelId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                        Height = c.Int(),
                        Width = c.Int(),
                        Weight = c.Int(),
                        MethodId = c.Int(nullable: false),
                        ItemDetails = c.String(),
                        Product_Image = c.String(),
                        Product_Image1 = c.String(),
                        Product_Image2 = c.String(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.Methods", t => t.MethodId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategoryId)
                .ForeignKey("dbo.SubSubSubSubCategories", t => t.SubSubSubSubCategoryId)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubSubSubCategoryId)
                .Index(t => t.ModelId)
                .Index(t => t.BrandId)
                .Index(t => t.UnitId)
                .Index(t => t.MethodId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProcurementDetails",
                c => new
                    {
                        PO_Details_ID = c.Int(nullable: false, identity: true),
                        DateOfExpired = c.DateTime(),
                        DateOfNextMaintainance = c.DateTime(),
                        Barcode = c.String(),
                        Item_Unique_Number = c.String(),
                        Chesis_Number = c.String(),
                        Engine_Number = c.String(),
                        ItemId = c.Int(nullable: false),
                        CountryId = c.Int(),
                        Principle_id = c.Int(),
                        ConditionOfItemId = c.Int(),
                        WarrantyId = c.Int(),
                        PO_QTD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_HD_ID = c.Int(nullable: false),
                        CategoryId = c.Int(),
                        SubCategoryId = c.Int(),
                        SubSubCategoryId = c.Int(),
                        SubSubSubCategoryId = c.Int(),
                        SubSubSubSubCategoryId = c.Int(),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                        UnitId = c.Int(),
                        MethodId = c.Int(),
                        IsHot = c.Boolean(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PO_Details_ID)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.ConditionOfItems", t => t.ConditionOfItemId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.ProcurementMasters", t => t.PO_HD_ID, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategoryId)
                .ForeignKey("dbo.SubSubSubSubCategories", t => t.SubSubSubSubCategoryId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Warranties", t => t.WarrantyId)
                .Index(t => t.CountryId)
                .Index(t => t.ConditionOfItemId)
                .Index(t => t.WarrantyId)
                .Index(t => t.PO_HD_ID)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubSubSubCategoryId)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.ConditionOfItems",
                c => new
                    {
                        ConditionOfItemId = c.Int(nullable: false, identity: true),
                        ConditionOfItemName = c.String(),
                    })
                .PrimaryKey(t => t.ConditionOfItemId);
            
            CreateTable(
                "dbo.TemporaryTransferInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Inv_HD_ID = c.Int(nullable: false),
                        Inv_Details_ID = c.Int(nullable: false),
                        TransferId = c.Int(nullable: false),
                        TransferDetailId = c.Int(nullable: false),
                        SupplierCompanyId = c.Int(),
                        SubContractCompanyId = c.Int(),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                        DateOfExpired = c.DateTime(),
                        DateOfNextMaintainance = c.DateTime(),
                        Barcode = c.String(),
                        Item_Unique_Number = c.String(),
                        Chesis_Number = c.String(),
                        Engine_Number = c.String(),
                        ItemId = c.Int(nullable: false),
                        ProductId = c.Int(),
                        CountryId = c.Int(),
                        Principle_id = c.Int(),
                        ConditionOfItemId = c.Int(),
                        WarrantyId = c.Int(),
                        TransactionQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(),
                        SubCategoryId = c.Int(),
                        SubSubCategoryId = c.Int(),
                        SubSubSubCategoryId = c.Int(),
                        SubSubSubSubCategoryId = c.Int(),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                        UnitId = c.Int(),
                        MethodId = c.Int(),
                        DateOfActualTransfer = c.DateTime(nullable: false),
                        ToStoreId = c.Int(),
                        FromStoreId = c.Int(nullable: false),
                        TransferTypeId = c.Int(),
                        DateOfTransferOrder = c.DateTime(nullable: false),
                        Recieve = c.String(),
                        ReceiveItemQuantity = c.Decimal(precision: 18, scale: 2),
                        PendingItemQuantity = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.ConditionOfItems", t => t.ConditionOfItemId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.SubSubSubSubCategories", t => t.SubSubSubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Warranties", t => t.WarrantyId)
                .ForeignKey("dbo.TransferMasters", t => t.TransferId, cascadeDelete: true)
                .ForeignKey("dbo.TransferTypes", t => t.TransferTypeId)
                .ForeignKey("dbo.InventoryMasters", t => t.Inv_HD_ID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Methods", t => t.MethodId)
                .Index(t => t.Inv_HD_ID)
                .Index(t => t.TransferId)
                .Index(t => t.ItemId)
                .Index(t => t.CountryId)
                .Index(t => t.ConditionOfItemId)
                .Index(t => t.WarrantyId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubSubSubCategoryId)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.UnitId)
                .Index(t => t.MethodId)
                .Index(t => t.TransferTypeId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.PermanentAddresses",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        PermanentAddressFull = c.String(),
                        PerPoliceStationId = c.Int(),
                        PerPostOfficeId = c.Int(),
                        PerDistrictId = c.Int(),
                        PerDivisionId = c.Int(),
                        PerCountryId = c.Int(),
                        Country_CountryId = c.Int(),
                        District_DistrictId = c.Int(),
                        PoliceStation_PoliceStationId = c.Int(),
                        Division_DivisionId = c.Int(),
                        PostOffice_PostOfficeId = c.Int(),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .ForeignKey("dbo.Districts", t => t.District_DistrictId)
                .ForeignKey("dbo.PoliceStations", t => t.PoliceStation_PoliceStationId)
                .ForeignKey("dbo.Divisions", t => t.Division_DivisionId)
                .ForeignKey("dbo.PostOffices", t => t.PostOffice_PostOfficeId)
                .Index(t => t.Country_CountryId)
                .Index(t => t.District_DistrictId)
                .Index(t => t.PoliceStation_PoliceStationId)
                .Index(t => t.Division_DivisionId)
                .Index(t => t.PostOffice_PostOfficeId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(),
                        ShippingCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DistrictId);
            
            CreateTable(
                "dbo.PoliceStations",
                c => new
                    {
                        PoliceStationId = c.Int(nullable: false, identity: true),
                        PoliceStationName = c.String(),
                        DistrictId = c.Int(),
                    })
                .PrimaryKey(t => t.PoliceStationId)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.PressentAddresses",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        PresentAddressFull = c.String(),
                        PrePoliceStationId = c.Int(),
                        PrePostOfficeId = c.Int(),
                        PreDistrictId = c.Int(),
                        PreDivisionId = c.Int(),
                        PreCountryId = c.Int(),
                        Country_CountryId = c.Int(),
                        District_DistrictId = c.Int(),
                        Division_DivisionId = c.Int(),
                        PoliceStation_PoliceStationId = c.Int(),
                        PostOffice_PostOfficeId = c.Int(),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .ForeignKey("dbo.Districts", t => t.District_DistrictId)
                .ForeignKey("dbo.Divisions", t => t.Division_DivisionId)
                .ForeignKey("dbo.PoliceStations", t => t.PoliceStation_PoliceStationId)
                .ForeignKey("dbo.PostOffices", t => t.PostOffice_PostOfficeId)
                .Index(t => t.Country_CountryId)
                .Index(t => t.District_DistrictId)
                .Index(t => t.Division_DivisionId)
                .Index(t => t.PoliceStation_PoliceStationId)
                .Index(t => t.PostOffice_PostOfficeId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        DivisionName = c.String(),
                    })
                .PrimaryKey(t => t.DivisionId);
            
            CreateTable(
                "dbo.PostOffices",
                c => new
                    {
                        PostOfficeId = c.Int(nullable: false, identity: true),
                        PostOfficeName = c.String(),
                        DistrictId = c.Int(),
                    })
                .PrimaryKey(t => t.PostOfficeId)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        ProfileName = c.String(),
                        FatherName = c.String(),
                        MotherName = c.String(),
                        DateofBirth = c.DateTime(nullable: false),
                        BirthPlace = c.String(),
                        Genderid = c.Int(),
                        BloodGroupId = c.Int(),
                        NationalityID = c.Int(),
                        MaritalStatusId = c.Int(),
                        DateofMarriage = c.DateTime(nullable: false),
                        RegionId = c.Int(),
                        NID = c.Int(),
                        TIN = c.Int(),
                        SpouseName = c.String(),
                        SpouseProfession = c.String(),
                        MailAddress = c.String(),
                        ContactNumber = c.Int(nullable: false),
                        EmergencyContactNumber = c.Int(nullable: false),
                        PassportNumber = c.String(),
                        DrivingLicenceNumber = c.String(),
                        Hobby = c.String(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                        District_DistrictId = c.Int(),
                        PermanentAddress_ProfileId = c.Int(),
                        PressentAddress_ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.BloodGroups", t => t.BloodGroupId)
                .ForeignKey("dbo.Districts", t => t.District_DistrictId)
                .ForeignKey("dbo.Genders", t => t.Genderid)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId)
                .ForeignKey("dbo.Nationalities", t => t.NationalityID)
                .ForeignKey("dbo.PermanentAddresses", t => t.PermanentAddress_ProfileId)
                .ForeignKey("dbo.PressentAddresses", t => t.PressentAddress_ProfileId)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.Genderid)
                .Index(t => t.BloodGroupId)
                .Index(t => t.NationalityID)
                .Index(t => t.MaritalStatusId)
                .Index(t => t.RegionId)
                .Index(t => t.District_DistrictId)
                .Index(t => t.PermanentAddress_ProfileId)
                .Index(t => t.PressentAddress_ProfileId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderID = c.Int(nullable: false, identity: true),
                        GenderName = c.String(),
                    })
                .PrimaryKey(t => t.GenderID);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        MaritalStatusId = c.Int(nullable: false, identity: true),
                        MaritalStatusName = c.String(),
                    })
                .PrimaryKey(t => t.MaritalStatusId);
            
            CreateTable(
                "dbo.Nationalities",
                c => new
                    {
                        NationalityID = c.Int(nullable: false, identity: true),
                        NationalityName = c.String(),
                    })
                .PrimaryKey(t => t.NationalityID);
            
            CreateTable(
                "dbo.ProfileFamilyDetails",
                c => new
                    {
                        ProfileDetailsId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        SpouseName = c.String(),
                        SpouseDOB = c.DateTime(nullable: false),
                        SpouseProfession = c.String(),
                        SpouseGenderId = c.Int(nullable: false),
                        SlNoOfMarriage = c.Int(nullable: false),
                        ChildName = c.String(),
                        ChildDOB = c.DateTime(nullable: false),
                        ChildProfession = c.String(),
                        GenderID = c.Int(),
                        SlNoOfChild = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileDetailsId)
                .ForeignKey("dbo.Genders", t => t.GenderID)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.GenderID);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        RegionName = c.String(),
                    })
                .PrimaryKey(t => t.RegionId);
            
            CreateTable(
                "dbo.Principles",
                c => new
                    {
                        Principle_id = c.Int(nullable: false, identity: true),
                        Principle_name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Principle_id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceID = c.Int(nullable: false, identity: true),
                        ProvinceName = c.String(),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProvinceID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StateID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.InventoryMasters",
                c => new
                    {
                        Inv_HD_ID = c.Int(nullable: false, identity: true),
                        PO_HD_ID = c.Int(),
                        DateOfPurchase = c.DateTime(),
                        DateOfReceive = c.DateTime(),
                        PO_GRAND_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProcurementTypeId = c.Int(),
                        SupplierCompanyId = c.Int(),
                        SubContractCompanyId = c.Int(),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                        PurchasedBy_ProcureBy = c.Int(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        TransferId = c.Int(),
                        DamagedItemId = c.Int(),
                        ProductionMasterId = c.Int(),
                    })
                .PrimaryKey(t => t.Inv_HD_ID)
                .ForeignKey("dbo.DamagedItems", t => t.DamagedItemId)
                .ForeignKey("dbo.ProcurementMasters", t => t.PO_HD_ID)
                .ForeignKey("dbo.ProductionMasters", t => t.ProductionMasterId)
                .ForeignKey("dbo.TransferMasters", t => t.TransferId)
                .Index(t => t.PO_HD_ID)
                .Index(t => t.TransferId)
                .Index(t => t.DamagedItemId)
                .Index(t => t.ProductionMasterId);
            
            CreateTable(
                "dbo.DamagedItems",
                c => new
                    {
                        DamagedItemId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(),
                        ProductId = c.Int(),
                        DamagedItemType = c.String(),
                        DamageQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DamageDate = c.DateTime(nullable: false),
                        PO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Item_Unique_Number = c.String(),
                        DateOfPurchase = c.DateTime(),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                    })
                .PrimaryKey(t => t.DamagedItemId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .ForeignKey("dbo.SubSubStores", t => t.SubSubStoreId)
                .ForeignKey("dbo.SubSubSubStores", t => t.SubSubSubStoreId)
                .ForeignKey("dbo.SubSubSubSubStores", t => t.SubSubSubSubStoreId)
                .Index(t => t.ItemId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId)
                .Index(t => t.SubSubStoreId)
                .Index(t => t.SubSubSubStoreId)
                .Index(t => t.SubSubSubSubStoreId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        StoreName = c.String(),
                    })
                .PrimaryKey(t => t.StoreId);
            
            CreateTable(
                "dbo.ProcurementMasters",
                c => new
                    {
                        PO_HD_ID = c.Int(nullable: false, identity: true),
                        DateOfPurchase = c.DateTime(),
                        DateOfReceive = c.DateTime(),
                        PO_GRAND_TOTAL = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProcurementTypeId = c.Int(nullable: false),
                        SupplierCompanyId = c.Int(nullable: false),
                        SubContractCompanyId = c.Int(),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                        PurchasedBy_ProcureBy = c.Int(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PO_HD_ID)
                .ForeignKey("dbo.ProcurementTypes", t => t.ProcurementTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .ForeignKey("dbo.SubSubStores", t => t.SubSubStoreId)
                .ForeignKey("dbo.SubSubSubStores", t => t.SubSubSubStoreId)
                .ForeignKey("dbo.SubSubSubSubStores", t => t.SubSubSubSubStoreId)
                .ForeignKey("dbo.SupplierCompanies", t => t.SupplierCompanyId, cascadeDelete: true)
                .Index(t => t.ProcurementTypeId)
                .Index(t => t.SupplierCompanyId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId)
                .Index(t => t.SubSubStoreId)
                .Index(t => t.SubSubSubStoreId)
                .Index(t => t.SubSubSubSubStoreId);
            
            CreateTable(
                "dbo.ProcurementTypes",
                c => new
                    {
                        ProcurementTypeId = c.Int(nullable: false, identity: true),
                        ProcurementTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ProcurementTypeId);
            
            CreateTable(
                "dbo.SubStores",
                c => new
                    {
                        SubStoreId = c.Int(nullable: false, identity: true),
                        SubStoreName = c.String(),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubStoreId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.ProductionMasters",
                c => new
                    {
                        ProductionMasterId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductionQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Productiondate = c.DateTime(nullable: false),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                        CreatedBy = c.String(),
                        ProductDetails_SerialId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductionMasterId)
                .ForeignKey("dbo.ProductDetails", t => t.ProductDetails_SerialId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .ForeignKey("dbo.SubSubStores", t => t.SubSubStoreId)
                .ForeignKey("dbo.SubSubSubStores", t => t.SubSubSubStoreId)
                .ForeignKey("dbo.SubSubSubSubStores", t => t.SubSubSubSubStoreId)
                .Index(t => t.ProductId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId)
                .Index(t => t.SubSubStoreId)
                .Index(t => t.SubSubSubStoreId)
                .Index(t => t.SubSubSubSubStoreId)
                .Index(t => t.ProductDetails_SerialId);
            
            CreateTable(
                "dbo.InventoryDetails",
                c => new
                    {
                        Inv_Details_ID = c.Int(nullable: false, identity: true),
                        Inv_HD_ID = c.Int(nullable: false),
                        DateOfExpired = c.DateTime(),
                        DateOfNextMaintainance = c.DateTime(),
                        ProductionDate = c.DateTime(),
                        Barcode = c.String(),
                        Item_Unique_Number = c.String(),
                        Chesis_Number = c.String(),
                        Engine_Number = c.String(),
                        ItemId = c.Int(nullable: false),
                        ProductId = c.Int(),
                        CountryId = c.Int(),
                        Principle_id = c.Int(),
                        ConditionOfItemId = c.Int(),
                        WarrantyId = c.Int(),
                        ProductionMasterId = c.Int(),
                        TransactionQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PO_SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DamageQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(),
                        SubCategoryId = c.Int(),
                        SubSubCategoryId = c.Int(),
                        SubSubSubCategoryId = c.Int(),
                        SubSubSubSubCategoryId = c.Int(),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                        UnitId = c.Int(),
                        MethodId = c.Int(),
                    })
                .PrimaryKey(t => t.Inv_Details_ID)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.ConditionOfItems", t => t.ConditionOfItemId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.InventoryMasters", t => t.Inv_HD_ID, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.ProductionMasters", t => t.ProductionMasterId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategoryId)
                .ForeignKey("dbo.SubSubSubSubCategories", t => t.SubSubSubSubCategoryId)
                .ForeignKey("dbo.Units", t => t.UnitId)
                .ForeignKey("dbo.Warranties", t => t.WarrantyId)
                .Index(t => t.Inv_HD_ID)
                .Index(t => t.CountryId)
                .Index(t => t.ConditionOfItemId)
                .Index(t => t.WarrantyId)
                .Index(t => t.ProductionMasterId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubSubSubCategoryId)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.UnitId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(),
                    })
                .PrimaryKey(t => t.ModelId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SubSubCategories",
                c => new
                    {
                        SubSubCategoryId = c.Int(nullable: false, identity: true),
                        SubSubCategoryName = c.String(),
                        SubCategoryId = c.Int(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSubCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SubSubSubCategories",
                c => new
                    {
                        SubSubSubCategoryId = c.Int(nullable: false, identity: true),
                        SubSubSubCategoryName = c.String(),
                        SubSubCategoryId = c.Int(),
                        SubCategoryId = c.Int(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSubSubCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SubSubSubSubCategories",
                c => new
                    {
                        SubSubSubSubCategoryId = c.Int(nullable: false, identity: true),
                        SubSubSubSubCategoryName = c.String(),
                        SubSubSubCategoryId = c.Int(),
                        SubSubCategoryId = c.Int(),
                        SubCategoryId = c.Int(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSubSubSubCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.Warranties",
                c => new
                    {
                        WarrantyId = c.Int(nullable: false, identity: true),
                        WarrantyPeriod = c.String(),
                    })
                .PrimaryKey(t => t.WarrantyId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        SerialId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemId = c.Int(nullable: false),
                        ItemQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SerialId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.ProductionDetails",
                c => new
                    {
                        ProductionDetailsId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(),
                        ItemQuantity = c.Decimal(precision: 18, scale: 2),
                        ItemCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionMasterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductionDetailsId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.ProductionMasters", t => t.ProductionMasterId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.ProductionMasterId);
            
            CreateTable(
                "dbo.SubSubStores",
                c => new
                    {
                        SubSubStoreId = c.Int(nullable: false, identity: true),
                        SubSubStoreName = c.String(),
                        StoreId = c.Int(nullable: false),
                        SubStoreId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSubStoreId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId);
            
            CreateTable(
                "dbo.SubSubSubStores",
                c => new
                    {
                        SubSubSubStoreId = c.Int(nullable: false, identity: true),
                        SubSubSubStoreName = c.String(),
                        StoreId = c.Int(nullable: false),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSubSubStoreId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .ForeignKey("dbo.SubSubStores", t => t.SubSubStoreId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId)
                .Index(t => t.SubSubStoreId);
            
            CreateTable(
                "dbo.SubSubSubSubStores",
                c => new
                    {
                        SubSubSubSubStoreId = c.Int(nullable: false, identity: true),
                        SubSubSubSubStoreName = c.String(),
                        StoreId = c.Int(nullable: false),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                    })
                .PrimaryKey(t => t.SubSubSubSubStoreId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .ForeignKey("dbo.SubSubStores", t => t.SubSubStoreId)
                .ForeignKey("dbo.SubSubSubStores", t => t.SubSubSubStoreId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId)
                .Index(t => t.SubSubStoreId)
                .Index(t => t.SubSubSubStoreId);
            
            CreateTable(
                "dbo.SupplierCompanies",
                c => new
                    {
                        SupplierCompanyId = c.Int(nullable: false, identity: true),
                        SupplierCompanyName = c.String(),
                    })
                .PrimaryKey(t => t.SupplierCompanyId);
            
            CreateTable(
                "dbo.TransferMasters",
                c => new
                    {
                        TransferId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(),
                        SubStoreId = c.Int(),
                        SubSubStoreId = c.Int(),
                        SubSubSubStoreId = c.Int(),
                        SubSubSubSubStoreId = c.Int(),
                        FromStoreId = c.Int(nullable: false),
                        Transferby = c.Int(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.TransferId)
                .ForeignKey("dbo.Stores", t => t.StoreId)
                .ForeignKey("dbo.SubStores", t => t.SubStoreId)
                .ForeignKey("dbo.SubSubStores", t => t.SubSubStoreId)
                .ForeignKey("dbo.SubSubSubStores", t => t.SubSubSubStoreId)
                .ForeignKey("dbo.SubSubSubSubStores", t => t.SubSubSubSubStoreId)
                .Index(t => t.StoreId)
                .Index(t => t.SubStoreId)
                .Index(t => t.SubSubStoreId)
                .Index(t => t.SubSubSubStoreId)
                .Index(t => t.SubSubSubSubStoreId);
            
            CreateTable(
                "dbo.TransferDetails",
                c => new
                    {
                        TransferDetailId = c.Int(nullable: false, identity: true),
                        TransferId = c.Int(nullable: false),
                        TransferOrderId = c.Int(nullable: false),
                        ToStoreId = c.Int(),
                        ItemId = c.Int(nullable: false),
                        TransactionQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransferTypeId = c.Int(),
                        UnitId = c.Int(),
                        ConditionOfItemId = c.Int(),
                        DateOfActualTransfer = c.DateTime(nullable: false),
                        Recieve = c.String(),
                        RecieveBy = c.String(),
                        DateOfReceive = c.DateTime(),
                        ReceiveItemQuantity = c.Decimal(precision: 18, scale: 2),
                        PendingItemQuantity = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransferDetailId)
                .ForeignKey("dbo.ConditionOfItems", t => t.ConditionOfItemId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.TransferMasters", t => t.TransferId, cascadeDelete: true)
                .ForeignKey("dbo.TransferTypes", t => t.TransferTypeId)
                .Index(t => t.TransferId)
                .Index(t => t.ItemId)
                .Index(t => t.TransferTypeId)
                .Index(t => t.ConditionOfItemId);
            
            CreateTable(
                "dbo.TransferTypes",
                c => new
                    {
                        TransferTypeId = c.Int(nullable: false, identity: true),
                        TransferTypeName = c.String(),
                    })
                .PrimaryKey(t => t.TransferTypeId);
            
            CreateTable(
                "dbo.Methods",
                c => new
                    {
                        MethodId = c.Int(nullable: false, identity: true),
                        MethodName = c.String(),
                    })
                .PrimaryKey(t => t.MethodId);
            
            CreateTable(
                "dbo.RawMaterials",
                c => new
                    {
                        RawMaterialId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(),
                        ItemElementId = c.Int(),
                    })
                .PrimaryKey(t => t.RawMaterialId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .ForeignKey("dbo.ItemElements", t => t.ItemElementId)
                .Index(t => t.ItemId)
                .Index(t => t.ItemElementId);
            
            CreateTable(
                "dbo.ItemElements",
                c => new
                    {
                        ItemElementId = c.Int(nullable: false, identity: true),
                        ItemElementName = c.String(),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(),
                        SubSubCategoryId = c.Int(),
                        SubSubSubCategoryId = c.Int(),
                        SubSubSubSubCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemElementId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId)
                .ForeignKey("dbo.SubSubCategories", t => t.SubSubCategoryId)
                .ForeignKey("dbo.SubSubSubCategories", t => t.SubSubSubCategoryId)
                .ForeignKey("dbo.SubSubSubSubCategories", t => t.SubSubSubSubCategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId)
                .Index(t => t.SubSubCategoryId)
                .Index(t => t.SubSubSubCategoryId)
                .Index(t => t.SubSubSubSubCategoryId);
            
            CreateTable(
                "dbo.Celebrations",
                c => new
                    {
                        CelebrationId = c.Int(nullable: false, identity: true),
                        CelebrationName = c.String(),
                        CelebrationType = c.String(),
                    })
                .PrimaryKey(t => t.CelebrationId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.EducationQualifications",
                c => new
                    {
                        EducationQualificationId = c.Int(nullable: false, identity: true),
                        EducationQualificationName = c.String(),
                    })
                .PrimaryKey(t => t.EducationQualificationId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        FromName = c.String(nullable: false),
                        ToEmail = c.String(nullable: false),
                        FromEmail = c.String(),
                        Message = c.String(nullable: false),
                        Subject = c.String(),
                        CC = c.String(),
                        Bcc = c.String(),
                        ViewMessage = c.String(),
                    })
                .PrimaryKey(t => t.EmailId);
            
            CreateTable(
                "dbo.EmailProviders",
                c => new
                    {
                        EmailProviderId = c.Int(nullable: false, identity: true),
                        EmailProviderName = c.String(),
                    })
                .PrimaryKey(t => t.EmailProviderId);
            
            CreateTable(
                "dbo.Festivals",
                c => new
                    {
                        FestivalId = c.Int(nullable: false, identity: true),
                        FestivalName = c.String(),
                        ReligionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FestivalId)
                .ForeignKey("dbo.Religions", t => t.ReligionId, cascadeDelete: true)
                .Index(t => t.ReligionId);
            
            CreateTable(
                "dbo.Religions",
                c => new
                    {
                        ReligionId = c.Int(nullable: false, identity: true),
                        ReligionName = c.String(),
                    })
                .PrimaryKey(t => t.ReligionId);
            
            CreateTable(
                "dbo.Hobbies",
                c => new
                    {
                        HobbyId = c.Int(nullable: false, identity: true),
                        HobbyName = c.String(),
                    })
                .PrimaryKey(t => t.HobbyId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.LayerAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        LayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Layers", t => t.LayerId, cascadeDelete: true)
                .Index(t => t.LayerId);
            
            CreateTable(
                "dbo.Layers",
                c => new
                    {
                        LayerId = c.Int(nullable: false, identity: true),
                        LayerName = c.String(),
                    })
                .PrimaryKey(t => t.LayerId);
            
            CreateTable(
                "dbo.MENUMAINs",
                c => new
                    {
                        MainMenuId = c.Int(nullable: false, identity: true),
                        MenuName = c.String(),
                        MenuURL = c.String(),
                        RoleId = c.String(),
                    })
                .PrimaryKey(t => t.MainMenuId);
            
            CreateTable(
                "dbo.MENUSUBs",
                c => new
                    {
                        SubMenuId = c.Int(nullable: false, identity: true),
                        MainMenuId = c.Int(nullable: false),
                        MenuName = c.String(),
                        MenuURL = c.String(),
                    })
                .PrimaryKey(t => t.SubMenuId)
                .ForeignKey("dbo.MENUMAINs", t => t.MainMenuId, cascadeDelete: true)
                .Index(t => t.MainMenuId);
            
            CreateTable(
                "dbo.MENUSUBSUBs",
                c => new
                    {
                        SubSubMenuId = c.Int(nullable: false, identity: true),
                        SubMenuId = c.Int(nullable: false),
                        MenuName = c.String(),
                        MenuURL = c.String(),
                    })
                .PrimaryKey(t => t.SubSubMenuId)
                .ForeignKey("dbo.MENUSUBs", t => t.SubMenuId, cascadeDelete: true)
                .Index(t => t.SubMenuId);
            
            CreateTable(
                "dbo.PhoneProviders",
                c => new
                    {
                        PhoneProviderId = c.Int(nullable: false, identity: true),
                        PhoneProviderName = c.String(),
                    })
                .PrimaryKey(t => t.PhoneProviderId);
            
            CreateTable(
                "dbo.ProfessionInformations",
                c => new
                    {
                        ProfessionId = c.Int(nullable: false, identity: true),
                        ProfessionName = c.String(),
                    })
                .PrimaryKey(t => t.ProfessionId);
            
            CreateTable(
                "dbo.RightOfAcces",
                c => new
                    {
                        RightOfAccesId = c.Int(nullable: false, identity: true),
                        TransferId = c.Int(nullable: false),
                        TransferDetailsId = c.Int(nullable: false),
                        TransferOrderId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FromStoreId = c.Int(nullable: false),
                        ToStoreId = c.Int(),
                        Reason = c.String(),
                        Remarks = c.String(),
                        PO_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitId = c.Int(),
                        UnitName = c.String(),
                        ProductId = c.Int(),
                        ProductName = c.String(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RightOfAccesId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.TransferMasters", t => t.TransferId, cascadeDelete: true)
                .Index(t => t.TransferId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.SafetyStocks",
                c => new
                    {
                        SafetyStockLimitId = c.Int(nullable: false, identity: true),
                        ItemOrProductId = c.Int(nullable: false),
                        SafetyStokLimit = c.Int(nullable: false),
                        UnitId = c.String(),
                    })
                .PrimaryKey(t => t.SafetyStockLimitId);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false, identity: true),
                        SeasonName = c.String(),
                    })
                .PrimaryKey(t => t.SeasonId);
            
            CreateTable(
                "dbo.StoreAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubContractCompanies",
                c => new
                    {
                        SubContractCompanyId = c.Int(nullable: false, identity: true),
                        SubContractCompanyName = c.String(),
                    })
                .PrimaryKey(t => t.SubContractCompanyId);
            
            CreateTable(
                "dbo.TransferOrders",
                c => new
                    {
                        TransferOrderId = c.Int(nullable: false, identity: true),
                        ToStoreId = c.Int(nullable: false),
                        FromStoreId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        TransactionQuantity = c.Int(nullable: false),
                        TransferOrderby = c.String(),
                        TransferOrderdate = c.DateTime(),
                        TransferOrderDeliverydate = c.DateTime(),
                        OrderRecieve = c.String(),
                        TransferOrderReceiveby = c.String(),
                        TransferOrderReceiveDate = c.DateTime(),
                        TransferTypeId = c.Int(),
                        TransferTypeName = c.String(),
                        UnitId = c.Int(),
                        UnitName = c.String(),
                        TransferOrderSent = c.String(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdateDate = c.DateTime(),
                        ConditionOfItemId = c.Int(),
                    })
                .PrimaryKey(t => t.TransferOrderId)
                .ForeignKey("dbo.ConditionOfItems", t => t.ConditionOfItemId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.TransferTypes", t => t.TransferTypeId)
                .Index(t => t.ItemId)
                .Index(t => t.TransferTypeId)
                .Index(t => t.ConditionOfItemId);
            
            CreateTable(
                "dbo.Upazilas",
                c => new
                    {
                        UpazilaId = c.Int(nullable: false, identity: true),
                        UpazilaName = c.String(),
                    })
                .PrimaryKey(t => t.UpazilaId);
            
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        WeatherId = c.Int(nullable: false, identity: true),
                        WeatherName = c.String(),
                    })
                .PrimaryKey(t => t.WeatherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransferOrders", "TransferTypeId", "dbo.TransferTypes");
            DropForeignKey("dbo.TransferOrders", "ItemId", "dbo.Items");
            DropForeignKey("dbo.TransferOrders", "ConditionOfItemId", "dbo.ConditionOfItems");
            DropForeignKey("dbo.RightOfAcces", "TransferId", "dbo.TransferMasters");
            DropForeignKey("dbo.RightOfAcces", "ItemId", "dbo.Items");
            DropForeignKey("dbo.MENUSUBSUBs", "SubMenuId", "dbo.MENUSUBs");
            DropForeignKey("dbo.MENUSUBs", "MainMenuId", "dbo.MENUMAINs");
            DropForeignKey("dbo.LayerAssigns", "LayerId", "dbo.Layers");
            DropForeignKey("dbo.Festivals", "ReligionId", "dbo.Religions");
            DropForeignKey("dbo.Items", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Items", "SubSubSubSubCategoryId", "dbo.SubSubSubSubCategories");
            DropForeignKey("dbo.Items", "SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.Items", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.Items", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.ItemElements", "SubSubSubSubCategoryId", "dbo.SubSubSubSubCategories");
            DropForeignKey("dbo.ItemElements", "SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.ItemElements", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.ItemElements", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.RawMaterials", "ItemElementId", "dbo.ItemElements");
            DropForeignKey("dbo.ItemElements", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.RawMaterials", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TemporaryTransferInformations", "MethodId", "dbo.Methods");
            DropForeignKey("dbo.Items", "MethodId", "dbo.Methods");
            DropForeignKey("dbo.TemporaryTransferInformations", "ItemId", "dbo.Items");
            DropForeignKey("dbo.TemporaryTransferInformations", "Inv_HD_ID", "dbo.InventoryMasters");
            DropForeignKey("dbo.TransferDetails", "TransferTypeId", "dbo.TransferTypes");
            DropForeignKey("dbo.TemporaryTransferInformations", "TransferTypeId", "dbo.TransferTypes");
            DropForeignKey("dbo.TransferDetails", "TransferId", "dbo.TransferMasters");
            DropForeignKey("dbo.TransferDetails", "ItemId", "dbo.Items");
            DropForeignKey("dbo.TransferDetails", "ConditionOfItemId", "dbo.ConditionOfItems");
            DropForeignKey("dbo.TemporaryTransferInformations", "TransferId", "dbo.TransferMasters");
            DropForeignKey("dbo.TransferMasters", "SubSubSubSubStoreId", "dbo.SubSubSubSubStores");
            DropForeignKey("dbo.TransferMasters", "SubSubSubStoreId", "dbo.SubSubSubStores");
            DropForeignKey("dbo.TransferMasters", "SubSubStoreId", "dbo.SubSubStores");
            DropForeignKey("dbo.TransferMasters", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.TransferMasters", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.InventoryMasters", "TransferId", "dbo.TransferMasters");
            DropForeignKey("dbo.InventoryMasters", "ProductionMasterId", "dbo.ProductionMasters");
            DropForeignKey("dbo.DamagedItems", "SubSubSubSubStoreId", "dbo.SubSubSubSubStores");
            DropForeignKey("dbo.DamagedItems", "SubSubSubStoreId", "dbo.SubSubSubStores");
            DropForeignKey("dbo.DamagedItems", "SubSubStoreId", "dbo.SubSubStores");
            DropForeignKey("dbo.DamagedItems", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.DamagedItems", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.ProcurementMasters", "SupplierCompanyId", "dbo.SupplierCompanies");
            DropForeignKey("dbo.SubStores", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.SubSubSubSubStores", "SubSubSubStoreId", "dbo.SubSubSubStores");
            DropForeignKey("dbo.SubSubSubSubStores", "SubSubStoreId", "dbo.SubSubStores");
            DropForeignKey("dbo.SubSubSubSubStores", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.SubSubSubSubStores", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.ProductionMasters", "SubSubSubSubStoreId", "dbo.SubSubSubSubStores");
            DropForeignKey("dbo.ProcurementMasters", "SubSubSubSubStoreId", "dbo.SubSubSubSubStores");
            DropForeignKey("dbo.SubSubSubStores", "SubSubStoreId", "dbo.SubSubStores");
            DropForeignKey("dbo.SubSubSubStores", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.SubSubSubStores", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.ProductionMasters", "SubSubSubStoreId", "dbo.SubSubSubStores");
            DropForeignKey("dbo.ProcurementMasters", "SubSubSubStoreId", "dbo.SubSubSubStores");
            DropForeignKey("dbo.SubSubStores", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.SubSubStores", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.ProductionMasters", "SubSubStoreId", "dbo.SubSubStores");
            DropForeignKey("dbo.ProcurementMasters", "SubSubStoreId", "dbo.SubSubStores");
            DropForeignKey("dbo.ProductionMasters", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.ProductionMasters", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.ProductionDetails", "ProductionMasterId", "dbo.ProductionMasters");
            DropForeignKey("dbo.ProductionDetails", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ProductionMasters", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductionMasters", "ProductDetails_SerialId", "dbo.ProductDetails");
            DropForeignKey("dbo.ProductDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductDetails", "ItemId", "dbo.Items");
            DropForeignKey("dbo.InventoryDetails", "WarrantyId", "dbo.Warranties");
            DropForeignKey("dbo.TemporaryTransferInformations", "WarrantyId", "dbo.Warranties");
            DropForeignKey("dbo.ProcurementDetails", "WarrantyId", "dbo.Warranties");
            DropForeignKey("dbo.InventoryDetails", "UnitId", "dbo.Units");
            DropForeignKey("dbo.TemporaryTransferInformations", "UnitId", "dbo.Units");
            DropForeignKey("dbo.ProcurementDetails", "UnitId", "dbo.Units");
            DropForeignKey("dbo.InventoryDetails", "SubSubSubSubCategoryId", "dbo.SubSubSubSubCategories");
            DropForeignKey("dbo.InventoryDetails", "SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.InventoryDetails", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.InventoryDetails", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.TemporaryTransferInformations", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.TemporaryTransferInformations", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.TemporaryTransferInformations", "SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.TemporaryTransferInformations", "SubSubSubSubCategoryId", "dbo.SubSubSubSubCategories");
            DropForeignKey("dbo.SubSubSubSubCategories", "SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.SubSubSubSubCategories", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.SubSubSubSubCategories", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.ProcurementDetails", "SubSubSubSubCategoryId", "dbo.SubSubSubSubCategories");
            DropForeignKey("dbo.SubSubSubSubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Brands", "SubSubSubSubCategory_SubSubSubSubCategoryId", "dbo.SubSubSubSubCategories");
            DropForeignKey("dbo.SubSubSubCategories", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.SubSubSubCategories", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.ProcurementDetails", "SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.SubSubSubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Brands", "SubSubSubCategory_SubSubSubCategoryId", "dbo.SubSubSubCategories");
            DropForeignKey("dbo.SubSubCategories", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.ProcurementDetails", "SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.SubSubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Brands", "SubSubCategory_SubSubCategoryId", "dbo.SubSubCategories");
            DropForeignKey("dbo.ProcurementDetails", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Brands", "SubCategory_SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.InventoryDetails", "ProductionMasterId", "dbo.ProductionMasters");
            DropForeignKey("dbo.InventoryDetails", "ModelId", "dbo.Models");
            DropForeignKey("dbo.TemporaryTransferInformations", "ModelId", "dbo.Models");
            DropForeignKey("dbo.ProcurementDetails", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Items", "ModelId", "dbo.Models");
            DropForeignKey("dbo.InventoryDetails", "Inv_HD_ID", "dbo.InventoryMasters");
            DropForeignKey("dbo.InventoryDetails", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.InventoryDetails", "ConditionOfItemId", "dbo.ConditionOfItems");
            DropForeignKey("dbo.InventoryDetails", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.InventoryDetails", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.ProcurementMasters", "SubStoreId", "dbo.SubStores");
            DropForeignKey("dbo.ProcurementMasters", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.ProcurementMasters", "ProcurementTypeId", "dbo.ProcurementTypes");
            DropForeignKey("dbo.ProcurementDetails", "PO_HD_ID", "dbo.ProcurementMasters");
            DropForeignKey("dbo.InventoryMasters", "PO_HD_ID", "dbo.ProcurementMasters");
            DropForeignKey("dbo.DamagedItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.InventoryMasters", "DamagedItemId", "dbo.DamagedItems");
            DropForeignKey("dbo.TemporaryTransferInformations", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.State", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Provinces", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.ProcurementDetails", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Principles", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Profiles", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.ProfileFamilyDetails", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileFamilyDetails", "GenderID", "dbo.Genders");
            DropForeignKey("dbo.Profiles", "PressentAddress_ProfileId", "dbo.PressentAddresses");
            DropForeignKey("dbo.Profiles", "PermanentAddress_ProfileId", "dbo.PermanentAddresses");
            DropForeignKey("dbo.Profiles", "NationalityID", "dbo.Nationalities");
            DropForeignKey("dbo.Profiles", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.Profiles", "Genderid", "dbo.Genders");
            DropForeignKey("dbo.Profiles", "District_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Profiles", "BloodGroupId", "dbo.BloodGroups");
            DropForeignKey("dbo.PressentAddresses", "PostOffice_PostOfficeId", "dbo.PostOffices");
            DropForeignKey("dbo.PermanentAddresses", "PostOffice_PostOfficeId", "dbo.PostOffices");
            DropForeignKey("dbo.PostOffices", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.PressentAddresses", "PoliceStation_PoliceStationId", "dbo.PoliceStations");
            DropForeignKey("dbo.PressentAddresses", "Division_DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.PermanentAddresses", "Division_DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.PressentAddresses", "District_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.PressentAddresses", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.PermanentAddresses", "PoliceStation_PoliceStationId", "dbo.PoliceStations");
            DropForeignKey("dbo.PoliceStations", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.PermanentAddresses", "District_DistrictId", "dbo.Districts");
            DropForeignKey("dbo.PermanentAddresses", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.TemporaryTransferInformations", "ConditionOfItemId", "dbo.ConditionOfItems");
            DropForeignKey("dbo.TemporaryTransferInformations", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TemporaryTransferInformations", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.ProcurementDetails", "ConditionOfItemId", "dbo.ConditionOfItems");
            DropForeignKey("dbo.ProcurementDetails", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProcurementDetails", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Items", "BrandId", "dbo.Brands");
            DropIndex("dbo.TransferOrders", new[] { "ConditionOfItemId" });
            DropIndex("dbo.TransferOrders", new[] { "TransferTypeId" });
            DropIndex("dbo.TransferOrders", new[] { "ItemId" });
            DropIndex("dbo.RightOfAcces", new[] { "ItemId" });
            DropIndex("dbo.RightOfAcces", new[] { "TransferId" });
            DropIndex("dbo.MENUSUBSUBs", new[] { "SubMenuId" });
            DropIndex("dbo.MENUSUBs", new[] { "MainMenuId" });
            DropIndex("dbo.LayerAssigns", new[] { "LayerId" });
            DropIndex("dbo.Festivals", new[] { "ReligionId" });
            DropIndex("dbo.ItemElements", new[] { "SubSubSubSubCategoryId" });
            DropIndex("dbo.ItemElements", new[] { "SubSubSubCategoryId" });
            DropIndex("dbo.ItemElements", new[] { "SubSubCategoryId" });
            DropIndex("dbo.ItemElements", new[] { "SubCategoryId" });
            DropIndex("dbo.ItemElements", new[] { "CategoryId" });
            DropIndex("dbo.RawMaterials", new[] { "ItemElementId" });
            DropIndex("dbo.RawMaterials", new[] { "ItemId" });
            DropIndex("dbo.TransferDetails", new[] { "ConditionOfItemId" });
            DropIndex("dbo.TransferDetails", new[] { "TransferTypeId" });
            DropIndex("dbo.TransferDetails", new[] { "ItemId" });
            DropIndex("dbo.TransferDetails", new[] { "TransferId" });
            DropIndex("dbo.TransferMasters", new[] { "SubSubSubSubStoreId" });
            DropIndex("dbo.TransferMasters", new[] { "SubSubSubStoreId" });
            DropIndex("dbo.TransferMasters", new[] { "SubSubStoreId" });
            DropIndex("dbo.TransferMasters", new[] { "SubStoreId" });
            DropIndex("dbo.TransferMasters", new[] { "StoreId" });
            DropIndex("dbo.SubSubSubSubStores", new[] { "SubSubSubStoreId" });
            DropIndex("dbo.SubSubSubSubStores", new[] { "SubSubStoreId" });
            DropIndex("dbo.SubSubSubSubStores", new[] { "SubStoreId" });
            DropIndex("dbo.SubSubSubSubStores", new[] { "StoreId" });
            DropIndex("dbo.SubSubSubStores", new[] { "SubSubStoreId" });
            DropIndex("dbo.SubSubSubStores", new[] { "SubStoreId" });
            DropIndex("dbo.SubSubSubStores", new[] { "StoreId" });
            DropIndex("dbo.SubSubStores", new[] { "SubStoreId" });
            DropIndex("dbo.SubSubStores", new[] { "StoreId" });
            DropIndex("dbo.ProductionDetails", new[] { "ProductionMasterId" });
            DropIndex("dbo.ProductionDetails", new[] { "ItemId" });
            DropIndex("dbo.ProductDetails", new[] { "ItemId" });
            DropIndex("dbo.ProductDetails", new[] { "ProductId" });
            DropIndex("dbo.SubSubSubSubCategories", new[] { "CategoryId" });
            DropIndex("dbo.SubSubSubSubCategories", new[] { "SubCategoryId" });
            DropIndex("dbo.SubSubSubSubCategories", new[] { "SubSubCategoryId" });
            DropIndex("dbo.SubSubSubSubCategories", new[] { "SubSubSubCategoryId" });
            DropIndex("dbo.SubSubSubCategories", new[] { "CategoryId" });
            DropIndex("dbo.SubSubSubCategories", new[] { "SubCategoryId" });
            DropIndex("dbo.SubSubSubCategories", new[] { "SubSubCategoryId" });
            DropIndex("dbo.SubSubCategories", new[] { "CategoryId" });
            DropIndex("dbo.SubSubCategories", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.InventoryDetails", new[] { "UnitId" });
            DropIndex("dbo.InventoryDetails", new[] { "ModelId" });
            DropIndex("dbo.InventoryDetails", new[] { "BrandId" });
            DropIndex("dbo.InventoryDetails", new[] { "SubSubSubSubCategoryId" });
            DropIndex("dbo.InventoryDetails", new[] { "SubSubSubCategoryId" });
            DropIndex("dbo.InventoryDetails", new[] { "SubSubCategoryId" });
            DropIndex("dbo.InventoryDetails", new[] { "SubCategoryId" });
            DropIndex("dbo.InventoryDetails", new[] { "CategoryId" });
            DropIndex("dbo.InventoryDetails", new[] { "ProductionMasterId" });
            DropIndex("dbo.InventoryDetails", new[] { "WarrantyId" });
            DropIndex("dbo.InventoryDetails", new[] { "ConditionOfItemId" });
            DropIndex("dbo.InventoryDetails", new[] { "CountryId" });
            DropIndex("dbo.InventoryDetails", new[] { "Inv_HD_ID" });
            DropIndex("dbo.ProductionMasters", new[] { "ProductDetails_SerialId" });
            DropIndex("dbo.ProductionMasters", new[] { "SubSubSubSubStoreId" });
            DropIndex("dbo.ProductionMasters", new[] { "SubSubSubStoreId" });
            DropIndex("dbo.ProductionMasters", new[] { "SubSubStoreId" });
            DropIndex("dbo.ProductionMasters", new[] { "SubStoreId" });
            DropIndex("dbo.ProductionMasters", new[] { "StoreId" });
            DropIndex("dbo.ProductionMasters", new[] { "ProductId" });
            DropIndex("dbo.SubStores", new[] { "StoreId" });
            DropIndex("dbo.ProcurementMasters", new[] { "SubSubSubSubStoreId" });
            DropIndex("dbo.ProcurementMasters", new[] { "SubSubSubStoreId" });
            DropIndex("dbo.ProcurementMasters", new[] { "SubSubStoreId" });
            DropIndex("dbo.ProcurementMasters", new[] { "SubStoreId" });
            DropIndex("dbo.ProcurementMasters", new[] { "StoreId" });
            DropIndex("dbo.ProcurementMasters", new[] { "SupplierCompanyId" });
            DropIndex("dbo.ProcurementMasters", new[] { "ProcurementTypeId" });
            DropIndex("dbo.DamagedItems", new[] { "SubSubSubSubStoreId" });
            DropIndex("dbo.DamagedItems", new[] { "SubSubSubStoreId" });
            DropIndex("dbo.DamagedItems", new[] { "SubSubStoreId" });
            DropIndex("dbo.DamagedItems", new[] { "SubStoreId" });
            DropIndex("dbo.DamagedItems", new[] { "StoreId" });
            DropIndex("dbo.DamagedItems", new[] { "ItemId" });
            DropIndex("dbo.InventoryMasters", new[] { "ProductionMasterId" });
            DropIndex("dbo.InventoryMasters", new[] { "DamagedItemId" });
            DropIndex("dbo.InventoryMasters", new[] { "TransferId" });
            DropIndex("dbo.InventoryMasters", new[] { "PO_HD_ID" });
            DropIndex("dbo.State", new[] { "CountryID" });
            DropIndex("dbo.Provinces", new[] { "CountryID" });
            DropIndex("dbo.Principles", new[] { "CountryId" });
            DropIndex("dbo.ProfileFamilyDetails", new[] { "GenderID" });
            DropIndex("dbo.ProfileFamilyDetails", new[] { "ProfileId" });
            DropIndex("dbo.Profiles", new[] { "PressentAddress_ProfileId" });
            DropIndex("dbo.Profiles", new[] { "PermanentAddress_ProfileId" });
            DropIndex("dbo.Profiles", new[] { "District_DistrictId" });
            DropIndex("dbo.Profiles", new[] { "RegionId" });
            DropIndex("dbo.Profiles", new[] { "MaritalStatusId" });
            DropIndex("dbo.Profiles", new[] { "NationalityID" });
            DropIndex("dbo.Profiles", new[] { "BloodGroupId" });
            DropIndex("dbo.Profiles", new[] { "Genderid" });
            DropIndex("dbo.PostOffices", new[] { "DistrictId" });
            DropIndex("dbo.PressentAddresses", new[] { "PostOffice_PostOfficeId" });
            DropIndex("dbo.PressentAddresses", new[] { "PoliceStation_PoliceStationId" });
            DropIndex("dbo.PressentAddresses", new[] { "Division_DivisionId" });
            DropIndex("dbo.PressentAddresses", new[] { "District_DistrictId" });
            DropIndex("dbo.PressentAddresses", new[] { "Country_CountryId" });
            DropIndex("dbo.PoliceStations", new[] { "DistrictId" });
            DropIndex("dbo.PermanentAddresses", new[] { "PostOffice_PostOfficeId" });
            DropIndex("dbo.PermanentAddresses", new[] { "Division_DivisionId" });
            DropIndex("dbo.PermanentAddresses", new[] { "PoliceStation_PoliceStationId" });
            DropIndex("dbo.PermanentAddresses", new[] { "District_DistrictId" });
            DropIndex("dbo.PermanentAddresses", new[] { "Country_CountryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "TransferTypeId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "MethodId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "UnitId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "ModelId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "BrandId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "SubSubSubSubCategoryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "SubSubSubCategoryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "SubSubCategoryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "SubCategoryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "CategoryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "WarrantyId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "ConditionOfItemId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "CountryId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "ItemId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "TransferId" });
            DropIndex("dbo.TemporaryTransferInformations", new[] { "Inv_HD_ID" });
            DropIndex("dbo.ProcurementDetails", new[] { "UnitId" });
            DropIndex("dbo.ProcurementDetails", new[] { "ModelId" });
            DropIndex("dbo.ProcurementDetails", new[] { "BrandId" });
            DropIndex("dbo.ProcurementDetails", new[] { "SubSubSubSubCategoryId" });
            DropIndex("dbo.ProcurementDetails", new[] { "SubSubSubCategoryId" });
            DropIndex("dbo.ProcurementDetails", new[] { "SubSubCategoryId" });
            DropIndex("dbo.ProcurementDetails", new[] { "SubCategoryId" });
            DropIndex("dbo.ProcurementDetails", new[] { "CategoryId" });
            DropIndex("dbo.ProcurementDetails", new[] { "PO_HD_ID" });
            DropIndex("dbo.ProcurementDetails", new[] { "WarrantyId" });
            DropIndex("dbo.ProcurementDetails", new[] { "ConditionOfItemId" });
            DropIndex("dbo.ProcurementDetails", new[] { "CountryId" });
            DropIndex("dbo.Items", new[] { "MethodId" });
            DropIndex("dbo.Items", new[] { "UnitId" });
            DropIndex("dbo.Items", new[] { "BrandId" });
            DropIndex("dbo.Items", new[] { "ModelId" });
            DropIndex("dbo.Items", new[] { "SubSubSubSubCategoryId" });
            DropIndex("dbo.Items", new[] { "SubSubSubCategoryId" });
            DropIndex("dbo.Items", new[] { "SubSubCategoryId" });
            DropIndex("dbo.Items", new[] { "SubCategoryId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Brands", new[] { "SubSubSubSubCategory_SubSubSubSubCategoryId" });
            DropIndex("dbo.Brands", new[] { "SubSubSubCategory_SubSubSubCategoryId" });
            DropIndex("dbo.Brands", new[] { "SubSubCategory_SubSubCategoryId" });
            DropIndex("dbo.Brands", new[] { "SubCategory_SubCategoryId" });
            DropIndex("dbo.Brands", new[] { "Category_CategoryId" });
            DropTable("dbo.Weathers");
            DropTable("dbo.Upazilas");
            DropTable("dbo.TransferOrders");
            DropTable("dbo.SubContractCompanies");
            DropTable("dbo.StoreAssigns");
            DropTable("dbo.Seasons");
            DropTable("dbo.SafetyStocks");
            DropTable("dbo.RightOfAcces");
            DropTable("dbo.ProfessionInformations");
            DropTable("dbo.PhoneProviders");
            DropTable("dbo.MENUSUBSUBs");
            DropTable("dbo.MENUSUBs");
            DropTable("dbo.MENUMAINs");
            DropTable("dbo.Layers");
            DropTable("dbo.LayerAssigns");
            DropTable("dbo.Languages");
            DropTable("dbo.Hobbies");
            DropTable("dbo.Religions");
            DropTable("dbo.Festivals");
            DropTable("dbo.EmailProviders");
            DropTable("dbo.Emails");
            DropTable("dbo.EducationQualifications");
            DropTable("dbo.Companies");
            DropTable("dbo.Celebrations");
            DropTable("dbo.ItemElements");
            DropTable("dbo.RawMaterials");
            DropTable("dbo.Methods");
            DropTable("dbo.TransferTypes");
            DropTable("dbo.TransferDetails");
            DropTable("dbo.TransferMasters");
            DropTable("dbo.SupplierCompanies");
            DropTable("dbo.SubSubSubSubStores");
            DropTable("dbo.SubSubSubStores");
            DropTable("dbo.SubSubStores");
            DropTable("dbo.ProductionDetails");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Warranties");
            DropTable("dbo.Units");
            DropTable("dbo.SubSubSubSubCategories");
            DropTable("dbo.SubSubSubCategories");
            DropTable("dbo.SubSubCategories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Models");
            DropTable("dbo.InventoryDetails");
            DropTable("dbo.ProductionMasters");
            DropTable("dbo.SubStores");
            DropTable("dbo.ProcurementTypes");
            DropTable("dbo.ProcurementMasters");
            DropTable("dbo.Stores");
            DropTable("dbo.DamagedItems");
            DropTable("dbo.InventoryMasters");
            DropTable("dbo.State");
            DropTable("dbo.Provinces");
            DropTable("dbo.Principles");
            DropTable("dbo.Regions");
            DropTable("dbo.ProfileFamilyDetails");
            DropTable("dbo.Nationalities");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.Genders");
            DropTable("dbo.Profiles");
            DropTable("dbo.PostOffices");
            DropTable("dbo.Divisions");
            DropTable("dbo.PressentAddresses");
            DropTable("dbo.PoliceStations");
            DropTable("dbo.Districts");
            DropTable("dbo.PermanentAddresses");
            DropTable("dbo.Countries");
            DropTable("dbo.TemporaryTransferInformations");
            DropTable("dbo.ConditionOfItems");
            DropTable("dbo.ProcurementDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Items");
            DropTable("dbo.Brands");
            DropTable("dbo.BloodGroups");
            DropTable("dbo.ApprovalLayers");
        }
    }
}
