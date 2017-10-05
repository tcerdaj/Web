using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Data.Mappings
{
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Table("Users");
			Id(x => x.Id)
				.Column("UserId")
				.GeneratedBy.Increment();
			Map(x => x.ImageUrl);
			Map(x => x.UserName);
			Map(x => x.FirstName);
			Map(x => x.LastName);
			Map(x => x.Gender);
			Map(x => x.PasswordHash);
			Map(x => x.SecurityStamp);
			Map(x => x.Email);
			Map(x => x.Address);
			Map(x => x.City);
			Map(x => x.State);
			Map(x => x.Zip);
			Map(x => x.PhoneNumber).Column("Phone");
			Map(x => x.Active);
			Map(x => x.LockoutEnabled);
			Map(x => x.TwoFactorEnabled);
			Map(x => x.FailedCount);
			Map(x => x.ConfirmationToken);
			Map(x => x.IsConfirmed);
			Map(x => x.IsChurchMember);
			Map(x => x.ChurchName);
			Map(x => x.ChurchAddress);
			Map(x => x.ChurchPhone);
			Map(x => x.ChurchPastor);
			Map(x => x.NeedToBeVisited);
			Map(x => x.Comments);
			Map(x => x.CreatedOn);
			Map(x => x.ModifiedOn);
			References(x => x.CreatedBy)
				.Column("CreatedBy")
				.ForeignKey();
			References(x => x.ModifiedBy)
				.Column("ModifiedBy")
				.ForeignKey();
		}
	}

	public class DonationMap : ClassMap<Donation>
	{
		public DonationMap()
		{
			Table("Donations");
			Id(x => x.Id)
				.Column("DonationId")
				.GeneratedBy.Increment();
			
			Map(x => x.Title);
			Map(x => x.Description);
			Map(x => x.Amount);
			Map(x => x.ExpireOn);
			Map(x => x.DonationStatus).CustomType<DonationStatus>();
            Map(x => x.RequestId);
            Map(x => x.CreatedOn);
			Map(x => x.ModifiedOn);
			Map(x => x.DonatedOn);
			HasMany(x => x.DonationDetails)
				.KeyColumn("DonationId")
				.Inverse()
				.Cascade.All();
			References(x => x.RequestedBy)
				.Column("RequestedBy")
				.Nullable()
				.ForeignKey();
			References(x => x.CreatedBy)
				.Column("CreatedBy")
				.Not.Nullable()
				.ForeignKey();
			References(x => x.ModifiedBy)
				.Column("ModifiedBy")
				.Nullable()
				.ForeignKey();
		}
	}

    public class RequestMap : ClassMap<Request>
    {
        public RequestMap()
        {
            Table("Request");
            Id(x => x.Id)
                .Column("RequestId")
                .GeneratedBy.Increment();
            Map(x => x.Title);
            Map(x => x.Description);
            Map(x => x.DonationStatus)
                .CustomType<DonationStatus>()
                .Column("RequestStatus");
            Map(x => x.ItemType)
                .CustomType<DonationType>();
            Map(x => x.ImageUrl);
            Map(x => x.CreatedOn);
            Map(x => x.ModifiedOn);
            References(x => x.CreatedBy)
                .Column("CreatedBy")
                .Not.Nullable()
                .ForeignKey();
            References(x => x.ModifiedBy)
                .Column("ModifiedBy")
                .Nullable()
                .ForeignKey();
        }
    }

    public class DonationDetailMap : ClassMap<DonationDetails>
	{
		public DonationDetailMap()
		{
			Table("DonationDetails");
			Id(x => x.Id)
				.Column("ItemId")
				.GeneratedBy.Guid();
			References(x => x.Donation, "DonationId"); 
			HasMany(x => x.Images)
				.KeyColumn("ItemId")
				.Inverse()
				.Cascade.All();
			Map(x => x.Line).Column("Line");
			Map(x => x.ItemType)
				.CustomType<DonationType>(); 
			Map(x => x.ItemName);
			Map(x => x.DonationStatus)
				.CustomType<DonationStatus>();
			Map(x => x.CreatedOn);
			Map(x => x.ModifiedOn);
			References(x => x.RequestedBy)
				.Column("RequestedBy")
				.ForeignKey();
			References(x => x.CreatedBy)
				.Column("CreatedBy")
				.ForeignKey();
			References(x => x.ModifiedBy)
				.Column("ModifiedBy")
				.ForeignKey();
		}
	}

	public class DonationImageMap : ClassMap<DonationImage>
	{
		public DonationImageMap()
		{
			Table("DonationImages");
			Id(x => x.Id)
				.Column("DonationImageId")
				.GeneratedBy.Increment();
			References(x => x.Item, "ItemId");
			Map(x => x.ImageUrl);
			Map(x => x.CreatedOn);
			Map(x => x.ModifiedOn);
			References(x => x.CreatedBy)
				.Column("CreatedBy")
				.ForeignKey();
			References(x => x.ModifiedBy)
				.Column("ModifiedBy")
				.ForeignKey();
		}
	}
}
