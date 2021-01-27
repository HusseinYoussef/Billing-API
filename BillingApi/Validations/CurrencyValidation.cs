using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillingApi.Validations
{
    public class CurrencyNameAttribute : ValidationAttribute
    {
        private readonly List<string> _currencies;

        public CurrencyNameAttribute()
        {
            _currencies = new List<string>() {
                "USD",
                "EGP",
                "EUR",
                "CAD",
                "JPY"
            };
        }

        public override bool IsValid(object value)
        {
            string strValue = value as string;
            return _currencies.Contains(strValue);
        }
    }
}