﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vanara.Extensions.Reflection;
using Vanara.PInvoke;

namespace XrayGUI.ViewModle
{
    internal abstract class ViewModleBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public bool HasErrors => _Errors.Count > 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        protected Dictionary<string, List<ValidationResult>> _Errors;
        public ViewModleBase()
        {
            _Errors = new Dictionary<string, List<ValidationResult>>();
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && _Errors.ContainsKey(propertyName))
            {
                return _Errors[propertyName];
            }
            return null!;
        }
        protected void OnPropertyChanged( [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        //protected void SetProerty<T>(ref T property,T value, [CallerMemberName] string? propertyName = null)
        //{
        //    property = value;
        //    if(propertyName is not null)
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        public bool ValidationAllProperty()
        {
            var res = true;
            foreach(var property in  this.GetType().GetProperties())
            {
                var attributes = Attribute.GetCustomAttributes(property);
                if (attributes.OfType<ValidationAttribute>().Any())
                {
                    if(!ValidationProperty(property.Name))
                    {
                        res = false;
                    }
                }
            }
            return res;
        }
        protected virtual bool ValidationProperty([CallerMemberName] string? propertyName = null)
        {
            var res = false;
            if(propertyName is not null)
            {
                ValidationContext context = new(this) { MemberName = propertyName };
                List<ValidationResult> results = new();
                var propertInfo = GetType().GetProperty(propertyName);
                var value = propertInfo?.GetValue(this);
                res = Validator.TryValidateProperty(value, context, results);
                _Errors.Remove(propertyName);
                if (!res)
                {
                    _Errors[propertyName] = results;
                }
                OnErrorsChanged(propertyName);
            }
            return res;
        }

        protected void ClearErrors(string propertyName)
        {
            _Errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
    }
}
