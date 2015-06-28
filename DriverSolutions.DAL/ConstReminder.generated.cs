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
	[Table("const_reminders")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class ConstReminder : INotifyPropertyChanged
	{
		private uint _reminderID;
		[Column("ReminderID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
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
		
		private string _reminderName;
		[Column("ReminderName", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, Length = 255, Scale = 0, SqlType = "nvarchar")]
		[Storage("_reminderName")]
		public virtual string ReminderName
		{
			get
			{
				return this._reminderName;
			}
			set
			{
				if(this._reminderName != value)
				{
					this._reminderName = value;
					this.OnPropertyChanged("ReminderName");
				}
			}
		}
		
		private uint _reminderInterval;
		[Column("ReminderInterval", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_reminderInterval")]
		public virtual uint ReminderInterval
		{
			get
			{
				return this._reminderInterval;
			}
			set
			{
				if(this._reminderInterval != value)
				{
					this._reminderInterval = value;
					this.OnPropertyChanged("ReminderInterval");
				}
			}
		}
		
		private uint _reminderType;
		[Column("ReminderType", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_reminderType")]
		public virtual uint ReminderType
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
		
		private bool _isMedical;
		[Column("IsMedical", OpenAccessType = OpenAccessType.Bit, Length = 0, Scale = 0, SqlType = "bit")]
		[Storage("_isMedical")]
		public virtual bool IsMedical
		{
			get
			{
				return this._isMedical;
			}
			set
			{
				if(this._isMedical != value)
				{
					this._isMedical = value;
					this.OnPropertyChanged("IsMedical");
				}
			}
		}
		
		private bool _isLicense;
		[Column("IsLicense", OpenAccessType = OpenAccessType.Bit, Length = 0, Scale = 0, SqlType = "bit")]
		[Storage("_isLicense")]
		public virtual bool IsLicense
		{
			get
			{
				return this._isLicense;
			}
			set
			{
				if(this._isLicense != value)
				{
					this._isLicense = value;
					this.OnPropertyChanged("IsLicense");
				}
			}
		}
		
		private IList<DriversMedicalsReminder> _driversMedicalsReminders = new List<DriversMedicalsReminder>();
		[Collection(InverseProperty = "ConstReminder")]
		[Storage("_driversMedicalsReminders")]
		public virtual IList<DriversMedicalsReminder> DriversMedicalsReminders
		{
			get
			{
				return this._driversMedicalsReminders;
			}
		}
		
		private IList<DriversLicensesReminder> _driversLicensesReminders = new List<DriversLicensesReminder>();
		[Collection(InverseProperty = "ConstReminder")]
		[Storage("_driversLicensesReminders")]
		public virtual IList<DriversLicensesReminder> DriversLicensesReminders
		{
			get
			{
				return this._driversLicensesReminders;
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