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
	[Table("drivers_medicals")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class DriversMedical : INotifyPropertyChanged
	{
		private uint _driverMedicalID;
		[Column("DriverMedicalID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_driverMedicalID")]
		public virtual uint DriverMedicalID
		{
			get
			{
				return this._driverMedicalID;
			}
			set
			{
				if(this._driverMedicalID != value)
				{
					this._driverMedicalID = value;
					this.OnPropertyChanged("DriverMedicalID");
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
		
		private uint _medTypeID;
		[Column("MedTypeID", OpenAccessType = OpenAccessType.Int32, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_medTypeID")]
		public virtual uint MedTypeID
		{
			get
			{
				return this._medTypeID;
			}
			set
			{
				if(this._medTypeID != value)
				{
					this._medTypeID = value;
					this.OnPropertyChanged("MedTypeID");
				}
			}
		}
		
		private DateTime _examinationDate;
		[Column("ExaminationDate", OpenAccessType = OpenAccessType.Date, Length = 0, Scale = 0, SqlType = "date")]
		[Storage("_examinationDate")]
		public virtual DateTime ExaminationDate
		{
			get
			{
				return this._examinationDate;
			}
			set
			{
				if(this._examinationDate != value)
				{
					this._examinationDate = value;
					this.OnPropertyChanged("ExaminationDate");
				}
			}
		}
		
		private DateTime _validityDate;
		[Column("ValidityDate", OpenAccessType = OpenAccessType.Date, Length = 0, Scale = 0, SqlType = "date")]
		[Storage("_validityDate")]
		public virtual DateTime ValidityDate
		{
			get
			{
				return this._validityDate;
			}
			set
			{
				if(this._validityDate != value)
				{
					this._validityDate = value;
					this.OnPropertyChanged("ValidityDate");
				}
			}
		}
		
		private Driver _driver;
		[ForeignKeyAssociation(ConstraintName = "FK_drivers_const_medical_types_drivers_DriverID", SharedFields = "DriverID", TargetFields = "DriverID")]
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
		
		private ConstMedicalType _constMedicalType;
		[ForeignKeyAssociation(ConstraintName = "FK_drivers_const_medical_types_const_medical_types_MedTypeID", SharedFields = "MedTypeID", TargetFields = "MedTypeID")]
		[Storage("_constMedicalType")]
		public virtual ConstMedicalType ConstMedicalType
		{
			get
			{
				return this._constMedicalType;
			}
			set
			{
				if(this._constMedicalType != value)
				{
					this._constMedicalType = value;
					this.OnPropertyChanged("ConstMedicalType");
				}
			}
		}
		
		private IList<DriversMedicalsReminder> _driversMedicalsReminders = new List<DriversMedicalsReminder>();
		[Collection(InverseProperty = "DriversMedical")]
		[Storage("_driversMedicalsReminders")]
		public virtual IList<DriversMedicalsReminder> DriversMedicalsReminders
		{
			get
			{
				return this._driversMedicalsReminders;
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
