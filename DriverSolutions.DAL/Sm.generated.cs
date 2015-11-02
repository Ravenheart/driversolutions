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

namespace DriverSolutions.DAL	
{
	[Table("sms")]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class Sm : INotifyPropertyChanged
	{
		private uint _smsID;
		[Column("SmsID", OpenAccessType = OpenAccessType.Int32, IsBackendCalculated = true, IsPrimaryKey = true, Length = 0, Scale = 0, SqlType = "integer unsigned")]
		[Storage("_smsID")]
		public virtual uint SmsID
		{
			get
			{
				return this._smsID;
			}
			set
			{
				if(this._smsID != value)
				{
					this._smsID = value;
					this.OnPropertyChanged("SmsID");
				}
			}
		}
		
		private string _smsDestination;
		[Column("SmsDestination", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, IsNullable = true, Length = 255, Scale = 0, SqlType = "nvarchar")]
		[Storage("_smsDestination")]
		public virtual string SmsDestination
		{
			get
			{
				return this._smsDestination;
			}
			set
			{
				if(this._smsDestination != value)
				{
					this._smsDestination = value;
					this.OnPropertyChanged("SmsDestination");
				}
			}
		}
		
		private string _smsMessage;
		[Column("SmsMessage", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, IsNullable = true, Length = 255, Scale = 0, SqlType = "nvarchar")]
		[Storage("_smsMessage")]
		public virtual string SmsMessage
		{
			get
			{
				return this._smsMessage;
			}
			set
			{
				if(this._smsMessage != value)
				{
					this._smsMessage = value;
					this.OnPropertyChanged("SmsMessage");
				}
			}
		}
		
		private string _smsSource;
		[Column("SmsSource", OpenAccessType = OpenAccessType.UnicodeStringVariableLength, IsNullable = true, Length = 255, Scale = 0, SqlType = "nvarchar")]
		[Storage("_smsSource")]
		public virtual string SmsSource
		{
			get
			{
				return this._smsSource;
			}
			set
			{
				if(this._smsSource != value)
				{
					this._smsSource = value;
					this.OnPropertyChanged("SmsSource");
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
