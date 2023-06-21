﻿using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class QuicSettingViewModle : ViewModleBase
    {
        private QuicInfo info;
        public QuicSettingViewModle(QuicInfo info)
        {
            this.info = info;
        }
        public QuicSettingViewModle() : this(new())
        {

        }
        public IEnumerable<SecurityMode> SecurityModeValues { get; set; } = new List<SecurityMode>
        {
            SecurityMode.None,
            SecurityMode.Chacha20_poly1305,
            SecurityMode.Aes_128_gcm
        };
        public SecurityMode Security
        {
            get => info.Security;
            set
            {
                info.Security = value;
                OnPropertyChanged(nameof(Security));
            }
        }
        [Required(ErrorMessage = "密钥不能为空")]
        public string Key
        {
            get => info.Key;
            set
            {
                info.Key = value;
                OnPropertyChanged();
                ValidationProperty();
            }
        }
        public IEnumerable<FeignType> Feign { get; private set; } = Enum.GetValues<FeignType>().Cast<FeignType>();
        public FeignType FeignSelectedValue
        {
            get => info.Feign;
            set
            {
                info.Feign = value;
                OnPropertyChanged(nameof(Feign));
            }
        }
        protected override bool ValidationProperty([CallerMemberName] string? propertyName = null)
        {
            if(Security == SecurityMode.None && propertyName == nameof(Key))
            {
                return true;
            }
            return base.ValidationProperty(propertyName);
        }

    }
}
