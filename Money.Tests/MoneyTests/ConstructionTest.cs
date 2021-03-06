﻿using System;
using Shouldly;
using Xunit;

namespace Money.Tests.MoneyTests
{
    using Money = Money<decimal>;

    public class ConstructionTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrowWithEmptyCurrency(string currency)
        {
            Should.Throw<ArgumentNullException>(() => new Money(42, currency));
        }

        [Theory]
        [InlineData(42, "AUD")]
        public void ShouldNotThrowWithProperValues(decimal amount, string currency)
        {
            Should.NotThrow(() => new Money(amount, currency));
        }

        [Theory]
        [InlineData("aud")]
        [InlineData("Aud")]
        [InlineData("aUd")]
        [InlineData("auD")]
        [InlineData("AUd")]
        [InlineData("AuD")]
        [InlineData("aUD")]
        [InlineData("AUD")]
        public void CurrencyCodeShouldBeCaseIgnorant(string currency)
        {
            const Currency expectedCurrency = Currency.AUD;
            var money = new Money(42, currency);

            money.Currency.ShouldBe(expectedCurrency);
        }

        [Fact]
        public void WhenCurrencyIsNotSpecified_ShouldCreateWithCurrentCultureCurrency()
        {
            var money = new Money(42);
            var expectedCurrencyCode =
                new System.Globalization.RegionInfo(System.Globalization.CultureInfo.CurrentUICulture.LCID)
                    .ISOCurrencySymbol.ToUpperInvariant();

            var expected = (Currency) Enum.Parse(typeof (Currency), expectedCurrencyCode);

            money.Currency.ShouldBe(expected);
        }
    }
}