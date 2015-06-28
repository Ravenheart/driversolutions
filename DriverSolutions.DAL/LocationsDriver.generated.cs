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
	[Table("locations_drivers")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class LocationsDriver : INotifyPropertyChanged
	{
		private uint _locationDriverID;
		[Column("LocationDriverID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_locationDriverID")]
		public virtual uint LocationDriverID
		{
			get
			{
				return this._locationDriverID;
			}
			set
			{
				if(this._locationDriverID != value)
				{
					this._locationDriverID = value;
					this.OnPropertyChanged("LocationDriverID");
				}
			}
		}
		
		private uint _locationID;
		[Column("LocationID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_locationID")]
		public virtual uint LocationID
		{
			get
			{
				return this._locationID;
			}
			set
			{
				if(this._locationID != value)
				{
					this._locationID = value;
					this.OnPropertyChanged("LocationID");
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
		
		private decimal _travelPay;
		[Column("TravelPay", OpenAccessType = OpenAccessType.Decimal, Length = 10, Scale = 2, SqlType = "decimal")]
		[Storage("_travelPay")]
		public virtual decimal TravelPay
		{
			get
			{
				return this._travelPay;
			}
			set
			{
				if(this._travelPay != value)
				{
					this._travelPay = value;
					this.OnPropertyChanged("TravelPay");
				}
			}
		}
		
		private Location _location;
		[ForeignKeyAssociation(ConstraintName = "FK_locations_drivers_locations_LocationID", SharedFields = "LocationID", TargetFields = "LocationID")]
		[Storage("_location")]
		public virtual Location Location
		{
			get
			{
				return this._location;
			}
			set
			{
				if(this._location != value)
				{
					this._location = value;
					this.OnPropertyChanged("Location");
				}
			}
		}
		
		private Driver _driver;
		[ForeignKeyAssociation(ConstraintName = "FK_locations_drivers_drivers_DriverID", SharedFields = "DriverID", TargetFields = "DriverID")]
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
