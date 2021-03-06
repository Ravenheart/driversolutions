#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using System.ComponentModel;
using DriverSolutions.DAL;

namespace DriverSolutions.DAL	
{
	[Table("companies_licenses_payrate")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class CompaniesLicensesPayrate : INotifyPropertyChanged
	{
		private uint _companyDriverID;
		[Column("CompanyDriverID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_companyDriverID")]
		public virtual uint CompanyDriverID
		{
			get
			{
				return this._companyDriverID;
			}
			set
			{
				if(this._companyDriverID != value)
				{
					this._companyDriverID = value;
					this.OnPropertyChanged("CompanyDriverID");
				}
			}
		}
		
		private uint _companyID;
		[Column("CompanyID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_companyID")]
		public virtual uint CompanyID
		{
			get
			{
				return this._companyID;
			}
			set
			{
				if(this._companyID != value)
				{
					this._companyID = value;
					this.OnPropertyChanged("CompanyID");
				}
			}
		}
		
		private uint _licenseID;
		[Column("LicenseID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_licenseID")]
		public virtual uint LicenseID
		{
			get
			{
				return this._licenseID;
			}
			set
			{
				if(this._licenseID != value)
				{
					this._licenseID = value;
					this.OnPropertyChanged("LicenseID");
				}
			}
		}
		
		private decimal _payRate;
		[Column("PayRate", OpenAccessType = OpenAccessType.Decimal, Length = 10, Scale = 2, SqlType = "decimal")]
		[Storage("_payRate")]
		public virtual decimal PayRate
		{
			get
			{
				return this._payRate;
			}
			set
			{
				if(this._payRate != value)
				{
					this._payRate = value;
					this.OnPropertyChanged("PayRate");
				}
			}
		}
		
		private decimal _payRateOT;
		[Column("PayRateOT", OpenAccessType = OpenAccessType.Decimal, Length = 10, Scale = 2, SqlType = "decimal")]
		[Storage("_payRateOT")]
		public virtual decimal PayRateOT
		{
			get
			{
				return this._payRateOT;
			}
			set
			{
				if(this._payRateOT != value)
				{
					this._payRateOT = value;
					this.OnPropertyChanged("PayRateOT");
				}
			}
		}
		
		private DateTime _fromDate;
		[Column("FromDate", OpenAccessType = OpenAccessType.Date, Length = 0, Scale = 0, SqlType = "date")]
		[Storage("_fromDate")]
		public virtual DateTime FromDate
		{
			get
			{
				return this._fromDate;
			}
			set
			{
				if(this._fromDate != value)
				{
					this._fromDate = value;
					this.OnPropertyChanged("FromDate");
				}
			}
		}
		
		private DateTime? _toDate;
		[Column("ToDate", OpenAccessType = OpenAccessType.Date, IsNullable = true, Length = 0, Scale = 0, SqlType = "date")]
		[Storage("_toDate")]
		public virtual DateTime? ToDate
		{
			get
			{
				return this._toDate;
			}
			set
			{
				if(this._toDate != value)
				{
					this._toDate = value;
					this.OnPropertyChanged("ToDate");
				}
			}
		}
		
		private Company _company;
		[ForeignKeyAssociation(ConstraintName = "FK_companies_const_licenses_companies_CompanyID", SharedFields = "CompanyID", TargetFields = "CompanyID")]
		[Storage("_company")]
		public virtual Company Company
		{
			get
			{
				return this._company;
			}
			set
			{
				if(this._company != value)
				{
					this._company = value;
					this.OnPropertyChanged("Company");
				}
			}
		}
		
		#region INotifyPropertyChanged members
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if(this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		#endregion
		
	}
}
#pragma warning restore 1591
