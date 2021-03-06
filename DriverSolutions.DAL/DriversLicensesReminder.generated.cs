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
	[Table("drivers_licenses_reminders")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class DriversLicensesReminder : INotifyPropertyChanged
	{
		private uint _driverLicenseReminderID;
		[Column("DriverLicenseReminderID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_driverLicenseReminderID")]
		public virtual uint DriverLicenseReminderID
		{
			get
			{
				return this._driverLicenseReminderID;
			}
			set
			{
				if(this._driverLicenseReminderID != value)
				{
					this._driverLicenseReminderID = value;
					this.OnPropertyChanged("DriverLicenseReminderID");
				}
			}
		}
		
		private uint _driverLicenseID;
		[Column("DriverLicenseID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_driverLicenseID")]
		public virtual uint DriverLicenseID
		{
			get
			{
				return this._driverLicenseID;
			}
			set
			{
				if(this._driverLicenseID != value)
				{
					this._driverLicenseID = value;
					this.OnPropertyChanged("DriverLicenseID");
				}
			}
		}
		
		private uint _reminderID;
		[Column("ReminderID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_reminderID")]
		public virtual uint ReminderID
		{
			get
			{
				return this._reminderID;
			}
			set
			{
				if(this._reminderID != value)
				{
					this._reminderID = value;
					this.OnPropertyChanged("ReminderID");
				}
			}
		}
		
		private byte _reminderType;
		[Column("ReminderType", OpenAccessType = OpenAccessType.Byte, Length = 0, Scale = 0, SqlType = "tinyint unsigned")]
		[Storage("_reminderType")]
		public virtual byte ReminderType
		{
			get
			{
				return this._reminderType;
			}
			set
			{
				if(this._reminderType != value)
				{
					this._reminderType = value;
					this.OnPropertyChanged("ReminderType");
				}
			}
		}
		
		private bool _shouldRemind;
		[Column("ShouldRemind", OpenAccessType = OpenAccessType.Bit, Length = 0, Scale = 0, SqlType = "bit")]
		[Storage("_shouldRemind")]
		public virtual bool ShouldRemind
		{
			get
			{
				return this._shouldRemind;
			}
			set
			{
				if(this._shouldRemind != value)
				{
					this._shouldRemind = value;
					this.OnPropertyChanged("ShouldRemind");
				}
			}
		}
		
		private bool _hasSentReminder;
		[Column("HasSentReminder", OpenAccessType = OpenAccessType.Bit, Length = 0, Scale = 0, SqlType = "bit")]
		[Storage("_hasSentReminder")]
		public virtual bool HasSentReminder
		{
			get
			{
				return this._hasSentReminder;
			}
			set
			{
				if(this._hasSentReminder != value)
				{
					this._hasSentReminder = value;
					this.OnPropertyChanged("HasSentReminder");
				}
			}
		}
		
		private DriversLicense _driversLicense;
		[ForeignKeyAssociation(ConstraintName = "FK_drivers_licenses_reminders_drivers_licenses_DriverLicenseID", SharedFields = "DriverLicenseID", TargetFields = "DriverLicenseID")]
		[Storage("_driversLicense")]
		public virtual DriversLicense DriversLicense
		{
			get
			{
				return this._driversLicense;
			}
			set
			{
				if(this._driversLicense != value)
				{
					this._driversLicense = value;
					this.OnPropertyChanged("DriversLicense");
				}
			}
		}
		
		private ConstReminder _constReminder;
		[ForeignKeyAssociation(ConstraintName = "FK_drivers_licenses_reminders_const_reminders_ReminderID", SharedFields = "ReminderID", TargetFields = "ReminderID")]
		[Storage("_constReminder")]
		public virtual ConstReminder ConstReminder
		{
			get
			{
				return this._constReminder;
			}
			set
			{
				if(this._constReminder != value)
				{
					this._constReminder = value;
					this.OnPropertyChanged("ConstReminder");
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
