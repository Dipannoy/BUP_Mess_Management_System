using System;
using System.Collections.Generic;
using System.Text;
using Mess_Management_System_Alpha_V2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mess_Management_System_Alpha_V2.Models.MessModels;
using System.Linq;

namespace Mess_Management_System_Alpha_V2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserIdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuSub> SubMenu { get; set; }


        public DbSet<UnitType> UnitType { get; set; }


        public DbSet<StoreInItem> StoreInItem { get; set; }
        public DbSet<StoreInItemCategory> StoreInItemCategory { get; set; }



        public DbSet<StoreOutItemCategory> StoreOutItemCategory { get; set; }
        public DbSet<StoreOutItem> StoreOutItem { get; set; }
        public DbSet<StoreOutItemRecipe> StoreOutItemRecipe { get; set; }



        public DbSet<OrderHistory> OrderHistory { get; set; }
       // public DbSet<OrderHistoryV2> OrderHistoryV2 { get; set; }
        public DbSet<OrderHistoryVr2> OrderHistoryVr2 { get; set; }


        public DbSet<PreOrderSchedule> PreOrderSchedule { get; set; }


        public DbSet<BillHistory> BillHistory { get; set; }
        


        public DbSet<MealType> MealType { get; set; }
        public DbSet<SetMenu> SetMenu { get; set; }
        public DbSet<SetMenuDetails> SetMenuDetails { get; set; }
        public DbSet<ExtraItem> ExtraItem { get; set; }


        public DbSet<WarehouseStorage> WarehouseStorage { get; set; }
        public DbSet<RemainingBalanceAndWeightedPriceCalculation> RemainingBalanceAndWeightedPriceCalculation { get; set; }

        public DbSet<MaintenanceBillHistory> MaintenanceBillHistory { get; set; }

        public DbSet<OrderType> OrderType { get; set; }

        public DbSet<CustomerChoice> CustomerChoice { get; set; }
        public DbSet<CustomerChoiceV2> CustomerChoiceV2 { get; set; }


        public DbSet<Period> Period { get; set; }

        public DbSet<MenuItem> MenuItem { get; set; }

        public DbSet<DailySetMenu> DailySetMenu { get; set; }

        public DbSet<AccessoryBill> AccessoryBill { get; set; }

        public DbSet<ConsumerMonthlyBillRecord> ConsumerMonthlyBillRecord { get; set; }


        public DbSet<SpecialMenuOrder> SpecialMenuOrder { get; set; }
        public DbSet<SpecialMenuParent> SpecialMenuParent { get; set; }

        public DbSet<Office> Office { get; set; }

        public DbSet<TestClassMod> TestClassMod { get; set; }
        public DbSet<Constants> Constants { get; set; }
        public DbSet<CustomerDailyMenuChoice> CustomerDailyMenuChoice { get; set; }
        public DbSet<UserDateChoiceMaster> UserDateChoiceMaster { get; set; }

        public DbSet<UserDateChoiceDetail> UserDateChoiceDetail { get; set; }
        public DbSet<ExtraChitParent> ExtraChitParent { get; set; }
        public DbSet<OnSpotParent> OnSpotParent { get; set; }
        public DbSet<ConsumerMealWiseExtrachit> ConsumerMealWiseExtrachit { get; set; }

        public DbSet<ConsumerMealWiseExtraChitParent> ConsumerMealWiseExtraChitParent { get; set; }

        public DbSet<ConsumerPaymentInfo> ConsumerPaymentInfo { get; set; }

        public DbSet<ConsumerBillHistory> ConsumerBillHistory { get; set; }

        public DbSet<ConsumerBillParent> ConsumerBillParent { get; set; }

        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        public DbSet<ConsumerPaymentAttachment> ConsumerPaymentAttachment { get; set; }

       public DbSet<NavigationMenu> NavigationMenu { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }
        public DbSet<DailyOfferItem> DailyOfferItem { get; set; }

        public DbSet<ConsumerOthersBillParent> ConsumerOthersBillParent { get; set; }
        public DbSet<ConsumerOthersBill> ConsumerOthersBill { get; set; }

        //public DbSet<ConsumerPaymentAttachment> ConsumerPaymentAttachment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            modelBuilder.Entity<StoreOutItemRecipe>()
                .HasOne(o => o.StoreOutItem)
                .WithMany(m=> m.StoreOutItemRecipeList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreOutItemRecipe>()
                .HasOne(o => o.StoreInItem)
                .WithMany(m => m.StoreOutItemRecipeList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreInItem>()
                .HasOne(o => o.StoreInItemCategory)
                .WithMany(m => m.StoreInItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreInItem>()
                .HasOne(o => o.UnitType)
                .WithMany(m => m.StoreInItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StoreOutItem>()
                .HasOne(o => o.StoreOutItemCategory)
                .WithMany(m => m.StoreOutItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SetMenuDetails>()
                .HasOne(o => o.SetMenu)
                .WithMany(m => m.SetMenuDetailsList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SetMenuDetails>()
               .HasOne(o => o.StoreOutItem)
               .WithMany(m => m.SetMenuDetailsList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SetMenuDetails>()
            .HasOne(o => o.ExtraItem)
            .WithMany(m => m.SetMenuDetailList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SetMenu>()
                .HasOne(o => o.MealType)
                .WithMany(m => m.SetMenuList).OnDelete(DeleteBehavior.Restrict);
                        

            modelBuilder.Entity<ExtraItem>()
               .HasOne(o => o.SetMenu)
               .WithMany(m => m.ExtraItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExtraItem>()
               .HasOne(o => o.StoreOutItem)
               .WithMany(m => m.ExtraItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PreOrderSchedule>()
              .HasOne(o => o.MealType)
              .WithMany(m => m.PreOrderScheduleList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PreOrderSchedule>()
              .HasOne(o => o.ApplicationUser)
              .WithMany(m => m.PreOrderScheduleList).OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<OrderHistory>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(m => m.OrderHistoryList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderHistory>()
               .HasOne(o => o.SetMenu)
               .WithMany(m => m.OrderHistoryList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistory>()
              .HasOne(o => o.MealType)
              .WithMany(m => m.OrderHistoryList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistory>()
              .HasOne(o => o.StoreOutItem)
              .WithMany(m => m.OrderHistoryList).OnDelete(DeleteBehavior.Restrict);

         //   modelBuilder.Entity<OrderHistory>()
         //.HasOne(o => o.OrderType)
         //.WithMany(m => m.OrderHistoryList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderHistoryVr2>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(m => m.OrderHistoryVr2List).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderHistoryVr2>()
               .HasOne(o => o.SetMenu)
               .WithMany(m => m.OrderHistoryVr2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistoryVr2>()
              .HasOne(o => o.MealType)
              .WithMany(m => m.OrderHistoryVr2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistoryVr2>()
              .HasOne(o => o.StoreOutItem)
              .WithMany(m => m.OrderHistoryVr2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistoryVr2>()
         .HasOne(o => o.OrderType)
         .WithMany(m => m.OrderHistoryVr2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WarehouseStorage>()
             .HasOne(o => o.StoreInItem)
             .WithMany(m => m.WarehouseStorageList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WarehouseStorage>()
        .HasOne(o => o.StoreOutItem)
        .WithMany(m => m.WarehouseStorageList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RemainingBalanceAndWeightedPriceCalculation>()
            .HasOne(o => o.StoreInItem)
            .WithMany(m => m.RemainingBalanceAndWeightedPriceCalculationList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaintenanceBillHistory>()
            .HasOne(o => o.ApplicationUser)
            .WithMany(m => m.MaintenanceBillHistoryList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CustomerChoice>()
            .HasOne(o => o.ApplicationUser)
            .WithMany(m => m.CustomerChoiceList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoice>()
              .HasOne(o => o.SetMenu)
              .WithMany(m => m.CustomerChoiceList).OnDelete(DeleteBehavior.Restrict);

             modelBuilder.Entity<CustomerChoice>()
            .HasOne(o => o.OrderType)
            .WithMany(m => m.CustomerChoiceList).OnDelete(DeleteBehavior.Restrict);

             modelBuilder.Entity<CustomerChoice>()
            .HasOne(o => o.ExtraItem)
            .WithMany(m => m.CustomerChoiceList).OnDelete(DeleteBehavior.Restrict);

              modelBuilder.Entity<CustomerChoice>()
            .HasOne(o => o.MealType)
            .WithMany(m => m.CustomerChoiceList).OnDelete(DeleteBehavior.Restrict);



            //-------------------------------------------------------------------------------
            modelBuilder.Entity<CustomerChoiceV2>()
          .HasOne(o => o.ApplicationUser)
          .WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoiceV2>()
      .HasOne(o => o.SetMenu)
      .WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoiceV2>()
.HasOne(o => o.OrderType)
.WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoiceV2>()
           .HasOne(o => o.ExtraItem)
           .WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoiceV2>()
          .HasOne(o => o.MealType)
          .WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoiceV2>()
       .HasOne(o => o.OnSpotParent)
       .WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerChoiceV2>()
        .HasOne(o => o.ExtraChitParent)
        .WithMany(m => m.CustomerChoiceV2List).OnDelete(DeleteBehavior.Restrict);






            //---------------------------------------------------------------------------------

            modelBuilder.Entity<Period>()
          .HasOne(o => o.MealType)
          .WithMany(m => m.PeriodList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Period>()
          .HasOne(o => o.ApplicationUser)
          .WithMany(m => m.PeriodList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuItem>()
         .HasOne(o => o.ExtraItem)
         .WithMany(m => m.MenuItemList).OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<MenuItem>()
         .HasOne(o => o.MealType)
         .WithMany(m => m.MenuItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuItem>()
        .HasOne(o => o.StoreOutItem)
        .WithMany(m => m.MenuItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DailySetMenu>()
        .HasOne(o => o.MealType)
        .WithMany(m => m.DailySetMenuList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DailySetMenu>()
     .HasOne(o => o.SetMenu)
     .WithMany(m => m.DailySetMenuList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerMonthlyBillRecord>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.ConsumerMonthlyBillRecordList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SpecialMenuParent>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.SpecialMenuParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpecialMenuOrder>()
.HasOne(o => o.StoreOutItem)
.WithMany(m => m.SpecialMenuOrderList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpecialMenuOrder>()
.HasOne(o => o.SpecialMenuParent)
.WithMany(m => m.SpecialMenuOrderList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpecialMenuParent>()
.HasOne(o => o.MealType)
.WithMany(m => m.SpecialMenuParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpecialMenuParent>()
.HasOne(o => o.Office)
.WithMany(m => m.SpecialMenuParentList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CustomerDailyMenuChoice>()
.HasOne(o => o.MealType)
.WithMany(m => m.CustomerDailyMenuChoiceList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerDailyMenuChoice>()
.HasOne(o => o.OrderType)
.WithMany(m => m.CustomerDailyMenuChoiceList).OnDelete(DeleteBehavior.Restrict);

            

            modelBuilder.Entity<CustomerDailyMenuChoice>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.CustomerDailyMenuChoiceList).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserDateChoiceMaster>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.UserDateChoiceMasterList).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserDateChoiceMaster>()
.HasOne(o => o.MealType)
.WithMany(m => m.UserDateChoiceMasterList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserDateChoiceDetail>()
.HasOne(o => o.UserDateChoiceMaster)
.WithMany(m => m.UserDateChoiceDetailList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnSpotParent>()
.HasOne(o => o.Constants)
.WithMany(m => m.OnSpotParentList).OnDelete(DeleteBehavior.Restrict);

            //            modelBuilder.Entity<ExtraChitParent>()
            //.HasOne(o => o.ApplicationUser)
            //.WithMany(m => m.ExtraChitParentList).OnDelete(DeleteBehavior.Restrict);

            //            modelBuilder.Entity<ExtraChitParent>()
            //.HasOne(o => o.Office)
            //.WithMany(m => m.ExtraChitParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnSpotParent>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.OnSpotParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OnSpotParent>()
.HasOne(o => o.Office)
.WithMany(m => m.OnSpotParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerMealWiseExtraChitParent>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.ConsumerMealWiseExtraChitParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerMealWiseExtraChitParent>()
.HasOne(o => o.MealType)
.WithMany(m => m.ConsumerMealWiseExtraChitParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerMealWiseExtraChitParent>()
.HasOne(o => o.OrderType)
.WithMany(m => m.ConsumerMealWiseExtraChitParentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerMealWiseExtrachit>()
.HasOne(o => o.StoreOutItem)
.WithMany(m => m.ConsumerMealWiseExtrachitList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerMealWiseExtrachit>()
.HasOne(o => o.ConsumerMealWiseExtraChitParent)
.WithMany(m => m.ConsumerMealWiseExtrachitList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ConsumerBillHistory>()
.HasOne(o => o.ConsumerBillParent)
.WithMany(m => m.ConsumerBillHistoryList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ConsumerBillHistory>()
.HasOne(o => o.ConsumerPaymentInfo)
.WithMany(m => m.ConsumerBillHistoryList).OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<ConsumerBillParent>()
.HasOne(o => o.ApplicationUser)
.WithMany(m => m.ConsumerBillParentList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ConsumerPaymentInfo>()
.HasOne(o => o.ConsumerBillParent)
.WithMany(m => m.ConsumerPaymentInfoList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerPaymentInfo>()
.HasOne(o => o.PaymentMethod)
.WithMany(m => m.ConsumerPaymentInfoList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerPaymentAttachment>()
.HasOne(o => o.ConsumerBillParent)
.WithMany(m => m.ConsumerPaymentAttachmentList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerPaymentAttachment>()
.HasOne(o => o.ConsumerPaymentInfo)
.WithMany(m => m.ConsumerPaymentAttachmentList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RoleMenu>()
.HasOne(o => o.UserIdentityRole)
.WithMany(m => m.RoleMenuList).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<RoleMenu>()
.HasOne(o => o.NavigationMenu)
.WithMany(m => m.RoleMenuList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DailyOfferItem>()
.HasOne(o => o.StoreOutItem)
.WithMany(m => m.DailyOfferItemList).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConsumerOthersBill>()
.HasOne(o => o.ConsumerOthersBillParent)
.WithMany(m => m.ConsumerOthersBillList).OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }


    }
}
