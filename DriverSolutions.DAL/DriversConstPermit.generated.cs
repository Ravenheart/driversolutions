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
	[Table("drivers_const_permits")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class DriversConstPermit : INotifyPropertyChanged
	{
		private uint _driverPermitID;
		[Column("DriverPermitID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_driverPermitID")]
		public virtual uint DriverPermitID
		{
			get
			{
				return this._driverPermitID;
			}
			set
			{
				if(this._driverPermitID != value)
				{
					this._driverPermitID = value;
					this.OnPropertyChanged("DriverPermitID");
				}
			}
		}
		
		private uint _driverID;
		[Column("DriverID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_driverID")]
		public virtual uint DriverID
		{
			get
			{
				return this._driverID;
			}
			set
			{
				if(this._driverID != value)
				{
					this._driverID = value;
					this.OnPropertyChanged("DriverID");
				}
			}
		}
		
		private uint _permitID;
		[Column("PermitID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_permitID")]
		public virtual uint PermitID
		{
			get
			{
				return this._permitID;
			}
			set
			{
				if(this._permitID != value)
				{
					this._permitID = value;
					this.OnPropertyChanged("PermitID");
				}
			}
		}
		
		private Driver _driver;
		[ForeignKeyAssociation(ConstraintName = "FK_drivers_const_permits_drivers_DriverID", SharedFields = "DriverID", TargetFields = "DriverID")]
		[Storage("_driver")]
		public virtual Driver Driver
		{
			get
			{
				return this._driver;
			}
			set
			{
				if(this._driver != value)
				{
					this._driver = value;
					this.OnPropertyChanged("Driver");
				}
			}
		}
		
		private ConstPermit _constPermit;
		[ForeignKeyAssociation(ConstraintName = "FK_drivers_const_permits_const_permits_PermitID", SharedFields = "PermitID", TargetFields = "PermitID")]
		[Storage("_constPermit")]
		public virtual ConstPermit ConstPermit
		{
			get
			{
				return this._constPermit;
			}
			set
			{
				if(this._constPermit != value)
				{
					this._constPermit = value;
					this.OnPropertyChanged("ConstPermit");
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
